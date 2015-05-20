# Installation Guide for Apache Tomcat #

**Content**


## Installation instructions ##

### On Windows ###

  * Download Tomcat from http://tomcat.apache.org/download-60.cgi
    * Select the "32-bit/64-bit Windows Service Installer" (exe-file)

  * Execute the downloaded file and follow the wizard
    * Accept default values except for:
      * Page "Choose Components": Select the "Full" type of installation
      * Page "Configuration": Enter a User Name and Password for your Administration Login
> > > (you will need this later on when you deploy Integration Components to Tomcat)
    * The wizard will wrap up by registering Tomcat as a Windows Service and starting the Tomcat service for you.

  * Verify Tomcat installation by open the URL http://localhost:8080/ in a web browser. A page should be displayed with the text "_If you're seeing this page via a web browser, it means you've setup Tomcat successfully. Congratulations!_”

> ![http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/Install-Tomcat-1.png](http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/Install-Tomcat-1.png)

  * Configure Tomcat.
    * Define an environment variable, `app.home`, for storing integration components config and log files (and whatever files that is not appropriate to package with the deploy files, e.g. the war-files)
    * Configure memory parameters for heap size and perm gen space
    * Declare a port for JMX access

> The following configuration of the default installation is recommended if you plan to deploy a number of integration components to your Tomcat instance:
    * Start Tomcats configuration tool from `Windows Start menu --> Programs --> Apache Tomcat 6.0 --> Configure Tomcat`
    * Go to the tab named "Java" and add the following lines in the textarea names "Java Options":
> > (adjust port number, memory levels and app.home folder according to your preferences)
```
-Dcom.sun.management.jmxremote
-Dcom.sun.management.jmxremote.port=9090
-Dcom.sun.management.jmxremote.ssl=false
-Dcom.sun.management.jmxremote.authenticate=false
-XX:PermSize=32m
-XX:MaxPermSize=256m
-Dapp.home=C:\tomcat.app.home
```
    * Enter values for initial and max size of the Java heap in the fields "Initial memory pool" and  "Maximum memory pool", e.g. 128 and 256 (MB)  depending on how much you plan to deploy and how much memory you have available.
    * Click on the "Apply"-button.

> ![http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/Install-Tomcat-2.png](http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/Install-Tomcat-2.png)

  * Go to the tab names "General" and restart Tomcat using the "Stop" and "Start" buttons.

  * Verify the memory settings with JConsole (comes with Java SE JDK)
> > Start JConsole in a command window with the command:
```
jconsole localhost:9090
```

  * Goto the tab "VM Summary"

> In the end of the field **VM arguments** you should be able to find your settings.
> ![http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/Install-Tomcat-3.png](http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/Install-Tomcat-3.png)

  * You can also look into the log-files to ensure that everything works as expected.
> See `C:\Program Files\Apache Software Foundation\Tomcat 6.0\logs` if you installed Tomcat in the default installation folder.
> Typically the catalina.log file contains the most important information, a successful start of Tomcat should typically be logged as:
```
INFO: Server startup in 847 ms
```