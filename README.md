PoshSecFramework
================

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