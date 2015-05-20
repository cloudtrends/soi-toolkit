# Generate a Service using Maven #

**Content**


# Introduction #

The generator can be launched with the command:

```
mvn soitoolkit-generator:genService -Darg1=value1 ...
```

E.g.:

```
mvn soitoolkit-generator:genService -Dservice=sample1 
```

The generators can also be launched with a longer command-form, see [Using the source code generators](UG_UsingGenerators.md) for differences in usage of the two variants.


# Parameters #

All parameters are optional.

| **Name** | **Type** | **Default Value** | **Description** |
|:---------|:---------|:------------------|:----------------|
| outDir   | File     | . (i.e. your current directory) | Location of the output folder. The folder must be the root folder of a service-project of an integration component. |
| service  | String   | sample            | The name of the service, the suffix "-service" will be added so avoid it in this value.  |
| messageExchangePattern | Enum     | Request/Response  | The Message Exchange Pattern for this service. Allowed values: Request/Response, One-Way |
| inboundTransport | Enum     | SOAPHTTP          | The inbound transport for the service. Allowed values: For Request/Response services: SOAPHTTP, SOAPSERVLET, for One-Way services: VM, JMS, JDBC, FILE, FTP, SFTP, HTTP, SERVLET, POP3, IMAP |
| outboundTransport | Enum     | SOAPHTTP          | The outbound transport for the service. Allowed values: For Request/Response services: SOAPHTTP, RESTHTTP, JMS, for One-Way services: VM, JMS, JDBC, FILE, FTP, SFTP, SMTP |

# Examples #

## Create a request/response SOAP to REST proxy ##
You can add any of the parameters described above to the generator command, e.g. for creating a SOAP to REST proxy in the integration component ic1:

```
cd ic1/trunk/ic1-services/
mvn soitoolkit-generator:genService -Dservice=sample1 -DmessageExchangePattern=Request/Response -DinboundTransport=SOAPHTTP -DoutboundTransport=RESTHTTP
```

It will then create a service in the integration component using the values you have supplied:

```
...
[INFO] ==========================
[INFO] = Creating a new Service =
[INFO] ==========================
[INFO] 
[INFO] ARGUMENTS:
[INFO] (change an arg by suppling: -Darg=value):
[INFO] 
[INFO] outDir=/Users/magnuslarsson/Documents/temp/st-test/ic1/ic1/trunk/ic1-services
[INFO] service=sample1
[INFO] messageExchangePattern=Request/Response
[INFO] inboundTransport=SOAPHTTP
[INFO] outboundTransport=RESTHTTP
[INFO] 
[INFO] EXTRACTED POM-INFO:
[INFO] (from /Users/magnuslarsson/Documents/temp/st-test/ic1/ic1/trunk/ic1-services/pom.xml)
[INFO] artifactId=ic1
[INFO] groupId=org.myorg.ic1
...
[INFO] ------------------------------------------------------------------------
[INFO] BUILD SUCCESSFUL
[INFO] ------------------------------------------------------------------------
...
```

The following files are created in the integration components service-project (ic1-services in this example):

```
`-- src
    |-- main
    |   |-- java
    |   |   `-- org
    |   |       `-- myorg
    |   |           `-- ic1
    |   |               `-- sample1
    |   |                   |-- Sample1RequestTransformer.java
    |   |                   `-- Sample1ResponseTransformer.java
    |   `-- resources
    |       `-- services
    |           `-- sample1-service.xml
    `-- test
        |-- java
        |   `-- org
        |       `-- myorg
        |           `-- ic1
        |               `-- sample1
        |                   |-- Sample1IntegrationTest.java
        |                   |-- Sample1RequestTransformerTest.java
        |                   |-- Sample1ResponseTransformerTest.java
        |                   |-- Sample1TestConsumer.java
        |                   `-- Sample1TestProducer.java
        `-- resources
            |-- testfiles
            |   |-- sample1-request-expected-result.csv
            |   |-- sample1-request-input.xml
            |   |-- sample1-response-expected-result.xml
            |   `-- sample1-response-input.rest
            `-- teststub-services
                `-- sample1-teststub-service.xml
```

The following two files have been updated (appended)  with service specific properties:

```
`-- src
    `-- environment
        |-- ic1-config.properties
        `-- ic1-security.properties
```

You can now test the new service with standard maven commands:

```
mvn test
```

This will result in something like:

