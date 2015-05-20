# User Guidelines #

**Content**


# Introduction #

This section contains detailes not covered by the tutorials.

### Supporting all lifecycle phases of a service ###

Soi-toolkit not only wants to support the initial getting started phases but support each phase in the lifetime of a service and/or integration.

The following picture visualize a generic lifecycle and its phases for a service or an integration:

![http://soi-toolkit.googlecode.com/svn/wiki/images/life-cycle-support.png](http://soi-toolkit.googlecode.com/svn/wiki/images/life-cycle-support.png)

The instructions below will evolve over time to support all phases.

### Initial Setup ###
  * [Editor setup (for source encoding and more)](UG_EditorSetup.md)
  * [Customizing a Maven parent POM for Integration Components](UG_CustomMavenParentPom.md)
  * TBD: Customize file- and foldernames created by the generators
  * TBD: Specify your own dependencies for Mule
  * TBD: Custom handling of log-events

### Development and Maintenance ###
  * [Using Mule Studio with soi-toolkit](UG_UsingMuleStudio.md)
  * [Using the source code generators](UG_UsingGenerators.md)
  * [Property file configuration and usage (Since v0.5.0)](UG_PropertyFile.md)
  * [Integration testing](UG_IntegrationTesting.md)
  * [Customizing the log-mechanism (Experimental since v0.4.1)](UG_CustomizeLogging.md)
  * [Change JMS provider to HornetQ (Since v0.5.0)](UG_ChangeJmsProviderToHornetQ.md)
  * [Introducing HTTPS Sender ID Validation (Since v0.6.0)](UG_IntroducingHttpsSenderIdValidation.md)
  * [Testing and building against snapshot versions of soi-toolkit](DG_TestingSnapshotVersions.md).


### Test and Production ###
  * [Using Integration Teststubs Components](UG_UsingIntegrationTeststubsComponent.md)
  * [Releasing an Integration Component](UG_ReleasingAnIntegrationComponent.md)
  * [Deploying an Integration Component](UG_DeployIntegrationComponents.md)
  * [Deploying an Integration Component from Source Code](UG_DeployIntegrationComponentsFromSource.md)
  * [Using the WMQ transport (Mule EE only)](UG_WmqTransport.md)