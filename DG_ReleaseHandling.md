**Content**



# Release handling #

To release a new version of soi-toolkit we use mavens release-plugin.

**NOTE**: Do not use mobile internet during `mvn release:perform` or `mvn deploy` commands since your IP adress might change during a lengthy deploy operation and sonatypes maven repositories will get very confused by that typ of upload and you will not be able to get your artifacts transferred into the staging repository...

## Add release notes ##
Make sure documentation is up to date when tagging the Subversion repository by:
  * Update the issue list so only issues fixed in the current release are marked as fixed for the current release (move any non-resolved issues to a later milestone)

  * Add release notes to: https://code.google.com/p/soi-toolkit/wiki/ReleaseNotes

  * Add a short update/news note to the landing page: https://code.google.com/p/soi-toolkit/#Latest_releases

## Prepare ##

Perform the following steps:

  * gpg key
> If you have more that one private gpg key registered you have to specify what key to use as the default key.
> Edit ~/.gnupg/gpg.conf and add a line like:
```
default-key nnnnnnnn
```

  * Eclipse plugin-check
> Have you added a new source folder since the last release in the plugin?

> Remember to add it to the "source.." - property in build.properties before releasing!

  * Go to the components trunk-folder
```
cd .../trunk
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

  * Run tests
```
mvn clean test
```

  * Run generator tests (takes a looong time, e.g. some 30 minutes...)
```
mvn test -PrunGeneratorTests
```

**TODO 141129:** Update-tool outdated, move to mvn + jdk1.7, updates files are a mess, eclipse - plugin files should not need updates from this tool anymore. Update <soitoolkit.version> in soitoolkit-default-parent-pom by hand for now!

  * Use the update-tool to update soi-toolkit version to the release version in files not updated by the release plugin.
    * Update the variable "newVersion" to the new version, e.g. "0.4.0"
    * Set the variable "isSnapshot" to false
    * Run the update-tool.

> TODO: Make it possible to run the update-tool as a mvn command (in the same way as for the generator)

  * Manually update the soi-toolkit version to the release version in the following files:
    * Constant `SOITOOLKIT_VERSION` in `tools/soitoolkit-generator/soitoolkit-generator/src/main/java/org/soitoolkit/tools/generator/model/impl/DefaultModelImpl.java`
    * Bundle version and Bundle-ClassPath\*2 (lib/soitoolkit-generator-n.n.n.jar) in `tools/soitoolkit-generator/soitoolkit-generator-plugin/META-INF/MANIFEST.MF`
    * Soitoolkit-version\*2 in bin.includes + jars.extra.classpath in `tools/soitoolkit-generator/soitoolkit-generator-plugin/build.properties`
    * Soitoolkit-version\*2 in `tools/soitoolkit-generator/soitoolkit-generator-plugin/.classpath`
    * Make sure the default Mule version for the Maven-generator plugin is among valid version in `tools/soitoolkit-generator/soitoolkit-generator-maven-plugin/src/main/java/org/soitoolkit/tools/generator/maven/GenIntegrationComponentMojo.java`

**NOTE:** If only releasing maven artefacts (e.g. not the eclipse plugin) you need to update only the following by hand:
  * Manually update the soi-toolkit version to the release version in the following files:
    * Constant `SOITOOLKIT_VERSION` in `tools/soitoolkit-generator/soitoolkit-generator/src/main/java/org/soitoolkit/tools/generator/model/impl/DefaultModelImpl.java`
  * commons/poms/default-parent/pom.xml" --> "/ns:project/ns:properties/ns:soitoolkit.version"
  * Make sure the default Mule version for the Maven-generator plugin is among valid version in `tools/soitoolkit-generator/soitoolkit-generator-maven-plugin/src/main/java/org/soitoolkit/tools/generator/maven/GenIntegrationComponentMojo.java`

**END NOTE**

<a href='Hidden comment: 
OLD STUFF!!!
* Manually update the soi-toolkit version to the release version in commons/poms/default-parent/pom.xml.
E.g. change:
```
 <soitoolkit.version>0.1.2-SNAPSHOT</soitoolkit.version>
```
To:
```
 <soitoolkit.version>0.1.2</soitoolkit.version>
```

NOTE: DO NOT CHANGE THE FOLLOWING LINE, JUST THE LINE ABOVE!!!
```
  <version>0.1.2-SNAPSHOT</version>
