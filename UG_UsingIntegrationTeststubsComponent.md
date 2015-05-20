**Content**


# Integration Teststubs Component #
An Integration Teststubs Component is intended to be separately deployed with the purpose to act as a stub for a system that is not available (or to let us control the behaviour of a system during testing), read more on [Teststub components](Architecture#Test_Driven_Development.md).

An Integration Teststubs Component in it's simplest form is really only a deployment artifact (zip-file) that bundles dependencies from the Integration Component it is supposed to provide services for. The teststubs component re-uses teststubs from it's Integration Component dependency and let those teststubs be deployable in a Mule ESB container, leaving the production ready Integration Component free from teststubs artifacts (and avoiding the risk of unintentional usage of the teststubs in a production environment).

Integration Teststub Components can be generated as described in [UG\_UsingGenerator\_genICTS](UG_UsingGenerator_genICTS.md).


Note: an Integration Components Maven pom.xml file is set up to produce a jar-file with a "core"-classifier (in addition to the zip-deployment package) that can be used as a Maven dependency by an Integration Teststub Component.