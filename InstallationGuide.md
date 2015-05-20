# Installation Guide - Development environment #

**Content**


# Introduction #

To get started with soi-toolkit and Mule ESB you only need to install Java SE and Maven. Mule Studio and the soi-toolkit Eclipse plugin are however strongly recommended as well, see section [Required installations](#Required_installations.md).

Mule Studio is the preferred Java IDE for soi-toolkit and soi-toolkit provides a specific Eclipse plugin with functionality not available for other Java IDE's.

Users of other Java IDE's (e.g. NetBeans or IntelliJ) should however be fine using soi-toolkit's Maven based source code generators that have the same functionality as the Eclipse plugin. see [Using the source code generators](UG_UsingGenerators.md) for more information.

We also recommend using soapUI as a test tool for Web Services (SOAP and REST based), see section [Recommended installations](#Recommended_installations.md).

Depending on your needs you also might want to install some run time products in your development environment such as Mule ESB, ActiveMQ, or setting up PKI - keys for usage with the SFTP transport. See [Installing Run time products](InstallationGuideRuntime.md) for instructions on how to install and configure run time products.

The installation guides below assumes your on a 32 bit Microsoft Windows PC but are for most parts applicable for a Mac OS X or Linux user as well.

The following versions (or newer) are recommended:
| **Tool** | **Version** |
|:---------|:------------|
|Java SE   |6 Update 37, see Note #1|
|Maven     |3.0.4        |
|Mule Studio|1.3.1        |
|Soi-toolkit plugin for Eclipse|0.6.1        |
|soapUI    |4.5.1        |

**Note #1:** We have noted some problems with Java SE 7 and certificates, see [issue 292](http://code.google.com/p/soi-toolkit/issues/detail?id=292) for more details.

# Required installations #

## Java SE ##
Install Java SE according to the following instructions:


  * Download JDK for Java SE 6 from http://www.oracle.com/technetwork/java/javase/downloads/index.html.
> Select the JDK download in the section named "Java SE 6 Update nn", select the Windows x86 version and download it to your PC. A file named similar to `jdk-6u37-windows-i586.exe` should be downloaded.

  * Download JDK for Java SE 6 from http://www.oracle.com/technetwork/java/javase/downloads/index.html.
> A file named similar to `jdk-6u37-windows-i586.exe` is downloaded to your PC.

  * Execute the downloaded installation program and use default values for the installation of both the JDK and the JRE.

  * Create an environment variable, JAVA\_HOME, in Windows that points to the installation directory.
> E.g. `JAVA_HOME=C:\Program Files\Java\jdk1.6.0_37`

  * Add the bin-folder of the JDK to the Windows PATH-environment variable.
> E.g. `PATH=...;%JAVA_HOME%\bin`

  * Verify the installation in a command window with the commands `java –version`,  `javac –version` och `set JAVA_HOME`. The result should be similar to:

> ![http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/JavaSeVerifyInstallation.png](http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/JavaSeVerifyInstallation.png)

  * For full installation instructions see [Microsoft Windows Installation (32-bit)](http://www.oracle.com/technetwork/java/javase/documentation/install-windows-152927.html)


## Maven ##

Install Maven according to the following instructions:

  * Download Maven from http://maven.apache.org/download.html
> Download the file named `apache-maven-3.0.4-bin.zip` to your computer.

  * Unzip the zip file to the desired installation folder, eg `C:\opt`.

  * Create an environment variable, `M2_HOME`, in Windows pointing out the installation.
> For example: `M2_HOME=C:\opt\apache-maven-3.0.4`

  * Add Maven's bin folder to the Windows `PATH` variable.
> For example: `PATH=...;%M2_HOME%\bin`

  * Verifying the installation of a command window with the command `mvn -v`,
> it should produce a result like:

> ![http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/MavenVerifyInstallation.png](http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/MavenVerifyInstallation.png)

  * For full installation instructions see http://maven.apache.org/download.html#Installation



### Optional - Downloading Maven dependencies ###
_**Since: 0.5.0**_

<br>To speed up initial download of dependencies used by soi-toolkit the maven-script described below can be used.<br>
<br>This step is optional, if not executed then the dependencies will be downloaded the first time the generator is used.<br>
<br>
<ol><li>Download the script for the version of soi-toolkit you will use from:<br>
<blockquote><a href='http://search.maven.org/#search|ga|1|soitoolkit-populate-local-maven-repo'>http://search.maven.org/#search|ga|1|soitoolkit-populate-local-maven-repo</a>
<br>Note: groupId: org.soitoolkit.tools, artifactId: soitoolkit-populate-local-maven-repo<br>
<br>
<br>Save the script as <b>pom.xml</b>
<br>
</blockquote></li><li>Put the script in an empty tmp-dir, for example C:\temp\poprepo, and run the script:<br>
<pre><code>    cd poprepo<br>
    mvn install<br>
</code></pre>
<blockquote>to download dependencies with the default Mule-version for the used Soi-toolkit version.<br>
<br>To specify a Mule-version use the "muleVersion" argument like:<br>
<pre><code>    mvn install -DmuleVersion=3.4.0<br>
</code></pre></blockquote></li></ol>

<b>Note:</b> if dependencies can not be resolved/downloaded, it may be because your organization is using a Maven repository manager that does not mirror all required repositories.<br>
<ul><li>Verify if that is the case by temporary renaming your $USER_HOME/.m2/settings.xml to $USER_HOME/.m2/settings.xml.nop and re-run the script.<br>
</li><li>The Maven mirror configuration may alternatively have been entered in: $MAVEN_HOME/conf/settings.xml in which case you need comment out the mirror-settings and test by re-running the script.<br>
</li><li>If your Maven repository manager does not mirror necessary repositories, you need to configure the repository manager.</li></ul>



<h3>Optional - Mule ESB EE dependencies</h3>

If you are using Mule ESB EE you need to manually populate your local maven repository with the Mule ESB EE dependencies.<br>
<br>
<ul><li>Download and unzip Mule ESB EE from MuleSoft Customer Portal<br>
</li><li>Define the environment varible MULE_HOME to point to the folde you unzipped Mule ESB EE to<br>
</li><li>Go to the bin folder and run the script <code>populate_m2_repo</code> (<code>populate_m2_repo.cmd</code> for Windows) with the location of your local maven repository as input, e.g.:</li></ul>

On Windows:<br>
<pre><code>C:\Users\magnus&gt;set MULE_HOME=C:\opt\mule-enterprise-standalone-3.3.1<br>
C:\Users\magnus&gt;cd %MULE_HOME%\bin<br>
C:\opt\mule-enterprise-standalone-3.3.1\bin&gt;populate_m2_repo.cmd C:\Users\magnus\.m2\repository<br>
</code></pre>

On Linux:<br>
<pre><code>export MULE_HOME=/usr/local/mule-enterprise-standalone-3.3.1<br>
cd $MULE_HOME/bin<br>
./populate_m2_repo ~/.m2/repository<br>
</code></pre>

<h2>Mule Studio</h2>

Mule Studio is available in two flavors in the the same way as Mule ESB, Community Edition (CE) and Enterprise Edition (EE).<br>
This instruction is for Mule Studio CE but Mule Studio EE can be downloaded from MuleSoft Customer Portal and used according to the instructions below.<br>
<br>
Install Mule Studio CE according to the following instructions:<br>
<br>
<ul><li>Download <code>Download Mule ESB 3.3 Community Edition (with Mule Studio)</code> from <a href='http://www.mulesoft.org/download-mule-esb-community-edition'>http://www.mulesoft.org/download-mule-esb-community-edition</a>.<br>
</li></ul><blockquote>A file named similar to <code>MuleStudio-CE-for-win-32bit-1.3.1-201209061215.zip</code> is downloaded to your computer.</blockquote>

<ul><li>Unzip the zip file to the desired installation folder, eg C:\opt.</li></ul>

<ul><li>Create a shortcut to the file <code>MuleStudio.exe</code> (eg <code>C:\opt\mule-studio-ce-1.3.1\MuleStudio.exe</code>) in the installation folder to the desktop.</li></ul>

<ul><li>Verify the installation, start Mule Studio through the desktop shortcut.<br>
</li></ul><blockquote>Enter the desired workspace folder where requested and click on the "Workbench" icon (top right) when the welcome screen is presented. The the Mule Studio workbench should be displayed and look like:</blockquote>

<blockquote><img src='http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/MuleStudioVerifyInstallation.png' /></blockquote>

<ul><li>Ensure that you save your source files in UTF-8 format in your Eclipse workspace as described in <a href='UG_EditorSetup.md'>editor setup</a>.</li></ul>

<ul><li>Ensure that you have the classpath variable M2_REPO setup in Mule Studio<br>
<ul><li>Goto Mule Studio Preferences and select "Java --> Build Path --> Classpath Variables"<br>
</li><li>If the variable M2_REPO is not already defined then click on the "New..." button<br>
</li><li>Enter M2_REPO as the name and click on the "Folder..." button and navigate and select you local Maven repository folder.<br>
</li><li>Click on "Ok" buttons and accept a full rebuild of your workspace if requested.</li></ul></li></ul>

<blockquote><img src='http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/MuleStudio_M2_REPO_variable.png' /></blockquote>


<h2>Soi-toolkit Eclipse plugin</h2>

If you want to be able to start the soi-toolkit source code generators from within Mule Studio you should install this Eclipse plugin.<br>
<br>
<b>NOTE #1</b>: an alternative is to use Maven to start the source code generators, see <a href='UG_UsingGenerators.md'>Using the source code generators</a>.<br>
<br>
<b>NOTE #2</b>: Since soi-toolkit v0.5.0 the eclipse plugins (also for older versions of soi-toolkit) are distributed as a downloadable zip'ed update site that needs to be downloaded and unzipped to the local file system. The reason for this change is improved stability and performance during the installation and update process.<br>
<br>
<ul><li>Download the zip'ed update site, <code>soitoolkit-eclipse-plugin-0.6.1.zip</code>, from: <a href='http://soi-toolkit.googlecode.com/svn/eclipse-update-site/zip/'>http://soi-toolkit.googlecode.com/svn/eclipse-update-site/zip/</a>
</li><li>Unzip it to a local folder, e.g. your <code>Downloads</code> - folder.</li></ul>

<ul><li>Select in the Mule Studio menu "Help --> Install New Software..."</li></ul>

<blockquote><img src='http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/SoiToolkitInstallation1.png' /></blockquote>

<ul><li>Click on the "Add..." button to add the soi-toolkit update site<br>
<ul><li>Enter "soi-toolkit update site" in the name-field<br>
</li><li>Click on the "Local..." button and point out the local folder <code>org.soitoolkit.generator.update</code></li></ul></li></ul>

<blockquote><img src='http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/SoiToolkitInstallation2.A.png' /></blockquote>

<ul><li>Click on the "Ok" button</li></ul>

<blockquote><img src='http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/SoiToolkitInstallation2.png' /></blockquote>

<ul><li>The folder like <code>file:/C:/Users/magnus/Downloads/soitoolkit-eclipse-plugin-0.6.1/org.soitoolkit.generator.update/</code> should now have been selected<br>
</li><li>Click on the "Ok" button</li></ul>

<ul><li>Allow Mule Studio to process the update site</li></ul>

<ul><li>Expand the available selection "soi-toolkit generator" and select the "soi-toolkit generator feature"</li></ul>

<blockquote><img src='http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/SoiToolkitInstallation3.png' /></blockquote>

<ul><li>Click on the "Next >" button<br>
</li><li>Click on the "Next >" button again</li></ul>

<blockquote><img src='http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/SoiToolkitInstallation4.png' /></blockquote>

<ul><li>Accept the terms of the license agreement and press the "Finish" button<br>
<ul><li>The plugin is now downloaded and installed.</li></ul></li></ul>

<ul><li>Click on the "Ok" button when asked to accept unsigned content</li></ul>

<blockquote><img src='http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/SoiToolkitInstallation5.png' /></blockquote>

<ul><li>Click on the "Restart Now" button when asked to restart Mule Studio</li></ul>

<blockquote><img src='http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/SoiToolkitInstallation6.png' /></blockquote>

<ul><li>Configure the plugin by open Mule Studio Preference page in the menu and select the "SOI-Toolkit Generator"<br>
<ul><li>Specify the folder where you have Maven installed in the field "<b><i>Maven home folder</i></b>"<br>
</li><li>Specify the default folder where the generator should create files in the field "<b><i>Default root folder</i></b>"<br>
</li><li>You can leave the rest of the fields as they are.<br>
</li><li>Click on the "<b><i>OK</i></b>" button to conclude the configuration.</li></ul></li></ul>

<blockquote><img src='http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/SoiToolkitInstallation7.png' /></blockquote>

<b>NOTE:</b> Regarding the problem with disabled Apply and Ok buttons.<br>
If, for example, a non existing file or folder is specified in the fields "Maven home folder" or "Default root folder" then the Ok and Apply buttons are disabled. Please verify the correctness of the content in all fields if the Ok and Apply buttons gets disabled.<br>
<br>
<br>
<h1>Recommended installations</h1>

<h2>soapUI</h2>
If you want to test a soap-webservice without having to write code for a test, then soapUI is a good tool:<br>
<ul><li>Download the latest version from <a href='http://sourceforge.net/projects/soapui/files/soapui/'>http://sourceforge.net/projects/soapui/files/soapui/</a>
</li><li>For Windows: select soapUI-x32-<code>&lt;VERSION&gt;</code>.exe<br>
</li><li>Run the installer.</li></ul>


<h1>Updates</h1>

<h2>Updating the Soi-toolkit Eclipse plugin</h2>

<b>NOTE</b>: Since soi-toolkit v0.5.0 the eclipse plugins (also for older versions of soi-toolkit) are distributed as a downloadable zip'ed update site that needs to be downloaded and unzipped to the local file system. The reason for this change is improved stability and performance during the installation and update process.<br>
<br>
To update the Soi-toolkit Eclipse plugin do the following:<br>
<br>
<ul><li>First take a snapshot of the preference-setting you have for the existing version of the Soi-toolkit Eclipse plugin, if you need to reenter them after the upgrade. Normally this is not the case but just to be sure.<br>
<ul><li>Open the Mule Studio Preference page in the menu and select the "SOI-Toolkit Generator"</li></ul></li></ul>

<ul><li>Download the zip'ed update site, <code>soitoolkit-eclipse-plugin-n.n.n.zip</code>, from: <a href='http://soi-toolkit.googlecode.com/svn/eclipse-update-site/zip/'>http://soi-toolkit.googlecode.com/svn/eclipse-update-site/zip/</a>
</li><li>Unzip it to a local folder, we will name this folder as <code>local-update-site</code> in the instructions below.<br>
</li><li>Select in the Mule Studio menu "Help --> Install New Software..."</li></ul>

<blockquote><img src='http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/SoiToolkitInstallation1.png' /></blockquote>

<ul><li>Click on the link "Available Software Sites"<br>
</li><li>Select "soi-toolkit update site" and click on Edit-button.<br>
</li><li>Update the Location-field to point out the new <code>local-update-site</code> folder.</li></ul>

<blockquote><img src='http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/SoiToolkitUpdate2.png' /></blockquote>

<ul><li>Click on the Ok-button twice to close the two dialogs<br>
</li><li>Back in the "Install" - Dialog select the soi-toolkit update site<br>
</li><li>The soi-toolkit generator should be displayed, expand it and select the new version.</li></ul>

<blockquote><img src='http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/SoiToolkitInstallation3.png' /></blockquote>

<ul><li>Now follow the instruction from the initial installation of the plugin above.</li></ul>

<ul><li>After the update is done open the Mule Studio Preference page in the menu and select the "SOI-Toolkit Generator" and verify that your preference-settings are intact or if required reenter them again based on the snapshot you took in the first step above.