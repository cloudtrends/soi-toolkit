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
using soitoolkit_nms_tests.Properties;

namespace Soitoolkit.Nms.Tests 
{
    [TestClass]
    public class QueueTests : AbstractNmsTest
    {
        private static readonly int LONG_WAIT = Settings.Default.LONG_WAIT_MS;

        [ClassInitialize]
        public static void InitClass(TestContext testContext) 
        {
            // Direct logging to the console with DEBUG level for easy debugging of unit-tests
            LogFactory.LogLevel = LogLevelEnum.DEBUG;
            LogFactory.LogAdapter = new ConsoleLogAdapter();
        }

        [TestInitialize]
        public void InitTest()
        {
            // Direct logging to the console 
            LogFactory.LogAdapter = new ConsoleLogAdapter();

            // Lower loglevel during the purge operation to WARN
            LogFactory.LogLevel = LogLevelEnum.WARN;

            // Purge test test-queue
            PurgeQueue(BROKER_URL, TEST_QUEUE);

            // Increase the loglevel to DEBUG level for easy debugging of unit-tests
            LogFactory.LogLevel = LogLevelEnum.DEBUG;
        }

        // Summary:
        //    Verifies polling receive of messages with a receive timeout
        [TestMethod]
        public void TestSendAndReceiveWithPollTimeout()
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

        // Summary:
        //    Verifies polling receive of messages with a receive timeout in the case where no mesages are sent withing the timeout
        [TestMethod]
        public void TestReceiveOnlyWithPollTimeout()
        {
            // Create a session to the message broker
            using (ISession s = SessionFactory.CreateSession(BROKER_URL))
            {
                // List of received test messages
                List<string> msgs = new List<string>();

                // Create a receiver for the test queue
                using (IQueueReceiver qr = s.CreateQueueReceiver(TEST_QUEUE))
                {
                    ITextMessage msg = null;
                    do	
                    {
                        msg = qr.Receive(SHORT_WAIT_TS);
                        if (msg != null)  msgs.Add(msg.TextBody);
                    } 
                    while (msg != null);
                }

                // Verify that the expected messages where received
                Assert.AreEqual(msgs.Count, 0);
            }
        }

        // Summary:
        //    Verifies polling receive of messages without any timeout
        [TestMethod]
        public void TestSendAndReceiveWithPoll()
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
                    // Read the two messages and add them to the array and nothing more since the receive call will not return until it has read a message...
                    msgs.Add(qr.Receive().TextBody);
                    msgs.Add(qr.Receive().TextBody);
                }

                // Verify that the expected messages where received
                Assert.AreEqual(msgs.Count, 2);
                Assert.IsTrue(msgs.Contains(TEST_MSG_1));
                Assert.IsTrue(msgs.Contains(TEST_MSG_2));
            }
        }

        // Summary:
        //    Verifies receive of messages using a listener, receiving messages in a separate thread
        [TestMethod]
        public void TestSendAndReceiveWithListener()
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
                    // Setup a listener for incoming messages that simply stores the received messaged in the list
                    qr.OnMessageReceived += Message => msgs.Add(Message.TextBody);

                    // Start the listener
                    qr.StartListener();

                    // Wait for a while for the messages to be received
                    Thread.Sleep(SHORT_WAIT);
                }

                // Verify that the expected messages where received
                Assert.AreEqual(msgs.Count, 2);
                Assert.IsTrue(msgs.Contains(TEST_MSG_1));
                Assert.IsTrue(msgs.Contains(TEST_MSG_2));
            }
        }

        // Summary:
        //    Verifies use of asynchronous request/response using a ReplyTo queue
        [TestMethod]
        public void TestSendAndReceiveWithReplyTo()
        {
            // Create a session to the message broker
            using (ISession s = SessionFactory.CreateSession(BROKER_URL))
            {

                // Create a receiver that sends back the incoming message on a reply-to queue
                using (IQueueReceiver qr = s.CreateQueueReceiver(TEST_QUEUE))
                {

                    // Setup a listener for incoming messages that simply stores the received messaged in the list
                    qr.OnMessageReceived += RequestMessage =>
                    {
                        string responseTextBody = RequestMessage.TextBody;
                        qr.SendReplyMessage(RequestMessage, responseTextBody);
                    };

                    // Start the listener
                    qr.StartListener();

                    // Create a sender
                    using (IQueueSender qs = s.CreateQueueSender(TEST_QUEUE))
                    {
                        // Send a test message and wait for reply on a temp queue
                        ITextMessage response = qs.SendMessageWaitForReplyOnTmpQueue(TEST_MSG_1, LONG_WAIT);

                        // Verify that the expected message where received
                        Assert.IsTrue(TEST_MSG_1.Equals(response.TextBody));


                        // Send another test message and wait for reply on a temp queue
                        response = qs.SendMessageWaitForReplyOnTmpQueue(TEST_MSG_2, LONG_WAIT);

                        // Verify that the expected message where received
                        Assert.IsTrue(TEST_MSG_2.Equals(response.TextBody));
                    }
                }
            }
        }

        ///<Summary>
        /// Verifies sending and receiving bytes messages...
        ///</Summary>
        [TestMethod]
        public void TestSendAndReceiveBytesWithPollTimeout()
        {
            // Create a session to the message broker
            using (ISession s = SessionFactory.CreateSession(BROKER_URL))
            {
                // Send some test messages to the test queue, test bytes message = new byte[] { 1, 2, 3 }
                SendTestBytesMsg(s, TEST_BYTES_MSG_1);

                // List of received test messages
                List<byte[]> msgs = new List<byte[]>();

                // Create a receiver for the test queue
                using (IQueueReceiver qr = s.CreateQueueReceiver(TEST_BYTES_QUEUE))
                {
                    byte[] msg = null;
                    do
                    {
                        msg = qr.ReceiveBytesMessage(SHORT_WAIT_TS);
                        if (msg != null) msgs.Add(msg);
                    }
                    while (msg != null);
                }

                // Verify that the expected messages where received
                Assert.AreEqual(msgs.Count, 1);
                byte[] expected = TEST_BYTES_MSG_1;
                byte[] actual = msgs[0];

                Assert.AreEqual(expected.Length, actual.Length);

                for (int i = 0; i < expected.Length; i++)
                {
                    log.Debug(expected[i] + " = " + actual[i] + "?");
                    Assert.AreEqual(expected[i], actual[i]);
                }
            }
        }
    }
}