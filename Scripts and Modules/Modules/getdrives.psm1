<#
.SYNOPSIS
Enumerates all Drives and Partitions on a System 

.DESCRIPTION
Enumerates all Drives and Partitions on a System

.EXAMPLE
PS C:\> Get-Drives
#>
Function Get-Drives {
  Get-WmiObject Win32_DiskPartition | Select-Object Name, VolumeName, DiskIndex, Index
}

Export-ModuleMember Get-Drives