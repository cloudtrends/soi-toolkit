# Kickstart Service Oriented Integration with Mule ESB #

**Content**


<b><font color='red'>FOLLOW THE DEVELOPMENT OF SOI-TOOLKIT v2 AT:</font></b> https://github.com/soi-toolkit/soi-toolkit.github.io/wiki

## What is soi-toolkit? ##
Soi-toolkit helps you getting started with Mule ESB in no time using Maven and Mule Studio.

Soi-toolkit provides a pattern based source code generator (as a [Eclipse plugin](InstallationGuide#Soi-toolkit_Eclipse_plugin.md)) that can generate:
  * [Projects](TutorialCreateIntegrationComponent.md) with a structure and content that supports an efficient development process based on Maven.

  * Skeleton source code for services and integrations by selecting a [high-level pattern](PatternCatalog.md) ([request/response](TutorialCreateRequestResponseService.md), [one-way](TutorialCreateOneWayService.md) or publish/subscribe).

  * jUnit based test code is also generated to help you both with unit- and integration-testing of the services according to [Test Driven Development](Architecture#Test_Driven_Development.md).

The generator also generates proper setups for handling logging and tracing, configuration through property files, creation of [projects for WSDL and XML Schemas](TutorialCreateServiceDescriptionComponent.md), automatic creation of deployable files (Mule App zip-files) , release handling and more...

See [Overview](Overview.md) for more introductory information.

If you have any questions or want more information please send us a mail at [info@soi-toolkit.org](mailto:info@soi-toolkit.org).

### Building blocks ###

Soi-toolkit also provides you with some runtime components, reference applications and documentation to further help you to get started quickly.

The following picture summarize the different building blocks of soi-toolkit:

![http://soi-toolkit.googlecode.com/svn/wiki/images/building-blocks.png](http://soi-toolkit.googlecode.com/svn/wiki/images/building-blocks.png)

<wiki:gadget url="http://www.ajaxgaier.com/iGoogle/rss-reader+.xml" up\_feed="http://twitter.com/statuses/user\_timeline/247419961.rss" up\_title="Follow us on Twitter!" up\_bullet="1" up\_contentnr="100" height="100" width="900" border="2" up\_fontsize="9"/>

## Latest releases ##

**February 24 2014:** soi-toolkit v0.6.1 with support for Mule v3.4.0 is released, read more in the [Release Notes](ReleaseNotes#soi-toolkit_v0.6.1.md)!

**October 3 2012:** soi-toolkit v0.6.0 with support for Mule Studio 1.3.1 and Mule v3.3.0 is released, read more in the [Release Notes](ReleaseNotes#soi-toolkit_v0.6.0.md)!

**January 7 2012:** soi-toolkit v0.5.0 with support for Mule Studio 1.0 and Mule v3.2.1 is released, read more in the [Release Notes](ReleaseNotes#soi-toolkit_v0.5.0.md)!

**June 17 2011:** soi-toolkit v0.4.1 with support for Mule v3.1.2 is released, read more in the [Release Notes](ReleaseNotes#soi-toolkit_v0.4.1.md)!

**March 16 2011:** soi-toolkit v0.4.0 with support for Mule v3.1.1 is released, read more in the [Release Notes](ReleaseNotes#soi-toolkit_v0.4.0.md)!

**Feb 8 2011:** soi-toolkit v0.3.1 is released, read more in the [Release Notes](ReleaseNotes#soi-toolkit_v0.3.1.md)!

[News archive](ReleaseNotes.md)

## Documentation ##
  * [Installation Guide](InstallationGuide.md)
  * [Tutorials](Tutorials.md)
  * [Concepts and Definitions](ConceptsAndDefinitions.md)
  * [Overview](Overview.md)
  * [Architecture](Architecture.md)
  * [Pattern Catalog](PatternCatalog.md)
  * [User Guidelines](UserGuidelines.md)
  * [Release Notes](ReleaseNotes.md)
  * [Roadmap](Roadmap.md)

### soitoolkit-nms ###
  * [Using Apache ActiveMQ from Microsoft .Net](UG_Soitoolkit_NMS.md)

<a href='Hidden comment: 
--Collaboration--

We use Google groups for discussions, help and announcements
* [http://groups.google.com/group/soi-toolkit-user soi-toolkit-user] for discussing usage of soi-toolkit
* [http://groups.google.com/group/soi-toolkit-dev soi-toolkit-dev] for discussing development of soi-toolkit itself
* [http://groups.google.com/group/soi-toolkit-scm soi-toolkit-scm] publish failed builds of soi-toolkit
'></a>

## Downloads ##
  * Tools are installed as Eclipse plugins from a [Eclipse update site](http://soi-toolkit.googlecode.com/svn/eclipse-update-site/), see the [Installation Guide](InstallationGuide.md)
  * Runtime components are download from Maven's [Central Repository](http://repo2.maven.org/maven2/org/soitoolkit/commons/)
    * **Note:** This is done automatically for projects created by the soi-toolkit generator so no manual downloads are required
  * Snapshots can be downloaded from the [Snapshot Repository](https://repository-soi-toolkit.forge.cloudbees.com/snapshot/)
  * Reference applications are downloaded from [Subversion](http://code.google.com/p/soi-toolkit/source/browse/#svn/trunk/refapps) (TBS)
<a href='Hidden comment: 
[https://repository-soi-toolkit.forge.cloudbees.com/snapshot/org/soitoolkit/ Snapshot Repository]
'></a>

## For developers of soi-toolkit ##
  * [Developer Guidelines](DeveloperGuidelines.md)
  * [Jenkins@CloudBees](https://soi-toolkit.ci.cloudbees.com/)
  * [Google Analytics](https://www.google.com/analytics/reporting/?id=37696735)