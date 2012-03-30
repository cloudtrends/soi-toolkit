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
    public interface IQueueSender : IDisposable
    {
        // Summary:
        //    Sends a message based on the supplied string as text body
        void SendMessage(string TextBody);

        // Summary:
        //    Sends a message
        void SendMessage(ITextMessage Message);

        // Summary:
        //    Sends a message and waits for a reply for the given timeout on a response on a temp-reply-queue.
        //    The temp-queue is created by the implementation of this method.
        ITextMessage SendMessageWaitForReplyOnTmpQueue(string TextBody, int timeout);
    }
}
