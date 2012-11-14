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
        
        public static readonly string         DEFAULT_EVENT_LOG_NAME    = "soi-toolkit-nms-log";
        public static readonly string         DEFAULT_EVENT_LOG_SOURCE  = "My-ActiveMQ-App";
        public static readonly long           DEFAULT_MAXIMUM_KILOBYTES = 1024;
        public static readonly OverflowAction DEFAULT_OVERFLOW_ACTION   = OverflowAction.OverwriteAsNeeded;
        public static readonly int            DEFAULT_RETENTION_DAYS    = 7;

    // EventLogName destination value
        private string _EventLogName = "";
        
        /// <value>Get or set the name of the destination log</value>
        public string EventLogName   {
            get { return this._EventLogName; }
            set { this._EventLogName = value; }
        }
        
        // EventLogSource value
        private string _EventLogSource;
        
        /// <value>Get or set the name of the source of entry</value>
        public string EventLogSource      {
            get { return this._EventLogSource; }
            set { this._EventLogSource = value; }
        } 
    
        // MaximumKilobytes value
        private long _MaximumKilobytes;
        
        /// <value>Get or set the size of the logfile</value>
        public long MaximumKilobytes      {
            get { return this._MaximumKilobytes; }
            set { this._MaximumKilobytes = value; }
        }

        // OverflowAction value
        private OverflowAction _OverflowAction;
        
        /// <value>Get or set the logfile overflow action</value>
        public OverflowAction OverflowAction      {
            get { return this._OverflowAction; }
            set { this._OverflowAction = value; }
        }

        // RetentionDays value
        private int _RetentionDays;

        /// <value>Get or set the number of retentionDays</value>
        public int RetentionDays
        {
            get { return this._RetentionDays; }
            set { this._RetentionDays = value; }
        }

        // The event log
        private EventLog _EventLog;

        /// <value>Get the EventLog if required</value>
        public EventLog EventLog
        {
            get { return this._EventLog; }
        }

        /// <summary>
        /// Constructor   
        /// </summary>   
        public EventLogAdapter(string EventLogName, string EventLogSource,  long MaximumKilobytes, OverflowAction OverflowAction, int RetentionDays)
        {
            InitEventLogAdapter(EventLogName, EventLogSource, MaximumKilobytes, OverflowAction, RetentionDays);
        }

        /// <summary>
        /// Constructor, defaults MaximumKilobytes OverflowAction and RetentionDays to DEFAULT_MAXIMUM_KILOBYTES, DEFAULT_OVERFLOW_ACTION, and DEFAULT_RETENTION_DAYS.
        /// </summary>   
        public EventLogAdapter(string EventLogName, string EventLogSource)
        {
            InitEventLogAdapter(EventLogName, EventLogSource, DEFAULT_MAXIMUM_KILOBYTES, DEFAULT_OVERFLOW_ACTION, DEFAULT_RETENTION_DAYS);
        }

        /// <summary>
        /// Constructor, defaults EventLogName, EventLogSource, MaximumKilobytes, OverflowAction and RetentionDays to DEFAULT_EVENT_LOG_NAME, DEFAULT_EVENT_LOG_SOURCE, DEFAULT_MAXIMUM_KILOBYTES, DEFAULT_OVERFLOW_ACTION, DEFAULT_RETENTION_DAYS.
        /// </summary>   
        public EventLogAdapter()
        {
            InitEventLogAdapter(DEFAULT_EVENT_LOG_NAME, DEFAULT_EVENT_LOG_SOURCE, DEFAULT_MAXIMUM_KILOBYTES, DEFAULT_OVERFLOW_ACTION, DEFAULT_RETENTION_DAYS);
        }

        /// <summary>
        /// Initiates the EventLog for the constructors   
        /// </summary>   
        private void InitEventLogAdapter(string EventLogName, string EventLogSource, long MaximumKilobytes, OverflowAction OverflowAction, int RetentionDays)
        {
            this.EventLogName = EventLogName;
            this.EventLogSource = EventLogSource;
            this.MaximumKilobytes = MaximumKilobytes;
            this.OverflowAction = OverflowAction;
            this.RetentionDays = RetentionDays;

            _EventLog = new EventLog();

            // Create the source if it does not already exist
            if (!EventLog.SourceExists(_EventLogSource))
            {
                EventLog.CreateEventSource(_EventLogSource, _EventLogName);
            }

            _EventLog.Source = _EventLogSource;
            _EventLog.Log = _EventLogName;
            _EventLog.MaximumKilobytes = _MaximumKilobytes;
            _EventLog.ModifyOverflowPolicy(_OverflowAction, _RetentionDays);
        }


        /// <summary>   
        /// Log a message.   
        /// </summary>   
        /// <param name="Message">Message to log.</param>   
        /// <param name="Severity">Error severity level.</param>
        public override void RecordMessage(string Message, LogLevelEnum Severity)
        {

            StringBuilder message = new StringBuilder();

            // Determine what the EventLogEventType should be
            // based on the LogSeverity passed in
            EventLogEntryType type = EventLogEntryType.Information; 
            
            switch(Severity.ToString()) {
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
            _EventLog.WriteEntry(message.ToString(), type);
        }
    }
}