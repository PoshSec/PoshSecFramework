<#
.DESCRIPTION
Lists the architecture and OS for all hosts selected in Systems.

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
$archs = @()

$hosts = $PSHosts.GetHosts()

if($hosts) {
  foreach($h in $hosts) {
    if($h.Status -eq "Up") {
      $PSStatus.Update("Querying " + $h.Name + "...")
      $arch = Get-RemoteArchitecture $h.Name
      if($arch) {
        $archs += $arch
      }      
    }    
  }
  
  if($archs.count -gt 0) {
    if($showintab) {
      $PSTab.AddObjectGrid($archs, "System Architecture")
      Write-Output "System Architecture Tab Created."
    }
    else {
      $archs | Out-String
    }    
  }
  else {
    Write-Output "Unable to find any system architecture information."
  }
}
else {
  Write-Output "Please select the hosts in the Systems tab to scan."
}

#End Script