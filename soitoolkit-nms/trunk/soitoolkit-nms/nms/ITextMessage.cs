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

namespace Soitoolkit.Nms
{
    public enum MsgDeliveryMode
    {
        Persistent = 0,
        NonPersistent = 1
    }

    public enum MsgPriority
    {
        Lowest = 0,
        VeryLow = 1,
        Low = 2,
        AboveLow = 3,
        BelowNormal = 4,
        Normal = 5,
        AboveNormal = 6,
        High = 7,
        VeryHigh = 8,
        Highest = 9
    }

    public interface ITextMessage
    {
        // Message body represented as a simple string
        string TextBody { get; set; }

        // Relevant NMS headers
        string          NMSCorrelationID   { get; set; }
        MsgDeliveryMode NMSDeliveryMode    { get; set; }
        string          NMSMessageId       { get; }
        MsgPriority     NMSPriority        { get; set; }
        bool            NMSRedelivered     { get; }
        IDestination    NMSReplyTo         { get; }
        DateTime        NMSTimestamp       { get; }
        TimeSpan        NMSTimeToLive      { get; set; }

        // Custom headers
        Dictionary<string, string> CustomHeaders { get; set; }
    }
}
