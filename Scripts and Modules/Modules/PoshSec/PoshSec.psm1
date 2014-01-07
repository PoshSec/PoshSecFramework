Get-ChildItem $PSScriptRoot | ? {$_.PSIsContainer -and $_.Name -ne "PoshSec.PowerShell.Commands" -and $_.Name -ne "PoshSec.PowerShell.Commands 3.5" } | % {
    Import-Module $_.FullName -ErrorAction SilentlyContinue
}

if ($PSVersionTable.PSVersion.Major -gt 2) {
    Import-Module $PSScriptRoot\PoshSec.PowerShell.Commands\PoshSec.PowerShell.Commands.dll -ErrorAction SilentlyContinue    
} else {
    Import-Module $PSScriptRoot\PoshSec.PowerShell.Commands 3.5\PoshSec.PowerShell.Commands.dll -ErrorAction SilentlyContinue
}

#List Custom Modules Here
Import-Module $PSModRoot\getdrives.psm1 -ErrorAction SilentlyContinue