# Concepts and definitions #

**Content**


# Introduction #

This page describes concepts and definitions used in the documentation of soi-toolkit.

The page is divided in the following sections:
  * **Definitions**
    * **Core Concepts**, describes some core concepts such as producer, consumer and messages.
    * **Exchange Patterns**, describes in what styles consumers and producers can exchange messages.
    * **Integration Scenarios**, describes how exchange patterns can be combines into larger interaction when more than one producer and consumer is involved.
  * **Service Model**, describes a meta-model that can be used to describe a landscape of producers and consumers and how they exchange messages using services.

# Definitions #

## Core Concepts ##

| **Icon** | **Term** | **Description**|
|:---------|:---------|:|
| ![http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/SoftwareComponent.png](http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/SoftwareComponent.png) | Software Component | A _Software Component_ is any type of software, e.g. business system, web app, eai-platform, esb, integration-platform, that contains a number of _service consumers_ and/or _service producers_. A _software component_ is a coarse grained component and has its own CM-scope, i.e. its own release cycle. |
| ![http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/IntegrationComponent.png](http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/IntegrationComponent.png) | Integration Component | An _Integration Component_ is a soi-toolkit**specific _software component_ that contains a number of related _service consumers_ and/or _service producers_ that runs on Mule ESB.**|
| ![http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/ServiceDescriptionComponent.png](http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/ServiceDescriptionComponent.png) | Service Description Component | A _Service Description Component_ is a **soi-toolkit** specific _software component_ that contains a number of related WSDL's and XML Schemas and corresponding JAX-WS and JAXB binding classes. Can be used by any type of software component (Mule based, pure Java or non Java, e.g. Microsoft .Net) to get access to WSDL's and XML Schemas in a formal and version controlled way (i.e. avoiding sending various versions of the files to a number of people using e-mail...). |
| ![http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/ServiceProducer.png](http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/ServiceProducer.png) | Service Producer | A _Service Producer_ is a fine grained component that lives within a _software component_ and that publish (produce) a service. Short name: _producer_. |
| ![http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/ServiceTestStubProducer.png](http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/ServiceTestStubProducer.png) | Service Test stub Producer | A _Service Test stub Producer_ is a fine grained component that is used during development and tests to replace a real _Service Producer_ to minimize dependencies. Short name: _test stub producer_. |
| ![http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/ServiceConsumer.png](http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/ServiceConsumer.png) | Service Consumer |  A _Service Consumer_ is a fine grained component that lives within a _software component_ that uses (consumes) a service. Short name:  _consumer_. |
| ![http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/ServiceTestConsumer.png](http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/ServiceTestConsumer.png) | Service Test Consumer |  A _Service Test Consumer_ is a fine grained component that is used during development and tests to replace a real _Service Consumer_ to minimize dependencies. Short name:  _test consumer_. |
| ![http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/Message.png](http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/Message.png) | Message  | The actual _Message_ (or payload) sent between the _service consumer_ and _service producer_ |
| ![http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/SychronousRequestResponseMessages.png](http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/SychronousRequestResponseMessages.png) | Synchronous Request/Response Invocation | The _service consumer_ sends a request _message_ to the _service producer_ and waits synchronously for a response _message_. A synchronous invocation is denoted by a solid arrowhead following conventions defined by UML. |
| ![http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/Asychronous%20Message.png](http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/Asychronous%20Message.png) | Asynchronous One-Way Invocation | The _service consumer_ sends a _message_ to the _service producer_ and does not wait for the _service producer_ to process the _message_. An asynchronous invocation is denoted by a line arrowhead following conventions defined by UML. |
| ![http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/Asychronous%20Response%20Message.png](http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/Asychronous%20Response%20Message.png) | Asynchronous One-Way Response Invocation | The _service producer_ sends back a response _message_ to the _service consumer_ and does not wait for the _service consumer_ to process the _message_. An asynchronous invocation is denoted by a line arrowhead following conventions defined by UML. |

**Note:** The concepts _service consumer_ and _service producer_ are referring to consuming and producing **the service**, **not the message**. In some cases, e.g. a one-way or publish/subscribe exchange pattern, this might look confusing since it is the service consumer that produce the message and the service producer that consumes the message so it is very important to remember that the roles are related to the service and not the message!

## Exchange Patterns ##

_Exchange patterns_ defines different styles for how _consumers_ and _producers_ can exchange _messages_ on an abstract (logical) level.

**Note**: The actual physical exchange taking place is transport dependent. E.g. a one-way exchange patterns describes that the consumer sends a message to the service producer but for polling transports (e.g. file based or database based trasports) it is actually the producer that polls the transport for new messages (e.g. files or database table rows).

