**Content**



# Tabs/spaces #
Use spaces instead of tabs.

## Eclipse setup ##
For Java files, menu: Preferences/Java/Code Style/Formatter , edit profile, Indentation/Tab policy: "Spaces only".
For non-Java files, menu: Preferences/General/Editors/Text Editors/Insert spaces for tabs.

## Textmate ##
Select "Soft Tabs" from the "Tab Size"-menu in the status-bar at the bottom of an editor window.
http://manual.macromates.com/en/working_with_text


# Source encoding #
Use UTF-8.
## Maven setup ##
For Maven builds the source encoding is already declared in the parent pom: org.sonatype.oss:oss-parent
using property:
```
<project.build.sourceEncoding>UTF-8</project.build.sourceEncoding>
```

## Eclipse setup ##
Uses platform encoding by default.
To set UTF-8 (on a per workspace basis), use menu: "Preferences/General/Workspace/Text file encoding".

Note: source encoding can also be set on a per project basis but thats not recommended since we generate eclipse project files using Maven's eclipse-plugin - and the eclipse-plugin (v2.8) doesn't honour the Maven-defined source-encoding, i.e. re-generating eclipse-project files will drop any project specific settings.

## Textmate ##
UTF-8 is default. See menu: Preferences/Advanced/Saving/File Encoding.