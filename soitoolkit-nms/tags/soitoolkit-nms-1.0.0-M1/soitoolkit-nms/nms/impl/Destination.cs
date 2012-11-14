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
using System.Text;
using Apache.NMS;
using Apache.NMS.ActiveMQ;

namespace Soitoolkit.Nms.Impl
{
    internal class Destination : Soitoolkit.Nms.IDestination
    {
        private Soitoolkit.Log.Log log = new Soitoolkit.Log.Log();

        internal readonly Apache.NMS.IDestination NmsDestination;

        internal Destination(Apache.NMS.IDestination NmsDestination)
        {
            this.NmsDestination = NmsDestination;
        }

        public bool  IsQueue
        {
	        get {return NmsDestination.IsQueue; }
        }

        public bool  IsTemporary
        {
	        get {return NmsDestination.IsTemporary; }
        }

        public bool  IsTopic
        {
	        get {return NmsDestination.IsTopic; }
        }
    }
}