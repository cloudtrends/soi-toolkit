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
    internal class QueueReceiver : IQueueReceiver, IDisposable
    {
        private Soitoolkit.Log.Log log = new Soitoolkit.Log.Log();

        private Session          session;
        private IQueue           queue;
        private IMessageConsumer consumer { get; set; }

        internal QueueReceiver(Session session, string queueName)
        {
            if (log.IsDebugEnabled()) log.Debug("QueueReceiver constructor called");

            IQueue queue = session.nmsSession.GetQueue(queueName);
            init(session, queue);
        }

        internal QueueReceiver(Session session, IQueue queue)
        {
            if (log.IsDebugEnabled()) log.Debug("QueueReceiver constructor called");

            init(session, queue);
        }

        private void init(Session session, IQueue queue)
        {
            this.session = session;
            this.queue = queue;
            consumer = session.nmsSession.CreateConsumer(queue);
        }

        public ITextMessage Receive()
        {
            IMessage nmsMsg  = consumer.Receive();
            return CreateTextMessage(nmsMsg);
        }

        public ITextMessage Receive(TimeSpan timeout)
        {
            IMessage nmsMsg = consumer.Receive(timeout);
            return CreateTextMessage(nmsMsg);
        }

        public byte[] ReceiveBytesMessage(TimeSpan timeout)
        {
            IMessage nmsMsg = consumer.Receive(timeout);
            if (nmsMsg == null) return null;

            IBytesMessage nmsBytesMsg = (IBytesMessage)nmsMsg;
            return nmsBytesMsg.Content;
        }


        // Summary:
        //    We don't make this method a part of the public API since (According to the NMS API doc):
        //        Receives the next message if one is immediately available for delivery on the client side otherwise this method returns null. 
        //        It is never an error for this method to return null,
        //        the time of Message availability varies so your client cannot rely on this method to receive a message immediately after one has been sent.
        internal ITextMessage ReceiveNoWait()
        {
            IMessage nmsMsg = consumer.ReceiveNoWait();
            return CreateTextMessage(nmsMsg);
        }

        private ITextMessage CreateTextMessage(IMessage nmsMsg)
        {
            ITextMessage message = (nmsMsg == null) ? null : new TextMessage(nmsMsg);
            if (log.IsDebugEnabled()) log.Debug("Received from queue: " + queue.QueueName + ", message: " + ((nmsMsg == null) ? null : message.TextBody));
            return message;
        }

        public event MessageReceivedDelegate OnMessageReceived;

        public void StartListener()
        {
            consumer.Listener += (nmsMsg =>
                {
                    if (OnMessageReceived != null)
                    {
                        ITextMessage message = new TextMessage(nmsMsg);
                        if (log.IsDebugEnabled()) log.Debug("Notifying receive from queue: " + queue.QueueName + " of message: " + message.TextBody);
                        OnMessageReceived(message);
                    }
                });
        }

        public void SendReplyMessage(ITextMessage RequestMessage, string ResponseTextBody)
        {
            SendReplyMessage(RequestMessage, new TextMessage(ResponseTextBody));
        }

        public void SendReplyMessage(ITextMessage RequestMessage, ITextMessage ResponseMessage)
        {
            // TODO: ML FIX. Should we use NMSMessageId and/or NMSCorrelationID??? Maybe it is actually hard to get the NMSMessageId in a NMS client after sending the request-message???
            string       corrId          = RequestMessage.NMSMessageId;
            IDestination replyToQueue    = RequestMessage.NMSReplyTo;
            IQueue       nmsReplyToQueue = (IQueue)((Destination)replyToQueue).NmsDestination;

            if (log.IsDebugEnabled()) log.Debug("Sending message: " + ResponseMessage.TextBody);
            using (QueueSender rs = new QueueSender(session, nmsReplyToQueue))
            {
                ResponseMessage.NMSCorrelationID = corrId;
                rs.SendMessage(ResponseMessage, nmsReplyToQueue);
            }
        }

        public void Dispose()
        {
            if (consumer != null)
            {
                try
                {
                    consumer.Close();
                }
                catch (Exception ex)
                {
                    if (log.IsDebugEnabled()) log.Debug("Error during consumer close", ex);
                }

                try
                {
                    consumer.Dispose();
                }
                catch (Exception ex)
                {
                    if (log.IsDebugEnabled()) log.Debug("Error during consumer dispose", ex);
                }
            }
            consumer = null;
            if (log.IsDebugEnabled()) log.Debug("Consumer closed and disposed");
        }

        ~QueueReceiver()
        {
            Dispose();
        }
    }
}