```

NOTE: Same for:
* Parent version for commons/poms/mule-dependencies/mule-2.2.5-dependencies/pom.xml
* Parent version for commons/poms/mule-dependencies/mule-2.2.7-dependencies/pom.xml
* Parent version for commons/poms/mule-dependencies/mule-3.0.0-dependencies/pom.xml
* Parent version for commons/poms/mule-dependencies/mule-3.0.1-dependencies/pom.xml
* Parent version for commons/poms/mule-dependencies/mule-3.1.0-dependencies/pom.xml
* Constant SOITOOLKIT_VERSION in tools/soitoolkit-generator/soitoolkit-generator-plugin/src/org/soitoolkit/tools/generator/plugin/model/impl/DefaultModelImpl.java
* Bundle version in tools/soitoolkit-generator/soitoolkit-generator-plugin/META-INF/MANIFEST.MF
* Feature version in tools/soitoolkit-generator/org.soitoolkit.generator.update/site.xml (*two places*)
* Feature version in tools/soitoolkit-generator/org.soitoolkit.generator.feature/feature.xml
'></a>

  * Revert any personal settings in the property file for the generator plugin, `tools/soitoolkit-generator/soitoolkit-generator/src/main/resources/soi_toolkit_generator_plugin_default_preferences.properties`
> NOTE: Save them in some safe place so that you easily can re-apply them after the release is done!

  * Check versions of dependencies and parent pom, replace any SNAPSHOT-versions with stable ones.
```
mate pom.xml
```

  * Run tests again to ensure that nothing was broken
```
mvn clean test
```

  * Commit changes
```
svn commit -m "Commit for releasing v0.4.0"
```

## Perform ##

Perform the following steps:

  * First ensure that Eclipse is shut down so that it won't intervene with recreation of target folders and it content!

  * Perform a dryrun to verify that everything is ok, i.e. a release build will be successful and not fail in the middle leaving the release build in an inconsistent state. **Note:** Version numbers shall follow the format defined below.
```
mvn release:clean release:prepare -DdryRun=true -Darguments="-Dgpg.passphrase=nnn"
```

  * Fix any problems identified by the dryrun then perform a prepare-step to create the tag in svn
```
mvn release:clean release:prepare -Darguments="-Dgpg.passphrase=nnn"
```

  * Deploy to Sonatypes staging repository
```
mvn release:perform
```

  * Deploy other mule-version-dependency files + populate repo script to staging repository
```
 cd ../tags
 svn update
 cd soitoolkit-n.n.n
 mvn deploy -f commons/poms/mule-dependencies/mule-3.2.0-dependencies/pom.xml -Psonatype-oss-release
 mvn deploy -f tools/soitoolkit-populate-local-maven-repo/pom.xml -Psonatype-oss-release
 cd ../../trunk
```

<a href='Hidden comment: 
(*NONE FOR THE MOMENT*)
'></a>

  * Go to Sonatypes staging repository and release it to synch with maven central repo
    * Go to: https://oss.sonatype.org
    * Login to the Nexus UI.
    * Go to Staging Repositories page.
    * Select a staging repository.
    * Select the soi-toolkit release
    * Click the Close button.

## Validate ##

Perform the following steps:

  * Validate against the staging repo https://oss.sonatype.org/content/repositories/staging/org/soitoolkit/
    * Activate the maven profile `soi-toolkit-sonatype` in the maven settings.xml - file:
```
  <activeProfiles>
    <activeProfile>soi-toolkit-sonatype</activeProfile>
  </activeProfiles>
```
    * Remove `org.soitoolkit` from the local maven repository (to ensure that the artifacts are downloaded as expected from the Sonatypes staging repository)
    * Refresh the generator plugin project in eclipse to get the updated version-numbers from above into the eclipse-project
    * Re-apply any personal settings in the property file for the generator plugin, `tools/soitoolkit-generator/soitoolkit-generator/src/main/resources/soi_toolkit_generator_plugin_default_preferences.properties`
    * Run all unit tests in soitoolkit-generator, including the generator tests to verify that the generated code works with the new version of soi-toolkit
    * Manually copy soitoolkit-generator-n.n.n.jar and soitoolkit-commons-xml-n.n.n.jar to the soitoolkit-generator-plugin/lib - folder and update the build-path.
    * Start a runtime eclipse workbench and run the new version of the generator plugin to create an integration component and service description (schema) component.
    * Try out the new integration component by creating some services and run their integration tests
    * Create a service description component as well...
    * Test-migrate projects generated by the generator-tests from the previous versione of soi-toolkit
      * Generate using the previous versions svn-tag
      * Update dependency in the parent pom.xml file to the new soitoolkit-version and in the service-pom.xml file to a latest Mule version.
      * Apply all changes required by compatibility issues as described in the release notes
    * Wrap up the verification by deactivating the maven profile `soi-toolkit-sonatype` in the settings-file.

  * If errors then Click on the Drop button and correct error and redo the process...
> If something is wrong you can fix it on the release-tag in svn and perform av redeploy to the staging repo from the tag-folder using:
```
 mvn deploy -Psonatype-oss-release
 mvn deploy -f commons/poms/mule-dependencies/mule-3.2.0-dependencies/pom.xml -Psonatype-oss-release
