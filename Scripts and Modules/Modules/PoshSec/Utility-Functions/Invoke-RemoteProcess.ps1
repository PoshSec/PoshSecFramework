<#    
.SYNOPSIS
  Executes a process on a remote system.
.DESCRIPTION
  This function will use the process call create
  to execute a process on a remote system.
  
AUTHOR
  Ben0xA
  
REQUIRES
  Get-RemotePSVersion
  Invoke-RemotePSExec
  Invoke-RemoteWmiProcess
  
.PARAMETER computer
  The remote system on which to start the process.
  
.PARAMETER command
  The application or command to execute on the remote system.
  
.EXAMPLE
  PS> $output = Invoke-RemoteProcess -computer REMOTEPC -command "C:\Windows6.1-KB2506143-x64.msu /quiet /norestart"
  
.EXAMPLE
  PS> $output = Invoke-RemoteProcess REMOTEPC "C:\Windows6.1-KB2506143-x64.msu /quiet /norestart"
  
.EXAMPLE
  PS> $output = Invoke-RemoteProcess REMOTEPC "C:\Windows6.1-KB2506143-x64.msu /quiet /norestart" "C:\PSTools\PSExec.exe"

.LINK
   www.poshsec.com
.NOTES
  This function is a utility function for the PoshSec module.
#>

function Invoke-RemoteProcess{
  Param(
    [Parameter(Mandatory=$true,Position=1)]
    [string]$computer,
    
    [Parameter(Mandatory=$true,Position=2)]
    [string]$command,
    
    [Parameter(Mandatory=$false,Position=3)]
    [string]$psexecpath=""
  )
  
  #set this to default to 1
  $version = "1"
  $rtn = $null
  
  #Determine PSVersion
  #1 use PSExec or WMI
  #2+ attempt invoke-command then psexec then wmi.
  
  $psv = Get-RemotePSVersion $computer
  
  if($psv.Count -gt 0) {
    #always get the last entry which has the greatest version installed
    $tmpver = $psv[-1].Key
    if($tmpver -ne $null) {
      $version = $tmpver
    }    
  }
  
  if($version -eq "1") {
    #attempt PSExec first
    if($psexecpath -ne "") {
      $rtn = Invoke-RemotePSExec $psexecpath $computer $command
    }   
    
    #if null then attempt wmi win32_process call
    if($rtn -eq $null) {
      $rtn = Invoke-RemoteWmiProcess $computer $command
    }
  }
  else {
    #attempt Invoke-Command
    $rtn = Invoke-RemoteCommand $computer $command
    
    #attempt psexec if null
    if($rtn -eq $null) {
      if($psexecpath -ne "") {
        $rtn = Invoke-RemotePSExec $psexecpath $computer $command
      }
    
      #if null then attempt wmi win32_process call
      if($rtn -eq $null) {
        $rtn = Invoke-RemoteWmiProcess $computer $command
      }
    }
  }
  
  if($rtn -eq $null) {
    #default return object when all methods fail
    $rtn = New-Object PSObject
    $rtn | Add-Member -MemberType NoteProperty -Name "Computer" -Value $computer
    $rtn | Add-Member -MemberType NoteProperty -Name "Command" -Value $command
    $rtn | Add-Member -MemberType NoteProperty -Name "ExecuteMethod" -Value "None"
    $rtn | Add-Member -MemberType NoteProperty -Name "Details" -Value ""
    $rtn | Add-Member -MemberType NoteProperty -Name "Errors" -Value "All methods failed to execute on the remote machine."
  }
  
  return $rtn
  
  # 
  # $process = Get-RemoteProcess $computer
  # 
  # if($process) {
  #   if($arguments) {
  #     $command = $command + " " + '"' + $arguments + '"'
  #   }
  #   
  #   return $process.Create($command)
  # }
}