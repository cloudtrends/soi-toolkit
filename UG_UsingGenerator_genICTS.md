# Generate an Teststub Integration Component using Maven #

**Content**


# Introduction #
Read more on [Teststub components](Architecture#Test_Driven_Development.md).


The generator can be launched with the command:

```
mvn soitoolkit-generator:genICTS -DartifactId=myic -DgroupId=org.myorg.myic
```

where:
  * artifactId  is the artifactId of the integration component (IC) to generate a teststub for, the generated teststub-component will get an artifactId: ${artifactId}-teststub
  * groupId is the groupId of the IC to generate a teststub for, the generated teststub-component will get the same groupId