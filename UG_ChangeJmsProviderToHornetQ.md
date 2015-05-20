**Content**


# Change JMS provider to HornetQ #

This page describes how you can change the default JMS provider, Apache ActiveMQ, to HornetQ.

  1. First we create an integration component and create some services that use JMS based on ActiveMQ and verify that it work as expected.
  1. In the next step we perform the necessary changes to replace ActiveMQ with HornetQ.

## Create an integration component that use JMS and the default JMS provider, ActiveMQ ##

In this section we use the maven plugin (v0.5.0-SNAPSHOT) to create the code but you can as well use the eclipse plugin if you prefer.
Since we are using a snapshot version of soi-toolkit ensure that you have added soi-toolits snapshot-maven-repo to your settings.xml file as described here: [Testing snapshot versions of soi-toolkit](DeveloperGuidelines#Testing_snapshot_versions_of_soi-toolkit.md)

  * Create a new integration component
```
mvn org.soitoolkit.tools.generator:soitoolkit-generator-maven-plugin:0.5.0-SNAPSHOT:genIC -DartifactId=ic -DgroupId=org.myorg.ic -Dconnectors=FTP,SFTP,JDBC
```

  * Create a sample jsm-to-jms service
```
cd ic
mvn org.soitoolkit.tools.generator:soitoolkit-generator-maven-plugin:0.5.0-SNAPSHOT:genService -Dservice=jms2jms -DmessageExchangePattern=One-Way -DinboundTransport=JMS -DoutboundTransport=JMS
```

  * Build and run integrations tests with the default Apache ActiveMQ as JMS provider
```
mvn install
```

  * Start Mule server with ActiveMQ as JMS provider (launch ActiveMQ separately), test placing messages on the in-queue, IC.JMS2JMS.IN.QUEUE in this case, using ActiveMQ admin console, http://localhost:8161/admin/queues.jsp
```
activemq start
mvn -PmuleServer
```

## Replace ActiveMQ with HornetQ ##

  * If you want to edit the source code in Eclipse you can create Eclipse project files with the command:
```
mvn eclipse:eclipse
```

  * Update **pom.xml** with HornetQ dependencies
> First add some properties for the versions used of HornetQ and its io-package Netty:
```
    <properties>
        <hornetq.version>2.2.5.Final</hornetq.version>
        <netty.version>3.2.3.Final</netty.version>
    </properties>
```

> Next declare dependencies for HornetQ client and for embedded server used during tests:
```
<!-- HornetQ Client -->
<dependency>
    <groupId>org.hornetq</groupId>
    <artifactId>hornetq-core-client</artifactId>
    <version>${hornetq.version}</version>
</dependency>
<dependency>
    <groupId>org.hornetq</groupId>
    <artifactId>hornetq-jms</artifactId>
    <version>${hornetq.version}</version>
</dependency>
<dependency>
    <groupId>org.jboss.netty</groupId>
    <artifactId>netty</artifactId>
    <version>${netty.version}</version>
</dependency>
	
<!-- HornetQ Embedded Server -->
<dependency>
    <groupId>org.hornetq</groupId>
    <artifactId>hornetq-spring-integration</artifactId>
    <version>${hornetq.version}</version>
    <scope>test</scope>
</dependency>
<dependency>
    <groupId>org.hornetq</groupId>
    <artifactId>hornetq-core</artifactId>
    <version>${hornetq.version}</version>
    <scope>test</scope>
</dependency>  
```

  * Add HornetQ specific properties to the property-file, **/ic/src/main/resources/ic-config.properties**
```
SOITOOLKIT_MULE_HORNETQ_HOST=localhost
SOITOOLKIT_MULE_HORNETQ_PORT=5445
```

  * Change the name of the DLQ-queue in the property-file, **/ic/src/main/resources/ic-config.properties**
> from:
```
MYSAMPLE_DL_QUEUE=DLQ.IC6.MYSAMPLE.IN.QUEUE
```
> to:
```
MYSAMPLE_DL_QUEUE=DLQ
```


  * Add the file **hornetq-jms.xml** to **src/main/resources**
> Unlike ActiveMQ HornetQ requires a configuration file that declare what JMS queues that is shall provide.
> For this example the following queues are required. Look into your proeprty-file for what queue-names you need!
```
<configuration 
   xmlns="urn:hornetq"
   xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
   xsi:schemaLocation="urn:hornetq /schema/hornetq-jms.xsd">

   <queue name="SOITOOLKIT.LOG.INFO">
      <entry name="SOITOOLKIT.LOG.INFO"/>
   </queue>

   <queue name="SOITOOLKIT.LOG.ERROR">
      <entry name="SOITOOLKIT.LOG.ERROR"/>
   </queue>
   
   <queue name="IC.JMS2JMS.IN.QUEUE">
      <entry name="IC.JMS2JMS.IN.QUEUE"/>
   </queue>

   <queue name="DLQ.IC.JMS2JMS.IN.QUEUE">
      <entry name="DLQ.IC.JMS2JMS.IN.QUEUE"/>
   </queue>

   <queue name="IC.JMS2JMS.OUT.QUEUE">
      <entry name="IC.JMS2JMS.OUT.QUEUE"/>
   </queue>
</configuration>
```

  * Change the integration test to use a HornetQ-connector and rerun the integration tests.
> Edit the method **getConfigResources()** and replace:
> `soitoolkit-mule-jms-connector-activemq-embedded.xml` with
> `soitoolkit-mule-jms-connector-hornetq-embedded.xml` for embedded HornetQ or
> `soitoolkit-mule-jms-connector-hornetq-external.xml` for external HornetQ.
```
	protected String getConfigResources() {
//	 	return "soitoolkit-mule-jms-connector-activemq-embedded.xml," + 
		return "soitoolkit-mule-jms-connector-hornetq-embedded.xml," + 
//		return "soitoolkit-mule-jms-connector-hornetq-external.xml," +   
		...
	}
```

  * In the integration test class replace "`new ActiveMqJmsTestUtil()`" with "`new HornetQJmsTestUtil()`" in the `doSetUpJms()` method.

  * In the integration test class change the negative test testMySample\_transformationError() method.
> from:
```
        // Verify that the message is still on the in-queue and now marked for redelivery
        List<Message> inMsgs = jmsUtil.browseMessagesOnQueue(IN_QUEUE);
        assertEquals(1, inMsgs.size());
```
> to:
```
        // Verify that the message is still on the in-queue and now marked for redelivery
        assertEquals(1, jmsUtil.getNoOfMsgsIncludingPendingForRetry(IN_QUEUE));
```


  * Run the integration-tests again, but now with HornetQ as JMS provider.
```
mvn install
```

  * Update mule config files to use HornetQ
> Change the config.xml - files that the mule server, `IcMuleServer.java`, e.g. `ic-teststubs-and-services-config.xml` to use external HornetQ instead of ActiveMQ:
```
<!-- 
        <spring:import resource="classpath:soitoolkit-mule-jms-connector-activemq-external.xml"/>
 -->
        <spring:import resource="classpath:soitoolkit-mule-jms-connector-hornetq-external.xml"/>
```

  * Copy queue definitions to the externa HornetQ definition
> Copy queues from `src/main/resources/hornetq-jms.xml` to `HORNET_HOME/config/stand-alone/non-clustered/hornetq-jms.xml`

  * Config the external HornetQ serve for redelivery
> Ensure that the a configuration similar to the following is part of the file `HORNET_HOME/config/stand-alone/non-clustered/hornetq-configuration.xml`.
> Should be placed in the end of the config-file:
```
<address-settings>
  <address-setting match="#">
	<dead-letter-address>jms.queue.DLQ</dead-letter-address>
	<max-delivery-attempts>4</max-delivery-attempts>
	<redelivery-delay>500</redelivery-delay>
  </address-setting>
</address-settings>

<message-counter-enabled>true</message-counter-enabled>
```

  * Start the externa HornetQ server
```
HORNETQ_HOME/bin/run.sh
```

  * Start Mule
```
mvn -PmuleServer
```

  * Place a message on the in queue and ensure that it is processed correctly...