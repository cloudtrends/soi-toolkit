# Generate an Integration Component using Maven #

**Content**


# Introduction #

The generator can be launched with the command:

```
mvn soitoolkit-generator:genIC -Darg1=value1 ...
```

E.g.:

```
mvn soitoolkit-generator:genIC -DartifactId=ic1
```

The generators can also be launched with a longer command-form, see [Using the source code generators](UG_UsingGenerators.md) for differences in usage of the two variants.


# Parameters #

All parameters are optional.

| **Name** | **Type** | **Default Value** | **Description** |
|:---------|:---------|:------------------|:----------------|
| outDir   | File     | . (i.e. your current directory) | Location of the output folder. |
| artifactId | String   | sample1           | The artifactId of the Integration Component.  |
| groupId  | String   | org.sample        | The groupId of the Integration Component. |
| version  | String   | 1.0.0-SNAPSHOT    | The initial version of the Integration Component. |
| muleVersion | Enum     | 3.1.1             | The Mule version to use within the Integration Component. Allowed values: 3.1.1 |
| deployModel | Enum     | Standalone        | The Mule deploy model to use for the Integration Component. Allowed values: Standalone, War |
| connectors | Enum     | JDBC,FTP,SFTP     | Declared connectors to be used by the Integration Component, specified as a comma separated list without any space characters. The JMS connector is required and does not need to be specified. Allowed values: JDBC,FTP,SFTP,SERVLET |
| groovyModel | URL      | not specified     | Groovy model for customizing the generator. Example: [http://.../GroovyModelImpl.groovy](http://soi-toolkit.googlecode.com/svn/trunk/tools/soitoolkit-generator/soitoolkit-generator/src/test/java/org/soitoolkit/tools/generator/model/GroovyModelImpl.groovy)  |


# Example #

You can add any of the parameters described above to the generator command, e.g.:

```
 mvn soitoolkit-generator:genIC -DartifactId=ic1 -DgroupId=org.myorg.ic1 -Dconnectors=FTP,SFTP
```

It will then create an integration component using the values you have supplied:

```
...
[INFO] =========================================
[INFO] = Creating an new Integration Component =
[INFO] =========================================
[INFO] 
[INFO] ARGUMENTS:
[INFO] (change an arg by suppling: -Darg=value):
[INFO] 
[INFO] outDir=/Users/magnuslarsson/Documents/temp/st-test
[INFO] artifactId=ic1
[INFO] groupId=org.myorg.ic1
[INFO] version=1.0.0-SNAPSHOT
[INFO] muleVersion=3.1.1
[INFO] deployModel=Standalone
[INFO] connectors=JMS,FTP,SFTP
[INFO] groovyModel=null
...
[INFO] ------------------------------------------------------------------------
[INFO] BUILD SUCCESSFUL
[INFO] ------------------------------------------------------------------------
...
```

It will result in a folder structure like:

```
ic1
  |-- branches
  |-- tags
  `-- trunk
      |-- ic1-services
      |   |-- pom.xml
      |   `-- src
      |       |-- environment
      |       |   |-- ic1-config.properties
      |       |   |-- ic1-security.properties
      |       |   |-- log4j.dtd
      |       |   `-- log4j.xml
      |       |-- main
      |       |   |-- java
      |       |   |   `-- org
      |       |   |       `-- myorg
      |       |   |           `-- ic1
      |       |   `-- resources
      |       |       |-- ic1-common.xml
      |       |       |-- ic1-config.xml
      |       |       `-- services
      |       `-- test
      |           |-- java
      |           |   `-- org
      |           |       `-- myorg
      |           |           `-- ic1
      |           |               `-- Ic1MuleServer.java
      |           `-- resources
      |               |-- ic1-teststubs-and-services-config.xml
      |               |-- ic1-teststubs-only-config.xml
      |               |-- testfiles
      |               `-- teststub-services
      |-- ic1-standalone
      |   |-- pom.xml
      |   `-- src
      |       `-- main
      |           `-- app
      |               `-- mule-config.xml
      |-- ic1-teststub-standalone
      |   |-- pom.xml
      |   `-- src
      |       `-- main
      |           `-- app
      |               `-- mule-config.xml
      `-- pom.xml
```


You can build the integration component with standard maven commands:

```
cd ic1/trunk
mvn install
```

Or import the maven projects under the `trunk`-folder in your Java IDE (e.g. using the Maven-plugin for Eclipse).

**NOTE:** The first time you run a maven command on a component created by soi-toolkit a lot of dependencies are download, please be patient :-)