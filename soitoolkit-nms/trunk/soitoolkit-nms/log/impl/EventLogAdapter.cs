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
using System.Diagnostics;
using System.Text;

namespace Soitoolkit.Log.Impl
{  
    /// <remarks>  
    /// Log messages to the Windows Event Log.  
    /// </remarks>  
    public class EventLogAdapter : AbstractLogAdapter  {
        
        // Internal EventLogName destination value
        private string _EventLogName = "";
        
        /// <value>Get or set the name of the destination log</value>
        public string EventLogName   {
            get { return this._EventLogName; }
            set { this._EventLogName = value; }
        }
        
        // Internal EventLogSource value
        private string _EventLogSource;
        
        /// <value>Get or set the name of the source of entry</value>
        public string EventLogSource      {
            get { return this._EventLogSource; }
            set { this._EventLogSource = value; }
        } 
        
        // Internal MachineName value
        private string _MachineName = "";
        
        /// <value>Get or set the name of the computer</value>
        public string MachineName   {
            get { return this._MachineName; }
            set { this._MachineName = value; }
        }

        /// <summary>
        /// Constructor   
        /// </summary>   
        public EventLogAdapter(string EventLogName, string EventLogSource) : this()
        {
            this.EventLogName   = EventLogName;
            this.EventLogSource = EventLogSource;
        }

        /// <summary>
        /// Constructor   
        /// </summary>   
        public EventLogAdapter()
        {
            this.MachineName = ".";
            //            this.EventLogName = "Apache ActiveMQ NMS";
            //            this.EventLogSource = "ActiveMQ Adapter";
            this.EventLogName = "MyEventLog";
            this.EventLogSource = "ActiveMQ-Adapter";
        }

        /// <summary>   
        /// Log a message.   
        /// </summary>   
        /// <param name="Message">Message to log.</param>   
        /// <param name="Severity">Error severity level.</param>
        public override void RecordMessage(string Message, LogLevelEnum Severity)
        {
            StringBuilder message = new StringBuilder();
            System.Diagnostics.EventLog eventLog = new System.Diagnostics.EventLog();
            
            // Create the source if it does not already exist
            if( !System.Diagnostics.EventLog.SourceExists(this._EventLogSource) )     {
                System.Diagnostics.EventLog.CreateEventSource(this._EventLogSource, this._EventLogName);
            }
            
            eventLog.Source = this._EventLogSource;
            eventLog.MachineName = this._MachineName;
            
            // Determine what the EventLogEventType should be
            // based on the LogSeverity passed in
            EventLogEntryType type = EventLogEntryType.Information; 
            
            switch(Severity.ToString())     {
                case "DEBUG": type = EventLogEntryType.Information;
                    break;

                case "INFO":  type = EventLogEntryType.Information;
                    break;

                case "WARN":  type = EventLogEntryType.Warning;
                    break;

                case "ERROR": type = EventLogEntryType.Error;
                    break;
            }

            message.Append(Severity.ToString()).Append(" ").Append(Message);
            eventLog.WriteEntry(message.ToString(), type);
        }
    }
}