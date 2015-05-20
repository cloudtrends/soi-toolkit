# Releasing An Integration Component #

**Content**



# Introduction #

When development and local testing is finished for a new version of an Integration Component its time to release it, i.e. tag the source code with a version-label, build the deployable artifacts an publish them for deployment in system test, qa test and finally production environments.

To ensure that the release process is performed in a consistent and repeatable way we use [Mavens release plugin](http://maven.apache.org/plugins/maven-release-plugin/) to do the job for us. An integration components maven pom-files, created by soi-toolkit generators, have been carefully designed to work with the release plugin (there are many ways of setting up pom-files so that they don't work with the release plugin...)

The release is done in two steps:
  1. First we ensure that the source code in trunk is ready to be deployed
  1. Next we use the release plugin to perform the release

# Verify that the source code in trunk is ready to be released #

  * Go to the integration components trunk-folder
```
cd .../trunk
```

  * Verify that the maven pom.xml files contains an scm element like.
```
<scm>
  <connection>scm:svn://${svn-host}/${svn-path}/${artifactId}/trunk</connection>
  <developerConnection>scm:svn:svn://${svn-host}/${svn-path}/${artifactId}/trunk</developerConnection>
  <url>https://${svn-host}/${svn-path}/${artifactId}/trunk</url>
</scm>
```


  * Check that the code is in synch
> check for updates:
```
svn st -u
```
> and if required:
```
svn update
```

  * Verify that no SNAPSHOT-dependencies remains in the maven pom-files, replace with dependencies to stable versions if any.

  * Run tests
```
mvn clean test
```

  * Commit changes, if any
```
svn commit -m "Commit for releasing vn.n.n"
```

# Perform the release #

It is recommended to use the naming conventions defined below for version numbers specified during the release.

  * Perform a dryrun to verify that everything is ok, i.e. a release build will be successful and not fail in the middle leaving the release build in an inconsistent state. **Note:** Version numbers shall follow the format defined below.
```
mvn release:clean release:prepare -DdryRun=true
```

  * Fix any problems identified by the dryrun then perform a prepare-step to create the tag in svn
```
mvn release:clean release:prepare
```

  * Deploy to maven repository
```
mvn release:perform
```

# Naming Conventions #

The following naming conventions is recommended for version numbers:
  * `n.n.n` for releases
  * `${artifactId}-n.n.n` for svn tags of releases
  * `n.n.n-SNAPSHOT` for unreleased snapshots used during development