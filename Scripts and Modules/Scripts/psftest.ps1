<#
.DESCRIPTION
Tests the import function for the PoshSec Framework.

AUTHOR
Ben0xA
#>

# Begin Script Flow

Write-Output "This is the test script for PoshSec Framework"
Write-Output "There should be 5 alerts listed."
$PSAlert.Add("This is an Information alert.", 0)
$PSAlert.Add("This is an Error alert.", 1)
$PSAlert.Add("This is a Warning alert.", 2)
$PSAlert.Add("This is a Severe alert.", 3)
$PSAlert.Add("This is a Critical alert.", 4)
Write-Output ""

$PSStatus.Update("This updates the status item for this script")

Write-Output "This is the PSFramework variable for the main framework script location."
Write-Output $PSFramework
Write-Output "This is the PSModRood variable for the Modules root folder."
Write-Output $PSModRoot
Write-Output "This is the PSRoot variable for the Scripts root folder."
Write-Output $PSRoot

Get-Drives

#End Script