```
...
Running org.myorg.ic1.sample1.Sample1IntegrationTest
Tests run: 3, Failures: 0, Errors: 0, Skipped: 0, Time elapsed: 20.896 sec
Running org.myorg.ic1.sample1.Sample1RequestTransformerTest
Tests run: 1, Failures: 0, Errors: 0, Skipped: 0, Time elapsed: 0.032 sec
Running org.myorg.ic1.sample1.Sample1ResponseTransformerTest
Tests run: 1, Failures: 0, Errors: 0, Skipped: 0, Time elapsed: 0.005 sec

Results :

Tests run: 5, Failures: 0, Errors: 0, Skipped: 0

[INFO] ------------------------------------------------------------------------
[INFO] BUILD SUCCESSFUL
[INFO] ------------------------------------------------------------------------
...
```

Or run the junit tests inside your Java IDE, e.g. Eclipse.

The generated source code is now yours and you can update it to fit your needs, e.g. refine the transformers or apply other wsdl's and xml-schemas to the SOAP-part...

...and updating the test-classes to always ensure that you have proper test-coverage!


## Create an one-way file-adapter ##

You can add any of the parameters described above to the generator command, e.g. for creating a file-adapter that picks up files in the local filesystem and place it contents on a JMS queue in the integration component ic1:

```
cd ic1/trunk/ic1-services/
mvn soitoolkit-generator:genService -Dservice=sample2 -DmessageExchangePattern=One-Way -DinboundTransport=FILE -DoutboundTransport=JMS
```

It will then create a service in the integration component using the values you have supplied:

```
...
[INFO] ==========================
[INFO] = Creating a new Service =
[INFO] ==========================
[INFO] 
[INFO] ARGUMENTS:
[INFO] (change an arg by suppling: -Darg=value):
[INFO] 
[INFO] outDir=/Users/magnuslarsson/Documents/temp/st-test/ic1/ic1/trunk/ic1-services
[INFO] service=sample2
[INFO] messageExchangePattern=One-Way
[INFO] inboundTransport=FILE
[INFO] outboundTransport=JMS
[INFO] 
[INFO] EXTRACTED POM-INFO:
[INFO] (from /Users/magnuslarsson/Documents/temp/st-test/ic1/ic1/trunk/ic1-services/pom.xml)
[INFO] artifactId=ic1
[INFO] groupId=org.myorg.ic1
...
[INFO] ------------------------------------------------------------------------
[INFO] BUILD SUCCESSFUL
[INFO] ------------------------------------------------------------------------
...
```

The following files are created in the integration components service-project (ic1-services in this example):

```
`-- src
    |-- main
    |   |-- java
    |   |   `-- org
    |   |       `-- myorg
    |   |           `-- ic1
    |   |               `-- sample2
    |   |                   `-- Sample2Transformer.java
    |   `-- resources
    |       `-- services
    |           `-- sample2-service.xml
    `-- test
        |-- java
        |   `-- org
        |       `-- myorg
        |           `-- ic1
        |               `-- sample2
        |                   |-- Sample2IntegrationTest.java
        |                   |-- Sample2TestReceiver.java
        |                   `-- Sample2TransformerTest.java
        `-- resources
            |-- testfiles
            |   |-- sample2-expected-result.txt
            |   `-- sample2-input.txt
            `-- teststub-services
                `-- sample2-teststub-service.xml
```

The following two files have been updated (appended)  with service specific properties:

```
`-- src
    `-- environment
        |-- ic1-config.properties
        `-- ic1-security.properties
```

You can now test the new service with standard maven commands:

```
mvn test
```

This will result in something like:

```
...
Running org.myorg.ic1.sample2.Sample2IntegrationTest
Tests run: 1, Failures: 0, Errors: 0, Skipped: 0, Time elapsed: 2.627 sec
Running org.myorg.ic1.sample2.Sample2TransformerTest
Tests run: 1, Failures: 0, Errors: 0, Skipped: 0, Time elapsed: 0.007 sec

Results :

Tests run: 2, Failures: 0, Errors: 0, Skipped: 0

[INFO] ------------------------------------------------------------------------
[INFO] BUILD SUCCESSFUL
[INFO] ------------------------------------------------------------------------
...
```

Or run the junit tests inside your Java IDE, e.g. Eclipse.

The generated source code is now yours and you can update it to fit your needs, e.g. refine the transformers or apply other wsdl's and xml-schemas to the SOAP-part...

...and updating the test-classes to always ensure that you have proper test-coverage!