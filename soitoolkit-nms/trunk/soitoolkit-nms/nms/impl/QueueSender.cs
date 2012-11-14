/* 
 * Licensed to the soi-toolkit project under one or more
 * contributor license agreements.  See the NOTICE file distributed with
 * this work for additional information regarding copyright ownership.
 * The soi-toolkit project licenses this file to You under the Apache License, Version 2.0
 * (the "License"); you may not use this file except in compliance with
 * the License.  You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using Apache.NMS;
using Apache.NMS.ActiveMQ.Commands;

namespace Soitoolkit.Nms.Impl
{
    internal class QueueSender : IQueueSender, IDisposable
    {
        private Soitoolkit.Log.Log log = new Soitoolkit.Log.Log();

        private Session          session;
        private IQueue           queue;
        private IMessageProducer producer { get; set; }

        private ITemporaryQueue  tmpReplyToQueue;
        private IQueueReceiver   tmpReplyToQueueReceiver;

        internal QueueSender(Session session, string queueName)
        {
            if (log.IsDebugEnabled()) log.Debug("QueueSender constructor called");

            IQueue queue = session.nmsSession.GetQueue(queueName);
            init(session, queue);
        }


        internal QueueSender(Session session, IQueue queue)
        {
            if (log.IsDebugEnabled()) log.Debug("QueueSender constructor called");

            init(session, queue);
        }

        private void init(Session session, IQueue queue)
        {
            this.session = session;
            this.queue = queue;
            producer = session.nmsSession.CreateProducer(queue);
        }

        public void SendMessage(String TextBody)
        {
            SendMessage(new TextMessage(TextBody));
        }

        public void SendMessage(ITextMessage Message)
        {
            SendMessage(Message, null);
        }

        internal void SendMessage(ITextMessage Message, IQueue queue)
        {
            // Create a nms text-message
            IMessage nmsMsg = producer.CreateTextMessage(Message.TextBody);

            // Copy nsm and custom headers, if any.
            ((TextMessage)Message).CopyHeadersToNmsMessage(nmsMsg);

            // Send the message (use the default queue if no queue is specified in the SendMessage call)
            if (queue == null)
            {
                if (log.IsDebugEnabled()) log.Debug("Sending to queue: " + this.queue.QueueName + ", message: " + Message.TextBody);
                producer.Send(nmsMsg);
            }
            else
            {
                if (log.IsDebugEnabled()) log.Debug("Sending to queue: " + queue.QueueName + ", message: " + Message.TextBody);
                producer.Send(queue, nmsMsg);
            }
        }

        public void SendBytesMessage(byte[] BytesBody)
        {
            SendBytesMessage(new BytesMessage(BytesBody, null));
        }

        public void SendBytesMessage(IBytesMessage Message)
        {
            SendBytesMessage(Message, null);
        }

        internal void SendBytesMessage(IBytesMessage Message, IQueue queue)
        {
            // Create a nms text-message
            byte[] BytesBody = Message.BytesBody;
            IMessage nmsMsg = producer.CreateBytesMessage(Message.BytesBody);

            // Copy nsm and custom headers, if any.
            ((BaseMessage)Message).CopyHeadersToNmsMessage(nmsMsg);

            // Send the message (use the default queue if no queue is specified in the SendMessage call)
            if (queue == null)
            {
                if (log.IsDebugEnabled()) log.Debug("Sending to queue: " + this.queue.QueueName + ", message: " + BytesBody.Length + " bytes");
                producer.Send(nmsMsg);
            }
            else
            {
                if (log.IsDebugEnabled()) log.Debug("Sending to queue: " + queue.QueueName + ", message: " + BytesBody.Length + " bytes");
                producer.Send(queue, nmsMsg);
            }
        }

        public ITextMessage SendMessageWaitForReplyOnTmpQueue(string TextBody, int timeoutMs)
        {
            TimeSpan    ts    = TimeSpan.FromMilliseconds(timeoutMs);
            TextMessage msg   = (new TextMessage(TextBody));
            msg.NMSReplyTo    = new Destination(GetTmpReplyToQueue());
            msg.NMSTimeToLive = ts;
            Console.WriteLine(System.DateTime.Now.ToString() + ", ### MSG ID BEF SEND: " + msg.NMSMessageId);
            SendMessage(msg);
            Console.WriteLine(System.DateTime.Now.ToString() + ", ### MSG ID AFT SEND: " + msg.NMSMessageId);

            // TODO: ML FIX. Read messages using a jms-selector based on the correlation id!!!

            if (log.IsDebugEnabled()) log.Debug("Waiting for response on replyTo-queue: " + GetTmpReplyToQueue().QueueName + ", timeout: " + timeoutMs);
            IQueueReceiver rr       = GetTmpReplyToQueueReceiver();
            ITextMessage   response = rr.Receive(ts);

            if (log.IsDebugEnabled()) {
                if (response == null) { log.Debug("Received no response on replyTo-queue: " + GetTmpReplyToQueue().QueueName); }
                else { log.Debug("Received response on replyTo-queue: " + GetTmpReplyToQueue().QueueName + ", NMSCorrelationID: " + response.NMSCorrelationID); }
            }
            
            return response;
        }

        private ITemporaryQueue GetTmpReplyToQueue()
        {
            if (tmpReplyToQueue == null)
            {
                tmpReplyToQueue = session.nmsSession.CreateTemporaryQueue();
            }
            return tmpReplyToQueue;
        }

        private IQueueReceiver GetTmpReplyToQueueReceiver()
        {
            if (tmpReplyToQueueReceiver == null)
            {
                tmpReplyToQueueReceiver = new QueueReceiver(session, GetTmpReplyToQueue());
            }
            return tmpReplyToQueueReceiver;
        }

        public void Dispose()
        {

            if (producer != null)
            {
                try
                {
                    producer.Close();
                }
                catch (Exception ex)
                {
                    if (log.IsDebugEnabled()) log.Debug("Error during producer close", ex);
                }

                try
                {
                    producer.Dispose();
                }
                catch (Exception ex)
                {
                    if (log.IsDebugEnabled()) log.Debug("Error during producer dispose", ex);
                }

                producer = null;
                if (log.IsDebugEnabled()) log.Debug("Producer closed and disposed");
            }

            if (tmpReplyToQueueReceiver != null)
            {
                try
                {
                    tmpReplyToQueueReceiver.Dispose();
                }
                catch (Exception ex)
                {
                    if (log.IsDebugEnabled()) log.Debug("Error during tmpReplyToQueueReceiver dispose", ex);
                }

                tmpReplyToQueueReceiver = null;
                if (log.IsDebugEnabled()) log.Debug("tmpReplyToQueueReceiver disposed");
            }
        }

        ~QueueSender()
        {
            Dispose();
        }
    }
}
