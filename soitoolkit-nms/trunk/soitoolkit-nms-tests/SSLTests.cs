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
    public class SSLTests : AbstractNmsTest
    {
        private static readonly int    SSL_PORT                 = Settings.Default.SSL_PORT;
        private static readonly string SSL_SERVER_CERT_HOSTNAME = Settings.Default.SSL_SERVER_CERT_HOSTNAME;
        private static readonly string SSL_CLIENT_CERT_FILENME  = Settings.Default.SSL_CLIENT_CERT_FILENME;
        private static readonly string SSL_CLIENT_CERT_PASSWORD = Settings.Default.SSL_CLIENT_CERT_PASSWORD;

        private static readonly string SSL_BROKER_URL = 
        String.Format("failover:(ssl://{0}:{1}?transport.serverName={2}&transport.clientCertFilename={3}&transport.clientCertPassword={4})?transport.timeout={5}", new object[] {
            TCP_HOSTNAME,
            SSL_PORT,
            SSL_SERVER_CERT_HOSTNAME,
            SSL_CLIENT_CERT_FILENME,
            SSL_CLIENT_CERT_PASSWORD,
            TCP_TIMEOUT });

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
        //    Verifies receive of messages using a listener, receiving messages in a separate thread
        [TestMethod]
        public void TestSslWithMutualAuthentication()
        {
            // Create a session to the message broker
            using (ISession s = SessionFactory.CreateSession(SSL_BROKER_URL))
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
    }
}