**Content**


# Customizing a Maven parent POM for Integration Components #

Integration components are generated with a default Maven parent POM (boxes below represent Maven pom.xml files):

> ![http://soi-toolkit.googlecode.com/svn/wiki/UserGuide/CustomMavenParentPom/default-parent-pom.png](http://soi-toolkit.googlecode.com/svn/wiki/UserGuide/CustomMavenParentPom/default-parent-pom.png)

The main purpose of the default parent POM is to provide Integration Components with:
  * build repeatability by specifying Maven plugin versions in the `<pluginManagement>` section
  * classpath stability by specifying versions of common dependencies in the `<dependencyManagement>` section


## Reasons for using a custom parent POM ##
The main reasons for replacing the default parent POM with a custom POM for your organization are probably:
  1. If you are deploying/releasing to a Maven repository you need to add a `<distributionManagement>` section to the POM.
    * You likely want to use the same `<distributionManagement>` information for all Integration Components in your organization so you would typically only want to define it in one place, which would be in the parent POM for all Integration Components.
  1. If your organization already has an organization wide, top-level POM that you want to re-use.

## Options for customizing a parent POM ##

### Option 1: There is an existing organization-wide parent POM that you want to use ###
In this case you probably want to define a custom parent-POM that has the organization POM as a parent, since you probably don't want to update the organizational POM for the requirements put up by the default parent POM. You also probably don't wan't to update the organizational POM for new releases of SOI-Toolkit that might require changes to a custom parent POM.

![http://soi-toolkit.googlecode.com/svn/wiki/UserGuide/CustomMavenParentPom/adapt-to-organization-parent-pom.png](http://soi-toolkit.googlecode.com/svn/wiki/UserGuide/CustomMavenParentPom/adapt-to-organization-parent-pom.png)

**Note:** Great care must be taken when designing the custom parent in this case, for example it might have to override behavior defined in the organization-pom to honor behavior required by the `soitoolkit-default-parent`-pom (which should be seen as a the minimal contract to fulfill).

**Remember:** Remove the generated `<distributionManagement>`-section from your myIntegrationComponent/pom.xml.

**When not to use:**
  * Do not use this option if your organization-pom is very complex and/or you are not very confident using Maven.
  * Do not use this option if your organization-pom declares dependencies (which parent-poms generally shouldn't do). Such inherited dependencies might cause problems.
In any of the above cases you should explore the other options for customization.


### Option 2: There is no existing parent POM that you want to use ###
This is probably the simplest way since you don't need to care about definitions (possibly conflicting) from any other POM.
You replace the `soitoolkit-default-parent`-pom with a standalone integration component parent for your organization (without trying to adapt to any general organization-wide pom).

![http://soi-toolkit.googlecode.com/svn/wiki/UserGuide/CustomMavenParentPom/standalone-custom-parent-pom.png](http://soi-toolkit.googlecode.com/svn/wiki/UserGuide/CustomMavenParentPom/standalone-custom-parent-pom.png)

In this case the simplest thing to do is to take a copy of the default parent POM provided by SOI-Toolkit, change artifactId and groupId in the copy to fit your organisation, and then add any customizations to the POM (like `<distributionManagement>`-information for your organisation).

**Remember:** Remove the generated `<distributionManagement>`-section from your myIntegrationComponent/pom.xml.


### Option 3: Do not want to change parent-pom at all ###
This option isn't a customization of any parent-pom at all but rather a way to still use the `soitoolkit-default-parent`-pom and still be able to add for example `<distributionManagement>`-information for your organisation.

Add configuration directly to myIntegrationComponent/pom.xml, for example it has a default-generated `<distributionManagement>`-section that you can use.

![http://soi-toolkit.googlecode.com/svn/wiki/UserGuide/CustomMavenParentPom/custom-ic-pom.png](http://soi-toolkit.googlecode.com/svn/wiki/UserGuide/CustomMavenParentPom/custom-ic-pom.png)

**Note:** This option isn't recommended for long term use but allows for quick testing before moving configuration up to a parent-pom that is common for all your integration components.

**When not to use:**
  * Do not use this option as a permanent solution if you have more than one integration component since it duplicates configuration, possibly creating maintenance problems.