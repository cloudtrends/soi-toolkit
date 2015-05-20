**Content**



# Initial setup of development environment #

As a base setup the development environment according to the [installation guide](InstallationGuide.md) for soi-toolkit users.

## Setup of source code analysis eclipse plugins ##

It is recommended that developers of soi-toolkit have corresponding eclipse plugins to the source code analysis that are performed by the continuos integration server, Jenkins.

  * Install **PMD** from the Eclipse update site  http://pmd.sourceforge.net/eclipse
  * Install **findbugs** from the Eclipse update site http://findbugs.cs.umd.edu/eclipse
  * Install **checkstyle** from the Eclipse update site http://eclipse-cs.sourceforge.net/update
  * Install **eclemma** from the Eclipse update site http://update.eclemma.org

## Setup for Mule ##

TBS: Ways to get Mule 2.2.x > 2.2.1 in your local maven repo...

## Setup for generator-plugin ##

  * on mac for 32bit Eclipse specify -d32 om launch command...

  * jvm minimum paramenters on launch command

  * specify your environment in the propertyfile, `src/soi_toolkit_generator_plugin_default_preferences.properties`

  * how to create pki keys for sftp

## Setup for Sonatype OSS Maven Repository ##

The soi-toolkit uses Sonatype OSS Maven Repository to release artifacts to the Maven central repository, see https://docs.sonatype.org/display/repository/sonatype+oss+maven+repository+usage+guide for details.

Added to the setup of the development environment required for a user of soi-toolkit a developer must perform the following setup steps to be able to perform releases of soi-toolkit:

  1. Create an JIRA account at Sonatype, https://issues.sonatype.org/
```
firefox https://issues.sonatype.org/
```
  1. Add the following servers to the maven settings.xml - file:
```
  <servers>
    <server>
      <id>sonatype-nexus-snapshots</id>
      <username>your-jira-id</username>
      <password>your-jira-pwd</password>
    </server>
    <server>
      <id>sonatype-nexus-staging</id>
      <username>your-jira-id</username>
      <password>your-jira-pwd</password>
    </server>
  </servers>
```
  1. Add the following profile (used during verification of a release) to the maven settings.xml - file:
```
    <profile>
      <id>soi-toolkit-sonatype</id>

      <repositories> 
        <repository> 
          <id>sonatype-nexus-staging</id> 
          <name>Sonatype Nexus Staging</name> 
          <url>https://oss.sonatype.org/content/repositories/staging</url> 
          <releases> 
            <enabled>true</enabled> 
          </releases> 
          <snapshots> 
            <enabled>false</enabled> 
          </snapshots> 
        </repository> 
      </repositories>

      <pluginRepositories>
        <pluginRepository>
          <id>sonatype-nexus-staging-plugin-repository</id> 
          <name>Sonatype Nexus Staging plugin repository</name>
          <url>https://oss.sonatype.org/content/repositories/staging/</url>
          <releases> 
            <enabled>true</enabled> 
          </releases> 
          <snapshots> 
            <enabled>false</enabled> 
          </snapshots> 
        </pluginRepository>
      </pluginRepositories>

    </profile>
```
  1. Install pgp, create key-pair and distribute public key.
> See http://www.sonatype.com/people/2010/01/how-to-generate-pgp-signatures-with-maven/ for details.
    * Install pgp
> > If you are a Mac user and have macports installed (you really should!) then simply install pgp with the command:
```
sudo port install gnupg
```
    * Create key-pair
```
gpg --gen-key
```
    * List keys
```
gpg --list-keys
gpg --list-secret-keys
```
> > Keys will be listed in the following format where the key id, `nnnnnnnn`, is the most important
```
pub   mmmmm/nnnnnnnn date
uid                  name <email>
sub   mmmmm/ssssssss date
```

  * Distribute public key
```
gpg --keyserver hkp://pgp.mit.edu --send-keys nnnnnnnn
```

  * Define the default key

> If you have more than one private key you can define a default key by editing the configuration file:
```
mate ~/.gnupg/gpg.conf
```
> And set the `default-key` option to your preferred default key:
```
# If you have more than 1 secret key in your keyring, you may want to
# uncomment the following option and set your preferred keyid.
default-key nnnnnnnn
```