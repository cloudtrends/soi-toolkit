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
using System.Collections.Generic;
using System.Text;
using Apache.NMS;
using Apache.NMS.ActiveMQ;

namespace Soitoolkit.Nms.Impl
{
    internal class Session : Soitoolkit.Nms.ISession, IDisposable
    {
        private Soitoolkit.Log.Log log = new Soitoolkit.Log.Log();

        public IConnection nmsConnection { get; private set; }

        public Apache.NMS.ISession nmsSession { get; private set; }

        internal Session(string brokerUrl, string username, string password, string clientId)
        {
            IConnectionFactory cf = null;
            if (clientId == null)
            {
                cf = new ConnectionFactory(brokerUrl);
            } 
            else 
            {
                cf = new ConnectionFactory(brokerUrl, clientId);
            }

            if (username == null)
            {
                nmsConnection = cf.CreateConnection();
            }
            else
            {
                nmsConnection = cf.CreateConnection(username, password);
            }

            nmsConnection.Start();
            nmsSession = nmsConnection.CreateSession();
            if (log.IsDebugEnabled()) log.Debug("Session and connection created");
        }

        public IQueueSender CreateQueueSender(string QueueName)
        {
            return new QueueSender(this, QueueName);
        }

        public IQueueReceiver CreateQueueReceiver(string QueueName)
        {
            return new QueueReceiver(this, QueueName);
        }

        public ITextMessage CreateTextMessage(string Text, Dictionary<string, string> CustomHeaders)
        {
            return new TextMessage(Text, CustomHeaders);
        }

        public IBytesMessage CreateBytesMessage(byte[] BytesArray, Dictionary<string, string> CustomHeaders)
        {
            return new BytesMessage(BytesArray, CustomHeaders);
        }

        public void Dispose()
        {
            if (nmsSession != null)
            {
                try 
                { 
                    nmsSession.Close();
                } 
                catch (Exception ex)
                {
                    if (log.IsDebugEnabled()) log.Debug("Error during session close", ex);
                }
                nmsSession = null;
                if (log.IsDebugEnabled()) log.Debug("Session closed");
            }

            if (nmsConnection != null)
            {
                try
                {
                    nmsConnection.Close();
                }
                catch (Exception ex)
                {
                    if (log.IsDebugEnabled()) log.Debug("Error during connection close", ex);
                }
                nmsConnection = null;
                if (log.IsDebugEnabled()) log.Debug("Connection closed");
            }
        }

        ~Session()
        {
            Dispose();
        }
    }
}
