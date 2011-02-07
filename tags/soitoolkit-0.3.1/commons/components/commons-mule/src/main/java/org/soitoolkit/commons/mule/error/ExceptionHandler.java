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
package org.soitoolkit.commons.mule.error;

import java.util.Map;

import org.mule.DefaultExceptionStrategy;
import org.mule.api.MessagingException;
import org.mule.api.MuleException;
import org.mule.api.MuleMessage;
import org.mule.config.ExceptionHelper;
import org.soitoolkit.commons.mule.log.EventLogger;

/**
 * FIXME: Needs to be reimplemented for Mule 3.1's MessagingExceptionHandler and SystemExceptionHandler-interface
 * 
 * Base exception handler that catch errors and log them using the event-logger.
 * 
 * @author Magnus Larsson
 *
 */
public class ExceptionHandler extends DefaultExceptionStrategy {

	private static final EventLogger eventLogger = new EventLogger();

	@SuppressWarnings("unchecked")
	@Override
	protected void logException(Throwable t) {
//		No need to double log this type of errors
//		super.logException(t);

		// Inject the MuleContext in the EventLogger since we are creating the instance
		eventLogger.setMuleContext(muleContext);
		
        MuleException muleException = ExceptionHelper.getRootMuleException(t);
        if (muleException != null)
        {
        	if (muleException instanceof MessagingException) {
        		MessagingException me = (MessagingException)muleException;
            	eventLogger.logErrorEvent(muleException, me.getMuleMessage(), null, null);

        	} else {
                Map<String, Object> info = ExceptionHelper.getExceptionInfo(muleException);
            	eventLogger.logErrorEvent(muleException, info.get("Payload"), null, null);
        	}
        	
        } else {
        	eventLogger.logErrorEvent(t, (Object)null, null, null);
        }
	}

	@Override
	protected void logFatal(MuleMessage message, Throwable t) {
//		This type of fatal error (i.e. problem with the error handling itself) is best to log both with Mule's standard error-logging and our own
		super.logFatal(message, t);

		eventLogger.logErrorEvent(t, message, null, null);
	}
}