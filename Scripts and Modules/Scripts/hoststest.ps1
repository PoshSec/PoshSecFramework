<#
.DESCRIPTION
This tests the $PSHosts functionality with PoshSec Framework

AUTHOR
Ben0xA
#>

# Begin Script Flow

#Leave this here for things to play nicely!
Import-Module $PSFramework

#Start your code here.
Write-Output "Listing of Hosts that are checked."
Write-Output $PSHosts.GetHosts() | Out-String
Write-Output ""
Write-Output "Listing of all Hosts"
Write-Output $PSHosts.GetHosts($True) | Out-String
#End Script