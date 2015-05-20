**Content**


# Customizing the log-mechanism #

**NOTE**: Support for customizing the log-mechanism in soi-toolkit is introduced in v0.4.1 on an experimental level only. Try it out but be prepared for changes in coming releases!

## Introduction ##

To override the default behavior of handling logging in soi-toolkit a custom EventLogger can be declared in an integration components service-project.

## Details ##

The declaration should be placed in the common-xml-config-file: `src/main/resources/${artifactId}-common.xml`.

```
 <spring:beans>
   <spring:bean name="soitoolkit.eventLogger" class="org.sample.MyCustomEventLogger" primary="true"/>
 </spring:beans>
```

**NOTE:** The xml-attribute `primary="true"` allows us to autowire byName to override the DefaultEventLogger when we in the future expose it as a Spring bean (not in v0.4.1).

It is recommended to implement the custom event logger by specializing the DefaultEventLogger and override its public and protected methods.

## Example ##

Say that you want to disable the default sending of log-events to the default JMS log-queues 		`SOITOOLKIT.LOG.INFO` and `SOITOOLKIT.LOG.ERROR`:

```
package org.sample;

import org.soitoolkit.commons.mule.log.DefaultEventLogger;

public class MyCustomEventLogger extends DefaultEventLogger {

	@Override
	protected void dispatchInfoEvent(String msg) {
		// Do nothing here, instead rely only on the standard log4j logging
	}

	@Override
	protected void dispatchErrorEvent(String msg) {
		// Do nothing here, instead rely only on the standard log4j logging
	}
}
```