# Installation Guide for Mule ESB #

This installation guide describes how to install both the free Community Edition (CE) of Mule ESB and the commercial Enterprise Edition (EE) of Mule ESB.

The installation guide can be used to install several versions of Mule ESB that executes concurrently on one and the same server.

To be able to achieve this unique names (with regards to version and edition) are suggested for the daemon processes, e.g. "mule\_ce\_3.3.0".

Also attention is required to ensure that different Mule instances don't tries to use the sam ports, e.g. for communicating with the Mule Management Console (MMC).

**TBD:** Describe how to setup specific ports on a Mule instance!

**Note** that the file name differs slightly for the two editions. In general the file names for the Community Edition are used in the guide.

**Content**


## Installation instructions ##

### On Windows ###

This guide assumes you are using a 64 bit Microsoft Windows server, but most of the guide should however be straight forward to use on a 32 bit Microsoft Windows server as well.

**Note regarding Java Service Wrapper on 64 bit Windows**

Mule ESB relies on the [Java Service Wrapper](http://wrapper.tanukisoftware.com/doc/english/product-overview.html) from Tanuki Software to be able to run as a Windows Service.

The Community Edition of Mule ESB however doesn't contain any version of the Java Service Wrapper for 64 bit Windows.
The reason for this is that Tanuki Software does not provide a Community Edition of the Java Service Wrapper on 64 bit Windows, see http://wrapper.tanukisoftware.com/doc/english/download.jsp

Therefore a license of the Java Service Wrapper needs to be purchased to be able to use Mule ESB Community Edition on 64 bit Windows.
  * A license can be purchased from [here](http://wrapper.tanukisoftware.com/doc/english/requestTrial.jsp).
  * A 30 day trial can be downloaded from [here](http://wrapper.tanukisoftware.com/doc/english/accountLicenses.jsp).

For Mule ESB Community Edition on 64 bit Windows this guide assume that you have acquired and downloaded either a trial license or a purchased license of the Java Service Wrapper for 64 bit Windows.
  * License type: Server license
  * License edition: Standard edition
  * License filename: wrapper-license.conf

**END OF Note regarding Java Service Wrapper on 64 bit Windows**


  * Download the distribution for the Community Edition from http://dist.codehaus.org/mule/distributions/mule-standalone-3.3.0.zip

> The distribution for the Enterprise Edition is downloaded from the MuleSoft customer portal and is named like `mule-ee-distribution-standalone-3.3.0.zip`

  * Unzip to some folder, e.g. `C:\opt`
> Mule ESB files now exists in the folder C:\opt\mule-standalone-3.3.0 if you unzipped to the folder C:\opt. For the rest of this guide this folder is named MULE\_HOME. Note that **no** environment variable with this name is required!

> NOTE: Avoid path names that contain blanks as they can cause problems when using Mule ESB later on.

  * For Mule ESB Community Edition only: Install 64 bit Windows version of Tunaki Software Java Service Wrapper
    * Download the Java Service Wrapper from: http://wrapper.tanukisoftware.com/download/3.5.15/wrapper-delta-pack-3.5.15-st.zip
    * Extract the files from the download zip-file to a temp-folder
    * Remove the following files from the Mule ESB installation:
      * MULE\_HOME\lib\boot\wrapper-3.2.3.jar
      * MULE\_HOME\lib\boot\wrapper-windows-x86-32.dll
      * MULE\_HOME\lib\boot\exec\wrapper-windows-x86-32.exe
    * Copy the following files from the Java Service wrapper download to the Mule ESB installation:
      * wrapper.jar and wrapper-windows-x86-64.dll from wrapper-delta-pack-3.5.15-st\lib to MULE\_HOME\lib\boot\
      * wrapper-windows-x86-64.exe from wrapper-delta-pack-3.5.15-st\bin to MULE\_HOME\lib\boot\exec
    * Copy the license file wrapper-license.conf to the folder MULE\_HOME\conf
    * Edit the file MULE\_HOME\conf\wrapper.conf
      * Add the following line line directlry after the first line ("#encoding=UTF-8"):
```
   #include %MULE_BASE%/conf/wrapper-license.conf
```

  * Give the Mule ESB instance a unique Windows Service name
    * Edit the file MULE\_HOME\conf\wrapper.conf
> > Add a proper version-suffix to the three Windows Service name properties such as `"-CE-3.3.0"`
```
wrapper.ntservice.name=%MULE_APP%-CE-3.3.0
wrapper.ntservice.displayname=%MULE_APP_LONG%-CE-3.3.0
wrapper.ntservice.description=%MULE_APP_LONG%-CE-3.3.0
```

  * Add common non-Mule ESB jar-files to the MULE\_HOME\lib\user - folder

> To keep the size of Mule-apps down to a minimum (disk and memory) soi-toolkit no longer bundle jdbc-drivers nor jms-providers such as activemq-all-5.6.0.jar, so they needs to be copied manually to the MULE\_HOME\lib\user - folder. For details see [issue #283](http://code.google.com/p/soi-toolkit/issues/detail?id=283).

  * Install Mule ESB as a Windows Service
    * Go to the folder `MULE_HOME\bin` and execute the command `mule install`.

> Mule ESB is now registered as a windows service and will be started automatically when the Windows server is started.

> NOTE: If you prefer to start Mule ESB manually you can do that with the command `mule`.


  * Startup Mule ESB Windows Service manually with the command `mule start`

  * Lastly, ensure that Mule starts automatically after rebooting the OS and logging back in...
    * After reboot and login , look in the file MULE\_HOME/logs/mule.log (mule\_ee.log for Mule ESB Enterprise Edition):

You should have a message in the tail similar to the following:
```
**********************************************************************
* Mule ESB and Integration Platform                                  *
* Version: 3.3.0 Build: 24524                                        *
* MuleSoft, Inc.                                                     *
* For more information go to http://www.mulesoft.org                 *
*                                                                    *
* Server started: 11/10/12 11:54 PM                                  *
* JDK: 1.6.0_37 (mixed mode)                                         *
* OS: Windows Server 2008 R2 - Service Pack 1 (6.1, amd64)           *
* Host: vagrant-2008R2 (10.0.2.15)                                   *
**********************************************************************
```

Verify that the startup time is accurate to ensure that you are reading the right log output!

  * Also verify that Mule ESB is running correctly as a windows service.

> ![http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/MuleESB-Windows-Service.png](http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/MuleESB-Windows-Service.png)

  * Also use the event viewer to verify a successful start of the Mule ESB service:

> ![http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/MuleESB-Windows-Event-Viewer.png](http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/MuleESB-Windows-Event-Viewer.png)

#### Useful commands ####

The following commands can be executed from the folder `MULE_HOME\bin`:

  * To install the Mule ESB Service:
```
mule install
```

  * To uninstall the Mule ESB Service:
```
mule remove
```

  * To start the Mule ESB Service:
```
mule start
```

  * To restart the Mule ESB Service:
```
mule restart
```

  * To stop the Mule ESB Service:
```
mule stop
```

### On Linux ###

  * Download the distribution for the Community Edition with the command:
```
$ wget http://dist.codehaus.org/mule/distributions/mule-standalone-3.3.0.tar.gz
```

  * The distribution for the Enterprise Edition is downloaded from the MuleSoft customer portal and is named like `mule-ee-distribution-standalone-3.3.0.tar.gz`

  * Unpack the distribution with the command:
```
$ sudo tar -xvf mule-standalone-3.3.0.tar.gz -C /usr/local
```

  * Add common non-Mule ESB jar-files to the /usr/local/mule-standalone-3.3.0/lib/user - folder
> To keep the size of Mule-apps down to a minimum (disk and memory) soi-toolkit no longer bundle jdbc-drivers nor jms-providers such as activemq-all-5.6.0.jar, so they needs to be copied manually to the /usr/local/mule-standalone-3.3.0/lib/user - folder. For details see [issue #283](http://code.google.com/p/soi-toolkit/issues/detail?id=283).

  * Create soft-link for the Mule daemon to your daemon init script directory
```
$ sudo ln -s /usr/local/mule-standalone-3.3.0/bin/mule /etc/init.d/mule_ce_3.3.0
```

  * Make Mule startup at boot-time:
```
$ sudo update-rc.d mule_ce_3.3.0 start 67 2 3 4 5 . stop 67 0 1 6 .
```

  * Startup Mule manually:
```
$ sudo service mule_ce_3.3.0 start
```

  * Lastly, ensure that Mule starts automatically after rebooting the OS and logging back in...
```
$ sudo reboot
$ tail /usr/local/mule-standalone-3.3.0/logs/mule.log -n50
```

  * You should have a message in the tail similar to the following:
```
**********************************************************************
* Mule ESB and Integration Platform                                  *
* Version: 3.3.0 Build: 24555                                        *
* MuleSoft, Inc.                                                     *
* For more information go to http://www.mulesoft.org                 *
*                                                                    *
* Server started: 2012-09-03 08:53                                   *
* JDK: 1.6.0_24 (mixed mode)                                         *
* OS: Linux (3.2.0-29-generic, amd64)                                *
* Host: magnus-MacBookPro (127.0.1.1)                                *
**********************************************************************
```

  * Verify that the startup time is accurate to ensure that you are reading the right log output!
Verify in `/var/log/boot.log` that you got the expected start order, e.g. that ActiveMQ starts before Mule!

#### Useful commands ####

To verify if Mule is running use the command:
```
$ sudo service mule_ce_3.3.0 status
```

To restart Mule use the command:
```
$ sudo service mule_ce_3.3.0 restart
```

To stop Mule use the command:
```
$ sudo service mule_ce_3.3.0 stop
```

To uninstall the Mule service, do the following:
```
$ sudo update-rc.d -f mule_ce_3.3.0 remove
$ sudo rm /etc/init.d/mule_ce_3.3.0
```

To run Mule in the console: ???
  * Go to the sub folder `apache-activemq-5.6.0/bin` and execute the command: `activemq console`.
    * **Note:** On Mac OS X go to the sub folder `apache-activemq-5.6.0/bin/macosx` and execute the command: `activemq console`.

## Configuration ##

### Java Service Wrapper ###
Configure the Java Service Wrapper according to [this instruction](IG_RT_JavaServiceWrapper.md).

### Installing a Mule EE license ###

After installing Mule EE you have a default evaluation license valid for 30 days.
For instructions on how to install your license read this: [Installing a Commercial License](http://www.mulesoft.org/documentation/display/MULE3INSTALL/Installing+a+Commercial+License).

In short:
```
$ cd /usr/local/mule-enterprise-standalone-3.3.0/bin
$ sudo cp ...some-folder/mule-ee-license.lic .
$ sudo ./mule -installLicense mule-ee-license.lic 
```

**Note:** The mule -installLicense command will remove the license-key-file!

### Controlling Mule startup ###

For instructions on how Mule is started and how it can be customized read this: [Controlling Mule From Startup](http://www.mulesoft.org/documentation/display/MULE3INSTALL/Controlling+Mule+From+Startup)

**To Be Described:**

  * Env vars in the wrapper script
/usr/local/mule-standalone-3.1.3/conf/wrapper.conf
Location of your application's data storage on disk.
wrapper.java.additional.4=-Dmule.app.data=/somefolder/mule.app.data

  * editera filen /usr/local/mule-standalone-3.1.3/bin/mule och skapa:
The upper limit on the number of files mule can open
ulimit -n 10240

  * non root user
Om Mule skall exekvera under en annan user än root:
1. editera filen /usr/local/mule-standalone-3.1.3/bin/mule och ändra på raden som anger
RUN\_AS\_USER t ex till:
RUN\_AS\_USER=mule
2. ändra /usr/local/mule-standalone-3.1.3 ägare till user som exekvera Mule (RUN\_AS\_USER):
$sudo chown -R mule:mule /usr/local/mule-standalone-3.1.3