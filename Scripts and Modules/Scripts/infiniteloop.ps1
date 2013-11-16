<#
.DESCRIPTION
Tests the cancel script option with infinite loop.

.AUTHOR
Ben0xA
#>

# Begin Script Flow
Write-Output "This is the infinite loop script."

$v = $False

do
{

} while ($v -eq $False)
#End Script