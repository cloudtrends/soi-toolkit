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
using Soitoolkit.Log;

namespace Soitoolkit.Log.Impl
{
    public class NmsTraceAdapter : Apache.NMS.ITrace
    {
        private Log log = new Log();

        public void Debug(string message)
        {
            log.Debug(string.Format("NMS-Trace: {0}", message));
        }

        public void Error(string message)
        {
            log.Error(string.Format("NMS-Trace: {0}", message));
        }

        public void Fatal(string message)
        {
            log.Error(string.Format("NMS-Trace: {0}", message));
        }

        public void Info(string message)
        {
            log.Info(string.Format("NMS-Trace: {0}", message));
        }

        public void Warn(string message)
        {
            log.Warn(string.Format("NMS-Trace: {0}", message));
        }

        public bool IsDebugEnabled
        {
            get { return log.IsDebugEnabled(); }
        }

        public bool IsErrorEnabled
        {
            get { return log.IsErrorEnabled(); }
        }

        public bool IsFatalEnabled
        {
            get { return log.IsErrorEnabled(); }
        }

        public bool IsInfoEnabled
        {
            get { return log.IsInfoEnabled(); }
        }

        public bool IsWarnEnabled
        {
            get { return log.IsWarnEnabled(); }
        }

    }
}
