# Installation Guide, Extra Development Tools #

**Content**


# Introduction #

This page describes extra and/or outdated development tools that also can be used with soi-toolkit.

For not so experienced Maven users it is also strongly recommended to install the Maven Eclipse plugin for a very simple and transparent integration with Maven inside Eclipse.
We also recommend using the Subversive Eclipse plugin for a smooth integration with Subversion (if used) inside Eclipse, see section [Recommended installations](#Recommended_installations.md).


The installation guides below assumes your on a 32 bit Microsoft Windows PC.

The following versions (or newer) are recommended:
| **Tool** | **Version** |
|:---------|:------------|
|Eclipse EE|3.6.2 (Helios)|
|Maven plugin for Eclipse|0.10.0       |
|Subversive plugin for Subversion|0.7.9        |
|Subversive SVN Connectors|2.2.2        |

**Note:** Older version such as Java SE 5 and Eclipse 3.4, 3.5 should work without any problem (even though we don't test against these versions), but we strongly recommend using Maven 2.2.1 or newer since some older versions of Maven are known to produce incorrect GPG signatures and checksums .

# Extra installations #

## Eclipse ##

Install Eclipse according to the following instructions:

  * Download  `Eclipse IDE for Java EE Developers` from http://eclipse.org/downloads/.
> A file named `eclipse-jee-helios-SR1-win32.zip` is downloaded to your computer.

  * Unzip the zip file to the desired installation folder, eg C:\opt.

  * Create a shortcut to the file `eclipse.exe` (eg `C:\opt\eclipse\eclipse.exe`) in the installation folder to the desktop.

  * Verify the installation, start Eclipse through the desktop shortcut.
> Enter the desired workspace folder where requested and click on the "Workbench" icon (top right) when the welcome screen is presented. The the Eclipse workbench should be displayed and look like:

> ![http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/EclipseVerifyInstallation.png](http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/EclipseVerifyInstallation.png)



## Maven Eclipse plugin ##

Mavens Eclipse plugin makes Maven much easier to handle directly from within Eclipse when it comes to perform the most common tasks.

The plugin takes care of resolving maven dependencies automatically for you and and if you change any dependencies in yours pom-file the plugin automatically updates your eclipse projects classpath.
The plugin is also very convenient to use for making specific maven builds, e.g. perform a `mvn clean install` of your component or deploying a `war-sub-module` to Tomcat for tests, all without having to leave the Eclipse environment, e.g. without escaping to console windows.

However, if you really are into maven and at all times want to perform your maven commands on your own in a command window you are fully free to do so by simply not installing this plugin!

Install the Maven plugin for Eclipse according to the following instructions:

  * The installation is performed using the Eclipse "Update Site"-mechanism.
> The Update Site is: http://m2eclipse.sonatype.org/sites/m2e.

**NOTE:** In January 18th, 2011 v0.12.1 was released and we seen problems using this new version. Until this problem is resolved please install the plugin from the update site: "http://m2eclipse.sonatype.org/sites/archives/m2e-0.10.2.20100623-1649/". Verify that the version installed of the plugin is version 0.10.2.

If you need to downgrade you can uninstall the new version from the meny "Help --> Install New Software...", click on the link "already installed" (down right), select the plugin in the list, click on the "Uninstall..." - button (down mid) and follow the wizard that is displayed.

> For full installation instructions see http://m2eclipse.sonatype.org/installing-m2eclipse.html.

> Notes:
    * If the installation asks for approval of certificates, so be sure to select the available certificate before the next step in the installation.
    * Restart the workspace when so requested.

  * After the installation of the plugin open Eclipse Preferences
    * Navigate to the Maven Plugin and its Installations node
    * Click on the "Add..." button and add point out the home folder of your maven installation.
    * Ensure that your newly added maven installation is the selected one in the list.

> ![http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/MavenEclipsePluginInstallation.png](http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/MavenEclipsePluginInstallation.png)

  * To avoid the error message `"The Maven Integration requires that Eclipse be running in a JDK, because a number of Maven core plugins are using jars from the JDK. (...)"` follow the instructions on the following page http://blog.dawouds.com/2008/11/eclipse-is-running-in-jre-but-jdk-is.html


## Subversive Eclipse plugin ##

If you already use Subversion as your source control system the Subversive Eclipse plugin can help you to bring  Subversion client functionality into Eclipse.

The Subversive plugin is, due to license restrictions, divided into two separate installations, one for the Subversive plugin itself and one separate installation of the SVN connectors.

### Version check ###
To be able to install the SVN connector part you need to know what version of subversion you use.
Open a command window and enter the command `svn --version --quiet`.
The command will return something like `1.6.5`

### Install the plugin ###
  * The installation of the Eclipse plugin is performed using the Eclipse "Update Site"-mechanism.
> The Update Site is a part of Helios/Indigo Update Site.
  * Select the Eclipse menu "Help --> Install New Software...
  * In the field "Work with:" select the predefined update site "Helios - http://download.eclipse.org/releases/helios" for Eclipse 3.6, Indigo - http://download.eclipse.org/releases/indigo for Eclipse 3.7, Juno - http://download.eclipse.org/releases/juno/ for Eclipse 3.8
  * In the list below of available features expand the node "Collaboration"
  * Select:
    * Subversive SVN Team Provider (mandatory)
    * Subversive SVN JDT Ignore Extension (optional)
    * Subversive Revision Graph (optional)
  * Progress through the wizard as normal, approving licences and so on...

### Install the SVN connector ###

  * The installation of the SVN Connectors is performed automatically on the first usage of svn-functionality in Eclipse after the installation of the plugin.
    * A dialog named "Subversive SVN Connectors" dialog will popup.
    * Select appropriate svn connectors, e.g. "SVN Kit 1.3.5" if you have SVN 1.6.x installed in your environment and want to use a pure Java connector
> > (verify with you svn version check as described above).


> ![http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/SvnConnectors.png](http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/SvnConnectors.png)