# Using Apache ActiveMQ from Microsoft .Net #

**Content**


# Introduction #

**`soitoolkit-nms`** provides a very simple api for accessing [Apache ActiveMQ](http://activemq.apache.org) from an Microsoft .Net environment.

It is based on [Apache NMS](http://activemq.apache.org/nms/) but provides a easier to use api focused on supporting the following exchange patterns:

| **Icon** | **Pattern** |
|:---------|:------------|
| ![http://soi-toolkit.googlecode.com/svn/wiki/NMS/ep_store_forward.png](http://soi-toolkit.googlecode.com/svn/wiki/NMS/ep_store_forward.png) | **Store & Forward** (a.k.a One-way or Fire & Forget) |
| ![http://soi-toolkit.googlecode.com/svn/wiki/NMS/ep_asynch_request_response.png](http://soi-toolkit.googlecode.com/svn/wiki/NMS/ep_asynch_request_response.png) | **Asynchronous Request/Response** |
| ![http://soi-toolkit.googlecode.com/svn/wiki/NMS/ep_publish_subscribe.png](http://soi-toolkit.googlecode.com/svn/wiki/NMS/ep_publish_subscribe.png) | **Publish/Subscribe** |

Soitoolkit-nms also provides:
  * **Logger**: A lightweight log-mechanism that can be configured to log to either the console, file or to the Windows Event Log.
  * **Security**: Instructions on how to setup secure communication with ActiveMQ using SSL with mutual authentication.

# Exchange Patterns #

This section gives a quick introduction to the programming model for each exchange pattern with source code examples.

For each pattern a references is given to a fully working code example, typically a unit test.

For more details of the api look into the [api-section](#The_API.md) below.

## Store & Forward ##

### Text Messages ###

To send a text message to a queue you write code like:

```
// Message to be sent
string msg = "Message to be sent...";

// Create a session to the message broker
using (ISession s = SessionFactory.CreateSession(BROKER_URL))
{
    // Create a sender and send a message to a queue
    using (IQueueSender qs = s.CreateQueueSender(QUEUE_NAME))
    {
        qs.SendMessage(msg);
    }
}
```

To receive text messages from a queue you can either pull them using the Receive() -method like:

```
// Create a session to the message broker
using (ISession s = SessionFactory.CreateSession(BROKER_URL))
{
    // List of received messages
    List<string> msgs = new List<string>();

    // Create a receiver for the queue and receives all messages available,
    // wait max MAX_WAITTIME for new messages
    using (IQueueReceiver qr = s.CreateQueueReceiver(QUEUE_NAME))
    {
        ITextMessage msg = null;
        do
        {
            msg = qr.Receive(MAX_WAITTIME);
            if (msg != null) msgs.Add(msg.TextBody);
        }
        while (msg != null);
    }
}
```

**NOTE**: The `Receive()`-method can also be called without specifying any timeout, then the `Receive()`-method will wait for ever until a new message arrives (locking the current thread while waiting).

...or get the messages pushed to you by register a message listener like:

```
// Create a session to the message broker
using (ISession s = SessionFactory.CreateSession(BROKER_URL))
{
    // List of received messages
    List<string> msgs = new List<string>();

    // Create a receiver for the queue
    using (IQueueReceiver qr = s.CreateQueueReceiver(QUEUE_NAME))
    {
        // Setup a message listener for incoming messages that calls
        // a sample ProcessMessage()-method for each message
        qr.OnMessageReceived += Message => ProcessMessage(Message.TextBody);

        // Start the listener
        qr.StartListener();

        // Do other stuff while waiting for messages to be pushed to the message listener.
        ...
    }
}
```

**NOTE**: Messages are delivered in a separate thread, i.e. they can be process independently of what is going on in the main thread

For a fully working code example see [QueueTests.cs](http://code.google.com/p/soi-toolkit/source/browse/soitoolkit-nms/trunk/soitoolkit-nms-tests/QueueTests.cs).

### Bytes Messages ###

To send a byte-array message to a queue you write code like:

```
// Message to be sent
byte[] msg = new byte[] { 1, 2, 3 };;

// Create a session to the message broker
using (ISession s = SessionFactory.CreateSession(BROKER_URL))
{
    // Create a sender and send a message to a queue
    using (IQueueSender qs = s.CreateQueueSender(QUEUE_NAME))
    {
        qs.SendBytesMessage(msg);
    }
}
```

To receive byte-array messages from a queue you can pull them using the Receive() -method like:

```
// Create a session to the message broker
using (ISession s = SessionFactory.CreateSession(BROKER_URL))
{
    // List of received messages
    List<byte[]> msgs = new List<byte[]>();

    // Create a receiver for the queue and receives all messages available,
    // wait max MAX_WAITTIME for new messages
    using (IQueueReceiver qr = s.CreateQueueReceiver(QUEUE_NAME))
    {
        IBytesMessage msg = null;
        do
        {
            msg = qr.ReceiveBytesMessage(MAX_WAITTIME);
            if (msg != null) msgs.Add(msg.BytesBody);
        }
        while (msg != null);
    }
}
```



## Using message headers ##

A number of standard headers can be set as well as custom headers, for example to send a BytesMessage with a number of custom headers:

```
Dictionary<String,String> headers = new Dictionary<string,string>();
headers.Add("key1", "value1");
headers.Add("key2", "value2");

IBytesMessage bytesMsg = s.CreateBytesMessage(byte[] { 1, 2, 3 }, headers);
qs.SendBytesMessage(bytesMsg);
```


To access custom headers from a received message:

```
IBytesMessage msg = qr.ReceiveBytesMessage(SHORT_WAIT_TS);
Dictionary<String,String> headers = msgs[0].CustomHeaders;
```

## Asynchronous Request/Response ##

**TBD**: Will be documented in the v1.1-release of soitoolkit-nms

## Publish/Subscribe ##

**TBD**: Will be documented in the v1.1-release of soitoolkit-nms

# Logging #

Soitoolkit-nms implements a lightweight log-mechanism that can be configured to log to either the console, file or to the Windows Event Log.

By default it log warning messages to the Windows Event Logger. The log-mechanism can be used to better understand what is going on within soitoolkit-nms by increasing the log-level to INFO or DEBUG (specifically in case of problems).

Change the log level with a call like:

```
LogFactory.LogLevel = LogLevelEnum.DEBUG;
```

The log-mechanism can also be used to log self defined application events.

```
private Soitoolkit.Log.Log log = new Soitoolkit.Log.Log();
...
if (log.IsDebugEnabled()) log.Debug("Some application logging...");
```

Configure logging to the console with a call like:

```
LogFactory.LogAdapter = new ConsoleLogAdapter();
```

Configure logging to a file with a call like:

```
LogFactory.LogAdapter = new FileLogAdapter(LOG_FILENAME);
```

Finally configure logging to the Windows Event Log with a call like:

```
LogFactory.LogAdapter = new EventLogAdapter(
    EVENT_LOG_NAME,
    EVENT_LOG_SOURCE,
    MAX_LOG_SIZE,
    OVERFLOW_ACTION,
    RETENTION_DAYS);
```

**NOTE**: The [EventLogAdapter](http://code.google.com/p/soi-toolkit/source/browse/soitoolkit-nms/trunk/soitoolkit-nms/log/impl/EventLogAdapter.cs)-class provides a set of alternate constructors with fewer arguments, i.e. that relies on the default values for the configuration of the Event Log.

For a fully working code example see [EventLogTests.cs](http://code.google.com/p/soi-toolkit/source/browse/soitoolkit-nms/trunk/soitoolkit-nms-tests/EventLogTests.cs).

# Security #

Soitoolkit-nms support secure communication with ActiveMQ using SSL with mutual authentication.
Client certificates can either be installed in a Windows Certificate store or kept on the filesystem.

The ActiveMQ broker is expected to be configured for secure communication using SSL according to the following guidelines: [Configure ActiveMQ using SSL](http://code.google.com/p/soi-toolkit/wiki/InstallationGuideRuntime#Secure_connections_using_SSL_over_TCP)

The soitoolkit-nms client is configured by its broker-url. Using plain TCP a broker-url looks something like: `failover:(tcp://amq-broker:61616)`

To use SSL a number of query-parameters needs to be added and the `tcp:`-protocoll needs to be changed to `ssl:`.

| Query-parameter | Description |
|:----------------|:------------|
| `keyStoreName`  | Windows Certificate Store, e.g. 'StoreName.My.ToString()'. To be used if the client certificate is installed in a Windows Certificate store  |
| `keyStoreLocation` | Windows Certificate Location, e.g. '"CurrentUser"'. To be used if the client certificate is installed in a Windows Certificate store |
| `transport.clientCertFilename` | Points to the client-side certificate in the local filesystem.  To be used if the client certificate is kept on the filesystem |
| `transport.clientCertPassword` | Password for the client-side certificate.  To be used if the client certificate is kept on the filesystem |
| `transport.acceptInvalidBrokerCert` | Set to true to avoid validations of the server cert. Can be useful during debugging a not working SSL setup... |
| `transport.serverName` | If the ActiveMQ-broker use a certificate with a hostname different from the one in the url a connection-exception is raised. If it is ok that the ActiveMQ-broker cert specifies a different hostname this can be specified using this parameter to avoid the exception. |
| `transport.timeout` | This is a parameter on the `failover:`-transport (i.e. not on the `ssl:`-transport). Can be very useful to terminate a connection attempt when a SSL connection is not working. Otherwise the failover-protocoll just keeps on trying until stopped the very hard way... |

Example of a broker-url configured for SSL communication using mutual authentication with the client certificate installed in a Windows Certificate store:
```
failover:(ssl://amq-broker:61618?keyStoreName=" + StoreName.My.ToString() + "&keyStoreLocation=CurrentUser)?transport.timeout=5000
```

Example of a broker-url configured for SSL communication using mutual authentication with the client certificate kept on the filesystem :
```
failover:(ssl://amq-broker:61618?transport.clientCertFilename=C:/my_client_cert1.p12&transport.clientCertPassword=my_client_pwd)?transport.timeout=5000
```

For a fully working code example see [SSLTests.cs](http://code.google.com/p/soi-toolkit/source/browse/soitoolkit-nms/trunk/soitoolkit-nms-tests/SSLTests.cs).

## Debug SSL connections ##

It is, unfortunately, very frequent that initial SSL configurations doesn't work due to configuration mistakes. It can be quite hard to find where the problem is due to its complex and secure nature...

To ease the burden, make it easier to find the problem, the following guidelines can be followed:

  1. Increase the log-level to DEBUG and log to file or the console to make it easier to browse the log-info (it will be very verbose in debug-mode when SSL is turned on).
    1. Read the client-side log carefully, there often exist very valuable information, even though hard to find due to the verbose output...
  1. Enbable SSL-debug logging on the ActiveMQ-broker side as well.
  1. Start using only server-certs, i.e. do not require mutual-auth in the ActiveMQ-configuration of the SSL-connector.
  1. Use the `transport.acceptInvalidBrokerCert=true`, i.e. disable client side validation of server cert, to see if its helps
  1. Use the `transport.serverName` to specify the hostname in the server-cert if the client-side log complaint about invalid server-hostname.

# The API #

**TBD**: Will be documented before the release of soitoolkit-nms v1.0.