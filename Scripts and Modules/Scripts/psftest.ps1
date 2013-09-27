<#
.DESCRIPTION
Tests the import function for the PoshSec Framework.

.AUTHOR
Ben0xA
#>

# Begin Script Flow
Import-Module $PSFramework

Write-Output "This is the test script for PoshSec Framework"
Write-Output "There should be 5 alerts listed."
$PSAlert.Add("This is an Information alert.", 0)
$PSAlert.Add("This is an Error alert.", 1)
$PSAlert.Add("This is a Warning alert.", 2)
$PSAlert.Add("This is a Severe alert.", 3)
$PSAlert.Add("This is a Critical alert.", 4)
Write-Output ""
Write-Output "This is the results of the Get-Drives module imported with PSFramework"
Get-Drives
Write-Output "Rolando is $Rolando"
#End Script