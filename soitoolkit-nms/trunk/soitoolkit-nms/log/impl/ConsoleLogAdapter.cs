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
using System.IO;
using System.Text;

namespace Soitoolkit.Log.Impl
{  
    /// <remarks>  
    /// Log messages to the Console.  
    /// </remarks>  
    public class ConsoleLogAdapter : AbstractLogAdapter  { 
  
        /// <summary>
        /// Constructor   
        /// </summary>
        public ConsoleLogAdapter()
        {
        }
        
        /// <summary>  
        /// Log a message.   
        /// </summary>   
        /// <param name="Message">Message to log. </param>   
        /// <param name="Severity">Error severity level. </param>   
        public override void RecordMessage(string Message, LogLevelEnum Severity)
        {
            StringBuilder message = new StringBuilder();

            // Create the message
            message.Append(System.DateTime.Now.ToString()).Append(", ").Append(Severity.ToString().ToUpper()).Append(", ").Append(Message);

            Console.WriteLine(message.ToString());
        }  
    }
}