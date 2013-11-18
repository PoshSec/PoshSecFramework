<#
.DESCRIPTION
  Lists all of the applications that are currently running.

AUTHOR
Ben0xA

.PARAMETER showintab
  Specifies whether to show the results in a PoshSec Framework Tab.

.PARAMETER storedhosts
  This is for storing hosts from the framework for scheduling.
  
.PARAMETER processname
  The name of the process.

.NOTES
  pshosts=storedhosts
#>

Param(	
	[Parameter(Mandatory=$false,Position=1)]
	[boolean]$showintab=$True,
  
  [Parameter(Mandatory=$false,Position=2)]
	[string]$storedhosts,
  
  [Parameter(Mandatory=$false,Position=3)]
	[string]$processname
)
# Begin Script Flow

#Leave this here for things to play nicely!
Import-Module $PSFramework

#Start your code here.
$processes = @()

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
    $processes += Get-SecRunningProcess $h.Name $processname
  }
  
  if($processes) {
    if($showintab) {
      $PSTab.AddObjectGrid($processes, "Running Programs")
      Write-Output "Running Programs Tab Created."
    }
    else {
      $processes | Out-String
    }    
  }
  else {
    Write-Output "Unable to find any running programs"
  }
}
else {
  Write-Output "Please select the hosts in the Systems tab to scan."
}

#End Script