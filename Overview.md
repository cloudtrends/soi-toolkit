# Overview of soi-toolkit #

**Content**


# Introduction #

Soi-toolkit helps you getting started with Mule ESB in no time using Eclipse and Maven.

The project is based on experiences from earlier projects using Mule ESB and provides setups of source code structures and usage of tools that already has been proven to be working efficiently in other projects.

# Values of soi-toolkit #

  * [Integration Pattern catalog](PatternCatalog.md)
    * Reduce time for decision making and streamline implementations.

  * [Pattern based source code generator](Tutorials.md)
    * Reduce time from decision to initial implementation, gives a common structure for all services.

  * [Structures](Architecture.md)
    * Common component model, code structure and dependency management based on Maven.
    * Well proven structures that helps the projects to start quickly and without hitting the wall when growing later on.

  * [Testability](Architecture#Test_Driven_Development.md)
    * Test integrations without any infrastructure.
    * Great time saver and quality improver!

  * [Quality of Service](Tutorials.md)
    * Out of the box support for transactions, error handling, logging, resilience...

  * [Deployment & Configuration](UserGuidelines.md)
    * Standardized and automated deploy to standalone Mule or Tomcat/TCat.
    * Support for management of environment specific configurations through standardized setup of property files.

  * [Monitoring & Tracing](UserGuidelines.md)
    * Rich set of runtime information exposed by Mule using JMX.
    * Can be consumed by generic JMX consoles (i.e. Java SE JConsole) or more advanced 3’rd party monitoring tools (MMC, Nagios...).

  * [Changeability](UserGuidelines.md)
    * Test Driven Development and well defined and automated procedures for build, release and deploy minimize both risks and time to change and improve the overall quality of the services.

  * [Customization](UserGuidelines.md)
    * SOI-Toolkit can be customized in a number of ways to fit in an organization’s existing environment.

# Recommended reading #

  1. [Tutorials](Tutorials.md) to get a jump start.
  1. [Concepts and Definitions](ConceptsAndDefinitions.md) and [Pattern Catalog](PatternCatalog.md) to better understand the concepts we use.
  1. [Architecture](Architecture.md) to get a hold of the structures we use.
  1. [User Guidelines](UserGuidelines.md) to learn about suggested working procedures.
  1. [Roadmap](Roadmap.md) to see were we are going.


# Generate source code to get you started quicker #
Soi-toolkit provides a source code generator (as a Eclipse plugin) that can generate:
  * Projects for you with a structure and content that supports an efficient development process based on Maven
  * Skeleton services and integrations by selecting a basic integration pattern (request/response, oneway or publish/subscribe) and then selecting protocols for inbound and outbound messages.

Soi-toolkit also provides you with some runtime components, reference applications and documentation to further help you to get started quickly.

## Creation of projects ##

  * No other installations required in the development environment to get started
> Everything needed is downloaded as Maven dependencies (e.g. Mule, Spring, CXF, ActiveMQ...)
  * Creation of a source code structure that enables you to start small and grow without getting stuck
  * Maven pom.xml files that automates a lot of boring and error prune build activities such as:
    * Resolving and downloading dependencies to other components
    * Compilation of source code
    * Execution of unittests
    * Creation of deployable files, e,g, war files
    * Creation of Java code given a wsdl and xml schema files
    * Release a new version of the services and integrations
      * Automates creation of proper tags in CM tools such as Subversion and deploy of runtime files to MAven repositories
  * Createion of deployable war-files on any servelt engine,e,g Tomcat, TCat or any Java EE server
  * Schema projects autiomatic creation of jar files that contains wsdl'e ans wsd + corresponding Java code genertaed by Maven using wsdl2java
  * Preoprties for configurable infromation
  * Log
  * A Main class that can be used to quickly start a Mule server based on the configuration in the project.

## Creation of skeleton services and integrations ##

The source code generator can also generate skeleton code for specific serviceas and integrations by selecting a basic integration pattern and then selecting protocols for inbound and outbound messages. The generator creates:
  * A mule config file with a service for the selected pattern supportting the selected protocols
  * Approriates testcases that enables you to unit test the service in isolation inside Eclipse without being forced to create any deploy artifacts (e.g. war-files) or deploy anything to a test-infrastructure or even having any testsystems up and running
  * teststubs, separate mule services that has the purpose to act as receivers of the messages sent out by the Mule service