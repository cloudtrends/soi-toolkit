# Tutorial: Create a secure request/response service using HTTPS with mutual authentication #

**Content**


# Introduction #

**Prerequisites** for this tutorial is that the [installation guide](InstallationGuide.md) is completed, including installing soapUI.

This tutorial will help you get started with creating a [request/response service](ConceptsAndDefinitions#Request/Response.md) that is secured by using HTTPS with mutual authentication. Initial setup of HTTPS with mutual authentication can be quite challenging getting all the security pieces in the right places. Typically nothing works until all security pieces are configured correctly making it even more challenging. Getting a fully working skeleton code generated for you service (including test-consumer, test-producer and integration test) can therefore be a great time saver!

Before we jump into the tutorial we will briefly go through:

  1. HTTPS with mutual authentication
  1. The functionality in the generated service
  1. The code that is generated

## Briefly about HTTPS with mutual authentication ##

HTTPS with mutual authentication refers to a client authenticating themselves to a server and that server authenticating itself to the user in such a way that both parties are assured of the others identity using HTTPS, i.e. HTTP on top of the SSL/TLS protocol. Certificates of trusted parties are kept in a trust store and validated against during the initial handshaking.

The initial handshaking is summarized by the following picture:

![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/0-SSL-overview.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/0-SSL-overview.png)

For more details see fro example [client-authenticated TLS handshake](http://en.wikipedia.org/wiki/Secure_Sockets_Layer#Client-authenticated_TLS_handshake)

## Briefly about the service ##

For this tutorial we will create a service that act as a SOAP proxy between a service consumer and a service producer using HTTPS with mutual authentication to secure the communication.

![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/0-EP-SecureRequestResponse.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/0-EP-SecureRequestResponse.png)

The service will do the following:

  1. receives SOAP/HTTPS requests from service consumers
  1. process the request e.g. transform it and create log records regarding the processing
  1. invokes a service producer using SOAP/HTTPS, optionally using various kinds of routing
  1. waits for the response from the producer, given a configurable timeout period
  1. process the response (including errors) e.g. transform it and create log records regarding the processing
  1. send back the response to the consumer

## Briefly about the code generated ##

The soi-toolkit generator helps you get started by generating skeleton source code files for a new service illustrated by the following picture:

![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/RequestResponseService/RequestResponse-SourceFiles.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/RequestResponseService/RequestResponse-SourceFiles.png)

**Color schema:** yellow for files that build up the actual service, green for test-classes and gray for classes used by the test-classes.

In summary the generated files are:

  * A Mule service with:
    * A mule configuration file containing a mule flow for the service.
    * Transformers for both the request and the response.
    * Proper error handling, both for error messages from the producer and capturing technical errors, such as timeouts.
    * Tracing, by default log-events are created when both a request and a response are received and when they are sent.

  * jUnit based unit-tests for the transformers

  * jUnit based integration-tests for the Mule service
    * Including a test-consumer and a teststub for the producer.
    * Generated integration tests that cover both a "happy days" scenario and negative tests (invalid input + timeout tests)

  * Configurable properties for settings that needs to be environment specific
    * E.g. for endpoint adresses, timeouts and certificate information.
    * The property files can be encrypted to secure sensitive information, e.g. regarding certificates, see [property file configuration and usage](http://code.google.com/p/soi-toolkit/wiki/UG_PropertyFile) for more information

# Tutorial #

The tutorial is divided in the following tasks:

  1. Create a new integration component
  1. Create skeleton code for the service
  1. Test the service automatically
    * Run generated unit and integrations tests
  1. Test the service manually
    * Start Mule ESB with the service and a teststub service for the producer
    * Call the service using the generated CXF test consumer
    * Call the service using soapUI
    * Stop the server

## Create a new integration component ##

First we need to create an [integration component](ConceptsAndDefinitions#Core_Concepts.md), i.e. a Maven project for a number of related services and integrations. The Maven project is automatically imported into Mule Studio and creates a Mule App (zip-file) when its package goal is executed so it very easy to create a deployable artifact from an integration component.

Perform the following steps in Mule Studio:

  * Select the menu "File --> New --> Other" and expand the wizard "SOI Toolkit Code Generator"
  * Select the code generator "Create a new SOI Toolkit component"

> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/1.1_createIc.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/1.1_createIc.png)

  * Click on the "Next >" button
> The wizard "SOI Toolkit - Create a new Component" opens up
    * Select the component type "Integration Component" in the radio button control named "Type of component"
    * Specify a proper name of the component in the field "Artifact Id"
    * Specify a proper group name in the field "Group Id"
    * Select where you want the files to be created in the field "Root folder"

> Note: The pre-selected root folder is picked up from the preference page you filled in during the installation of the soi-toolkit plugin.

> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/1.2_createIc.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/1.2_createIc.png)

  * Click on the "Next >" button
> The wizard now displays a new page where you can perform some initial configuration of the new integration component
    * In the drop down box called "Mule version" you can select what version of Mule you want the integration component to use.
> > > Only v3.3.0 is available for soi-toolkit v0.6.0.
    * Deselect all transports except JMS, that is used for logging, since we will not need them for this tutorial


> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/1.3_createIc.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/1.3_createIc.png)

  * Click on the "Finish" button

> The wizard now starts to execute and logs its output to a text area.

> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/1.4_createIc.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/1.4_createIc.png)

  * The following work is performed by the wizard:
    * Created folders and files according to the input on the previous page.
    * Launch maven to do a initial build and also create eclipse files
    * Imports the project into the current Mule Studio workspace

