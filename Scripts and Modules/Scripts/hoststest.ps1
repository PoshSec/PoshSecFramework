<#
.DESCRIPTION
This tests the $PSHosts functionality with PoshSec Framework

AUTHOR
Ben0xA

$PSHosts Usage
$PSHosts.GetHosts([bool AllHosts])

Returns PSObject array with whatever columns are listed in the listview.
#>

# Begin Script Flow

#Start your code here.
Write-Output "Listing of Hosts that are checked."
Write-Output $PSHosts.GetHosts() | Out-String
Write-Output ""
Write-Output "Listing of all Hosts"
Write-Output $PSHosts.GetHosts($True) | Out-String
#End Script