# Release Notes #

**Content**


## soi-toolkit v0.6.1 ##
Release date: February 24 2014

This is a feature and maintenance release with 25+ new features and bug fixes (including changes from 0.6.1 milestones M1, M2, M3, M4).

The most important new features are:

  * [Issue 254, Separate log4j configuration for developer usage from production mode](https://code.google.com/p/soi-toolkit/issues/detail?id=254)
  * [Issue 307, Add support for new REST-router module](https://code.google.com/p/soi-toolkit/issues/detail?id=307)
  * [Issue 329, Make it easier and more secure to encrypt properties for property files](https://code.google.com/p/soi-toolkit/issues/detail?id=329)
  * [Issue 339, Add support for Mule 3.4 - \*also see known issues\*](https://code.google.com/p/soi-toolkit/issues/detail?id=339)
  * [Issue 343, Add support for oneway robust filetransfer](https://code.google.com/p/soi-toolkit/issues/detail?id=343)

**Known problems**
  * [Issue 367, SFTP inbound and outbound problems in Mule 3.4.0. Problem when creating new services using SFTP for 3.4.0 and when upgrading integration components created for Mule 3 versions prior to 3.4.0](https://code.google.com/p/soi-toolkit/issues/detail?id=367)

**Upgrading**

Issues affecting upgrades of soi-toolkit projects created with previous versions:
  * [Issue 307, Replace XML namespace as described in issue](https://code.google.com/p/soi-toolkit/issues/detail?id=307)
  * [Issue 344, Replace expired certificates for integration tests as described in issue](https://code.google.com/p/soi-toolkit/issues/detail?id=344)

**Full Release Notes** can be found here: [Release Notes](http://code.google.com/p/soi-toolkit/issues/list?can=1&q=label:Milestone-Release0.6.1)

## soi-toolkit v0.6.1 - Milestone 2 ##
Release date: March 18 2013

This is the second milestone release of v0.6.1 adding support for Mule CE 3.3.1 and more, see release notes.

**Release Notes** can be found here: [Release Notes](http://code.google.com/p/soi-toolkit/issues/list?can=7&q=Milestone%3DRelease0.6.1-M2)

## soi-toolkit v0.6.1 - Milestone 1 ##
Release date: January 15 2013

This is the first milestone release of v0.6.1

**Release Notes** can be found here: [Release Notes](http://code.google.com/p/soi-toolkit/issues/list?can=7&q=Milestone%3DRelease0.6.1-M1)

## soi-toolkit v0.6.0 ##
Release date: October 3 2012

This is a feature and maintenance release with 50+ new features and bug fixes.

The most important new features are:

  * [Issue #192, Replace soi-toolkit jaxb transformers with standrad Mule equivalents](http://code.google.com/p/soi-toolkit/issues/detail?id=192)
  * [Issue #229, Log connect-problems to ActiveMQ](http://code.google.com/p/soi-toolkit/issues/detail?id=229)
  * [Issue #242, Add support for XA transactions](http://code.google.com/p/soi-toolkit/issues/detail?id=242)
  * [Issue #243, Add support for Mule 3.3](http://code.google.com/p/soi-toolkit/issues/detail?id=243)
  * [Issue #244, Add support for Mule Studio 1.3.1](http://code.google.com/p/soi-toolkit/issues/detail?id=244)
  * [Issue #266, Use spring 3.1 bean definition profiles to reduce the number of mule-spring-config-files](http://code.google.com/p/soi-toolkit/issues/detail?id=266)
  * [Issue #278, Add support for the HTTPS transport for REST and SOAP services](http://code.google.com/p/soi-toolkit/issues/detail?id=278)
  * [Issue #283, Remove JMS and JDBC drivers from standalone deploy files](http://code.google.com/p/soi-toolkit/issues/detail?id=283)
  * [Issue #288, Add support of custom WSDL files when creating Service Description Components and also add support for MS .Net WCF](http://code.google.com/p/soi-toolkit/issues/detail?id=288)

**Full Release Notes** can be found here: [Release Notes](http://code.google.com/p/soi-toolkit/issues/list?can=1&q=label:Milestone-Release0.6.0)

## soi-toolkit v0.5.0 ##
Release date: January 7 2012

This is a feature and maintenance release with 63 new features and bug fixes

The most important new features are:

  * [Issue #160, Add support for Mule Studio v1.0](http://code.google.com/p/soi-toolkit/issues/detail?id=160)
  * [Issue #164, Add support for Mule ESB v3.2.1](http://code.google.com/p/soi-toolkit/issues/detail?id=164)
  * [Issue #177, Add support for HornetQ as JMS provider](http://code.google.com/p/soi-toolkit/issues/detail?id=177)

**Full Release Notes** can be found here: [Release Notes](http://code.google.com/p/soi-toolkit/issues/list?can=1&q=label:Milestone-Release0.5)

## soi-toolkit v0.4.1 ##
Release date: June 17 2011

This is a feature and maintenance release with both new features and bug fixes.

The most important new features are:

**Added support for Mule ESB 3.1.2**
  * [Issue #145, Add support for Mule ESB v3.1.2](http://code.google.com/p/soi-toolkit/issues/detail?id=145)
  * [Issue #149, Switch to Mule ESB 3.1.2 as the default version for soi-toolkit](http://code.google.com/p/soi-toolkit/issues/detail?id=149)
  * [Issue #150, Add support for the restlet-transport added by Mule ESB 3.1.2](http://code.google.com/p/soi-toolkit/issues/detail?id=150)
  * [Issue #116, Problem with error handling in Mule 3.1, null is returned in exception instead of relevant error info](http://code.google.com/p/soi-toolkit/issues/detail?id=116)

**Removed requirement on using Eclipse**
  * [Issue #109, Support starting the soi-toolkit generators from the command line](http://code.google.com/p/soi-toolkit/issues/detail?id=109)

**Support for new protocols in the pattern driven source code generators**
  * [Issue #120, Support for REST/HTTP based outbound endpoints for request/response based services](http://code.google.com/p/soi-toolkit/issues/detail?id=120)

  * [Issue #58, Support ftp as inbound transport and outbound transport for oneway services](http://code.google.com/p/soi-toolkit/issues/detail?id=58)
  * [Issue #124, Startup embedded ftp-server in integration tests](http://code.google.com/p/soi-toolkit/issues/detail?id=124)
  * [Issue #128, Move ftp and sftp username and password info to security-property-file](http://code.google.com/p/soi-toolkit/issues/detail?id=128)
  * [Issue #141, More than one FTP-test cause an "Address already in use" exception](http://code.google.com/p/soi-toolkit/issues/detail?id=141)

**Simplified setting up configuration on test and prod servers**
  * [Issue #132, Create a config-zip-file for deployable artifacts](http://code.google.com/p/soi-toolkit/issues/detail?id=132)

**Improved quality by new backward compatibility tests**
  * [Issue #131, Always test migration of source code from an old version in the process of releasing a new version of soi-toolkit](http://code.google.com/p/soi-toolkit/issues/detail?id=131)


**Full Release Notes** can be found here: [Release Notes](http://code.google.com/p/soi-toolkit/issues/list?can=1&q=label:Milestone-Release0.4.1)

**UPGRADE INSTRUCTIONS:**

To upgrade a soi-toolkit project from v0.4.0 the following has to be addressed:

  * See [Issue #132, Create a confi-zip-file for deployable artifacts](http://code.google.com/p/soi-toolkit/issues/detail?id=132)
> A manual change of the service-projects pom.xml file is required for enabling this new feature, see [Comment #5 in Issue #132](http://code.google.com/p/soi-toolkit/issues/detail?id=132#c5) for detailed instructions.


  * See [Issue #150, Add support for the restlet-transport](http://code.google.com/p/soi-toolkit/issues/detail?id=150)
> Custom specific parent-poms and maven-proxies such as Nexus used for Mule 3.1.2 and soi-toolkit 0.4.1 (or later) needs to add the repository:
```
<repository>
  <id>mulesoft</id>
  <name>MuleSoft Repository</name>
  <url>https://repository.mulesoft.org/nexus/content/repositories/releases</url>
</repository>
```
> to make it possible to download the restlet-transport.

## soi-toolkit v0.4.0 ##
Release date: March 16 2011

This is a feature and maintenance release with both new features and bug fixes.

The most important new feature is:

  * [Issue #92, Migrate soi-toolkit to Mule 3 (v3.1.1)](http://code.google.com/p/soi-toolkit/issues/detail?id=92)

Other important new features are:

  * [Issue #104, Add support for Mule 3 new deployment model](http://code.google.com/p/soi-toolkit/issues/detail?id=104)
  * [Issue #68	Do not save security related info in the preference page](http://code.google.com/p/soi-toolkit/issues/detail?id=68)
  * [Issue #34	Clarify instructions on how to setup PKI keys on a Windows PC](http://code.google.com/p/soi-toolkit/issues/detail?id=34)
  * [Issue #46	Document customized parent-poms replcaing soi-tk default-parent-pom](http://code.google.com/p/soi-toolkit/issues/detail?id=46)
  * [Issue #106	Add support for web service proxy](http://code.google.com/p/soi-toolkit/issues/detail?id=106)
  * [Issue #110	Simplify the code of generated soap-test-consumers](http://code.google.com/p/soi-toolkit/issues/detail?id=110)
  * [Issue #64	Change naming conventions for the sftp-protocol](http://code.google.com/p/soi-toolkit/issues/detail?id=64)

Full Release Notes can be found here: [Release Notes](http://code.google.com/p/soi-toolkit/issues/list?can=1&q=label:Milestone-Release0.4)

**BACKWARD COMPATIBILITY ISSUES:**

  * See [migration guides](http://www.mulesoft.org/documentation/display/MULEMIG/Home) from MuleSoft regarding migration from Mule ESB 2.2.x to Mule ESB 3.x.

## soi-toolkit v0.3.1 ##
Release date: Feb 8 2011

This is a minor maintenance release with only a few small enhancements, see release notes here: [Release Notes](http://code.google.com/p/soi-toolkit/issues/list?can=1&q=label:Milestone-Release0.3.1).

The most important new feature is:

  * [Issue #87, Prepare soi-toolkit for Mule 3.x](http://code.google.com/p/soi-toolkit/issues/detail?id=87)

**BACKWARD COMPATIBILITY ISSUES:**

  * **[Issue 87](https://code.google.com/p/soi-toolkit/issues/detail?id=87) and svn revision [r693](https://code.google.com/p/soi-toolkit/source/detail?r=693):**
> Added a MuleContext parameter to the public static methods `initEndpointDirectory()`, `getFilesInEndpoint()` and `getSftpFileContent()` to prepare for an upgrade to Mule 3.1.
    * **Action required:** Calls to these methods must be updated. Since these methods only are called from mule-junit-test-classes a muleContext member variable already exists in the callers code (from its baseclass) and can simply be added to the method calls!

Trunk will now be branched into a MULE\_2\_2\_BRANCH and future development on trunk will be focused on supporting Mule 3.x.

## soi-toolkit v0.3.0 ##
Release date: Jan 12 2011

This is a feature and maintenance release with both new features and bug fixes.

Of most importance new features are:

  * [Issue #52, Support jdbc as inbound transport and outbound transport for oneway services](http://code.google.com/p/soi-toolkit/issues/detail?id=52)
  * [Issue #59, Add support for cross reference lookups](http://code.google.com/p/soi-toolkit/issues/detail?id=59)
  * [Issue #62, Add suppor for the quartz-transport](http://code.google.com/p/soi-toolkit/issues/detail?id=62)
  * [Issue #63, Add support for a unzip transformer](http://code.google.com/p/soi-toolkit/issues/detail?id=63)
  * [Issue #65, convertStreamToString() handles BOM-markers and new method convertResourceBundleToProperties()](http://code.google.com/p/soi-toolkit/issues/detail?id=65)
  * [Issue #71, Add support for reading non UTF-8 files](http://code.google.com/p/soi-toolkit/issues/detail?id=71)
  * [Issue #73, Support file as inbound transport and outbound transport for oneway services](http://code.google.com/p/soi-toolkit/issues/detail?id=73)
  * [Issue #74, Support vm as inbound transport and outbound transport for oneway services](http://code.google.com/p/soi-toolkit/issues/detail?id=74)
  * [Issue #75, Support mail as inbound transport (imap and pop3) and outbound transport (smtp) for oneway services](http://code.google.com/p/soi-toolkit/issues/detail?id=75)
  * [Issue #77, Add support for request-response patterns](http://code.google.com/p/soi-toolkit/issues/detail?id=77)
  * [Issue #78, Add support for smooks based transformations](http://code.google.com/p/soi-toolkit/issues/detail?id=78)
  * [Issue #79, Support for Mule ESB v2.2.7](http://code.google.com/p/soi-toolkit/issues/detail?id=79)

Full Release Notes can be found here: [Release Notes](http://code.google.com/p/soi-toolkit/issues/list?can=1&q=label:Milestone-Release0.3.0)

## soi-toolkit v0.2.0 ##
Release date: Nov 13 2010

This is a minor feature and maintenance release with bug fixes and some new features.

Of most importance are:
  * [Issue #51, Allow free combinations of inbound transport and outbound transport](http://code.google.com/p/soi-toolkit/issues/detail?id=51)
  * [Issue #53, Support http multipart post as inbound transport for oneway services](http://code.google.com/p/soi-toolkit/issues/detail?id=53)
  * [Issue #54, Create unit test classes for generated transformer classes](http://code.google.com/p/soi-toolkit/issues/detail?id=54)

Full Release Notes can be found here: [Release Notes](http://code.google.com/p/soi-toolkit/issues/list?can=1&q=label:Milestone-Release0.2.0)

### Upgrade from earlier versions of soi-toolkit ###

  * Existing projects are updated by specifying the new soi-toolkit version in the parent-pom in the pom.xml file in each projects trunk-folder...
```
	<parent>
		<groupId>org.soitoolkit.commons.poms</groupId>
		<artifactId>soitoolkit-default-parent</artifactId>
		<version>0.2.0</version>
	</parent>
```

  * The Eclipse plugin can be upgraded by following the instructions [here](http://code.google.com/p/soi-toolkit/wiki/InstallationGuide#Updating_the_Soi-toolkit_Eclipse_plugin).

## soi-toolkit v0.1.9 ##
Release date: Oct 23 2010

This is a maintenance release with bug fixes and minor internal enhancements based on feedback from the first projects using soi-toolkit.

Release Notes can be found here: [Release Notes](http://code.google.com/p/soi-toolkit/issues/list?can=1&q=label:Milestone-Release0.1.9)

### Upgrade from earlier versions of soi-toolkit ###

  * Existing projects are updated by specifying the new soi-toolkit version in the parent-pom in the pom.xml file in each projects trunk-folder...
```
	<parent>
		<groupId>org.soitoolkit.commons.poms</groupId>
		<artifactId>soitoolkit-default-parent</artifactId>
		<version>0.1.9</version>
	</parent>
```

  * The Eclipse plugin can be upgraded by following the instructions [here](http://code.google.com/p/soi-toolkit/wiki/InstallationGuide#Updating_the_Soi-toolkit_Eclipse_plugin).

## soi-toolkit v0.1.8 ##
Release date: Oct 11 2010

This is the first release of soi-toolkit.

It contains:
  * An [Installation guide](InstallationGuide.md)
  * A set of initial [Getting started tutorials](Tutorials.md) on how to create components and services
> (very few types of services are supported initially)
  * Runtime components ([Maven repository](http://repo2.maven.org/maven2/org/soitoolkit/commons/))
  * Integration pattern driven code generator ([Eclipse plugin](http://soi-toolkit.googlecode.com/svn/eclipse-update-site/))
  * This web site