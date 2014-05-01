PoshSec Framework
========
For the Scripts and Modules to work, please refer to the below documentation.

The PoshSec Framework (or PSF) is a graphical front end utility for running PowerShell scripts, modules, and cmdlets. The PSF exposes parts of it's interface to PowerShell within the individualized PSessions. Each script or command that is executed is executed in a separate thread which allows you to run multiple scripts simultaneously.

About
===
The PoshSec Framework is primarily developed by Ben Ten (Ben0xA). The project is open to other developers who would like to contribute to the project as well. This project is directly linked to the PoshSec project. The PoshSec project is maintained by Matt Johnson (mwjcomputing) and is actively developed by multiple developers. These developers are listed below.

Pre-Requisites
===
The PoshSec Framework is designed to work with PowerShell 3.0 and works with PowerShell 2.0. While PSF will work with earlier versions of PowerShell, PSF requires .NET 4.5 which comes standard with PowerShell 3.0. These are the recommended pre-requisites to use PSF:

- Microsoft .NET Framework v4.5
	Download from http://microsoft.com/blah
- Microsoft PowerShell 3.0 or later
	Download from http://microsoft.com/blah

Please note that while it is not required for 'most' scripts that PowerShell be deployed on the target system, it must be installed and properly configured on the same machine that the PoshSec Framework is installed on.

First-Steps
===
If this is your first time utilizing PowerShell scripts, functions, or cmdlets, there are a few housekeeping things that must be done first. These need to happen regardless of whether you utilize the PoshSec Framework or if you invoke the script, function, or cmdlet yourself. Note: The 'First Time Utility' built within PSF will do all of these steps automatically for you. However, those steps are outlined below in detail for your knowledge or if you wish to do these steps by yourself instead.

- Set-ExecutionPolicy
- Unblock-File
- Update-Help

First-Time Utility
---
The first time utility in PoshSec Framework will do the steps listed above automatically. It will also:

- Ensure the Settings are correct.
- Create any required directories for the scripts, modules, and logging.
- Download the PoshSec project directly from Github under the PoshSec branch.

Please note: You can choose which steps to perform in the First Time utility or simply press the buttoned that says "Do not do anything, I'll do it myself".

Set-ExecutionPolicy
---
PowerShell by default has the ExecutionPolicy, which determines which scripts are able to be run, to "None". This prevents unsigned scripts from being executed on your system.  When you are utilizing scripts from other projects, like PoshSec, you will need to have this policy set to "RemoteSigned". The RemoteSigned policy is designed for scripts that you develop or that are developed by others. This command must be run under local administrator privileges.

<code>
ps C:\> Set-ExecutionPolicy RemoteSigned -Force
</code>

Unblock-File
---
With the release of Windows 7 and later Operating Systems from Microsoft, they introduced a "Block" on any file that is downloaded from the internet. This will cause User Access Control (UAC) to pop up when that downloaded file is executed or ran. When you use scripts, functions, or cmdlets from remote sites they will need to have this "Block" flag removed before you can run them from within PowerShell or PSF.

The Unblock-File Cmdlet removes this flag. The simplest way to Unblock multiple files in a directory is by iterating the directory and passing each file to the Unblock-File Cmdlet.

<code>
ps C:\> Get-ChildItem <path to blocked files> | ForEach-Object { Unblock-File $_.Name }
</code>

Update-Help
---
The PoshSec Framework utilizes the Get-Help Cmdlet from within PowerShell. This is used so that it can see if any command or script has Parameters. Before you can use Get-Help, it must be updated at least once. To do this you use the Update-Help Cmdlet. Note: You need an active internet connection to update the help.

<code>
ps C:\> Update-Help -Force
</code>
=======
<b>Note: Minimum of PowerShell 3.0 is required at this time.</b>

PowerShell 3.0 First Time Users
==
You need to make sure you run "Update-Help" in PowerShell Command Shell the first time you load. This is required for the framework to work properly!

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
* Bryan Smith - @securekomodo
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
