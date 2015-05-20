<font color='red' size='5'><b>NOTE:</b> This page is partially outdated.<br />Work is ongoing to update this page!</font>

# Tutorial: Create an Integration Component for separate deploy of Test stub Producers. #

**Content**


# Introduction #

This tutorial will help you get started with creating an [integration component](ConceptsAndDefinitions#Definitions.md) for [test stub producers](ConceptsAndDefinitions#Definitions.md), called an _Test stub Integration Component_.

This type of integration components can come very handy in a test environment where not all service producers are ready for test.

![http://soi-toolkit.googlecode.com/svn/wiki/Architecture/system-tests-problem.png](http://soi-toolkit.googlecode.com/svn/wiki/Architecture/system-tests-problem.png)

In these cases it has been proven to be very valuable to be able to package the teststub producers created for running local integration tests in a separate  Mule App and deploy them into the test environment having them taking the role for the systems that can't participate in the systems tests for the moment.

![http://soi-toolkit.googlecode.com/svn/wiki/Architecture/system-tests-solution.png](http://soi-toolkit.googlecode.com/svn/wiki/Architecture/system-tests-solution.png)

The test stub producers normally used in integration tests can be separately deployed into a test environment using this type of integration component. For more information regarding this concept see [test driven development](Architecture#Test_Driven_Development.md).

An integration component for test stub producers goes in pair with the original integration component that contains the actual code for the test stub producers. This means that an integration component for test stub producers doesn't contain any code of its own but instead have dependencies to the test code in the original integration component.

**Prerequisites** for this tutorial is that the tutorial [Create a new Integration Component](TutorialCreateIntegrationComponent.md) is completed. This tutorial assumes that you named the integration component "sample1".

# Create a new integration component for test stub producers #

First we need to create an [test stub integration component](ConceptsAndDefinitions#Core_Concepts.md), i.e. a Mule App as a Maven project for a the test stub producers. The Maven project is automatically imported into Mule Studio and creates a Mule App (zip-file) when its package goal is executed so it very easy to create a deployable artifact from a test stub integration component.

Perform the following steps in Mule Studio:

  * Select the menu "File --> New --> Other" and expand the wizard "SOI Toolkit Code Generator"
  * Select the code generator "Create a new SOI Toolkit component"

> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateTeststubIntegrationComponent/CreateTsIc-1.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateTeststubIntegrationComponent/CreateTsIc-1.png)

  * Click on the "Next >" button
> The wizard "SOI Toolkit - Create a new Component" opens up
    * Select the component type "Integration Teststubs Component" in the radio button control named "Type of component"
    * Specify a proper name of the component in the field "Artifact Id" SAME AS THE ORIGIN IC!!!!
    * Specify a proper group name in the field "Group Id" SAME AS THE ORIGIN IC!!!!
    * Select where you want the files to be created in the field "Root folder"

> Note: The pre-selected root folder is picked up from the preference page you filled in during the installation of the soi-toolkit plugin.

> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateTeststubIntegrationComponent/CreateTsIc-2.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateTeststubIntegrationComponent/CreateTsIc-2.png)

  * Click on the "Finish" button

> The wizard now starts to execute and logs its output to a text area.
> > The wizard now starts to generate files and refresh the workspace.



> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateTeststubIntegrationComponent/CreateTsIc-3.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateTeststubIntegrationComponent/CreateTsIc-3.png)

> When the wizard is done you can find the files for your new component in the Mule Studio Package Explorer:

> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateTeststubIntegrationComponent/CreateTsIc-4.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateTeststubIntegrationComponent/CreateTsIc-4.png)

  * The following work is performed by the wizard:
    * Created folders and files according to the input on the previous page.
    * Launch maven to do a initial build and also create eclipse files
    * Imports the project into the current Mule Studio workspace

### Other files of interest ###

  * Source folder `src/test/java`
> Package `org.sample.sample1` with the Java-classes:
    * `Sample1MuleServer.java` is a simple mule launcher for this integration component. It can either be started in Mule Studio or at the command prompt with the command `mvn -PmuleServer`
> Package `org.sample.sample1.mysample` with the Java-classes:
    * `MySampleIntegrationTest.java` contains some sample integrations tests.
    * `MySampleRequestTransformerTest.java` contains some sample unit tests.
    * `MySampleResponseTransformerTest.java` contains some sample unit tests.
    * `MySampleTestConsumer.java` contains a sample test consumer.
    * `MySampleTestProducer.java` contains a sample teststub producer.

  * Source folder `src/test/resources`
    * The folder `teststub-services` contains the file `mySample-teststub-service.xml` that is a teststub producer
    * The folder `testfiles/mySample` contains sample input and expected output messages that the tests classes use to validate the correct behavior.
    * `cxf-test-consumer-config.xml` contains apache cxf settings for the test consumer to be able to handle https with mutual authentication.

  * Source folder `src/main/java`
> Package `org.sample.sample1.mysample` with the Java-classes:
    * `MySampleRequestTransformer.java` contains a sample request transformer.
    * `MySampleResponseTransformer.java` contains a sample response transformer.

  * Source folder `src/main/resources`
    * Configuration files for Log4J, `log4j.dtd` and `log4j.xml`
    * The property file `sample1-config.properties`

  * Source folder `src/main/app`
    * `mule-deploy.properties` standard Mule ESB file that enumerates what Mule config files to use in the deployed Mule App.
    * `mySample-service.xml` that contains the actual definition of the new service.
    * `sample1-common.xml` that contains common definitions shared by all services within this integration component.

  * Source folder `src/test/certs`
> Contain sample certificates and truststore, **ONLY** to be used during development!

**TODO:** Add info regarding how to build teststub ic and troubleshooting!!