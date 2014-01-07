<#
.DESCRIPTION
Lists the .NET versions for all hosts selected in Systems.

AUTHOR
Ben0xA

.PARAMETER showintab
Specifies whether to show the results in a PoshSec Framework Tab.
#>

Param(	
	[Parameter(Mandatory=$false,Position=1)]
	[boolean]$showintab=$True
)
#Required to use PoshSec functions
Import-Module $PSModRoot\PoshSec

#Start your code here.
$vers = @()

$hosts = $PSHosts.GetHosts()

if($hosts) {
  foreach($h in $hosts) {
    if($h.Status -eq "Up") {
      $PSStatus.Update("Querying " + $h.Name + "...")
      $ver = Get-RemoteNETVersion $h.Name
      if($ver) {
        $vers += $ver
      }      
    }    
  }
  
  if($vers.count -gt 0) {
    if($showintab) {
      $PSTab.AddObjectGrid($vers, ".NET Versions")
      Write-Output ".NET Versions Tab Created."
    }
    else {
      $vers | Out-String
    }    
  }
  else {
    Write-Output "Unable to find any .NET Version information."
  }
}
else {
  Write-Output "Please select the hosts in the Systems tab to scan."
}

#End Script