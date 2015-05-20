# Integration testing #

**Content**



## Setting test timeout ##
If an integration test times out too soon (typically if you are debugging) you can override the default time out by setting:
```
-Dmule.test.timeoutSecs=XX
```
in your environment.

Examples:

1. Increasing the timeout to two minutes when using Maven in a command window:

```
mvn clean test -Dtest=UpdateIntegrationTest -Dmule.test.timeoutSecs=120
```

2. Increasing the timeout to two minutes when running Maven builds from Jenkins:

![http://soi-toolkit.googlecode.com/svn/wiki/UG_IncreaseTestTimeoutJenkins.png](http://soi-toolkit.googlecode.com/svn/wiki/UG_IncreaseTestTimeoutJenkins.png)