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
using Soitoolkit.Log.Impl;

namespace Soitoolkit.Log
{
    /// <remarks>  
    /// Managing class to provide the interface for and control  
    /// application logging.  It utilizes the logging objects in   
    /// ErrorLog.Logs to perform the actual logging as configured.    
    /// </remarks>  
    public class Log  {

        //
        // DEBUG - Log  
        //
        public bool IsDebugEnabled()
        {
            return IsLogLevelEnabled(LogLevelEnum.DEBUG);
        }
        public void Debug(string Message)
        {
            RecordMessage(Message, LogLevelEnum.DEBUG);
        }
        public void Debug(string Message, Exception Error)
        {
            RecordMessage(Message, Error, LogLevelEnum.DEBUG);
        }

        //
        // INFO - Log  
        //
        public bool IsInfoEnabled()
        {
            return IsLogLevelEnabled(LogLevelEnum.INFO);
        }
        public void Info(string Message)
        {
            RecordMessage(Message, LogLevelEnum.INFO);
        }
        public void Info(string Message, Exception Error)
        {
            RecordMessage(Message, Error, LogLevelEnum.INFO);
        }

        //
        // WARN - Log  
        //
        public bool IsWarnEnabled()
        {
            return IsLogLevelEnabled(LogLevelEnum.WARN);
        }
        public void Warn(string Message)
        {
            RecordMessage(Message, LogLevelEnum.WARN);
        }
        public void Warn(string Message, Exception Error)
        {
            RecordMessage(Message, Error, LogLevelEnum.WARN);
        }

        //
        // ERROR - Log
        //
        public bool IsErrorEnabled()
        {
            return IsLogLevelEnabled(LogLevelEnum.ERROR);
        }
        public void Error(string Message)
        {
            RecordMessage(Message, LogLevelEnum.ERROR);
        }
        public void Error(string Message, Exception Error)
        {
            RecordMessage(Message, Error, LogLevelEnum.ERROR);
        }

        //
        // PRIVATE METHODS
        //

        protected bool IsLogLevelEnabled(LogLevelEnum Severity)
        {
            return (int)Severity >= (int)LogFactory.LogLevel;
        }

        /// <summary>   
        /// Log an exception.   
        /// </summary>   
        /// <param name="Message">Exception to log.</param>   
        /// <param name="Severity">Error severity level.</param>
        protected void RecordMessage(string Message, Exception Error, LogLevelEnum Severity)
        {
            if (IsLogLevelEnabled(Severity)) 
            {
                LogFactory.LogAdapter.RecordMessage(Message, Error, Severity);
            }
        }

        /// <summary>   
        /// Log a message.   
        /// </summary>   
        /// <param name="Message">Message to log.</param>   
        /// <param name="Severity">Error severity level.</param>
        protected void RecordMessage(string Message, LogLevelEnum Severity)
        {
            if (IsLogLevelEnabled(Severity)) 
            {
                LogFactory.LogAdapter.RecordMessage(Message, Severity);
            }
        }
    
    }
}
