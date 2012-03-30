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
    internal class TextMessage : ITextMessage
    {
        // Message body represented as a simple string
        public string TextBody { get; set; }

        // Relevant NMS headers
        public string          NMSCorrelationID { get; set; }
        public MsgDeliveryMode NMSDeliveryMode  { get; set; }
        public string          NMSMessageId     { get; set; }
        public MsgPriority     NMSPriority      { get; set; }
        public bool            NMSRedelivered   { get; set; }
        public IDestination    NMSReplyTo       { get; set; }
        public DateTime        NMSTimestamp     { get; set; }
        public TimeSpan        NMSTimeToLive    { get; set; }

        // Custom headers
        private Dictionary<string, string> _customHeaders = null;
        public  Dictionary<string, string> CustomHeaders
        {
            get
            {
                if (_customHeaders == null) _customHeaders = new Dictionary<string, string>();
                return _customHeaders;
            }
            set
            {
                _customHeaders = value;
            }
        }

        public TextMessage() : this(null, null) { }

        public TextMessage(string TextBody) : this(TextBody, null) { }

        public TextMessage(string TextBody, Dictionary<string, string> CustomHeaders)
        {
            this.TextBody = TextBody;
            this.CustomHeaders = CustomHeaders;
        }

        /// <summary>Internal method for creating a TextMessage from a ActveMQ NMS TextMessage</summary>
        internal TextMessage(IMessage nmsMsg)
        {
            Apache.NMS.ITextMessage nmsTxtMsg = nmsMsg as Apache.NMS.ITextMessage;
            if (nmsTxtMsg == null) throw new InvalidCastException("Unexpected message of type: " + nmsMsg.GetType().Name);

            // Set the text-boxy
            string d = nmsTxtMsg.Text;
            this.TextBody = d;

            // Add nms headers if any
            NMSDeliveryMode = (MsgDeliveryMode)nmsMsg.NMSDeliveryMode; // Can't be null so no need to test...
            NMSPriority     = (MsgPriority)nmsMsg.NMSPriority;         // Can't be null so no need to test...
            NMSRedelivered  = nmsMsg.NMSRedelivered;                   // Can't be null so no need to test...

            if (nmsMsg.NMSCorrelationID  != null) NMSCorrelationID = nmsMsg.NMSCorrelationID;
            if (nmsMsg.NMSMessageId      != null) NMSMessageId     = nmsMsg.NMSMessageId;
            if (nmsMsg.NMSTimestamp.Year >  1)    NMSTimestamp     = nmsMsg.NMSTimestamp;
            if (nmsMsg.NMSTimeToLive     != null) NMSTimeToLive    = nmsMsg.NMSTimeToLive;
            if (nmsMsg.NMSReplyTo        != null) NMSReplyTo       = new Destination(nmsMsg.NMSReplyTo);

            // Add custom headers if any
            if (nmsMsg.Properties != null)
            {
                foreach (string hdr in nmsMsg.Properties.Keys)
                {
                    CustomHeaders.Add(hdr, nmsMsg.Properties.GetString(hdr));
                }
            }
        
        }

        /// <summary>Internal method for copying nsm and custom headers, if any.</summary>
        internal void CopyHeadersToNmsMessage(IMessage nmsMsg) 
        {
            // Add nms headers if any
            nmsMsg.NMSDeliveryMode = (Apache.NMS.MsgDeliveryMode)NMSDeliveryMode; // Can't be null so no need to test...
            nmsMsg.NMSPriority     = (Apache.NMS.MsgPriority)NMSPriority;         // Can't be null so no need to test...
            nmsMsg.NMSRedelivered  = NMSRedelivered;                              // Can't be null so no need to test...

            if (NMSCorrelationID   != null) nmsMsg.NMSCorrelationID = NMSCorrelationID;
            if (NMSMessageId       != null) nmsMsg.NMSMessageId     = NMSMessageId;
            if (NMSTimestamp.Year  > 1)     nmsMsg.NMSTimestamp     = NMSTimestamp;
            if (NMSTimeToLive      != null) nmsMsg.NMSTimeToLive    = NMSTimeToLive;
            if (NMSReplyTo         != null) nmsMsg.NMSReplyTo       = ((Destination)NMSReplyTo).NmsDestination;

            // Add custom headers if any
            if (CustomHeaders != null)
            {
                foreach (string hdr in CustomHeaders.Keys)
                {
                    nmsMsg.Properties.SetString(hdr, CustomHeaders[hdr]);
                }
            }
        }
    }
}
