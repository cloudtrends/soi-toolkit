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
    public interface IQueueReceiver : IDisposable
    {
        // Summary:
        //    Waits until a message is available and returns it
        ITextMessage Receive();

        // Summary:
        //    If a message is available within the timeout duration it is returned otherwise this method returns null
        ITextMessage Receive(TimeSpan timeout);

        byte[] ReceiveBytesMessage(TimeSpan timeout);

        // Summary:
        //    Register a Message Listerner
        event MessageReceivedDelegate OnMessageReceived;

        // Summary:
        //    Start Message Listerners
        void StartListener();

        // Summary:
        //    Send a response message to the incoming messages ReplyTo-queue in a Asynchronous Request/Response scenario
        void SendReplyMessage(ITextMessage RequestMessage, ITextMessage ResponseMessage);

        // Summary:
        //    Send a response message to the incoming messages ReplyTo-queue in a Asynchronous Request/Response scenario
        void SendReplyMessage(ITextMessage RequestMessage, string ResponseTextBody);
    }
}
