# Installation Guide for Apache Derby #

**Content**


## Installation instructions ##

If you want to run manual tests on your PC for services where JDBC is involved it can be useful to run those tests with an external JDBC provider, i.e. not a JDBC provider that is embedded as when you run unit tests.

Install Apache Derby by:
  * Go to http://db.apache.org/derby/derby_downloads.html
    * Select the latest version
    * Download db-derby-`<VERSION>`-bin.zip
  * Unpack the downloaded zip-file, on Windows for example to: C:\program

Start/stop Apache Derby:
  * Open the bin-folder (db-derby-`<VERSION>`-bin/bin)
  * Start by running startNetworkServer.bat (on Windows)
  * Stop by running stopNetworkServer.bat (on Windows)

Connecting to Derby:
  * Connect using an URL like (with your prefered databasename): jdbc:derby://localhost:1527/myDatabaseNameDb;create=true