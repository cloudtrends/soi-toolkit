# Architecture #

**Content**


# Introduction #

Soi-toolkit contains a number of suggested structures for various purposes. These structures are based on experiences from several Mule projects at several companies and have been proven to work very well establishing a de facto standard in each area.

The structures are:

  * [A Component Model](#Component_Model.md)
    * _**Answers the questions:**_
      * How can we package our Mule based services (and their service contracts) so that we can start small without a overcomplex structure but then grow without getting into a mess later on?
      * What are the components that we version and deploy as separate units to test and production?
      * How can we share and reuse services and service contracts?
    * Defines the concepts of [Integration Components](ConceptsAndDefinitions#Core_Concepts.md) and [Service Description Components](ConceptsAndDefinitions#Core_Concepts.md)

  * [Source Code](#Source_Code.md)
    * _**Answers the question:**_
      * How can we structure the source code inside an Integration Component and a Service Description Component so that services can be developed and tested efficiently without interfering with each other?

  * [Maven Dependency Management](#Maven_Dependency_Management.md)
    * _**Answers the questions:**_
      * How can we use Maven to manage all the dependencies that Mule and other open source products introduce?
      * How can we use Maven to manage a growing number of Integration Components and a Service Description Components?
      * How can we use Maven to automate the release handling of an Integration Component and a Service Description Component?

  * [Test Driven Development](#Test_Driven_Development.md)
    * _**Answers the questions:**_
      * How can we structure jUnit based unit and integrations tests so that it is very easy to get started and continue with Test Driven Development of Mule based services?
      * How can we reuse the teststub producers we generate for jUnit based integrations in system tests where some systems are not available?

# Component Model #

  * **Answers the questions:**
    * How can we package our Mule based services (and their service contracts) so that we can start small without a overcomplex structure but then grow without getting into a mess later on?
    * What are the components that we version and deploy as separate units to test and production?
    * How can we share and reuse services and service contracts?

  * Defines the concepts of [Integration Components](ConceptsAndDefinitions#Core_Concepts.md) and [Service Description Components](ConceptsAndDefinitions#Core_Concepts.md)

![http://soi-toolkit.googlecode.com/svn/wiki/Architecture/component-model.png](http://soi-toolkit.googlecode.com/svn/wiki/Architecture/component-model.png)

# Source Code Structures #

  * Source Code
    * **Answers the question:**
      * How can we structure the source code inside an Integration Component and a Service Description Component so that services can be developed and tested efficiently without interfering with each other?

## Structure of an Integration Component ##

![http://soi-toolkit.googlecode.com/svn/wiki/Architecture/code-structure-one-ic.png](http://soi-toolkit.googlecode.com/svn/wiki/Architecture/code-structure-one-ic.png)

## Structure of Mule configuration files ##

## Structure of files for an Service ##

![http://soi-toolkit.googlecode.com/svn/wiki/Architecture/code-structure-one-service.png](http://soi-toolkit.googlecode.com/svn/wiki/Architecture/code-structure-one-service.png)

## Structure of a Service Description Component ##

Creation of projects for skeleton WSDL and XML Schemas that complies to [WS-I Basic Profile](http://www.ws-i.org/deliverables/workinggroup.aspx?wg=basicsecurity) with automatic creation of Java code...

![http://soi-toolkit.googlecode.com/svn/wiki/Architecture/service-description-components.png](http://soi-toolkit.googlecode.com/svn/wiki/Architecture/service-description-components.png)

# Maven Dependeny Management #

  * Maven Dependency Management
    * **Answers the questions:**
      * How can we use Maven to manage all the dependencies that Mule and other open source products introduce?
      * How can we use Maven to manage a growing number of Integration Components and a Service Description Components?
      * How can we use Maven to automate the release handling of an Integration Component and a Service Description Component?

![http://soi-toolkit.googlecode.com/svn/wiki/Architecture/maven-dependeny-management.png](http://soi-toolkit.googlecode.com/svn/wiki/Architecture/maven-dependeny-management.png)

# Test Driven Development #

  * Test Driven Development
    * **Answers the questions:**
      * How can we structure jUnit based unit and integrations tests so that it is very easy to get started and continue with Test Driven Development of Mule based services?
      * How can we reuse the teststub producers we generate for jUnit based integrations in system tests when some systems are not available?

## Structuring jUnit based unit and integrations tests ##

Testing integrations and service is traditionally a very complex task involving many systems and infrastructure components like described in this picture:

![http://soi-toolkit.googlecode.com/svn/wiki/Architecture/integration-tests-problem.png](http://soi-toolkit.googlecode.com/svn/wiki/Architecture/integration-tests-problem.png)

With Mule, being based on Spring, Maven and jUnit we can however simplify this problem a lot and allow for local automatic integration testing of services and integrations.
Local integration tests means that a test class sends messages over the actual protocols to the service inbound endpoint and that a teststub producer receives the messages sent out from the service on the outbound protocols and that the test class compare these messages with predefined expected messages.

![http://soi-toolkit.googlecode.com/svn/wiki/Architecture/integration-tests-solution.png](http://soi-toolkit.googlecode.com/svn/wiki/Architecture/integration-tests-solution.png)

![http://soi-toolkit.googlecode.com/svn/wiki/Architecture/service-source-files.png](http://soi-toolkit.googlecode.com/svn/wiki/Architecture/service-source-files.png)

## Reusing teststub producers in system tests when some systems are not available ##

When moving into coordinated systems tests between a number of systems and Mule based services it is often a problem that one or more of the involved systems are not ready for testing while other systems are eager to get started with system testing. It could be the case that some systems don't yet support the latest release of common service descriptions or that they simply can't support the project with a test environment for the moment.

![http://soi-toolkit.googlecode.com/svn/wiki/Architecture/system-tests-problem.png](http://soi-toolkit.googlecode.com/svn/wiki/Architecture/system-tests-problem.png)

In these cases it has been proven to be very valuable to be able to package the teststub producers created for running local integration tests in separate deploy files and deploy them into the system test environment having them taking the role for the systems that can't participate in the systems tests for the moment.

![http://soi-toolkit.googlecode.com/svn/wiki/Architecture/system-tests-solution.png](http://soi-toolkit.googlecode.com/svn/wiki/Architecture/system-tests-solution.png)