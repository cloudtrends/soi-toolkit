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
    public class EventTests : AbstractNmsTest
    {
        [TestInitialize]
        public void InitTest()
        {
            // Purge test test-queue
            PurgeQueue(BROKER_URL, TEST_QUEUE);
        }

        // Summary:
        //    Verifies that a "log file is full" exception is NOT throwed if the log file big enough
        [TestMethod]
        public void TestEventLogOk_BigLogFile()
        {
            // Init EventLog to avoid a "log file is full" exception during the 100 calls to DoOneTest()
            LogFactory.LogLevel = LogLevelEnum.DEBUG;
            EventLogAdapter ela = new EventLogAdapter(
                EventLogAdapter.DEFAULT_EVENT_LOG_NAME,
                EventLogAdapter.DEFAULT_EVENT_LOG_SOURCE,
                2048,
                OverflowAction.DoNotOverwrite,
                EventLogAdapter.DEFAULT_RETENTION_DAYS);
            ela.EventLog.Clear();
            LogFactory.LogAdapter = ela;


            // 10 runs creates 404 log-event
            // 2048kB can hold 6000 log-events
            // 100 rows should blow the log-queue with its 4000 log-events if no overwrite...
            for (int i = 0; i < 100; i++)
            {
                DoOneTest();
                Console.WriteLine(i + ": " + System.DateTime.Now.ToString());
            }

            // 4025
            Console.WriteLine("Log events: " + ela.EventLog.Entries.Count);
            // Verify that we have at least 3900 log messages, due to the large log size
            Assert.IsTrue(ela.EventLog.Entries.Count > 3900);
        }

        // Summary:
        //    Verifies that a "log file is full" exception is NOT throwed if the log file is too small but configured to overrite old log entries if required
        [TestMethod]
        public void TestEventLogOk_SmallLogFileOverwriteOldEntries()
        {
            // Init EventLog to avoid a "log file is full" exception during the 100 calls to DoOneTest()
            LogFactory.LogLevel = LogLevelEnum.DEBUG;
            EventLogAdapter ela = new EventLogAdapter(
                EventLogAdapter.DEFAULT_EVENT_LOG_NAME,
                EventLogAdapter.DEFAULT_EVENT_LOG_SOURCE,
                1024,
                OverflowAction.OverwriteAsNeeded,
                EventLogAdapter.DEFAULT_RETENTION_DAYS);
            ela.EventLog.Clear();
            LogFactory.LogAdapter = ela;


            // 10 runs creates 404 log-event
            // 1024kB can hold 3000 log-events
            // 100 rows should blow the log-queue with its 4000 log-events if no overwrite, let's rely on that the overwrite works...
            for (int i = 0; i < 100; i++)
            {
                DoOneTest();
                Console.WriteLine(i + ": " + System.DateTime.Now.ToString());
            }

            Console.WriteLine("Log events: " + ela.EventLog.Entries.Count);
            // Verify that we have max 3000 log messages (> 4000 sent)), i.e. ensure that we have overwritten some of the oldest log events
            Assert.IsTrue(ela.EventLog.Entries.Count < 3000);
        }

        // Summary:
        //    Verifies that a "log file is full" exception is throwed if the log file is filled up
        [TestMethod]
        public void TestEventLog_FailOnLogFileIsFull()
        {
            // Init EventLog to cause a "log file is full" exception after 74 calls to DoOneTest()
            LogFactory.LogLevel = LogLevelEnum.DEBUG;
            EventLogAdapter ela = new EventLogAdapter(
                EventLogAdapter.DEFAULT_EVENT_LOG_NAME,
                EventLogAdapter.DEFAULT_EVENT_LOG_SOURCE,
                1024,
                OverflowAction.DoNotOverwrite,
                EventLogAdapter.DEFAULT_RETENTION_DAYS);
            ela.EventLog.Clear();
            LogFactory.LogAdapter = ela;


            // 10 runs creates 404 log-event
            // 1024kB can hold 3000 log-events
            // 100 rows should blow the log-queue with its 4000 log-events if no overwrite...
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    DoOneTest();
                    Console.WriteLine(i + ": " + System.DateTime.Now.ToString());
                }
                Assert.Fail("Expected exception regarding 'log file is full' here");
            }

            catch (AssertFailedException ex)
            {
                // Simply rethrow an assert exception, that one is important to simply rethrow for correct test-results, i.e. a failure :-)
                Console.WriteLine(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                // Expect System.ComponentModel.Win32Exception: The event log file is full
                Assert.AreEqual("System.ComponentModel.Win32Exception", ex.GetType().FullName);
                Assert.AreEqual("The event log file is full", ex.Message);
                Console.WriteLine("Ok error: " + ex.GetType().FullName + ":" + ex.Message);
            }

            Console.WriteLine("Log events: " + ela.EventLog.Entries.Count);
            // Verify that we have max 3000 log messages (> 4000 sent)), i.e. ensure that we got the "log file is full" when expected, when 1024 kB was filled up.
            Assert.IsTrue(ela.EventLog.Entries.Count < 3000);
        }
    }
}