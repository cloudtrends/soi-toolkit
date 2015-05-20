# Configuration of Java Service Wrapper #

This guide gives recommendations of how to configure Java Service Wrapper.

## Prevent problems with JSW processes that fails to start ##

### Background ###

This problem relates to Java Service Wrapper usage on Windows only.

If a Java Service Wrapper process (e.g. Mule or ActiveMQ) fails to start it can be very hard to shut it down to be able to fix the problem. In worst case a restart of the Windows server might be required, typically very inconvenient...

There is no controlled way to stop a Java Service Wrapper process that hangs during its startup phase and killing the process just results in Java Service Wrapper to restart it and it hangs again...

It seems to be a problem in Microsoft Windows to stop a Windows Service that is initializing during startup. You can't use the User Interface in Services since both start and stop buttons are grayed out and a `net stop` command fails as well:

```
C:\> net stop mule
The requested pause, continue, or stop is not valid for this service.

More help is available by typing NET helpmsg 2191st
```

A started service is however no problem to stop like:
```
C:\> net stop mule
The mule service is stopping .......
The mule service was stopped successfully.
```

### Configuration ###

To be able to stop a Java service that hangs during startup you need to temporary disable the restart feature of the Java Service Wrapper. Tht is done by adding the following line to the `wrapper.conf` - file:

```
wrapper.disable_restarts = TRUE
```

However, by default, Java Service Wrapper only reads the configuration file at startup so it won't help changing this one the Java Service Wrapper has started. By adding the following line to the `wrapper.conf` - file you can however instruct the Java Service Wrapper to reload the configuration file before restarting the Java process:

```
wrapper.restart.reload_configuration = TRUE
```

Now you can kill the failing Java process and the Java Service Wrapper will not try to restart it!

**NOTE**: Once you have solved the problem you must not forget to set the restart parameter to FALSE again.

```
wrapper.disable_restarts = FALSE
```

**HINT**: If you are not sure what Java process to kill in the you can use the JConsole that comes with the Java SE JDK to see what Java process is the one you search for:

Finally, the problem with the java process, you should stop, just guessing and aim of Task Manager seems a bit frivolous, to say the least :-)

![http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/JswConfig/JSW-process-hangs-jconsole-help.png](http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/JswConfig/JSW-process-hangs-jconsole-help.png)