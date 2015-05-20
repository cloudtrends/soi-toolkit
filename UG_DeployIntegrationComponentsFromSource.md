# Deploying an Integration Component from Source Code #

**Content**


## Introduction ##

If you want to test-deploy an integration component before you have a release you can build and deploy it based on source code using the [maven plugin for Tomcat](http://mojo.codehaus.org/tomcat-maven-plugin).

**NOTE:** This should only be performed in a test environment, never deploy from source code to production. Alway use a tagged release when deploying to production.

Before you can deploy to Tomcat using the maven plugin you have to configure it in the web-projects pom.xml - files.

You also need to setup a directory structure on the server for holding configuration and log-files for the integration component.

## Initial setup ##

### Configure Tomcat with a user for deployment ###
In the $TOMCAT\_HOME/conf/tomcat-users.xml file, add a user like:
```
<user username="deployer" password="SET_YOUR_OWN_PASSWORD" roles="manager"/>
```

### Prepare an Integration Component for deploy using the maven plugin for Tomcat ###

Add the following to the pom.xml-files for the web-projects

```
  <build>
    <plugins>
      <plugin>
        <groupId>org.codehaus.mojo</groupId>
        <artifactId>tomcat-maven-plugin</artifactId>
        <version>1.1</version>
        <configuration>
          <path>/${artifactId}</path>
        </configuration>
      </plugin>
    </plugins>
  </build>
```

**Note:** For the teststub-war project use a path variable following the naming schema:
```
          <path>/${artifactId}-teststubs</path>
```

Add the following to your maven settings.xml file if admin/"" is not appropriate as usr/pwd:
```
    <server>
      <id>${serverId}</id>
      <username>myusername</username>
      <password>mypassword</password>
    </server>
```

For details of each parameters settings see: [Usage of the Maven Tomcat plugin](http://mojo.codehaus.org/tomcat-maven-plugin/usage.html)

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

## Create deploy-files ##

Since we deploy to Tomcat the deploy-files are standard war-files.
They are created with the following command executed in the integration components root folder (i.e. the trunk-folder):

```
mvn clean install 
```

If you have the Maven-plugin installed in Eclipse then you can execute the build command without leaving Eclipse.
Right-click the integration components top-level project and select "`Run As --> Maven clean`" and then "`Run As --> Maven install`"

**UPDATE\_PICTURE\_http://soi-toolkit.googlecode.com/svn/wiki/UserGuide/Deploy/build-from-eclipse.png**

The command creates war-files under the target folders of the web-projects...

## Deploy ##

For the war file to be deployed to your Tomcat server run the following command in the web-projects folder:
```
cd ${artifactId}-web
mvn clean tomcat:redeploy -Dmaven.tomcat.server=serverId -Dmaven.tomcat.url=http://server:8080/manager
```

For more information on available Tomcat commands see the [Plugin Documentation](http://mojo.codehaus.org/tomcat-maven-plugin/plugin-info.html)

If you have the Maven-plugin installed in Eclipse then you can execute the deploy command without leaving Eclipse.
Right-click the integration component top-level project and select "`Run As --> Maven build...`"

**UPDATE\_PICTURE\_http://soi-toolkit.googlecode.com/svn/wiki/UserGuide/Deploy/deploy-from-eclipse-1.png**

Enter the build command:
```
clean tomcat:redeploy -Dmaven.tomcat.server=serverId -Dmaven.tomcat.url=http://server:8080/manager
```

And a proper name (for easier reuse of the command):
```
sample1 deploy
```

**UPDATE\_PICTURE\_http://soi-toolkit.googlecode.com/svn/wiki/UserGuide/Deploy/deploy-from-eclipse-2.png**

Since the deploy command can be quit memory intensive it is recommended to specify a larger maximum heap size than default in the "JRE"-tab in the field "VM arguments:", e.g. -Xmx256m

**UPDATE\_PICTURE\_http://soi-toolkit.googlecode.com/svn/wiki/UserGuide/Deploy/deploy-from-eclipse-3.png**