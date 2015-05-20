**Content**



# Testing snapshot versions of soi-toolkit #

To be able to test a new version of soi-toolkit before it is finally released snapshot versions are built from trunk and are published as:

  * **Update Site for the Eclipse plugin**
> > is published at http://soi-toolkit.merikan.com/myUpdateSite/ at zip-files. Download, unzip and install from local update site in Eclipse or Mule Studio.
  * **Maven plugin for the generators**
> > is deployed  to the maven snapshot repository: http://repository-soi-toolkit.forge.cloudbees.com/snapshot
  * **Runtime components**
> > are deployed to the maven snapshot repository: http://repository-soi-toolkit.forge.cloudbees.com/snapshot

Do the following to test a snapshot release of soi-toolkit:

## Maven setup ##

  * To be able to download snapshot versions of the maven-plugin and the runtime components the following profile has to be defined in the Maven settings.xml file:

```
    <profile>
      <id>soi-toolkit-snapshot-profile</id>
      <repositories> 
        <repository> 
          <id>soi-toolkit-snapshot-repository</id> 
          <name>soi-toolkit snapshot repository</name>
          <url>http://repository-soi-toolkit.forge.cloudbees.com/snapshot/</url>
          <releases> 
            <enabled>false</enabled> 
          </releases> 
          <snapshots> 
            <enabled>true</enabled> 
          </snapshots> 
        </repository> 
      </repositories>

      <pluginRepositories>
        <pluginRepository>
          <id>soi-toolkit-snapshot-plugin-repository</id> 
          <name>soi-toolkit snapshot plugin repository</name>
          <url>http://repository-soi-toolkit.forge.cloudbees.com/snapshot/</url>
          <releases> 
            <enabled>false</enabled> 
          </releases> 
          <snapshots> 
            <enabled>true</enabled> 
          </snapshots> 
        </pluginRepository>
      </pluginRepositories>
    </profile>
```

When testing snapshot versions of soi-toolkit the profile also needs to be activated by specifying the following in the Maven settings-xml file:

```
  <activeProfiles>
    <activeProfile>soi-toolkit-snapshot-profile</activeProfile>
  </activeProfiles>
```

**Note #1:** If you are using Maven repository mirrors please assure that they don't prevent accees to soi-toolkits snapshot-repository.

**Note #2:** Don't forget to deactivate this profile when going back to using released versions of soi-toolkit!

## Generate code using soi-toolkit Eclipse plugin ##

  * Install the snapshot version of the Eclipse plugin from the snapshot update site specified above in either Eclipse or Mule Studio.

  * Generate code using the soi-toolkit Eclipse plugin as [normal](Tutorials#Getting_started_tutorials.md).

## Generate code using soi-toolkit Maven plugin ##

  * When using the [Maven plugin](UG_UsingGenerators.md) for generating source code you have to use the longer format of the command to be able to use a SNAPSHOT-version, i.e.
```
mvn org.soitoolkit.tools.generator:soitoolkit-generator-maven-plugin:0.5.1-SNAPSHOT:genIC -Darg=value ...
```

> or:
```
mvn org.soitoolkit.tools.generator:soitoolkit-generator-maven-plugin:0.5.1-SNAPSHOT:genService -Darg=value ...
```

> for creating source code for a service.

**Note #3:** the generated code will have a dependency to the corresponding snapshot versions of the soi-toolkit runtime components!

## Test existing code against the snapshot version of soi-toolkit ##

  * Update the soi-toolkit-dependency to the snapshot-release:
> (e.g. change 0.5.0 to 0.5.1-SNAPSHOT)
```
<parent>
	<groupId>org.soitoolkit.commons.poms</groupId>
	<artifactId>soitoolkit-default-parent</artifactId>
	<version>0.5.1-SNAPSHOT</version>
</parent>
```

  * If the new version of ski-toolkit supports a newer version of Mule also update the soitoolkit-mule-n.n.n-dependencies to reflect that:
```
<dependency>
	<groupId>org.soitoolkit.commons.poms.mule-dependencies</groupId>
	<artifactId>soitoolkit-mule-3.3.0-dependencies</artifactId>
	<version>${soitoolkit.version}</version>
	<type>pom</type>
</dependency>
```
**Note #4:** Remember to revert these changes once you turn back to work with a released version of soi-toolkit!