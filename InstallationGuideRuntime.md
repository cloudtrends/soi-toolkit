# Installation Guide - Runtime environment #

**Content**


# Introduction #

The installation guides below assumes your on a 64 bit Microsoft Windows or Linux Server, but most of these guides should however be straight forward to use on a 32 bit server as well.

The following versions (or newer) are recommended:
| **Tool** | **Version** |
|:---------|:------------|
|Java SE   |6 Update 37, see Note #1|
|Mule ESB  |3.3.0        |
|ActiveMQ  |5.6.0, see Note #2|
|Tomcat    |7.0.32       |
|Derby     |10.7.1.1     |
|Cygwin    |1.7.8        |
|FileZilla Server|0.9.37       |

**Note #1:** We have noted some problems with Java SE 7 and certificates, see [issue 292](http://code.google.com/p/soi-toolkit/issues/detail?id=292) for more details.

**Note #2:** We have noted some problems with ActiveMQ 5.7.0 and backward compatibility when defining redelivery policies, see [issue 311](http://code.google.com/p/soi-toolkit/issues/detail?id=311) for more details.

# Installation instructions #

  * [Installing Java SE](IG_RT_Java.md)

  * [Installing Mule ESB](IG_RT_MuleESB.md)

  * [Installing Apache ActiveMQ](IG_RT_ActiveMQ.md)

## Optional runtime products ##

The following installations are optional depending on your usecase of Mule ESB

  * [Installing Apache Tomcat](IG_RT_Tomcat.md)

  * [Installing Apache Derby](IG_RT_Derby.md)

  * [Installing Cygwin (Windows only)](InstallationGuideCygwinSetup.md)
> Cygwin is useful for running a local SFTP-server on Windows.
> Note: Requires Soi-toolkit version 0.4.0 or later for SSH-keys created with Cygwin (due to http://www.mulesource.org/jira/browse/SFTP-38).

  * [Installing FileZilla Server (Windows only)](InstallationGuideFileZillaServerSetup.md)
> FileZilla Server is useful for running a local FTP-server on Windows.

  * [Configure Public Key cryptography for the SFTP transport](IG_RT_Public_Key_Cryptography.md)