## Create skeleton code for the service ##

We are now creating the skeleton code for the service:

  * Select the menu "File --> New --> Other" and expand the wizard "SOI Toolkit Code Generator"
  * Select the code generator "Create a new service"

> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/2.1_createService.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/2.1_createService.png)

  * Click on the "Next >" button
  * The wizard "SOI Toolkit - Create a new service" opens up
    * Select the message exchange pattern "Request/Response"
    * Select the inbound transport "SOAP/HTTPS"
    * Select the outbound transport "SOAP/HTTPS"
    * Select your service project using the "Browse..." button
    * Set a proper service-name
> > HINT: Avoid using "`service`" in the name of the service since the actual Mule service will be suffixed with "`-service`"


> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/2.2_createService.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/2.2_createService.png)

  * Click on the "Finish" button
    * The wizard now starts to generate files and refresh the workspace.

  * When the wizard is done you can find the files for your new service in the Mule Studio Package Explorer

> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/2.3_createService.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/2.3_createService.png)

> The two most important files generated are the Mule flow configuration file, `mySample-service.xml`:

> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/3_service.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/3_service.png)

> ...and the jUnit based integration test, `MySampleIntegrationTest.java`:

> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/4_integrationTest.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/4_integrationTest.png)

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

## Test the service automatically ##

The integration tests are configured to only require embedded resource managers, e.g. messaging, ftp, sftp and/or database servers. This means that Mule will startup these servers inside the JVM during startup of the integration tests eliminating the need of pre configured and started resource managers (or mule servers).

All generated tests are based on jUnit and will run when Maven executes it test goal. The tests can also be started by hand within Mule Studio using the default Eclipse support for jUnit as described below.

### Run generated unit and integrations tests ###

  * Run unit tests
    * Right-click the project `sample1` and select "Run As" --> "JUnit Test"
> > Both integration and unit tests are executed and its success are reported in the JUnit view.
> > The Console view displays at the same time log-messages from the execution:


> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/5_jUnitTestResults.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/5_jUnitTestResults.png)

> NOTE: Take a look at the unit test code and the transformer code for a better understanding on what is going on :-)

## Test the service manually ##

Writing automated integration tests are a strongly recommended but from time to time it is also valuable to be able to start Mule with services and teststub producer services defined in the integration component directly from within Mule Studio and do some tests manually.

### Start Mule ESB with the service and a teststub service for the producer ###

