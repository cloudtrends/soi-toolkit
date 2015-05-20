**Content**



# Install SFTP-server, Cygwin with OpenSSH #
  1. Download setup.exe from http://www.cygwin.com/ and save the file to "c:\cygwin\cygwin.install" (you will have to create the directory).
  1. Run setup.exe and follow instructions below:

![http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/CygwinSetup/cygwin-setup-1.png](http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/CygwinSetup/cygwin-setup-1.png)

![http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/CygwinSetup/cygwin-setup-2.png](http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/CygwinSetup/cygwin-setup-2.png)

![http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/CygwinSetup/cygwin-setup-3.png](http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/CygwinSetup/cygwin-setup-3.png)

![http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/CygwinSetup/cygwin-setup-4.png](http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/CygwinSetup/cygwin-setup-4.png)

![http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/CygwinSetup/cygwin-setup-5.png](http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/CygwinSetup/cygwin-setup-5.png)

![http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/CygwinSetup/cygwin-setup-6-openssh.png](http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/CygwinSetup/cygwin-setup-6-openssh.png)

![http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/CygwinSetup/cygwin-setup-7.png](http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/CygwinSetup/cygwin-setup-7.png)


# Configure home-directory #
**Note:** This step is necessary if roaming profiles are used in Windows.

  1. Start Cygwin
  1. Open "c:\cygwin\etc\passwd" with a text editor (Notepad for example)
  1. In passwd, change home-dir from "/home" to "C:\Documents and Settings" (example below for user id: dfc0364).
> Change:
```
dfc0364:unused_by..........-1851:/home/dfc0364:/bin/bash
```
> to:
```
dfc0364:unused_by..........-1851:/cygdrive/c/Documents and Settings/dfc0364:/bin/bash
```
  1. Close Cygwin


# Configure SSH-server #
Configure the SSH-server (sshd) in a Cygwin window:
```
$ ssh-host-config
*** Info: Generating /etc/ssh_host_key
*** Info: Generating /etc/ssh_host_rsa_key
*** Info: Generating /etc/ssh_host_dsa_key
*** Info: Creating default /etc/ssh_config file
*** Info: Creating default /etc/sshd_config file
*** Info: Privilege separation is set to yes by default since OpenSSH 3.3.
*** Info: However, this requires a non-privileged account called 'sshd'.
*** Info: For more info on privilege separation read /usr/share/doc/openssh/README.privsep.
*** Query: Should privilege separation be used? (yes/no) yes
*** Info: Updating /etc/sshd_config file


*** Warning: The following functions require administrator privileges!

*** Query: Do you want to install sshd as a service?
*** Query: (Say "no" if it is already installed as a service) (yes/no) yes
*** Info: Note that the CYGWIN variable must contain at least "ntsec"
*** Info: for sshd to be able to change user context without password.
*** Query: Enter the value of CYGWIN for the daemon: [ntsec]

*** Info: The sshd service has been installed under the LocalSystem
*** Info: account (also known as SYSTEM). To start the service now, call
*** Info: `net start sshd' or `cygrunsrv -S sshd'.  Otherwise, it
*** Info: will start automatically after the next reboot.

*** Info: Host configuration finished. Have fun!
```


# Start the SSH-service #
Start the SSH-server (sshd) in a Cygwin window:
```
$ cygrunsrv --start sshd
```


# Generate SSH-keys #
**Note:** Remember your passphrase, you will have to configure it in your service components security-property-file in order to connect to your local SFTP-server.

Generate the SSH-keys to use for public-key cryptography in a Cygwin window:
```
$ ssh-keygen -t dsa
Generating public/private dsa key pair.
Enter file in which to save the key (/cygdrive/c/Documents and Settings/dfc0364/.ssh/id_dsa):
Enter passphrase (empty for no passphrase):
Enter same passphrase again:
Your identification has been saved in /cygdrive/c/Documents and Settings/dfc0364/.ssh/id_dsa.
Your public key has been saved in /cygdrive/c/Documents and Settings/dfc0364/.ssh/id_dsa.pub.
The key fingerprint is:
07:5c:3f:f6:8e:b5:91:de:02:5c:c3:c8:3a:04:3f:aa dfc0364@dse31673
The key's randomart image is:
+--[ DSA 1024]----+
|        . .      |
|       . + o o   |
|        o + * +  |
|         + = + o |
|        S + o =  |
|       . . . * + |
|      E     . = .|
|               . |
|                 |
+-----------------+

dfc0364@dse31673 ~
$ cat ~/.ssh/id_dsa.pub >> ~/.ssh/authorized_keys2
```


# Test login to SFTP-server #
Test to login to your local SFTP-server in a Cygwin window (example for user id: dfc0364):
```
$ sftp dfc0364@localhost
Connecting to localhost...
Enter passphrase for key '/cygdrive/c/Documents and Settings/dfc0364/.ssh/id_dsa':
sftp> exit
```


# Uninstall the SSH-service #
If you want to uninstall the SSH-service, open up Cygwin and execute commands:
```
cygrunsrv --stop sshd
cygrunsrv --remove sshd
```