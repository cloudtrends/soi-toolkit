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
    internal class BytesMessage : BaseMessage, IBytesMessage
    {
        // Message body represented as a byte array
        public byte[] BytesBody { get; set; }

        public BytesMessage() : this(null, null) { }

        public BytesMessage(byte[] BytesBody) : this(BytesBody, null) { }

        public BytesMessage(byte[] BytesBody, Dictionary<string, string> CustomHeaders) : base(CustomHeaders)
        {
            this.BytesBody = BytesBody;
        }

        /// <summary>Internal method for creating a TextMessage from a ActveMQ NMS TextMessage</summary>
        internal BytesMessage(IMessage nmsMsg)
        {
            Apache.NMS.IBytesMessage nmsBytesMsg = nmsMsg as Apache.NMS.IBytesMessage;
            if (nmsBytesMsg == null) throw new InvalidCastException("Unexpected message of type: " + nmsMsg.GetType().Name);

            // Set the text-boxy
            byte[] bArr = nmsBytesMsg.Content;
            this.BytesBody = bArr;

            AddHeadersToBaseMessage(nmsMsg);
        
        }
    }
}