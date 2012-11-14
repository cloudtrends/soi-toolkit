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
using System.Linq;
using System.Text;
using Apache.NMS;

namespace Soitoolkit.Nms.Impl
{
    internal class TextMessage : BaseMessage, ITextMessage
    {
        // Message body represented as a simple string
        public string TextBody { get; set; }

        public TextMessage() : this(null, null) { }

        public TextMessage(string TextBody) : this(TextBody, null) { }

        public TextMessage(string TextBody, Dictionary<string, string> CustomHeaders) : base(CustomHeaders)
        {
            this.TextBody = TextBody;
        }

        /// <summary>Internal method for creating a TextMessage from a ActveMQ NMS TextMessage</summary>
        internal TextMessage(IMessage nmsMsg)
        {
            Apache.NMS.ITextMessage nmsTxtMsg = nmsMsg as Apache.NMS.ITextMessage;
            if (nmsTxtMsg == null) throw new InvalidCastException("Unexpected message of type: " + nmsMsg.GetType().Name);

            // Set the text-boxy
            string d = nmsTxtMsg.Text;
            this.TextBody = d;

            AddHeadersToBaseMessage(nmsMsg);
        }
    }
}
