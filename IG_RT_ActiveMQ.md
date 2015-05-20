# Installation Guide for Apache ActiveMQ #

**Content**


## Installation instructions ##

### On Windows ###

  * Download ActiveMQ from http://www.apache.org/dyn/closer.cgi?path=%2Factivemq%2Fapache-activemq%2F5.6.0%2Fapache-activemq-5.6.0-bin.zip

  * Unzip to some folder, e.g. `C:\opt`

> NOTE: Avoid path names that contain blanks as they can cause problems when using ActiveMQ later on.

  * Go to the folder `C:\opt\apache-activemq-5.6.0\bin\win64` and execute the file `InstallService.bat`.

> Active MQ is now registered as a windows service and will be started automatically when the Windows server is started.

> NOTE: If you prefer to start ActiveMQ manually you can find start script (activemq.bat) in the bin-folder.

  * Ensure that ActiveMQ is running after the installation and start its windows service if not already running.

> ![http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/ActiveMQ-Windows-Service.png](http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/ActiveMQ-Windows-Service.png)

> Also use the event viewer to verify a successful start of the ActiveMQ service:

> ![http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/ActiveMQ-Windows-Event-Viewer.png](http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/ActiveMQ-Windows-Event-Viewer.png)

Go to section [verify the installation](#Verify_the_installation.md) for further platform independent verification of that ActiveMQ is running as expected.

### On Linux ###

Download the distribution with the command:

```
$ wget http://apache.dataphone.se//activemq/apache-activemq/5.6.0/apache-activemq-5.6.0-bin.tar.gz
```

Unpack the distribution with the command:

```
$ sudo tar -xvf apache-activemq-5.6.0-bin.tar.gz -C /usr/local
```

Create soft-link for the activemq daemon to your daemon init script directory
```
$ sudo ln -s /usr/local/apache-activemq-5.6.0/bin/activemq /etc/init.d/activemq
```

Make ActiveMQ startup at boot-time:
```
$ sudo update-rc.d activemq start 66 2 3 4 5 . stop 66 0 1 6 .
```

Startup the ActiveMQ service manually:
```
$ sudo service activemq start
```

Lastly, ensure that ActiveMQ starts automatically after rebooting the OS and logging back in…
```
$ sudo reboot
$ tail /usr/local/apache-activemq-5.6.0/data/wrapper.log -n30
```

You should have a message in the tail output similar to the following: “`INFO | ActiveMQ Message Broker ... Started`”

You should also browse to the a URL similar to the following in you browser to meet the ActiveMQ admin gui: http://localhost:8161/admin/index.jsp

Verify in `/var/log/boot.log` that you got the expected start order, e.g. that ActiveMQ starts before Mule!

#### Some other useful commands ####

To verify if  ActiveMQ is running use the command:
```
$ sudo service activemq status
```

To restart ActiveMQ use the command:
```
$ sudo service activemq restart
```

To stop ActiveMQ use the command:
```
$ sudo service activemq stop
```

To uninstall the ActiveMQ service, do the following:
```
$ sudo update-rc.d -f activemq remove
$ sudo rm /etc/init.d/activemq
```

To run ActiveMQ in the console:
  * Go to the sub folder `apache-activemq-5.6.0/bin` and execute the command: `activemq console`.
    * **Note:** On Mac OS X go to the sub folder `apache-activemq-5.6.0/bin/macosx` and execute the command: `activemq console`.

### Verify the installation ###

Open the url http://localhost:8161/admin/index.jsp in a web browser.

A page like the following should now be displayed:

![http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/ActiveMQVerifyInstallation.png](http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/ActiveMQVerifyInstallation.png)

## Configuration ##

An ActiveMQ message broker has a configuration file typically named `${ACTIVEMQ_HOME}/conf/activemq.xml`.

Note that the XML elements under the root `<broker>`-element in this file shall be declared in alphabetic order

The following types of configuration is described below:

  * [How to configure the Java Service Wrapper](#Java_Service_Wrapper.md)
  * [How to setup retry and DLQ-handling policies](#Setup_retry_and_DLQ-handling_policies.md)
  * [Configure broker for reliable message persistence](#Configure_broker_for_reliable_message_persistence.md)
  * [Configure broker resource usage](#Configure_broker_resource_usage.md)
  * [How to setup a network of message brokers](#Setup_a_network_of_message_brokers.md)
  * [How to setup master/slaves message brokers for High Availability](#Setup_master/slaves_message_brokers_for_High_Availability.md)
  * [How to secure connections using SSL over TCP](#Secure_connections_using_SSL_over_TCP.md)

### Java Service Wrapper ###
Configure the Java Service Wrapper according to [this instruction](IG_RT_JavaServiceWrapper.md).

### Setup retry and DLQ-handling policies ###

For background information see:
  * http://activemq.apache.org/failover-transport-reference.html
  * http://activemq.apache.org/message-redelivery-and-dlq-handling.html

**TO BE DESCRIBED**

```
<broker...>
  <destinationPolicy>
    <policyMap>
      <policyEntries>
        <!-- Set the following policy on all queues using the '>' wildcard -->
        <policyEntry queue=">">
          <deadLetterStrategy>
            <!--
              Use the prefix 'DLQ.' for the destination name, and make
              the DLQ a queue rather than a topic
            -->
            <individualDeadLetterStrategy
              queuePrefix="DLQ." useQueueForQueueMessages="true" />
          </deadLetterStrategy>
        </policyEntry>
      </policyEntries>
    </policyMap>
  </destinationPolicy>
  ...
</broker>
```

### Configure broker for reliable message persistence ###
Change from:
```
<persistenceAdapter>
  <kahaDB directory="${activemq.data}/kahadb"/>
</persistenceAdapter>
```
to:
```
<persistenceAdapter>
  <kahaDB directory="${activemq.data}/kahadb"
      enableJournalDiskSyncs="true"
      concurrentStoreAndDispatchQueues="false"
      concurrentStoreAndDispatchTopics="false"/>
</persistenceAdapter>
```

Explanation:
  * enableJournalDiskSyncs="true": default value, configured only to be explicit together with below parameters
  * concurrentStoreAndDispatchQueues="false": true default, doesn't reliably sync messages to disc before dispatch if "true" (weighted slightly towards throughput performance over reliability)
  * concurrentStoreAndDispatchTopics="false": false default, configured only to be explicit

References:
  1. http://fusesource.com/docs/broker/5.4/persistence/KahaDB-Concurrent.html
  1. http://activemq.apache.org/kahadb.html

### Configure broker resource usage ###
To avoid producer flow-control kicking in frequently when using somewhat larger messages (below configuration tested with AMQ 5.6 and message sizes up to 20Mb (bytesmessages).
Note that the default config has a very low memory usage setting.

**broker**: add splitSystemUsageForProducersConsumers="true" :
```
<!--
By default this will split the broker’s Main memory usage into 60% for the producers and 40% for the consumers. To tune this even further, set the producerSystemUsagePortion and consumerSystemUsagePortion on the main broker element:
<broker splitSystemUsageForProducersConsumers="true" producerSystemUsagePortion="70" consumerSystemUsagePortion="30">
-->
<broker xmlns="http://activemq.apache.org/schema/core" brokerName="localhost" dataDirectory="${activemq.data}"
    splitSystemUsageForProducersConsumers="true">
```


**broker/destinationPolicy**: change from:
```
<policyEntry topic=">" producerFlowControl="true" memoryLimit="1mb">
<policyEntry queue=">" producerFlowControl="true" memoryLimit="1mb">
```
to:
```
<policyEntry topic=">" producerFlowControl="true" memoryLimit="1mb" topicPrefetch="1" durableTopicPrefetch="1">
<policyEntry queue=">" producerFlowControl="true" memoryLimit="25mb" queuePrefetch="1" reduceMemoryFootprint="true">￼
```

Explanation:
  * memoryLimit="25mb": the amount of memory that can be bound to a queue
  * queuePrefetch="1": the number of messages that can be pushed to a client before ACK, can cause out-of-memory with to many (large) messages, AND can cause starvation of consumers (that could otherwise have processed some of the message in parallell)
  * reduceMemoryFootprint="true": avoid that a copy of a persistent message is stored in memory (for quick dispatch), instead flush directly to disc (to handle load situations and abnormal situations where messages are built up, for example when consumers can't keep up or are absent)


**broker/systemUsage**: change memoryUsage limit like (use half the heapsize as value, a rough rule of thumb):
```
<systemUsage>
  <systemUsage>
    <memoryUsage>
      <!--
        AMQ 5.5.1 uses 512Mb max heap by default (win32, 64-bit Linux).
        AMQ 5.6.0 uses 1Gb max heap by default (win32, win64, 64-bit Linux).
        See bin/linux-x86-64/wrapper.conf:
          wrapper.java.maxmemory=1024
      -->
      <memoryUsage limit="512 mb"/>
```

References:
  1. http://activemq.apache.org/per-destination-policies.html
  1. http://activemq.apache.org/javalangoutofmemory.html
  1. http://fusesource.com/docs/broker/5.5/tuning/front.html
  1. http://www.christianposta.com/blog/?p=273


### Setup a network of message brokers ###

For background information see:
  * http://activemq.apache.org/networks-of-brokers.html

**TO BE DESCRIBED**

### Setup master/slaves message brokers for High Availability ###

For background information see:
  * http://activemq.apache.org/shared-file-system-master-slave.html

**TO BE DESCRIBED**

### Secure connections using SSL over TCP ###

For background information see:
  * http://activemq.apache.org/ssl-transport-reference.html
  * http://activemq.apache.org/how-do-i-use-ssl.html

Three different XML-element needs to be configured:
  * `<sslContext>` - defines certificates and trust stores to be used.
  * `<transportConnectors>` - defines secure connectors to be used by ActiveMQ - clients.
  * `<networkConnectors>` - defines secure connectors to be used by other ActiveMQ -brokers to establish a secure network of brokers.

**Reference:** [ActiveMQ-SSL-transport](http://activemq.apache.org/ssl-transport-reference.html).

#### Configure the `<sslContext>` - element ####

Server certificate and truststore is defined in a traditional Java-style, e.g.:

```
<sslContext>
    <sslContext 
	keyStore="file:.../server-cert.jks" keyStorePassword="password" keyStoreType="PKCS12"
        trustStore="file:.../truststore.jks" trustStorePassword="password"/>
</sslContext>
```

#### Configure the `<transportConnectors>` - element ####

Add secure-connector to the `<transportConnectors>` - element as required, e.g. one ssl connector for server-only-cert, `ssl1`, and one for mutual-authentication, `ssl2`:

```
<transportConnectors>
    <transportConnector name="openwire" uri="tcp://0.0.0.0:61616"/>
    <transportConnector name="ssl1" uri="ssl://0.0.0.0:61617" />
    <transportConnector name="ssl2" uri="ssl://0.0.0.0:61618?needClientAuth=true" />
</transportConnectors>
```

If no longer required the pure tcp-connector, called `openwire` in the example above, can be removed to avoid unsecured connections to the broker.

#### Configure the `<networkConnectors>` - element ####

Add secure-connector to other brokers in the `<networkConnectors>` - element as required, e.g.:

```
<networkConnectors>
    <networkConnector uri="static:(ssl://remote-amq-mb-ip-address:61618)" duplex="true"/>
</networkConnectors>
```

**Note:** ActiveMQ does not support a mix of multicast network-connectors and ssl according to: [ActiveMQ-SSL-transport](http://activemq.apache.org/ssl-transport-reference.html).

This means that any multicast connectors like the one below should be removed:

```
<networkConnector uri="multicast://default"/>
```