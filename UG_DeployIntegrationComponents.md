**Content**


# Deploying an Integration Component #

An integration component is deployed based on a released version typically picked up from the internal Maven repository.

# Offline install and upgrade #

  1. Stop Mule
  1. Remove the application folder under $MULE\_HOME/apps, if it exists
  1. Copy the deploy-file (zip-file) to the folder $MULE\_HOME/apps
  1. Update, if required, the override-property-file, $MULE\_HOME/config/${ic-name}-config-override.properties with new, updated or removed properties that needs to be environment specific (see property-file for default values in the deploy-file (classes/${ic-name}-config.properties). For info regarding use of property files, see: [Property file configuration and usage](UG_PropertyFile.md)
  1. Start Mule again, the deploy-file is now expanded into a folder under $MULE\_HOME/apps
  1. Verify in the log-files under $MULE\_HOME/logs, mule.log and mule-app-nnn.log, that the startup of mule and the new version of the integration component was successful
  1. Perform functional tests to verify that the integration component works and that the output is as expected in the log-file and event-log-queues
  1. Update documentation of deploy-configuration

NOTE: Before soi-toolkit v0.6.1 there is an issue with the log4j.xml file in the exploded classes-folder. It is setup for development use and typically use the console appender for output and not a file appender. Until changed this result in that all log output from the integration component is written to mule.log instead of to the integration component specific log-file.


# Online install and upgrade #

TO BE DESCRIBED


# OLD: Deploy to Tomcat #

Before you can deploy to Tomcat you have to setup a directory structure on the server for holding configuration and log-files for the integration component.

## Initial setup ##

### Configure Tomcat with a user for deployment ###
In the $TOMCAT\_HOME/conf/tomcat-users.xml file, add a user like:
```
<user username="deployer" password="SET_YOUR_OWN_PASSWORD" roles="manager"/>
```


### Setup a directory structure for the Integration Component on the server ###

Create the following directory structure under the app-home folder on the server:

```
${app.home}
   ${artifactId}
      config
      logs
```

The directory structure will be used to hold configuration files, log-files and other files used by the Integration Components, e.g. archive for data files processed by the component.

**Note:**  ${app.home} is an environment variable pointing to the app-home folder on the server and ${artifactId} is the artifact-id of your Integration Component.
The app.home variable should have been configured when installing the Tomcat server, see [InstallationGuide#Installing\_Apache\_Tomcat](InstallationGuide#Installing_Apache_Tomcat.md)

## Setup configuration files ##

  1. Copy and/or update files from the folder `src/main/environment` in the project to folder `${app.home}/${artifactId}/config` on the server
  1. Perform nessecary configuration, e.g.
    * update the configuration of (s)ftp-servers, jms providers and/or jdbc-datasources if used in the config-file
    * change URL's of endpoints in the config-file
    * update username and passwords in the security-file
    * configure log4j to log to files in a appropriate way, using rolling log-files instead of logging to the console...

## Download deploy-files ##

Copy war files from the maven repository to a local folder.

The war-files are typically stored in the maven repository in a form similar to:

```
- http://host/repositories/releases/${groupId}/${artifactId}/${version}/${artifactId}-${version}.war
- http://host/repositories/releases/${groupId}/${artifactId}-teststubs/${version}/${artifactId}-teststubs-${version}.war
```

Rename the downloaded war-files to `${artifactId}.war` and `${artifactId}-teststubs.war`.

## Undeploy existing versions of the integration component ##

Use Tomcat Web Application Manager to undeploy any already deployed versions of the integration component.

![http://soi-toolkit.googlecode.com/svn/wiki/UserGuide/Deploy/undeploy-using-tomcat-manager.png](http://soi-toolkit.googlecode.com/svn/wiki/UserGuide/Deploy/undeploy-using-tomcat-manager.png)

## Deploy ##

Use Tomcat Web Application Manager to deploy the integration components artifacts.

![http://soi-toolkit.googlecode.com/svn/wiki/UserGuide/Deploy/deploy-using-tomcat-manager.png](http://soi-toolkit.googlecode.com/svn/wiki/UserGuide/Deploy/deploy-using-tomcat-manager.png)