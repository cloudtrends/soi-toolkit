# Using the source code generators #

**Content**


# Introduction #

Soi-toolkits source code generators can be started either from the command prompt as Maven Goals or within Eclipse as a Wizard.

For information on how to use the _**Eclipse Wizard**_ see [Getting Started Tutorials](Tutorials#Getting_started_tutorials.md)

For information on how to use the _**Maven Goals**_ from the command prompt, read on :-)

## Start the generators as Maven Goals ##

### Full Documentation ###

  * [Generate a Service Description Component](UG_UsingGenerator_genSDC.md)
  * [Generate an Integration Component](UG_UsingGenerator_genIC.md)
  * [Generate a Service](UG_UsingGenerator_genService.md)
  * [Generate a Teststub Integration Component](UG_UsingGenerator_genICTS.md)

### Setup ###
To be able to run the generators from the command prompt you only need Java SE and Maven to be installed (as describen in the [Installation Guide](InstallationGuide.md)).

The generators can be launched with the command:

```
mvn org.soitoolkit.tools.generator:soitoolkit-generator-maven-plugin:${version}:${generator-goal} -Darg1=value1 ...
```

E.g.:

```
mvn org.soitoolkit.tools.generator:soitoolkit-generator-maven-plugin:0.4.1:genIC -DoutDir=ic1
```

The generators can also be launched with a shorter command:

```
mvn soitoolkit-generator:${generator-goal}
```

E.g.:

```
mvn soitoolkit-generator:genIC -DoutDir=ic1
```

The shorter command-form requires that you have specified the following Plugin Group in your Maven settings.xml-file:

```
<pluginGroups>
  <pluginGroup>org.soitoolkit.tools.generator</pluginGroup>
</pluginGroups>
```