<#    
.SYNOPSIS
  Issues Enable-PSRemoting on a remote system using WMI.
.DESCRIPTION
  This function will issue a WMI call to enable the
  PSRemoting opiton on the remote system.
  
AUTHOR
  Ben0xA
  
.PARAMETER computer
  THe remote system on which to enabled PSRemoting
  
.EXAMPLE
  PS> Set-PSRemoting -computer REMOTEPC

.LINK
   www.poshsec.com
.NOTES
  This function is a utility function for the PoshSec module.
#>

function Set-PSRemoting {
  Param(
    [Parameter(Mandatory=$true,Position=1)]
    [string]$computer
  )
  
  Execute-RemoteProcess $computer "powershell -ExecutionPolicy RemoteSigned" "Enable-PSRemoting -Force"
}