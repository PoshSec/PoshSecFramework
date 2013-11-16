<#
.DESCRIPTION
  Just a test. Ignore

AUTHOR
Ben0xA

.PARAMETER sourcefile
  The source file to copy to the remote hosts.
  
.PARAMETER storedhosts
  This is for storing hosts from the framework for scheduling.
  
.NOTES
  psfilename=sourcefile
  pshosts=storedhosts
#>

Param(
	[Parameter(Mandatory=$true,Position=1)]
	[string]$sourcefile,
  
  [Parameter(Mandatory=$false,Position=2)]
	[string]$storedhosts
)

# Begin Script Flow

#Start your code here.
[PSObject]$hosts = $null

if($storedhosts) {
  #The storedhosts have been serialized as a string
  #Before we use them we need to deserialize.
  $hosts = $PSHosts.DeserializeHosts($storedhosts)
}
else {
  $hosts = $PSHosts.GetHosts()
}

Write-Output $hosts

$results = @()

if($hosts.Count -gt 0) {
  foreach($h in $hosts) {
    $PSStatus.Update("Processing $($h.Name), please wait...")
    $copyitm = New-Object PSObject
    $copyitm | Add-Member -MemberType NoteProperty -Name "Computer" -Value $h.Name
    $copyitm | Add-Member -MemberType NoteProperty -Name "Filename" -Value $sourcefile
    $copyitm | Add-Member -MemberType NoteProperty -Name "Status" -Value "It worked!"
    $results += $copyitm
  }
  $PSTab.AddObjectGrid($results, "Test Results")
}
#End Script