```

> NOTE: Don't forget to apply the relevant changes to trunk as well...

## Deploy ##

Perform the following steps:

  * Manually deploy the eclipse plugin as described below

  * Publish to central repo
    * If ok go back to the Sonatype staging repository web-app, https://oss.sonatype.org
    * Select the soi-toolkit release again and click on the Release button.
    * Artifacts should now be synched with central repo on a hourly bases...

  * When the central maven repository is populated with the new release then also update an Eclipse workspace to the new version of the plugin and verify that it works as expected
    * Create an integration component some services and run their integration tests...

  * Publish release notes in the wiki-page ReleaseNotes and add a news line on the home-page as well.

  * Update eclipse plugin version in table in the [InstallationGuide](InstallationGuide.md)

### Manually deploy the eclipse logger-module plugin ###

  * Open a terminal in the module-logger folder
  * Build update site with the command:
```
mvn package -Ddevkit.studio.package.skip=false
```
  * Copy the update site to the test-server:
```
ftp target/UpdateSite.zip ...
```
  * Try it out in Mule Studio

### Manually deploy the eclipse plugin - NEW WAY ###

This is currently done manually, mostly since the Eclipse-projects are not yet build with maven :-(

  * get the source of new release
```
svn up tags
```
  * Open a new workspace and import plugin, feature and update project from the new tag
  * Perform a maven import of the generator-project from the tag
  * Edit soi\_toolkit\_generator\_plugin\_default\_preferences.properties and remove the default value for the root\_folder
    * Note: the existing default value is adopted to fit jenkin builds on cloudbees
  * Build generator-project
  * Copy generator-jar to plugin/lib - folder
  * Copy xml-commons-jar from local maven repo as well to the plugin/lib - folder
  * Perform a "build all" in the update-project
  * Zip the update-project and name it, soitoolkit-eclipse-plugin-n.n.n.zip
    * Remember to not include any .svn or .DS\_Store files!
```
zip -r soitoolkit-eclipse-plugin-0.6.0.zip org.soitoolkit.generator.update/ -x "*/.svn/*"
```
  * Move the zip-file to the eclipse-update-site - folder, e.g:
```
cp soitoolkit-eclipse-plugin-0.6.0.zip ../../../../eclipse-update-site/zip/
```
  * Commit the change of the eclipse-update-site - folder.
  * Update the wiki page: http://code.google.com/p/soi-toolkit/wiki/InstallationGuide?ts=1325980731&updated=InstallationGuide#Soi-toolkit_Eclipse_plugin
( **Commit all changes (tags))**

### Manually deploy the eclipse plugin - OLD STYLE ###

This is currently done manually :-(

  * get the source of new release
```
svn up tags
```

  * temporary remove the plugin, feature and update project from the eclipse workspace

> BETTER IDEA: Use a separate Eclipse workspace, you have to specify svn creds but you don't need to remove and add back projects...

  * import the plugin, feature and update site projects from the new tag

  * Manually copy the soitoolkit-commons-xml-n.n.n.jar and soitoolkit-generator-n.n.n.jar file to the soitoolkit-generator-plugin/lib - folder.

  * Verify/update version numbers
    * DefaultModelImpl.java
    * plugin/MANIFEST.MF
    * feature/feature.xml
    * update/site.xml

  * Commit any changed files

  * Ensure that `soi_toolkit_generator_plugin_default_preferences.properties` is reverted to the checked-in version, i.e. no developer specific settings here!
  * Ensure that the update site-project is fully reverted, i.e. no runtime files exists in the project
  * Perform a "build all" in the update-project (click on the "Build All" button)
  * Copy updated runtime files (4 jar-files) from the update project to svn/eclipse-update-site/soi-toolkit-n.n.n
> > NOTE: Do not copy folders, then .svn files will be removed from eclipse-update-site folders!!!
  * Update compositeArtifacts.xml and compositeContent.xml
  * Commit changes in svn/eclipse-update-site
  * Revert the update project to get rid of the runtime files

  * remove the plugin, feature and update projects from the new tag in the eclipse workspace
  * import plugin, feature and update project from trunk back into the eclipse workspace

## Prepare for next version ##

Perform the following steps:

  * Use the update-tool to update soi-toolkit version to the snapshot of the next version in files not updated by the release plugin.
    * Update the variable "newVersion" to the next version, e.g. "0.4.1"
    * Set the variable "isSnapshot" to true
    * Run the update-tool.


> TODO: Make it possible to run the update-tool as a mvn command (in the same way as for the generator)

  * Manually update the soi-toolkit version to the snapshot of the next version in the following files, e.g. 0.6.1-SNAPSHOT:

**NOTE:** See [DeveloperGuidelines#Publish\_snapshots\_of\_the\_Eclipse\_plugin](DeveloperGuidelines#Publish_snapshots_of_the_Eclipse_plugin.md) for instructions on how to specify version number for the eclipse plugin!

  * Constant `SOITOOLKIT_VERSION` in `tools/soitoolkit-generator/soitoolkit-generator/src/main/java/org/soitoolkit/tools/generator/model/impl/DefaultModelImpl.java`
  * Bundle version and Bundle-ClassPath\*2 (lib/soitoolkit-generator-n.n.n.jar) in `tools/soitoolkit-generator/soitoolkit-generator-plugin/META-INF/MANIFEST.MF`
  * Soitoolkit-version\*2 in bin.includes + jars.extra.classpath in `tools/soitoolkit-generator/soitoolkit-generator-plugin/build.properties`
  * Soitoolkit-version\*2 in `tools/soitoolkit-generator/soitoolkit-generator-plugin/.classpath`

<a href='Hidden comment: 
OLD STUFF!!!
* Manually update the soi-toolkit version to snapshot of the next release version in commons/poms/default-parent/pom.xml.
E.g. change:
```
<soitoolkit.version>0.1.2</soitoolkit.version>
```
To:
```
<soitoolkit.version>0.1.3-SNAPSHOT</soitoolkit.version>
```
NOTE: Same for:
* Parent version for commons/poms/mule-dependencies/mule-2.2.5-dependencies/pom.xml
* Parent version for commons/poms/mule-dependencies/mule-2.2.7-dependencies/pom.xml
* Parent version for commons/poms/mule-dependencies/mule-3.0.0-dependencies/pom.xml
* Parent version for commons/poms/mule-dependencies/mule-3.0.1-dependencies/pom.xml
* Parent version for commons/poms/mule-dependencies/mule-3.1.0-dependencies/pom.xml
* Constant SOITOOLKIT_VERSION in tools/soitoolkit-generator/soitoolkit-generator-plugin/src/org/soitoolkit/tools/generator/plugin/model/impl/DefaultModelImpl.java
* Bundle version in tools/soitoolkit-generator/soitoolkit-generator-plugin/META-INF/MANIFEST.MF
* Feature version in tools/soitoolkit-generator/org.soitoolkit.generator.update/site.xml (*two places*)
* Feature version in tools/soitoolkit-generator/org.soitoolkit.generator.feature/feature.xml
'></a>

  * Re-apply (if not already done during the verification) any personal settings in the property file for the generator plugin, `tools/soitoolkit-generator/soitoolkit-generator-plugin/src/soi_toolkit_generator_plugin_default_preferences.properties`

  * Remove org.soitoolkit from the local maven repository (to ensure that the artifacts are downloaded as expected from the central maven repository)

  * Perform a local build to get the new snapshot-versions of the parent pom installed in your local repository
```
mvn clean install
mvn clean install -f commons/poms/mule-dependencies/mule-3.2.0-dependencies/pom.xml 
```
<a href='Hidden comment: 
```
mvn clean install
mvn clean install -f commons/poms/mule-dependencies/mule-2.2.5-dependencies/pom.xml 
mvn clean install -f commons/poms/mule-dependencies/mule-2.2.7-dependencies/pom.xml 
mvn clean install -f commons/poms/mule-dependencies/mule-3.0.0-dependencies/pom.xml 
mvn clean install -f commons/poms/mule-dependencies/mule-3.0.1-dependencies/pom.xml 
mvn clean install -f commons/poms/mule-dependencies/mule-3.1.0-dependencies/pom.xml 
```
'></a>

  * Refresh the soitoolkit-projects in Eclipse, update their maven snapshot dependencies and verify that no build/compile problem exists
    * Select Maven --> Update Project Configuration on the project soitoolkit-commons-mule

  * Manually copy the new soitoolkit-commons-xml-n.n.n.jar and soitoolkit-generator-n.n.n-SNAPSHOT.jar file to the soitoolkit-generator-plugin/lib - folder and update the build path.

  * Commit the changes (except for changes in `soi_toolkit_generator_plugin_default_preferences.properties`)
```
svn commit -m "Start work on version n.n.n"
```