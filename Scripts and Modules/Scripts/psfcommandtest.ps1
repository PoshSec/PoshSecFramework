<#
.DESCRIPTION
Tests the ability to call PoshSec Framework Commands.

AUTHOR
Ben0xA
#>

# Begin Script Flow
Import-Module $PSFramework

Write-Output "This is the result of the Get-SecSoftwareInstalled function."

Get-SecSoftwareInstalled
#End Script