When doing manual tests we normally want to do it against external pre-started resource managers, such as an external messaging server.
Since the generated code depends on a Apache ActiveMQ server for sending log-messages you either has to install and start ActiveMQ in your development environment or change the configuration to use an embedded ActiveMQ server. See [install ActiveMQ](http://code.google.com/p/soi-toolkit/wiki/IG_RT_ActiveMQ) for instructions or simply change the jms connector in the file `src/main/app/sample1-common.xml` from:

```
soitoolkit-mule-jms-connector-activemq-external.xml
```

to:

```
soitoolkit-mule-jms-connector-activemq-embedded.xml
```

as visualized in the following screenshot:
![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/6.1_manualTest.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/6.1_manualTest.png)

  * Start Mule ESB with the service and its teststub service for the producer by right-click on `src/test/java/org/sample/sample1/Sample1MuleServer.java` and select "Run As" --> "Java Application".

![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/6.2_manualTest.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/6.2_manualTest.png)

The following should be written to the console after a few seconds:

![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/6.3_manualTest.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/6.3_manualTest.png)

**NOTE:** Currently we can't use the standard Mule Application launcher that comes with Mule Studio since it doesn't work with mule config files outside the `src/main/app` folder meaning that teststubs for service producers from the `src/test/resources` folder currently can't be launched using the default Mule Studio launcher.

### Call the service using the generated CXF test consumer ###

The simples way to test the service now running in the Mule instance is using the already existing Apache CXF based test consumer.

  * Start the test consumer by right-click on `src/test/java/org/sample/sample1/mysample/MySampleTestConsumer.java` and select "Run As" --> "Java Application".

![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/6.4_manualTest.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/6.4_manualTest.png)

  * The testconsumer connects to the service, makes a call and print outs some information of the response from the service:

![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/6.5_manualTest.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/6.5_manualTest.png)

  * In the Sample1MuleServer console window you can see four new logEvents. Two for the incoming and outgoing request and two for the incoming and outgoing response. You can also see some logging from the test producer (`MySampleTestProducer`) that confirms that the call reached the test producer and back.

![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/6.6_manualTest.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/6.6_manualTest.png)

### Call the service using soapUI ###

If you really want to see the request and response soap-payloads you can use soapUI.

First you have to configure soapUI with a client certificate since we are using HTTPS with mutual authentication. Select in the menu "File" --> "Preferences" and in the preference window select the tab "SLL Settings". Point out the client-test-certificate as keystore, enter its password (`password`) and finally enable client authentication by selecting the checkbox.

![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/7.1_soapui_sllSettings.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/7.1_soapui_sllSettings.png)

Now create a new project in soapUI (select in the menu "File" --> "New soapUI Project", enter a project name and point out the wsdl that the mule service expose: `https://localhost:8081/sample1/services/mySample/v1?wsdl` and hit the OK button.

Note: You can find the url above in the property `MYSAMPLE_INBOUND_URL` in the property-file `src/main/resources/sample1-config.properties`

![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/7.2_soapui_createProject.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/7.2_soapui_createProject.png)

soapUI will now parse the wsdl and create a sample request for the soap operation in the wsdl.
Expand the project down to the sample request, Request1, "sampleBinding" --> "sample" --> "Request 1" and open it:

![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/7.3_soapui_sendRequest.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/7.3_soapui_sendRequest.png)

In the "Request 1" - window ckick on the green start button to send the request and see in the Mule Studio console how Mule executes the request and then back in the soapUI window for "Request 1" you can see the response:

![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/7.4_soapui_receiveResponse.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/7.4_soapui_receiveResponse.png)

So only one main question remains, can anyone else see this traffic???

If you use some http or tcp package sniffer you can see that both the request and response are encrypted. I have used TCPmon to monitor the traffic between the consumer and mule with the following result:

![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/7.5_tcpmon.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/7.5_tcpmon.png)

Both the request and the response are for sure unreadable for someone not in possession of either the consumer or the producer certificate (including its password)!

Note that the actual readable text in the output above are identities of the certificates and not from the actual payload!

### Stop Mule ESB ###

Ok, so we are done. The only remaining thing to do is to shut down the mule server running inside Mule Studio.

  * Stop the mule server by setting focus in its console window and hit return, then the server will shutdown gracefully and displaying output like:

> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/7.6_stopServer.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/SecureRequestResponseService/7.6_stopServer.png)