# Configuration Guide for use of public key cryptography for the SFTP transport #

**Content**


## Setup of public key cryptography for the SFTP transport ##

When using the SFTP transport it is strongly recommended to not use traditional username/passwords but instead use [public key cryptography](http://en.wikipedia.org/wiki/Public-key_cryptography).

The instruction below is based on [DSA](http://en.wikipedia.org/wiki/Digital_Signature_Algorithm) keys but [RSA](http://en.wikipedia.org/wiki/RSA) keys can be used as well.

**NOTE:** The instructions for creating keys are based on creating the keys on a unix/linux machine and not a Windows PC. Once the keys are created they can be used on a Windows PC. We are working on instructions for using Cygwin in a Windows environment but are stuck on some version issues, see [issue 34](http://code.google.com/p/soi-toolkit/issues/detail?id=34).

  * **Verify proper security settings on files and folders**
> Ssh is quite picky on security settings (not unexpected :-) so before trying to create keys verify the following permission settings:
    * home folder: 711
    * folder .ssh: 700
    * file authorized\_keys, authorized\_keys2: 600
    * private keys (id\_dsa,id\_rsa): 600
    * public keys (id\_dsa.pub,id\_rsa.pub): 644
    * Use the chmod command to correct if any of the files or folders are too open, e.g.
```
chmod 700 .ssh
```

  * **Create a pair of private and public keys**

> Execute the command `ssh-keygen -t dsa`, accept default filename and enter a proper passphrase of your selection.

> The files `id_dsa` and `id_dsa.pub` are now created in your .ssh - folder.

> NOTE: Never give away your private key `id_dsa`, only share the public one `id_dsa.pub`!
```
MagnusMac-2:~ magnuslarsson$ ssh-keygen -t dsa
Generating public/private dsa key pair.
Enter file in which to save the key (/Users/magnuslarsson/.ssh/id_dsa): 
Enter passphrase (empty for no passphrase): 
Enter same passphrase again: 
Your identification has been saved in /Users/magnuslarsson/.ssh/id_dsa.
Your public key has been saved in /Users/magnuslarsson/.ssh/id_dsa.pub.
The key fingerprint is:
31:de:60:62:de:d0:44:b6:90:b6:b9:60:82:aa:d1:da magnuslarsson@MagnusMac-2.local

MagnusMac-2:~ magnuslarsson$ ls /Users/magnuslarsson/.ssh/id_dsa
/Users/magnuslarsson/.ssh/id_dsa

MagnusMac-2:~ magnuslarsson$ ls /Users/magnuslarsson/.ssh/id_dsa.pub
/Users/magnuslarsson/.ssh/id_dsa.pub
```


  * **Copy and activate public key to remote sftp-machines**
> To be able to authenticate yourself using your private key you must copy your public key to the accounts on the remote machines that you want to be able to use.
> Typically you have to copy the public file using the accounts username and password as:
```
MagnusMac-2:~ magnuslarsson$ scp ~/.ssh/id_dsa.pub user@sftpHost:
user@sftpHost's password:
id_dsa.pub                                                        100%  606     0.6KB/s   00:00
```
> Now you have to activate the public key by adding it to a list of known public keys.
> This is typically done by logging in to the remote machine and perform the activation by appending the public key to the file `~/.ssh/authorized_keys2`
> The commands below also create the `.ssh` folder, assigns correct permissions if not already existing, the same for the `~/.ssh/authorized_keys2` - file and finally it deletes the public key that no longer is of use.
```
MagnusMac-2:~ magnuslarsson$ ssh user@sftpHost
user@sftpHost's password:
sftpHost:~ user$ mkdir ~/.ssh
sftpHost:~ user$ chmod 700 ~/.ssh
sftpHost:~ user$ cat id_dsa.pub >> ~/.ssh/authorized_keys2
sftpHost:~ user$ chmod 600 ~/.ssh/authorized_keys2
sftpHost:~ user$ rm id_dsa.pub
sftpHost:~ user$ exit
logout
Connection to localhost closed.
MagnusMac-2:~ magnuslarsson$ 
```


  * **Validate setup**
> It should now be possible to login and start a sftp session to the remote sftp server without specifying any username.
> In the example below we use account `muletest1` on sftp-server `localhost`:
```
MagnusMac-2:.ssh magnuslarsson$ ssh muletest1@localhost
Enter passphrase:
Last login: Mon Oct 11 10:55:25 2010 from localhost
MagnusMac-2:~ muletest1$ pwd  
/Users/muletest1
MagnusMac-2:~ muletest1$ exit
logout
Connection to localhost closed.
MagnusMac-2:.ssh magnuslarsson$ 

MagnusMac-2:.ssh magnuslarsson$ sftp muletest1@localhost
Enter passphrase:
Connecting to localhost...
sftp> pwd
Remote working directory: /Users/muletest1
sftp> bye
MagnusMac-2:.ssh magnuslarsson$ 
```


  * **Configure default sftp-settings in soi-toolkit source code generator**
> To simplify setup of sftp-configuration in your development environment the soi-toolkit source code generator allows you to specify your favorite sftp-server, your private key location and your passphrase so you don't have to specify it over and over again while you create new sftp-based services and integrations.
    * Open Eclipse Preference page in the menu and select the "SOI-Toolkit Generator"
    * Specify the root folder of the sftp server you will use most frequently during development
> > E.g. `muletest1@localhost/~/sftp`
    * Specify the name of your private key
> > E.g. `/Users/magnuslarsson/.ssh/id_dsa`
    * Specify your passphrase for the private key.
    * Click on the "OK" button to conclude the configuration.


> ![http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/SoiToolkitInstallation8.png](http://soi-toolkit.googlecode.com/svn/wiki/InstallationGuide/SoiToolkitInstallation8.png)