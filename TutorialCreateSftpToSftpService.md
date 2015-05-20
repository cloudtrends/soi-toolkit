<font color='red' size='5'><b>NOTE:</b> This page is partially outdated.<br />Work is ongoing to update this page!</font>

# Tutorial: Create a one way Service based on the SFTP transport #

**Content**


# Introduction #

**Prerequisites for this tutorial are:**
  * The tutorial [Create a new Integration Component](TutorialCreateIntegrationComponent.md) is completed (and that the SFTP transport was selected)
  * The installation guide regarding [setup of public key cryptography for the SFTP transport](InstallationGuide#Setup_of_public_key_cryptography_for_the_SFTP_transport.md) is completed

This tutorial will help you to get started with a one-way service that consumes a file over SFTP, transform it and produce an outgoing file again using SFTP.

This tutorial is a variant of the generic tutorial of [creating a One-Way service](TutorialCreateOneWayService.md) focusing on file based transports.

# Create a SFTP to SFTP service #

The generated code includes error handling that:
  * Verifying that the file is stable, no one is writing to it
  * Renaming the file before consuming it to avoid that two threads start to consume one and the same file
  * Saving the file to a local archive before processing its content, e.g transforming it.
  * Sending files to a separate name avoiding that the receiving application starts to consume the file before it is completely sent.
  * Duplication detection, either overwrite, add seq-no or throw an exception
  * Manual routine for restart sending the file, either from original sender or from Mule.
  * Logging of central SFTP events, such as get, put, rename, delete and any errors that might occur

![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateSftpToSftpService/TutorialCreateSftpToSftpService0.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateSftpToSftpService/TutorialCreateSftpToSftpService0.png)

  * Select the menu "File --> New --> Other" and expand the wizard "SOI Toolkit Code Generator"
  * Select the code generator "Create a new service"

![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateSftpToSftpService/TutorialCreateSftpToSftpService1.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateSftpToSftpService/TutorialCreateSftpToSftpService1.png)

  * Click on the "Next >" button
  * The wizard "SOI Toolkit - Create a new service" opens up
    * Select the message exchange pattern "One Way"
    * Select the inbound transport "SFTP"
    * Select the outbound transport "SFTP"
    * Select your service project using the "Browse..." button, e.g. "/mySample-services"
    * Set a proper service-name, e.g. mySampleSftpService
> > NOTE: In real usage, avoid using "`service`" in the name of the service since the actual Mule service will be suffixed with "`-service`", see the file list screenshot below,  `mySampleSftpService-service.xml`

![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateSftpToSftpService/TutorialCreateSftpToSftpService2.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateSftpToSftpService/TutorialCreateSftpToSftpService2.png)

  * Click on the "Finish" button
    * The wizard now starts to generate files and refresh the workspace.

  * When the wizard is done you can find the files for your new service in the Eclipse Package Explorer


> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateSftpToSftpService/TutorialCreateSftpToSftpService3.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateSftpToSftpService/TutorialCreateSftpToSftpService3.png)

  * Files of interest:
    * Source folder `src/test/java`
> > A new package is created for the service `org.sample.mysample.mysamplesftpservice` with two Java-classes.
> > `MySampleSftpServiceIntegrationTest.java` contains some unit tests for the service that you can use as a start.
> > `MySampleSftpServiceTestReceiver.java` contains a teststub receiver that you can use as a start.
    * Source folder `src/test/resources`
> > The folder `teststub-services` contains the file `MySampleSftpService-teststub-service.xml` that is a teststub service that you can use as a start.
    * Source folder `src/main/java`
> > A new package is created for the service `org.sample.mysample.mysamplesftpservice` with one Java-class.
> > `MySampleSftpServiceTransformer.java` contains a sample transformation that you can use as a start.
    * Source folder `src/main/resources`
> > The folder `services` contains the file `MySampleSftpService-service.xml` that contains the actual definition of the new service.
    * Source folder `src/environment`
> > The configuration file `mySample-config.properties` has got the following properties added:
```
# Properties for sftp-service "mySampleSftpService"
# TODO: Update to reflect your settings
MYSAMPLESFTPSERVICE_SENDER_SFTP_ADDRESS=muletest1@localhost/~/sftp/mysamplesftpservice/sender
MYSAMPLESFTPSERVICE_SENDER_POLLING_MS=1000
MYSAMPLESFTPSERVICE_SENDER_SIZECHECK_MS=500
MYSAMPLESFTPSERVICE_RECEIVER_SFTP_ADDRESS=muletest1@localhost/~/sftp/mysamplesftpservice/receiver
MYSAMPLESFTPSERVICE_ARCHIVE_FOLDER=/Users/magnuslarsson/archive
MYSAMPLESFTPSERVICE_ARCHIVE_RESTART_POLLING_MS=1000
```
      * **NOTE:** Under Windows the archive folder path must be configured with forward-slashes since this is a URL used with the "file:"-protocol and not a native file-location (e.g. used by the java.io.File - class). Example:
```
MYSAMPLESFTPSERVICE_ARCHIVE_FOLDER=C:/tmp/soitksftp/archive/mysamplesftpservice
```

  * The configuration file `mySample-security.properties` contains since the creation fo the integration component the properties:
```
# Properties for the default sftp-connector
# TODO: Update to reflect your settings
SOITOOLKIT_SFTP_IDENTITYFILE=/Users/magnuslarsson/.ssh/id_dsa
SOITOOLKIT_SFTP_IDENTITYFILE_PASSPHRASE=yourPassphrase
```

  * Run unit tests
    * Right-click the test-class `MySampleJmsServiceIntegrationTest.java` and select "Run As" --> "JUnit Test"
> > The tests runs and its success are reported in the JUnit view:


> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateSftpToSftpService/TutorialCreateSftpToSftpService4.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateSftpToSftpService/TutorialCreateSftpToSftpService4.png)

> The Console view displays at the same time log-messages from the execution:

> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateSftpToSftpService/TutorialCreateSftpToSftpService5.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateSftpToSftpService/TutorialCreateSftpToSftpService5.png)

> NOTE: Take a look at the unit test code and the transformer code for a better understanding on what is going on :-)

  * Run tests manually
> Sometimes just running unit tests are not sufficient so it is a good idea to know how to perform manual tests locally in your own PC.
    * Right-click on the test-server `MySampleMuleServer.java` in package `org/sample/mysample` in source folder `src/test/java` and select "Run As --> Java Application"
    * Copy a file with ok content (e.g. "AAA") to the `sender` folder and watch the console and the `archive` folder.
> > NOTE: The `receiver`-folder will never contain any files for a long period since the teststub-service picks them up from there :-)
    * Copy a file with incorrect content (e.g. "ERROR") to the `sender` folder and watch the console and the `archive/invalid` folder.
> > NOTE: The `receiver`-folder will never contain any file since the teststub-service picks it up from there :-)


> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateSftpToSftpService/TutorialCreateSftpToSftpService6.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateSftpToSftpService/TutorialCreateSftpToSftpService6.png)

> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateSftpToSftpService/TutorialCreateSftpToSftpService7.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateSftpToSftpService/TutorialCreateSftpToSftpService7.png)