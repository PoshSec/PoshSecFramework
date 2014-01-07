<#
.DESCRIPTION
Tests the ability to call PoshSec Framework Commands.

AUTHOR
Ben0xA
#>
#Required to use PoshSec functions
Import-Module $PSModRoot\PoshSec

# Begin Script Flow
Write-Output "This is the result of the Get-SecSoftwareInstalled function."

Get-SecSoftwareInstalled
#End Script