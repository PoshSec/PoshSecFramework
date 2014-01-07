<#
.DESCRIPTION
  Lists all of the applications that are set to starup in the \Run
  folder of the registry.

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
    $progs +=  Get-RemoteRegistryValue $h.Name 3 "Software\Microsoft\Windows\CurrentVersion\Run\"
    $progs +=  Get-RemoteRegistryValue $h.Name 3 "Software\Wow6432Node\Microsoft\Windows\CurrentVersion\Run\"
  }
  
  if($progs) {
    if($showintab) {
      $PSTab.AddObjectGrid($progs, "Startup Programs")
      Write-Output "Startup Programs Tab Created."
    }
    else {
      $progs | Out-String
    }    
  }
  else {
    Write-Output "Unable to find any startup programs"
  }
}
else {
  Write-Output "Please select the hosts in the Systems tab to scan."
}

#End Script