Get-ChildItem $PSScriptRoot | ? { $_.PSIsContainer -and $_.Name -ne "PoshSec.PowerShell.Commands" -and $_.Name -ne "PoshSec.PowerShell.Commands 3.5" } | % { Import-Module $_.FullName }
if ($PSVersionTable.PSVersion.Major -gt 2) {
    Import-Module $PSScriptRoot\PoshSec.PowerShell.Commands\PoshSec.PowerShell.Commands.dll
} else {
    Import-Module $PSScriptRoot\PoshSec.PowerShell.Commands 3.5\PoshSec.PowerShell.Commands.dll
}

#List Custom Modules Here
Import-Module $PSModRoot\getdrives.psm1