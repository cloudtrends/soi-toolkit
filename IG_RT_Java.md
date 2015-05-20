# Installation Guide for Java SE #

This installation guide assumes your on a 64 bit Microsoft Windows or Linux Server, but most of the guide should however be straight forward to use on a 32 bit server as well.

It is preferred to install the full JDK (Java Development Kit) over the smaller JRE (Java Runtime Environment) distribution since the JDK contains a number of valuable debug tools such as JConsole (JMX console) and Java Visual VM (Java profiler).

## On Microsoft Windows ##

Install Java SE according to the following instructions:

  * Download JDK for Java SE 6 from http://www.oracle.com/technetwork/java/javase/downloads/index.html.
> Select the JDK download in the section named "Java SE 6 Update nn", select the Windows x64 version and download it to your server. A file named similar to `jdk-6u37-windows-x64.exe` should be downloaded.

  * Execute the downloaded installation program and use default values for the installation of both the JDK and the JRE.

  * Create an environment variable, JAVA\_HOME, in Windows that points to the installation directory.
> E.g. `JAVA_HOME=C:\Program Files\Java\jdk1.6.0_37`

  * Add the bin-folder of the JDK to the Windows PATH-environment variable.
> E.g. `PATH=...;%JAVA_HOME%\bin`

  * Verify the installation in a command window with the commands `java –version`,  `javac –version` och `set JAVA_HOME`. The result should be similar to:

> ![http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/JavaSeVerifyInstallationRuntime.png](http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/JavaSeVerifyInstallationRuntime.png)

  * For full installation instructions see [Microsoft Windows Installation (64-bit)](http://www.oracle.com/technetwork/java/javase/documentation/install-windows-142126.html)

## On Linux ##

To be described.