<font color='red' size='5'><b>NOTE:</b> This page is partially outdated.<br />Work is ongoing to update this page!</font>

# Tutorial: Create a One-Way Service #

**Content**


# Introduction #

**Prerequisites:** For this tutorial is that the tutorial [Create a new Integration Component](TutorialCreateIntegrationComponent.md) is completed.

This tutorial will help you to get started with creating a service that follows the [One-Way Exchange Pattern](ConceptsAndDefinitions#One-Way.md):

![http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/EP-OneWay.png](http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/EP-OneWay.png)

The soi-toolkit generator helps you get started by generating the following for a new service:
  * A Mule service with:
    * Transformer for the message, transformers can either be based on Java or Smooks.
    * Proper error handling, logging errors and rolling back transaction for transactional transports (jms, jdbc and vm) and retrying sending the message for transports that support automatic retries (jms).
    * Tracing, by default log-events are created when a message is received and when it is sent.

  * jUnit based unit-tests for the transformer.

  * jUnit based integration-tests for the Mule service
    * Including a teststub for the producer.
    * Generated integration tests cover both a "happy days" scenario and negative tests (invalid input + timeout tests)

  * Configurable properties for settings that needs to be environment specific
    * E.g. for endpoint adresses.

The generated source code files are summarized by the following picture:

![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/OneWayService/OneWay-SourceFiles.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/OneWayService/OneWay-SourceFiles.png)

**Color schema:** yellow for files that build up the actual service, green for test-classes and grey for classes used by the test-classes.

## Consumer support ##

The soi-toolkit generator support asynchronous consumers that use one of the following transports: JMS, JDBC, File, FTP, SFTP, HTTP (Multipart POST), POP3, IMAP or VM.

If Mule is deployed in a servlet container, e.g. Tomcat or TCat, the HTTP connectivity can be delegated to the servlet container for multipart HTTP POSTs.

Example of a consumer that communicates directly with Mule:

![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/OneWayService/OneWay-Consumer.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/OneWayService/OneWay-Consumer.png)

Example of a consumer that communicates with Mule via Tomcat:

![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/OneWayService/OneWay-ServletConsumer.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/OneWayService/OneWay-ServletConsumer.png)

## Producer support ##

The soi-toolkit generator support asynchronous producers that use one of the following transports: JMS, JDBC, File, FTP, SFTP, SMTP or VM.

Example of a producer:

![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/OneWayService/OneWay-Producer.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/OneWayService/OneWay-Producer.png)

# Create a one-way - service #

We are now going to create a service that both receives and send messages using JMS and transform them on the way. The service user Mule transaction support to never loose a message in case of an error and uses the underlying JMS provider (Apache ActiveMQ to automatically retry a failed message a configurable number of times before placing the message on a dead-letter queue.

![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/OneWayService/Create-OneWay-Service-0.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/OneWayService/Create-OneWay-Service-0.png)

  * Select the menu "File --> New --> Other" and expand the wizard "SOI Toolkit Code Generator"
  * Select the code generator "Create a new service"

![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/OneWayService/Create-OneWay-Service-1.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/OneWayService/Create-OneWay-Service-1.png)

  * Click on the "Next >" button
  * The wizard "SOI Toolkit - Create a new service" opens up
    * Select the message exchange pattern "One Way"
    * Select the inbound transport "JMS"
    * Select the outbound transport "JMS"
    * Select your service project using the "Browse..." button, e.g. "/mySample-services"
    * Set a proper service-name, e.g. mySampleJmsService
> > NOTE: In real usage, avoid using "`service`" in the name of the service since the actual Mule service will be suffixed with "`-service`", see the file list screenshot below,  `mySampleJmsService-service.xml`

![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/OneWayService/Create-OneWay-Service-2.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/OneWayService/Create-OneWay-Service-2.png)

  * Click on the "Finish" button
    * The wizard now starts to generate files and refresh the workspace.

  * When the wizard is done you can find the files for your new service in the Eclipse Package Explorer


> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/OneWayService/Create-OneWay-Service-3.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/OneWayService/Create-OneWay-Service-3.png)

  * Files of interest:
    * Source folder `src/test/java`
> > A new package is created for the service `org.sample.mysample.mysamplejmsservice` with two Java-classes.
> > `MySampleJmsServiceTransformerTest.java` contains some unit tests for the transformer class that you can use as a start.
> > `MySampleJmsServiceIntegrationTest.java` contains some integrations tests for the whole service that you can use as a start.
> > `MySampleJmsServiceTestReceiver.java` contains a teststub receiver that you can use as a start.
    * Source folder `src/test/resources`
> > The folder `teststub-services` contains the file `MySampleJmsService-teststub-service.xml` that is a teststub service that you can use as a start.
    * Source folder `src/main/java`
> > A new package is created for the service `org.sample.mysample.mysamplejmsservice` with one Java-class.
> > `MySampleJmsServiceTransformer.java` contains a sample transformation that you can use as a start.
    * Source folder `src/main/resources`
> > The folder `services` contains the file `MySampleJmsService-service.xml` that contains the actual definition of the new service.
    * Source folder `src/environment`
> > The configuration file `mySample-config.properties` has got the following properties added:
```
 # Properties for jms-service "mySampleJmsService"
 # TODO: Update to reflect your settings
 MYSAMPLEJMSSERVICE_IN_QUEUE=MYSAMPLE.MYSAMPLEJMSSERVICE.IN.QUEUE
 MYSAMPLEJMSSERVICE_OUT_QUEUE=MYSAMPLE.MYSAMPLEJMSSERVICE.OUT.QUEUE
 MYSAMPLEJMSSERVICE_DL_QUEUE=DLQ.MYSAMPLE.MYSAMPLEJMSSERVICE.IN.QUEUE
```

  * Run unit tests
    * Right-click the project `mySample-services` and select "Run As" --> "JUnit Test"
> > Both integration ans unit tests are executed and its success are reported in the JUnit view:


> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/OneWayService/Create-OneWay-Service-4.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/OneWayService/Create-OneWay-Service-4.png)

> The Console view displays at the same time log-messages from the execution:

> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/OneWayService/Create-OneWay-Service-5.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/OneWayService/Create-OneWay-Service-5.png)

> NOTE: Take a look at the unit test code and the transformer code for a better understanding on what is going on :-)

  * Run tests manually
> Sometimes just running unit tests are not sufficient so it is a good idea to know how to perform manual tests locally in your own PC.
    * NOTE: Requires that you have ActiveMQ installed separately and that it is started.
> > See [installation guide](InstallationGuide#Installing_Apache_ActiveMQ.md) for instructions.
    * Right-click on the test-server `MySampleMuleServer.java` in package `org/sample/mysample` in source folder `src/test/java` and select "Run As --> Java Application"
    * Go to the url `http://localhost:8161/admin/queues.jsp` in a web browser.
    * Write an ok test-message (e.g. "AAA") to the in-queue (`MYSAMPLE.MYSAMPLEJMSSERVICE.IN.QUEUE`) using the ActiveMQ console and watch the console output and the log + out-queue (`SOITOOLKIT.LOG.INFO` and `MYSAMPLE.MYSAMPLEJMSSERVICE.OUT.QUEUE`)
    * Write a incorrect message (e.g. "ERROR") to the in-queue and review the console + queues.
    * Stop the mule server by giving focus to the console view in Eclipse and pressing the return key.

![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/OneWayService/Create-OneWay-Service-6.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/OneWayService/Create-OneWay-Service-6.png)