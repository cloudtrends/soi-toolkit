<font color='red' size='5'><b>NOTE:</b> This page is partially outdated.<br />Work is ongoing to update this page!</font>

# Tutorial: Create a Service Description Component #

**Content**


# Introduction #

Service Description components are used to contain wsdl and xml schema files that describe message formats for xml based services, e.g. a soap based web service..

This tutorial helps you to create your first service description component.

**NOTE**: Prerequisites for this tutorial is that the [installation guide](InstallationGuide.md) is completed.

# Create a new Service Description Component #
  * Select the menu "File --> New --> Other" and expand the wizard "SOI Toolkit Code Generator"
  * Select the code generator "Create a new component"

> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateServiceDescriptionComponent/TutorialCreateServiceDescriptionComponent1.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateServiceDescriptionComponent/TutorialCreateServiceDescriptionComponent1.png)

  * Click on the "Next >" button
  * The wizard "SOI Toolkit - Create a new Component" opens up
    * Select the component type "Service Description Component" in the radio button control named "Type of component"
    * Specify a proper name of the component in the field "Artifact Id"
    * Specify a proper group name in the field "Group Id"
> > Note: The group name will be used for naming both java packages and xml schema namespaces.
    * Select where you want the files to be created in the field "Root folder"
> > Note: The preselected folder is picked up from the preference page you filled in during the installation of the soi-toolkit plugin.


> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateServiceDescriptionComponent/TutorialCreateServiceDescriptionComponent2.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateServiceDescriptionComponent/TutorialCreateServiceDescriptionComponent2.png)

  * Click on the "Finish" button
    * The wizard now starts to execute and logs its output to a text area.

> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateServiceDescriptionComponent/TutorialCreateServiceDescriptionComponent3.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateServiceDescriptionComponent/TutorialCreateServiceDescriptionComponent3.png)

  * The following work is performed by the wizard:
    * Creates folders and files according to the input on the previous page.
    * Launch maven to do a initial build and also create eclipse files
    * Opens the project in the current Eclipse workspace

> NOTE: If something goes wrong under this execution see section [if something goes wrong](#If_something_goes_wrong.md) for help

  * When the wizard is done you can find your new service description component in the Eclipse Package Explorer

> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateServiceDescriptionComponent/TutorialCreateServiceDescriptionComponent4.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateServiceDescriptionComponent/TutorialCreateServiceDescriptionComponent4.png)

  * Files of interest:
    * Source folder `src/main/resources`
> > Skeleton wsdl and xml schema files (`mySampleSchema.wsdl` and `mySample.xsd`) are created with some default method and some request and reply elements for you to fill in :-)
    * Source folder `target/generated-sources/cxf`
> > Generated jax-ws and jaxb Java classes according to the wsdl and xml schema.
> > These files are automatically created by maven when it builds the component.
    * Target folder `target`
> > The runtime files for this component:
      * `mySample-schemas-1.0-SNAPSHOT.jar` contains the wsdl and xml schema files and the class files for the corresponding generated java source files, typically used by Java based consumers and providers of these service descriptions.
      * `mySample-schemas-1.0-SNAPSHOT-schemas.zip` includes the wsdl(s) and xml schema files, typically used by non Java based consumers and providers of these service descriptions, such as Microsoft .NET based consumers and providers of these service descriptions. The zip assembly also includes a script (wcf.bat) in order to generate a WCF client for the .NET based consumers.

  * In the filesystem the following folder structure is created for you under the selected root folder:


> ![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateServiceDescriptionComponent/TutorialCreateServiceDescriptionComponent5.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateServiceDescriptionComponent/TutorialCreateServiceDescriptionComponent5.png)

  * After updating the wsdl and/or the xml schemas you can create new Java files and runtime files by opening a command window in the projects `trunk`-folder and enter the command
> `mvn clean install`.

> If everything goes well the build ends with a `BUILD SUCCESSFUL` - message

![http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateServiceDescriptionComponent/TutorialCreateServiceDescriptionComponent6.png](http://soi-toolkit.googlecode.com/svn/wiki/Tutorials/TutorialCreateServiceDescriptionComponent/TutorialCreateServiceDescriptionComponent6.png)


# If something goes wrong #

If something goes wrong during the creation of the component after you pressed the "Finish" button in the wizard, typically under the execution of the maven install command, you can manually fix the problem and then manually finish the process:

  * Ensure that the required and recommended tools are installed as specified in the [installation guide](InstallationGuide.md).
    * Specifically ensure that the "`mvn -v`"-command returns "`Apache Maven 2.2.1`" (or what version you decided to use if not 2.2.1).

  * Open a command window in the trunk folder of the newly created component
> The trunk folder is found in a folder named "`artifact-id`" specified in the wizard suffixed with "`-schemas`" under the "`root-folder`" as specified in the wizard.

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