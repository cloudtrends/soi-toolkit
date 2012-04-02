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
using System.Collections.Generic;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Soitoolkit.Log;
using Soitoolkit.Log.Impl;
using System;
using System.Diagnostics;

namespace Soitoolkit.Nms.Tests
{
    [TestClass]
    public class AbstractNmsTest
    {
//      protected static readonly string   BROKER_URL    = "failover:(tcp://magnusmac:61616)";
        protected static readonly string BROKER_URL = "tcp://10.211.55.2:61616"; // During tests a plain TCP-connection is better for detection of problems during tests (e.g. with failover-protocol the tests just hang if the broker is not started...)
        protected static readonly string TEST_QUEUE = "my-test-queue";
        protected static readonly string TEST_MSG_1 = "message 1";
        protected static readonly string TEST_MSG_2 = "message 2";
        protected static readonly int SHORT_WAIT = 500;
        protected static readonly TimeSpan SHORT_WAIT_TS = TimeSpan.FromMilliseconds(SHORT_WAIT);

        protected Soitoolkit.Log.Log log = new Soitoolkit.Log.Log();

        protected void DoOneTest()
        {
            // Create a session to the message broker
            using (ISession s = SessionFactory.CreateSession(BROKER_URL))
            {
                // Send some test messages to the test queue
                SendTestMsgs(s, new string[] { TEST_MSG_1, TEST_MSG_2 });

                // List of received test messages
                List<string> msgs = new List<string>();

                // Create a receiver for the test queue
                using (IQueueReceiver qr = s.CreateQueueReceiver(TEST_QUEUE))
                {
                    ITextMessage msg = null;
                    do
                    {
                        msg = qr.Receive(SHORT_WAIT_TS);
                        if (msg != null) msgs.Add(msg.TextBody);
                    }
                    while (msg != null);
                }

                // Verify that the expected messages where received
                Assert.AreEqual(msgs.Count, 2);
                Assert.IsTrue(msgs.Contains(TEST_MSG_1));
                Assert.IsTrue(msgs.Contains(TEST_MSG_2));
            }
        }

        protected void PurgeQueue(string BrokerUrl, string Queue)
        {
            // Create a session to the message broker
            using (ISession s = SessionFactory.CreateSession(BrokerUrl))
            {

                // Create a receiver for the queue
                using (IQueueReceiver qr = s.CreateQueueReceiver(Queue))
                {
                    // Setup a listener for incoming messages that simply writes a warning for each pruged message
                    qr.OnMessageReceived += Message => log.Warn("Purged message: " + Message.TextBody);

                    // Start the listener
                    qr.StartListener();

                    // Wait for a while for the messages to be purged
                    Thread.Sleep(SHORT_WAIT);
                }
            }
        }

        protected void SendTestMsgs(ISession s, string[] msgs)
        {
            // Create a sender and send some test messages to a test queue
            using (IQueueSender qs = s.CreateQueueSender(TEST_QUEUE))
            {
                foreach (string msg in msgs)
                {
                    qs.SendMessage(msg);
                }
            }
        }
    }
}