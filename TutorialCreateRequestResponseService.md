<font color='red' size='5'><b>NOTE:</b> This page is partially outdated.<br />Work is ongoing to update this page!</font>

# Tutorial: Create a Request/Response Service #

**Content**


# Introduction #

**Prerequisites:** For this tutorial is that the tutorial [Create a new Integration Component](TutorialCreateIntegrationComponent.md) is completed.

This tutorial will help you to get started with creating a service that follows the [Request/Response Exchange Pattern](ConceptsAndDefinitions#Request/Response.md), either synchronous:

![http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/EP-RequestResponse-Synchronous.png](http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/EP-RequestResponse-Synchronous.png)

and/or asynchronous:

![http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/EP-RequestResponse-Asynchronous.png](http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/EP-RequestResponse-Asynchronous.png)

The soi-toolkit generator helps you get started by generating the following for a new service:
  * A Mule service with:
    * Transformers for both the request and the response, transformers can either be based on Java or Smooks.
    * Proper error handling, both for error messages from the producer and capturing technical errors, such as timeouts.
    * Tracing, by default log-events are created when both a request and a response are received and when they are sent.

  * jUnit based unit-tests for the transformers

  * jUnit based integration-tests for the Mule service
    * Including a test-consumer and a teststub for the producer.
    * Generated integration tests cover both a "happy days" scenario and negative tests (invalid input + timeout tests)

  * Configurable properties for settings that needs to be environment specific
    * E.g. for endpoint adresses and timeouts

The generated source code files are summarized by the following picture:

![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/RequestResponseService/RequestResponse-SourceFiles.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/RequestResponseService/RequestResponse-SourceFiles.png)

**Color schema:** yellow for files that build up the actual service, green for test-classes and grey for classes used by the test-classes.

## Consumer support ##

The soi-toolkit generator support synchronous consumers that either use SOAP or REST over HTTP.
If Mule is deployed in a servlet container, e.g. Tomcat or TCat, the HTTP connectivity can be delegated to the servlet container.

Example of a consumer that communicates directly with Mule:

![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/RequestResponseService/RequestResponse-HttpConsumer.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/RequestResponseService/RequestResponse-HttpConsumer.png)

Example of a consumer that communicates with Mule via Tomcat:

![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/RequestResponseService/RequestResponse-ServletConsumer.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/RequestResponseService/RequestResponse-ServletConsumer.png)

## Producer support ##

The soi-toolkit generator support both JMS based asynchronous producers and SOAP, REST or JDBC based synchronous producers.

Example of a asynchronous producer:

![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/RequestResponseService/RequestResponse-AsynchronousProducer.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/RequestResponseService/RequestResponse-AsynchronousProducer.png)

Example of a synchronous producer:

![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/RequestResponseService/RequestResponse-SynchronousProducer.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/RequestResponseService/RequestResponse-SynchronousProducer.png)

**NOTE**: For the time being the following features are not supported:
  * Smooks based transformers, since the smooks module not yet supports Mule 3.1
  * REST based consumers, only SOAP based, work is in progress.
  * JDBC based producers, only JMS and REST based, work is in progress.

# Create a request/response - service #

We are now going to create a service that bridge between synchronous SOAP/HTTP based consumers and asynchronous JMS producers that use a CSV-format for their messages.

![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/RequestResponseService/Create-RequestResponse-Service-0.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/RequestResponseService/Create-RequestResponse-Service-0.png)

  * Select the menu "File --> New --> Other" and expand the wizard "SOI Toolkit Code Generator"
  * Select the code generator "Create a new service"

![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/RequestResponseService/Create-RequestResponse-Service-1.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/RequestResponseService/Create-RequestResponse-Service-1.png)

  * Click on the "Next >" button
  * The wizard "SOI Toolkit - Create a new service" opens up
    * Select the message exchange pattern "Request/Response"
    * Select the inbound transport "SOAP/HTTP"
    * Select the outbound transport "JMS"
    * Select your service project using the "Browse..." button, e.g. "/mySample-services"
    * Set a proper service-name, e.g. "soap2jms"
> > HINT: Avoid using "`service`" in the name of the service since the actual Mule service will be suffixed with "`-service`", see the file list screenshot below,  `soap2jms-service.xml`

![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/RequestResponseService/Create-RequestResponse-Service-2.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/RequestResponseService/Create-RequestResponse-Service-2.png)

  * Click on the "Finish" button
    * The wizard now starts to generate files and refresh the workspace.

  * When the wizard is done you can find the files for your new service in the Eclipse Package Explorer


> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/RequestResponseService/Create-RequestResponse-Service-3.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/RequestResponseService/Create-RequestResponse-Service-3.png)

  * Files of interest:
    * Source folder `src/test/java`
> > A new package is created for the service `org.sample.mysample.soap2jms` with the Java-classes:
      * `Soap2JmsServiceIntegrationTest.java` contains some integrations tests for the whole service that you can use as a start.
      * `Soap2JmsRequestTransformerTest.java` contains some unit tests for the request transformer that you can use as a start.
      * `Soap2JmsResponseTransformerTest.java` contains some unit tests for the response transformer that you can use as a start.
      * `Soap2JmsTestConsumer.java` contains a test consumer that you can use as a start.
      * `Soap2JmsTestReceiver.java` contains a teststub producer that you can use as a start.
    * Source folder `src/test/resources`
> > The folder `teststub-services` contains the file `soap2jms-teststub-service.xml` that is a teststub producer that you can use as a start.
    * Source folder `src/main/java`
> > A new package is created for the service `org.sample.mysample.soap2jms` with the Java-classes:
> > `MySampleJmsServiceTransformer.java` contains a sample transformation that you can use as a start.
      * `Soap2JmsRequestTransformer.java` contains a sample request transformer that converts the SOAP payload to a CVS format that you can use as a start.
      * `Soap2JmsResponseTransformer.java` contains a sample response transformer that converts the CVS formatted response back to a SOAP payload that  you can use as a start.
    * Source folder `src/main/resources`
> > The folder `services` contains the file `soap2jms-service.xml` that contains the actual definition of the new service.
    * Source folder `src/environment`
> > The configuration file `mySample-config.properties` has got the following properties added:
```
# Properties for service "soap2jms"
# TODO: Update to reflect your settings
SOAP2JMS_INBOUND_URL=http://localhost:8081/mySample/services/soap2jms/v1
SOAP2JMS_REQUEST_QUEUE=MYSAMPLE.SOAP2JMS.REQUEST.QUEUE
SOAP2JMS_RESPONSE_QUEUE=MYSAMPLE.SOAP2JMS.RESPONSE.QUEUE
```

  * Run unit tests
    * Right-click the project `mySample-services` and select "Run As" --> "JUnit Test"
> > Both integration ans unit tests are executed and its success are reported in the JUnit view:


> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/RequestResponseService/Create-RequestResponse-Service-4.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/RequestResponseService/Create-RequestResponse-Service-4.png)

> The Console view displays at the same time log-messages from the execution:

> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/RequestResponseService/Create-RequestResponse-Service-5.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/RequestResponseService/Create-RequestResponse-Service-5.png)

> NOTE: Take a look at the unit test code and the transformer code for a better understanding on what is going on :-)