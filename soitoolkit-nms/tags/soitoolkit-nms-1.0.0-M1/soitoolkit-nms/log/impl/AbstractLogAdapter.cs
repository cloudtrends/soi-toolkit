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

namespace Soitoolkit.Log.Impl
{

    /// <remarks>  
    /// Abstract class to dictate the format for the logs that our   
    /// logger will use.  
    /// </remarks>  
    public abstract class AbstractLogAdapter  {       

        /// <summary>   
        /// Log an exception.   
        /// </summary>   
        /// <param name="Message">Exception to log. </param>   
        /// <param name="Severity">Error severity level. </param>
        public void RecordMessage(string Message, Exception Error, LogLevelEnum Severity)
        {
            if (Message == null)
            {
                Message = Error.Message;
            }
            else
            {
                Message += ", Exception: " + Error.Message;
            }
            this.RecordMessage(Message, Severity);
            this.RecordMessage(Error.StackTrace, Severity);
        }

        /// <summary>   
        /// Abstract method to Log a message.
        /// </summary>   
        public abstract void RecordMessage(string Message, LogLevelEnum Severity);  

    }
}