soi-toolkit supports the following _exchange patterns_:

  * Request/Response
  * One-Way
  * Publish/Subscribe

### Request/Response ###

When a consumer needs a response from the producer a _request/response_ exchange patterns can be used.
Mainly depending on how fast the consumer expects a response from the producer the pattern can be divided in a synchronous and an asynchronous variant.

The synchronous variant is in many cases simple to implement, e.g. using web services tools, but the asynchronous variant provides in general more robust solutions since it can guarente the delivery for the messages and for messaging based transports (e.g. JMS) the delivery can also be transactional.

**Note**: An asynchronous request/response exchange pattern is actually built on two _one-way_ exchange patters, see below for more details.

If the expected response time is only a few seconds the synchronous variant can be used (due to its simplicity) but otherwise the asynchronous variant should be used if possible.

**Synchronous request/response exchange pattern**

![http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/EP-RequestResponse-Synchronous.png](http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/EP-RequestResponse-Synchronous.png)

**Asynchronous request/response exchange pattern**

![http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/EP-RequestResponse-Asynchronous.png](http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/EP-RequestResponse-Asynchronous.png)

### One-Way ###

If the consumer is not depending on a response from the producer a _one way_ exchange pattern is in most cases the preferred way.

The consumer sends the message asynchronously, i.e. does not wait for the producer to process the message and the underlying transport is expected to guarente the delivery of the message to the producer of the service.
Different transports fulfills the guarente of the delivery in various ways. In general messaging based transports (e.g. JMS) are preferred since they have the most robust implementation to guarente delivery of the messages and they can also be used by the consumer ans producer in a transactional way.

![http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/EP-OneWay.png](http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/EP-OneWay.png)

### Publish/Subscribe ###

In the case where a number of software components are interested in information owned and maintained by another software components (the _master_ of the data) it is in many cases a good approach to apply a publish/subscribe exchange pattern.
The master publish changes to subscribers in a asynchronous fashion very similar to the one-way exchange pattern but instead of sending the message to exactly one service producer it is sent to zero, one or many service producers (i.e. the subscribers).

![http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/EP-PublishSubscribe.png](http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/EP-PublishSubscribe.png)

## Integration Scenario ##

The exchange patterns described above can be combined into a _integration scenario_ to involve a number of software components to meet business demands, e.g. to fulfill a business process.

![http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/MessageFlow.png](http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/MessageFlow.png)

# Service Model #

Once the use of services grows (i.e. the number of consumers and producers) in the system landscape it becomes more and more important to be able to describe the exiting and planned services, integrations and integration scenarios in a structured and formal way.
Initially word documents, excel sheets and simple wiki pages will be sufficient but at some point will the number of consumers, producers and integration scenarios make it very inconvenient to maintain the information when the system landscape changes.
Performing accurate "_what if analysis_" and answering questions like "_when can we shut down this old version of service xyz?_" will be very hard to answer...

Some kind of governance tool needs to be used. Either it is a commercial high-end tool or a simple database application a proper meta-model needs to be defined before services, consumers and producers can be described.

Soi-toolkit provides the following service model and it can be used by an organisation that use soi-toolkit, either _as is_ or as a starting point for a service model tailored for an organisations specific needs.

## Logical View ##
![http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/ServiceModel.png](http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/ServiceModel.png)

**Explanation of the model:**

  * A _service_ is published by a _software component_ with the role _producer_.

  * A _software component_ that use a service describe its usage in a _contract_ with the role _consumer_

  * A _service_ expectes messages of a specific _message type_
    * Request/response based services define three types of messages:
      * Request message
      * Response message
      * Zero, one or meny Fault messages
    * One-way and publish/subscribe services only define one type of message, the request message type.

  * _Integration Scenario_ groups a number of cooperating services (by relating to the involved contracts) that combined implements for example a business process or other interaction between a number of software components. The motivation for creating a sequence is typically a need for tracking progress and measurements on an aggregated level (and not only on the individual services)

  * _Service Groups_ can be used to group services that are logically related to each other

## Physical View ##
![http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/ServiceModel-PhysicalView.png](http://soi-toolkit.googlecode.com/svn/wiki/ConceptsAndDefinitions/ServiceModel-PhysicalView.png)

**Explanation of the model:**

  * A _software component_ is deployed on an _application\_server_ that is installed on a _host_. The _application\_server_ is dedicated for a specific environment, e.g. _dev_, _test_, _qa_ or _prod_.

  * A _service_ expose an _inbound endpoint_ that is managed by a _resource manager_ that is installed on  a _host_. The _application\_server_ is dedicated for a specific environment, e.g. _dev_, _test_, _qa_ or _prod_.