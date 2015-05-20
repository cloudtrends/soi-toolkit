# Using the WMQ transport (Mule EE only) #

**Content**


_Note: The WMQ-transport is only available to Mule EE customers._

This chapter describes how to use the WMQ-connector that is pre-configured in SOI-toolkit. It also serves as a good basis if you want to declare a WMQ-connector on your own.


## Create a service that uses the WMQ-transport ##
Create a service with JMS inbound and/or outbound endpoints like you normally would, see for example TutorialCreateJmsToJmsService, and then follow the steps below to make it use the WMQ-transport.


## Add dependencies for WMQ-transport ##
We need to add some Maven-dependencies for the WMQ-transport to the service-projects pom.xml.

**Note:** If you are going to use WMQ in a lot of integration components, then it is recommended to create a support component/pom that holds the below dependencies - and then add a single dependency to that artifact in your service-project, rather than add all of the below dependencies to every integration component you create.

All of the below dependencies must exist (or be put) into your organizations Maven-repository since they do not exist in any public Maven-repository.

Add required dependencies to Mule EE artifacts:
```
    <!-- Mule EE (Enterprise Edition) -->
    <dependency>
      <groupId>com.mulesource.muleesb</groupId>
      <artifactId>mule-core-ee</artifactId>
      <version>${mule.version}</version>
    </dependency>
    <dependency>
      <groupId>com.mulesource.muleesb.transports</groupId>
      <artifactId>mule-transport-wmq-ee</artifactId>
      <version>${mule.version}</version>
    </dependency>
```

also add required dependencies for the WMQ-transport:

```
    <!-- WMQ dependencies -->
    <dependency>
      <groupId>com.ibm.mq</groupId>
      <artifactId>mq</artifactId>
      <version>${ibm.wmq.version}</version>
    </dependency>
    <dependency>
      <groupId>com.ibm.mq</groupId>
      <artifactId>mqjms</artifactId>
      <version>${ibm.wmq.version}</version>
    </dependency>
    <dependency>
      <groupId>com.ibm.mq</groupId>
      <artifactId>dhbcore</artifactId>
      <version>${ibm.wmq.version}</version>
    </dependency>
```

and for WMQ v7 also add:
```
    <dependency>
      <groupId>com.ibm.mq</groupId>
      <artifactId>jmqi</artifactId>
      <version>${ibm.wmq.version}</version>
    </dependency>
```


### Script for Maven-deploy of WMQ-libs ###
  1. Download the client SupportPac (MQC71 for WMQ 7.1) from IBM
  1. Install the supportpac
  1. Extract the jar-files we need (listed above) from the installed SupportPac and upload them to your organizations Maven-repository manager by copying and modifying script: https://soi-toolkit.googlecode.com/svn/trunk/tools/soitoolkit-maven-deploy-wmq-libs/mvn-deploy-wmq7-jars.bat


## Import the WMQ-connector ##
In the service-projects main/resources/`<`component-name`>`-config.xml file, change:

```
<spring:import resource="classpath:soitoolkit-mule-jms-connector-activemq-external.xml"/>
```
to:
```
<spring:import resource="classpath:soitoolkit-mule-jms-connector-wmq-ee.xml"/>
```


## Configure the WMQ-connector ##
In the service-projects environment/`<`component-name`>`-config.properties file, add the properties below and set values for the target environment:
```
SOITOOLKIT_MULE_WMQ_HOST=qm1host
SOITOOLKIT_MULE_WMQ_PORT=1414
SOITOOLKIT_MULE_WMQ_QM=QM1
SOITOOLKIT_MULE_WMQ_USR=
SOITOOLKIT_MULE_WMQ_PWD=
SOITOOLKIT_MULE_WMQ_CHANNEL=SYSTEM.DEF.SVRCONN
SOITOOLKIT_MULE_WMQ_NO_OF_CONSUMERS=2
```


## Enable switching transport between JMS and WMQ ##
The standard JMS-transports use `jms://` for addressing while the WMQ-transport use `wmq://`.
Assuming you still want the ability to run integration tests without having to set up any infrastructure, like a Websphere MQ Queue Manager with all the queues you need, we need a way to switch between transports so tests can still be run with an embedded JMS-broker.

Add a property to enable switch of transport:
  1. Add the JMS-property in the service-projects environment/`<`component-name`>`-config.properties file:
```
# jms for ActiveMQ (for test-execution and development), wmq for deploy to TEST, QA, PROD
JMS=jms
```
  1. Add property substitution for the address in the services where JMS/WMQ is used. Change the service-files in directory src/main/app, change inbound- and outbound endpoints like:
```
<jms:inbound-endpoint queue="${WMQ_EXAMPLE_IN_QUEUE}"
...
<jms:outbound-endpoint queue="${WMQ_EXAMPLE_OUT_QUEUE}"
```
> to:
```
<inbound-endpoint address="${JMS}://${WMQ_EXAMPLE_IN_QUEUE}"
...
<outbound-endpoint address="${JMS}://${WMQ_EXAMPLE_OUT_QUEUE}"
```