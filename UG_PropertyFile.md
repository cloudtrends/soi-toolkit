# Property file configuration and usage #

_**Since: 0.5.0**_

**Content**



## Default values ##
Property values in the file:
```
${artifactId}-config.properties
```
should always be configured with values that make integration tests portable between users/hosts/operating systems. In other words: user specific credentials or absolute file paths should not be used.

**Example:** use a relative file path to keep integration test portable:
```
SFTPTOSFTP1_ARCHIVE_FOLDER=target/soitoolkit/archive/sftptosftp1
```

**Note:** credentials (like for FTP or SFTP) are generated with default values that works with the embedded resource managers to make integration tests portable.


## Override default values ##
Property values that are specific for test/production environments are configured by following these steps:

  1. Override property values with an override-file
> > Override values in the file:
```
    ${artifactId}-config.properties
```
> > with values in a file named (you have to create this file):
```
    ${artifactId}-config-override.properties
```
> > and place the override file on the classpath.
> > <br><b>Note:</b> The override file should not be added to source-control as part of the classpath for an integration-component since the override file would then be used integration tests are run - and probably causing the tests to fail.<br>
</li></ul><ol><li>Placing the override-file on the classpath<br>
<blockquote>For a standalone Mule-installation, put the override-file in the $MULE_HOME/conf directory<br>
</blockquote></li><li>Adding encrypted property values<br>
<blockquote>The override-file is typically where encrypted values (see next section) are used.<br>
<br><b>Note:</b> Encrypted values should be avoided in the default property values since decryption requires some environment setup for integration tests to run.</blockquote></li></ol>

For some more background info see: [MuleSoft on using parameters in your configuration files](http://www.mulesoft.org/documentation/display/MULE3USER/Using+Parameters+in+Your+Configuration+Files)

## Encrypted passwords ##
Passwords in the property file can be encrypted using [jasypt](http://www.jasypt.org).

**High level description for using encrypted passwords**
  1. Select a _master password_ to use for encryption/decryption of passwords in the property file
  1. Encrypt passwords for the property file using the _master password_ and the jasypt command line tool
  1. Write the encrypted passwords in the property file
  1. Configure the Mule-server environment with the _master password_ (to allow for runtime decryption of passwords from the property file)

**Detailed description for using encrypted passwords**
  1. Select _master password_, example: MY\_SECRET\_MASTER\_PASSWORD
  1. Encrypt a password, using one of the alternatives:
    1. Using the encryption webapp (added for [issue 329](https://code.google.com/p/soi-toolkit/issues/detail?id=329)), see details in:
> > > https://code.google.com/p/soi-toolkit/source/browse/trunk/tools/soitoolkit-encryption-web-tool/README.txt
> > > <br><br><i>Note: this should be the prefered option since it minimizes the exposure of the master-password.</i>
</li></ul>    1. Using command-line tool
      1. Download jasypt command line tools http://www.jasypt.org/download.html
      1. Unzip the jasypt-distribution
      1. Encrypt a password:
```
bin $ cd /opt/sf/jasypt-1.8/bin
bin $ ./encrypt.sh input="PASSWORD_TO_ENCRYPT_FOR_PROPERTY_FILE" password=MY_SECRET_MASTER_PASSWORD

----ENVIRONMENT-----------------

Runtime: Apple Inc. Java HotSpot(TM) 64-Bit Server VM 20.4-b02-402 

----ARGUMENTS-------------------

input: PASSWORD_TO_ENCRYPT_FOR_PROPERTY_FILE
password: MY_SECRET_MASTER_PASSWORD

----OUTPUT----------------------

Sc2JJ0XmftcDg2472aXVrVtj6pq23eevHFRplV/WYgynlUhLRpF8n9yiH+6q70gn

```
  1. Write the encrypted password (i.e. the value after OUTPUT in the previous step) in the property file. Note the syntax with: ENC(encrypted-password)
```
MY_DATABASE_PASSWORD=ENC(Sc2JJ0XmftcDg2472aXVrVtj6pq23eevHFRplV/WYgynlUhLRpF8n9yiH+6q70gn)
```
  1. Configure the Mule-server environment with the _master password_:
    1. For Mule standalone, edit $MULE\_HOME/conf/wrapper.conf, add (as the second line after "#encoding=UTF-8"):
```
      set.SOITOOLKIT_ENCRYPTION_PASSWORD=MY_SECRET_MASTER_PASSWORD
```
> > > Ref: http://wrapper.tanukisoftware.com/doc/english/props-envvars.html
    1. Set permissions on the wrapper-file:
```
      chmod 400 $MULE_HOME/conf/wrapper.conf
```

**Note:** encrypted passwords should only be used in the ${artifactId}-config-override.properties file to avoid exposing the _master password_. Anyway, to test usage of encrypted passwords when running Mule from Eclipse or Maven you can:
  1. For running tests in eclipse, open "Run configurations" for the junit-testsuite and add environment variable SOITOOLKIT\_ENCRYPTION\_PASSWORD=MY\_SECRET\_MASTER\_PASSWORD
  1. For running tests with maven: export SOITOOLKIT\_ENCRYPTION\_PASSWORD=MY\_SECRET\_MASTER\_PASSWORD and then run your desired Maven-goal. Use "set" instead of "export" on Windows.