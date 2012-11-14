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
using Soitoolkit.Log.Impl;
using Apache.NMS;

namespace Soitoolkit.Log
{
    public class LogFactory
    {
        static LogFactory()
        {
            Tracer.Trace = new NmsTraceAdapter();
        }

        // Internal log adapter object
        private static AbstractLogAdapter _LogAdapter = new EventLogAdapter();

        // Internal log level value   
        private static LogLevelEnum _LogLevel = LogLevelEnum.WARN;

        /// <value>Get or set the log adapter object</value>
        public static AbstractLogAdapter LogAdapter
        {
            get { return _LogAdapter; }
            set { _LogAdapter = value; }
        }

        /// <value>Get or set the log level</value>
        public static LogLevelEnum LogLevel
        {
            get { return _LogLevel; }
            set { _LogLevel = value; }
        }
    }
}