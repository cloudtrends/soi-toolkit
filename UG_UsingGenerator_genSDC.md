# Generate an Service Description Component using Maven #

**Content**


# Introduction #

The generator can be launched with the command:

```
mvn soitoolkit-generator:genSDC -Darg1=value1 ...
```

E.g.:

```
mvn soitoolkit-generator:genSDC -DartifactId=ic1
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
| schema   | String   | sample1           | The name of the default schema-file and prefix for the wsdl-file.  |

# Example #

You can add any of the parameters described above to the generator command, e.g.:

```
 mvn soitoolkit-generator:genSDC -DartifactId=sdc1 -DgroupId=org.myorg.ic1 -Dschema=mySchema
```

It will then create an service description component using the values you have supplied:

```
...
[INFO] [soitoolkit-generator:genSDC {execution: default-cli}]
[INFO] 
[INFO] ================================================
[INFO] = Creating a new Service Description Component =
[INFO] ================================================
[INFO] 
[INFO] ARGUMENTS:
[INFO] (change an arg by suppling: -Darg=value):
[INFO] 
[INFO] outDir=/Users/magnuslarsson/Documents/temp/st-test
[INFO] artifactId=sdc1
[INFO] groupId=org.myorg.ic1
[INFO] version=1.0.0-SNAPSHOT
[INFO] schema=mySchema
...
[INFO] ------------------------------------------------------------------------
[INFO] BUILD SUCCESSFUL
[INFO] ------------------------------------------------------------------------
...
```

It will result in a folder structure like:

```
sdc1-schemas/
|-- branches
|-- tags
`-- trunk
    |-- pom.xml
    `-- src
        `-- main
            `-- resources
                `-- schemas
                    `-- org
                        `-- myorg
                            `-- ic1
                                `-- sdc1
                                    |-- mySchema.xsd
                                    `-- mySchemaService.wsdl
```


You can build the service description component with standard maven commands:

```
cd sdc1-schemas/trunk
mvn install
```

Or import the maven projects under the `trunk`-folder in your Java IDE (e.g. using the Maven-plugin for Eclipse).

**NOTE:** The first time you run a maven command on a component created by soi-toolkit a lot of dependencies are download, please be patient :-)

**NOTE**: If you import a service description project into Eclipse using Mavens Eclipse-plugin, the plugin will fail on detecting the generated source folder under the target folder as a source-folder. As a workaround for this bug you can right-click the project and select "`Maven --> Update Project Configuration`".