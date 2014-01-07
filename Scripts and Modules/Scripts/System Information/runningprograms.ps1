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

.PARAMETER ignoreprocesses
  A comma separated list of processes to ignore.
  
.PARAMETER baselinepath
  The path to the baseline xml for comparison.

.NOTES
  pshosts=storedhosts
  psfilename=baselinepath
#>

Param(	
	[Parameter(Mandatory=$false,Position=1)]
	[boolean]$showintab=$True,
  
  [Parameter(Mandatory=$false,Position=2)]
	[string]$storedhosts,
  
  [Parameter(Mandatory=$false,Position=3)]
	[string]$processname,
  
  [Parameter(Mandatory=$false,Position=4)]
	[string]$ignoreprocesses,
  
  [Parameter(Mandatory=$false,Position=5)]
	[string]$baselinepath
)
#Required to use PoshSec functions
Import-Module $PSModRoot\PoshSec

#Start your code here.
$processes = @()
$outprocs = @()
$ignore = ($ignoreprocesses -split ",")

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
    foreach($proc in $processes) {
      if($ignore -notcontains $proc.ProcessName) {
        $outprocs += $proc        
      }
    }
    if($baselinepath -ne "") {
      if(Test-Path $baselinepath) {
        $baseprocs = Import-Clixml -path $baselinepath
        $results = Compare-Object $baseprocs $outprocs -property Computer, ProcessName
        if($results) {
          if($showintab) {
            $PSTab.AddObjectGrid($results, "Process Comparison Results")
            Write-Output "Process Comparison Results Tab Created."
          }
          else {
            $results | Out-String
          }
          #overwrite baseline
          $outprocs | Export-Clixml -path $baselinepath
        }        
      }
      else {
        $outprocs | Export-Clixml -path $baselinepath
        Write-Output "Baseline file created."
      }
    }
    else {
      if($showintab) {
        $PSTab.AddObjectGrid($outprocs, "Running Programs")
        Write-Output "Running Programs Tab Created."
      }    
      else {
        $outprocs | Out-String
      }    
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