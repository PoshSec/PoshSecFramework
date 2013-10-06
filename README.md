PoshSec Framework
========
For the Scripts and Modules to work, please refer to the below documentation.

<b>Note: Minimum of PowerShell 3.0 is required at this time.</b>

If you are using the source code:
==
Open the poshsecframework.sln in Visual Studio 2012+ and build the project (F7). This will create the bin\Release folder at {your github repodirectory}\poshsecframework\poshsecframework\bin\Release.

Copy the "\Modules" and the "\Scripts" folder from the "\Scripts and Modules" folder to the bin\Release\ directory.

For example: 

C:\github\poshsecframework\poshsecframework\bin\Release\Modules\

C:\github\poshsecframework\poshsecframework\bin\Release\Scripts\

If you are using the Binary:
==
The binary folder has poshsecframework.zip.<br />
Extact the zip file to a directory of your choosing and just run the poshsecframework.exe. <br />You do not need to move the Scripts\ or Modules\ folder.


If you are using the Installer:
==
Copy the Modules and the Scripts folder from the "Scripts and Modules" folder to the installation directory.

C:\Program Files\PoshSecFramework\Modules\

C:\Program Files\PoshSecFramework\Scripts\

Execution-Policy
==
You will need to issue the following command in a PowerShell console running as Administrator if you have never run scripts on your system previously.
```
Set-ExecutionPolicy RemoteSigned
```
Then type "Y" when prompted.

Unblocking Files
==
For some systems, you may need to "unblock" the files. We are working on putting this directly into psf, but for now you need only to type the following command in a PowerShell console:
```
Get-ChildItem -recurse <path to poshsecframework> | Unblock-File
```
Example:
```
Get-ChildItem -recurse "C:\github\poshsecframework\poshsecframework" | Unblock-File
~or~
Get-ChildItem -recurse "C:\Program Files\PoshSecFramework\ | Unblock-File"
```


About PoshSec
========
This project started by Will Steele (@pen_test) and Matt Johnson (@mwjcomputing) has several goals:

- Publish a PowerShell module to aid people in the use of PowerShell in regards to security.
- Provide some guidance on how to use PowerShell in the information security space, on both the offensive and defensive side with blog posts and articles.
- Be a location to obtain links to others using PowerShell in the information security space.

The PoshSec Framework was 

Current "Core" Developers are:
* Matt Johnson - @mwjcomputing - our fearless leader!
* Ben Ten - @ben0xa - Primary Developer of the PoshSec Framework

In alphabetical order:
* Bryan Smith - @tweetbsmith
* J Wolfgang Goerlich - @jwgoerlich
* Michael Ortega - @securitymoey
* Nick Jacob - @MortiousPrime 
* Rich Cassara - @rjcassara

PoshSec is about supporting the community, empowering the community, and strengthening the security posture of organizations.


Contact
==========
* www.poshsec.com
* You can offer your support by emailing team@poshsec.com
* Twitter: @poshsec
* #PoshSec on FREENODE

[![githalytics.com alpha](https://cruel-carlota.pagodabox.com/5629ba33057907958b34e4e40bbefff0 "githalytics.com")](http://githalytics.com/PoshSec/PoshSec)
