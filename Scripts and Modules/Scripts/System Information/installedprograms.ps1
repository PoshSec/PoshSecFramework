<#
.DESCRIPTION
  Lists all of the applications that are installed on the system.

AUTHOR
Ben0xA

.PARAMETER showintab
  Specifies whether to show the results in a PoshSec Framework Tab.

.PARAMETER storedhosts
  This is for storing hosts from the framework for scheduling.

.NOTES
  pshosts=storedhosts
#>

Param(	
	[Parameter(Mandatory=$false,Position=1)]
	[boolean]$showintab=$True,
  
  [Parameter(Mandatory=$false,Position=2)]
	[string]$storedhosts
)
#Required to use PoshSec functions
Import-Module $PSModRoot\PoshSec

#Start your code here.
$progs = @()
$installedprogs = @()

if($storedhosts) {
  #The storedhosts have been serialized as a string
  #Before we use them we need to deserialize.
  $hosts = $PSHosts.DeserializeHosts($storedhosts)
}
else {
  $hosts = $PSHosts.GetHosts()
}

if($hosts) {
  foreach($h in $hosts) {
    $PSStatus.Update("Querying $($h.Name), please wait...")
    if($h.Status -eq "Up") {      
      $progs = Get-RemoteRegistryKey $h.Name 3 "Software\Microsoft\Windows\CurrentVersion\Uninstall\"
      if($progs) {
        $idx = 1
        foreach($p in $progs) {
          $PSStatus.Update("Adding $idx out of $($progs.Length) on $($h.Name), please wait...")
          $progdata = Get-RemoteRegistryValue $h.Name 3 "$($p.Path)$($p.Key)"
          $instprog = New-Object PSObject
          $instprog | Add-Member -MemberType NoteProperty -Name "Computer" -Value $p.Computer
          $rslt = $progdata | Where { $_.Name -eq "DisplayName"}
          if($rslt) {
            $instprog | Add-Member -MemberType NoteProperty -Name "DisplayName" -Value $rslt.Value
          }
          else {
            $instprog | Add-Member -MemberType NoteProperty -Name "DisplayName" -Value $p.Key
          }
          $instprog | Add-Member -MemberType NoteProperty -Name "DisplayVersion" -Value $($progdata | Where { $_.Name -eq "DisplayVersion"} | Select -ExpandProperty Value)
          $instprog | Add-Member -MemberType NoteProperty -Name "InstallLocation" -Value $($progdata | Where { $_.Name -eq "InstallLocation"} | Select -ExpandProperty Value)
          $instprog | Add-Member -MemberType NoteProperty -Name "InstallDate" -Value $($progdata  | Where { $_.Name -eq "InstallDate"} | Select -ExpandProperty Value)
          $instprog | Add-Member -MemberType NoteProperty -Name "InstallSource" -Value $($progdata  | Where { $_.Name -eq "InstallSource"} | Select -ExpandProperty Value)
          $installedprogs += $instprog
          $idx += 1
        }
      }
    }    
  } 
  
  if($installedprogs) {
    $installedprogs = $installedprogs | Sort-Object Computer, DisplayName
    if($showintab) {
      $PSTab.AddObjectGrid($installedprogs, "Installed Programs")
      Write-Output "Installed Programs Tab Created."
    }
    else {
      $installedprogs | Out-String
    }
  }   
  else {
    Write-Output "Unable to find any installed programs"
  }
}
else {
  Write-Output "Please select the hosts in the Systems tab to scan."
}

#End Script