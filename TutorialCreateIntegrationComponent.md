<font color='red' size='5'><b>NOTE:</b> This page is partially outdated.<br />Work is ongoing to update this page!</font>

<a href='Hidden comment: 
<font size="5" color="red">*NOTE:* This page is partially outdated.<br/>Work is ongoing to update this page!

Unknown end tag for &lt;/font&gt;


'></a>

# Tutorial: Create an Integration Component #

**Content**


# Introduction #

Integration components are used to contain Mule ESB based services and integrations.

This tutorial helps you to create your first integration component.

**NOTE**: Prerequisites for this tutorial is that the [installation guide](InstallationGuide.md) is completed.

# Create Integration component #
  * Select the menu "File --> New --> Other" and expand the wizard "SOI Toolkit Code Generator"
  * Select the code generator "Create a new component"

> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateIntegrationComponent/TutorialCreateIntegrationComponent1.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateIntegrationComponent/TutorialCreateIntegrationComponent1.png)

  * Click on the "Next >" button
> The wizard "SOI Toolkit - Create a new Component" opens up
    * Select the component type "Integration Component" in the radio button control named "Type of component"
    * Specify a proper name of the component in the field "Artifact Id"
    * Specify a proper group name in the field "Group Id"
    * Select where you want the files to be created in the field "Root folder"
> > Note: The pre-selected root folder is picked up from the preference page you filled in during the installation of the soi-toolkit plugin.


> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateIntegrationComponent/TutorialCreateIntegrationComponent2.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateIntegrationComponent/TutorialCreateIntegrationComponent2.png)

  * Click on the "Next >" button
> The wizard now displays a new page where you can perform some initial configuration of the new integration component
    * In the drop down box called "Mule version" you can select what version of Mule you want the integration component to use.
    * Select deployment model, standalone Mule or as a war files for deployment on a servlet engine such as Tomcat or TCat.
    * Select the transports you want initial support for
> > The generator will add connectors for these transports


> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateIntegrationComponent/TutorialCreateIntegrationComponent3.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateIntegrationComponent/TutorialCreateIntegrationComponent3.png)

  * Click on the "Finish" button
> If a connector was selected for a transport that requires security related setup (e.g. SFTP and JDBC) a security notice will be displayed regarding the need to modify added properties in the security-property-file:

> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateIntegrationComponent/TutorialCreateIntegrationComponent3-security-notice-popup.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateIntegrationComponent/TutorialCreateIntegrationComponent3-security-notice-popup.png)

> The wizard now starts to execute and logs its output to a text area.

> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateIntegrationComponent/TutorialCreateIntegrationComponent4.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateIntegrationComponent/TutorialCreateIntegrationComponent4.png)

  * The following work is performed by the wizard:
    * Created folders and files according to the input on the previous page.
    * Launch maven to do a initial build and also create eclipse files
    * Opens the project in the current Eclipse workspace

> NOTE #1: The first time an integration component is created this step will take very long time since Maven downloads a lot of dependencies to your local Maven repository. Next time this process will be much faster!

> NOTE #2: If something goes wrong under this execution see section [if something goes wrong](#If_something_goes_wrong.md) for help


  * When the wizard is done you can find your new integration component in the Eclipse Package Explorer

> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateIntegrationComponent/TutorialCreateIntegrationComponent5.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateIntegrationComponent/TutorialCreateIntegrationComponent5.png)

  * Files of interest:
> An integration component consists of three eclipse projects and each project contains a typical maven source structure of folders.  See [soi.toolkit architecture](architecture.md) for further explanations of this structure.
    * Project `mySample-services`
      * Source folder `src/test/java`
> > > Contains a main-program, `MySampleMuleServer.java`, that can start Mule inside Eclipse, mainly used for manual tests and debugging inside the development environment. Service specific test code (unit-tests and producer-teststubs) will be created here when creating services.
      * Source folder `src/test/resources`
> > > `mySample-teststubs-and-services-config.xml` and `mySample-teststubs-only-config.xml` are common test mule-configuration files used by all services in this integration component. The `teststub-service`-folder will contain the mule configuration files for the specific services teststubs as they are created.
      * Source folder `src/environment`
> > > This folder contains configuration files for log4j and the services themselves. The files in this folder are used during development inside the project (e.g. loaded by mule on the classpath) but when the integration component is deployed to test and production environments these files are copied to an separate folder in the target environment to be able to be changed and adopted to that specific environment without having to redeploy the integration component for each change.
      * Source folder `src/main/java`
