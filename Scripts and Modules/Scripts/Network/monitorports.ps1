<#
.DESCRIPTION
This script is for active monitoring of ports on a specified machine.

FRAMEWORK
PoshSec Framework

FRAMEWORKVERSION
0.2.0.0

AUTHOR
Ben0xA
#>
Param(
    [Parameter(Mandatory=$false,Position=1)]
    [string]$computer=""
  )

#Required to use PoshSec functions
Import-Module $PSModRoot\PoshSec
  
[boolean]$scan = $True;
$baseline = @()
$active = @()
$remoteportwhitelist = @(0,995,80,443)
$processwhitelist = @("ssh-agent", "firefox", "tweetdeck", "thunderbird", "Idle")
$localipwhitelist = @("127.0.0.1")
$remoteipwhitelist = @("127.0.0.1")
$compname = $computer
if($computer -eq "") {
  $compname = Get-Content env:ComputerName
}

$PSStatus.Update("Setting a baseline on $compname.")
$baseline = Get-SecOpenPorts $computer
do
{
  $PSStatus.Update("Pausing for 2 seconds")
  Start-Sleep -s 2
  
  $PSStatus.Update("Getting current ports on $compname.")
  $active = Get-SecOpenPorts $computer
  
  $rslts = Compare-SecOpenPort $baseline $active
  
  foreach($rslt in $rslts)
  {
    if(($rslt.InputObject.State -eq "ESTABLISHED") -and
        ($rslt.SideIndicator -eq "=>") -and
        ($remoteportwhitelist -notcontains $rslt.InputObject.RemotePort) -and
        ($processwhitelist -notcontains $rslt.InputObject.ProcessName) -and
        ($localipwhitelist -notcontains $rslt.InputObject.LocalAddress) -and
        ($remoteipwhitelist -notcontains $rslt.InputObject.RemoteAddress))
    {
      $protocol = $rslt.InputObject.Protocol
      $local = $rslt.InputObject.LocalAddress + ":" + $rslt.InputObject.LocalPort
      $remote = $rslt.InputObject.RemoteAddress + ":" + $rslt.InputObject.RemotePort
      $pname = $rslt.InputObject.ProcessName
      
      $PSAlert.Add("[$compname]Port Opened: $protocol $($local)<=>$($remote) ($pname)", 2)
      $baseline += $rslt.InputObject
    }
    elseif(($rslt.SideIndicator -eq "<=") -and
        ($remoteportwhitelist -notcontains $rslt.InputObject.RemotePort) -and
        ($processwhitelist -notcontains $rslt.InputObject.ProcessName) -and
        ($localipwhitelist -notcontains $rslt.InputObject.LocalAddress) -and
        ($remoteipwhitelist -notcontains $rslt.InputObject.RemoteAddress))
    {
      $protocol = $rslt.InputObject.Protocol
      $local = $rslt.InputObject.LocalAddress + ":" + $rslt.InputObject.LocalPort
      $remote = $rslt.InputObject.RemoteAddress + ":" + $rslt.InputObject.RemotePort
      $pname = $rslt.InputObject.ProcessName
      
      $PSAlert.Add("[$compname]Port Closed: $protocol $($local)<=>$($remote) ($pname)",0)
      
      # You can't remove items from an array. You have to rebuild it.
      [int]$blidx = 0
      $newbl = @()
      $rsobj = $rslt.InputObject
      $rsstr = $rsobj.Protocol + $rsobj.LocalAddress + $rsobj.LocalPort + $rsobj.RemoteAddress + $rsobj.RemotePort + $rsobj.ProcessName
      do
      {
        $blobj = $baseline[$blidx]        
        $blstr = $blobj.Protocol + $blobj.LocalAddress + $blobj.LocalPort + $blobj.RemoteAddress + $blobj.RemotePort + $blobj.ProcessName
        if($blstr -ne $rsstr)
        {
            $newbl += $blobj
        }
        $blidx++
      } while (($blidx -lt $baseline.count))
      $baseline = $newbl
      $newbl = $null
    }
  }
} while ($scan)


#End Script