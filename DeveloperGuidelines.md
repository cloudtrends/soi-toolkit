# Developer Guidelines #

**Content**



# Introduction #

The soi-toolkit project is an open source project, read here for [How It Works](DG_HowItWorks.md).

Committers needs to sign an [Individual Contributor License Agreement (ICLA)](http://soi-toolkit.googlecode.com/svn/licenses/soi-toolkit-icla.txt).

# Guidelines for developers of the soi-toolkit #

<a href='Hidden comment: 
soi-toolkit is based on the following components:
|| *Name* || *Svn-path* || *Description* ||
|| commons-schemas || trunk/commons/components/commons-schemas || TBS ||
|| commons-log || trunk/commons/components/commons-log || TBS ||
|| commons-mule || trunk/commons/components/commons-mule || TBS ||
|| default-parent || trunk/commons/poms/default-parent || TBS ||
|| assembly-descriptors || trunk/commons/poms/assembly-descriptors || TBS ||
|| soitoolkit-generator-plugin || trunk/tools/soitoolkit-generator || TBS ||

All components in soi-toolkit has one and the same release cycle (i.e. cm scope) and are released together as described below.
'></a>

## Initial setup of development environment ##

See the wiki page [Initial Setup](DG_InitialSetup.md).

To release software components we use Maven and its [release-plugin](http://maven.apache.org/maven-release/maven-release-plugin/). To be able to publish released artifacts on [the central Maven repository](http://repo1.maven.org/maven2/) we use [Sonatype OSS Repository Hosting Service](http://www.sonatype.org/central/participate).

See [instructions](https://docs.sonatype.org/display/repository/sonatype+oss+maven+repository+usage+guide) for more information on what is required for initial setup.

After a successful registration of an account at Sonatype send the user-id to one of the owners of the soi-toolkit project to get the account associated with the soi-toolkit project at Sonatype, e.g. so that you can perform releases of soi-toolkit components to the central Maven repository.

## Running tests ##

### Maven based tests ###

```
cd .../trunk
mvn clean install
```

**Note:** `mvn clean test` is actually sufficient but since the generator tests (see below) need the latest binaries in the local maven repository to run successfully `mvn clean install` is required anyhow.

### Generator tests ###

The generator is for now both tested and built manually using the built in support in Eclipse for building Eclipse-plugins.

Use Eclipse to run all tests in the project `soitoolkit-generator-plugin` or perform:

```
cd tools/soitoolkit-generator/soitoolkit-generator
mvn test -PrunGeneratorTests
```

## Running the code generator as a eclipse wizard from source ##
If you want to try out the code generator based on the source code in trunk you can do the following:

  * Ensure you have the latest code base in trunk

```
cd .../trunk
svn up
```

  * Build and install the lastes runtime components of soi-toolkit in your local maven repository based on the code in trunk

```
mvn clean install
```

  * Manually copy the newly build jar-files soitoolkit-commons-xml-0.5.0-SNAPSHOT.jar and soitoolkit-generator-0.5.0-SNAPSHOT.jar from their target folders to the lib-folder /soitoolkit-generator-plugin/lib.

  * Start the soi-toolkit generator plugin in a separate Eclipse runtime workspace

> In eclipse ensure that you have imported the soi-toolkit eclipse-plugin project, soi-toolkit/tools/soitoolkit-generator/soitoolkit-generator-plugin
    * Rightclick the project soitoolkit-generator-plugin and select "Run As --> Eclipse Application"

  * Launch the soi-toolkit generator wizard in the new Eclipse workspace and proceed as normal...

## Testing snapshot versions of soi-toolkit ##

See the wiki page [Testing snapshot versions of soi-toolkit](DG_TestingSnapshotVersions.md).

## Continuous Integration ##
The soi-toolkit project uses Jenkins as its continuous integration server.
Builds are performed after every commit to subversion and snapshots are published on a daily basis.
Jenkins runs as a cloud service, see [Jenkins@CloudBees](https://soi-toolkit.ci.cloudbees.com/).

## Creating a branch in svn ##

Not very frequently required but this is how it can be done :-)

Based on that you have the whole svn-tree checked out (trunk, branches, tags and so on)

**NOTE:** An alternate and maybe better way is to perform the svn copy command directly towards the svn-repo but this is how its done for now

  * Go to the svn root in your local file system

```
cd .../your-soi-toolkit-svn-root
```

  * Ensure that the state in trunk is as expected

```
svn st -u trunk
```

  * Copy trunk to a new branch locally

```
svn copy trunk branches/soitoolkit-mule-2.2.x
```

  * Verify the state in the new branch

```
svn st -u branches/soitoolkit-mule-2.2.x
```

  * Revert any non committed changes on trunk that was copied to the branch
> For example you might have non-committed changes in the file soi\_toolkit\_generator\_plugin\_default\_preferences.properties that you don't want to be copied to the new branch.

```
svn revert branches/soitoolkit-mule-2.2.x/tools/soitoolkit-generator/soitoolkit-generator-plugin/src/soi_toolkit_generator_plugin_default_preferences.properties
```

  * Commit the branch.

```
svn ci branches/soitoolkit-mule-2.2.x -m "Branch for mule-2.2.x fixes"
```


## Publish snapshots ##

To manually publish a snapshot of soi-toolkit do:
```
mvn clean deploy
mvn clean deploy -f commons/poms/mule-dependencies/mule-2.2.5-dependencies/pom.xml
mvn clean deploy -f commons/poms/mule-dependencies/mule-2.2.7-dependencies/pom.xml
mvn clean deploy -f commons/poms/mule-dependencies/mule-3.0.0-dependencies/pom.xml
```

Snapshot artifacts are deployed to the repository https://oss.sonatype.org/content/repositories/snapshots/org/soitoolkit


## Publish snapshots of the Eclipse plugin ##

  * Build soi-toolkit with mvn install from trunk
  * Copy jar's for soitoolkit-generator and soitoolkit-commons-xml to generator-plugin lib-folder
  * Edit the following files:
    * `trunk/tools/soitoolkit-generator/soitoolkit-generator-plugin/META-INF/MANIFEST.MF`
```
Bundle-Version: 0.5.0 --> 0.5.1.SNAPSHOT_2012-05-20_0830
```
    * `trunk/tools/soitoolkit-generator/org.soitoolkit.generator.update/site.xml`
```
/site/feature/@version: 0.5.0 --> 0.5.1.SNAPSHOT_2012-05-20_0830
```
    * `trunk/tools/soitoolkit-generator/org.soitoolkit.generator.update/site.xml`
```
/site/feature/@url: features/org.soitoolkit.generator.feature_0.5.0.jar --> features/org.soitoolkit.generator.feature_0.5.1.SNAPSHOT_2012-05-20_0830.jar
```
    * `trunk/tools/soitoolkit-generator/org.soitoolkit.generator.feature/feature.xml`
```
/feature/@version: 0.5.0 --> 0.5.1.SNAPSHOT_2012-05-20_0830
```

  * Build the update site in Eclipse (open site.xml and click "Build all")
  * Create a zip-fil of the update site (clean svn zip, or just compress the org.soitoolkit.generator.update projekt)
  * Rename the zip-file to something like soitoolkit\_0.5.1.SNAPSHOT\_2012-05-20\_0830.zip
  * Push the zip-file to http://soi-toolkit.merikan.com/myUpdateSite
  * Update the index.html file on http://soi-toolkit.merikan.com/myUpdateSite

## Release handling ##

See the wiki page [Release Handling](DG_ReleaseHandling.md).

## Naming Conventions ##

Version numbers:
  * `n.n.n` for releases
  * `soitoolkit-n.n.n` for svn tags of releases
  * `n.n.n-SNAPSHOT` for unreleased snapshots used during development