> > > This source folder is initially empty but will contain service specific java-code, e.g. transformers,as they are created.
      * Source folder `src/main/resources`
> > > `mySample-common.xml` and `mySample-config.xml` are common mule-configuration files used by all services in this integration component. The service-folder will contain the mule configuration files for the spoecific services as they are created.
    * Project `mySample-teststub-web`
      * Target folder `target`
> > > The runtime files for this integration components teststubs.
> > > `mySample-teststub-1.0-SNAPSHOT.war` contains the mule config files and class files for the services teststubs and all jar-files created for executing the teststubs on a standard servlet engine, e.g. Tomcat or Jetty.
    * Project `mySample-web`
      * Target folder `target`
> > > The runtime files for this integration component.
> > > `mySample-1.0-SNAPSHOT.war` contains the mule config files and class files for the services and all jar-files created for executing the integration component on a standard servlet engine, e.g. Tomcat or Jetty.

  * In the filesystem the following folder structure is created for you under the selected root folder:


> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateIntegrationComponent/TutorialCreateIntegrationComponent6.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateIntegrationComponent/TutorialCreateIntegrationComponent6.png)

# Building runtime files #

  * After development and local tests are done, you typically wants to build runtime files for deploy to a common test environment (and later on to a production environment).
> This is done by running the maven install command. If you have installed the Maven Eclipse plugin ([installation guide](InstallationGuide#Maven_Eclipse_plugin.md)) you can perform this (and other maven commands) directly within Eclipse without having to open a command window.
    * Start with importing the parent pom of the integration component.
      * In the menu select "File --> Import..." and select "Maven --> Existing Maven Project"

> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateIntegrationComponent/TutorialCreateIntegrationComponent8.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateIntegrationComponent/TutorialCreateIntegrationComponent8.png)

  * Click on the "Next" button
  * Click on the "Browse" button and navigate to the `trunk` folder of your integration component and select the top level pom.xml file (the parent pom)
  * Click on the "Finish" button

> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateIntegrationComponent/TutorialCreateIntegrationComponent9.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateIntegrationComponent/TutorialCreateIntegrationComponent9.png)

  * The parent-pom-project, `mySample`, is now imported into the Eclipse workspace.

  * Right click the parent-pom-project, `mySample`, and select "Run As --> Maven install"

> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateIntegrationComponent/TutorialCreateIntegrationComponent10.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateIntegrationComponent/TutorialCreateIntegrationComponent10.png)

  * In the command window you can see the progress of the maven build and it should end up with a `BUILD SUCCESSFUL` message. The integration components runtime files are now installed in your local maven repository.

> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateIntegrationComponent/TutorialCreateIntegrationComponent11.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateIntegrationComponent/TutorialCreateIntegrationComponent11.png)

  * See the [user guide](UserGuidelines.md) for how to deploy the runtime files to a Tomcat server.

# If something goes wrong #

If something goes wrong during the creation of the component after you pressed the "Finish" button in the wizard, typically under the execution of the maven install command, you can manually fix the problem and then manually finish the process:

  * Ensure that the required and recommended tools are installed as specified in the [installation guide](InstallationGuide.md).
    * Specifically ensure that the "`mvn -v`"-command returns "`Apache Maven 2.2.1`" (or what version you decided to use if not 2.2.1).

  * Open a command window in the trunk folder of the newly created component
> The trunk folder is found in a folder named "`artifact-id`" specified in the wizard under the "`root-folder`" as specified in the wizard.

  * Build the component by entering the command "`mvn clean install`"
> Correct any errors reported by the maven build command...

  * Create Eclipse-specific files using the command:
    * "`mvn eclipse:m2eclipse`" if you are using the Maven Eclipse plugin (recommended)
    * "`mvn eclipse:eclipse`" if you are not using the Maven Eclipse plugin

  * Now switch to Eclipse and import the Eclipse project(s) created under the `trunk`-folder.
    * In the menu select "File --> Import..."
    * In the Import-wizard expand the "General" node and select "Existing Projects into Workspace" and click on the "Next >" button.
    * Click on the "Browse..." button for the field named "Select root directory" and locate and select the trunk-folder
    * The Eclipse projects created above are now displayed in the "Projects:" - list.
    * Click on the "Finish" - button to complete the import.

You have now corrected the problem and manually finished the process that the wizard automates!