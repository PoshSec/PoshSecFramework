<#
.DESCRIPTION
  Copies a file to all selected hosts in the Systems tab.

AUTHOR
Ben0xA

.PARAMETER sourcefile
  The source file to copy to the remote hosts.
  
.PARAMETER destination
  The share name and folder on the remote host.
  
.PARAMETER force
  This will attempt to copy the file even if the host appears down.
  
.NOTES
  psfilename=sourcefile
#>

Param(
	[Parameter(Mandatory=$true,Position=1)]
	[string]$sourcefile,
  
  [Parameter(Mandatory=$false,Position=2)]
	[string]$destination="C$\Windows\Temp\",
  
  [Parameter(Mandatory=$false,Position=3)]
  [switch]$force
)

#Start your code here.
$hosts = $PSHosts.GetHosts()

$results = @()

if($hosts.Count -gt 0) {
  if(Test-Path $sourcefile) {
    foreach($h in $hosts) {
      $PSStatus.Update("Copying file to $($h.Name), please wait...")
      $rmtfolder = "\\$($h.Name)\$($destination)\"
      $copyitm = New-Object PSObject
      $copyitm | Add-Member -MemberType NoteProperty -Name "Computer" -Value $h.Name
      $copyitm | Add-Member -MemberType NoteProperty -Name "Source File" -Value $sourcefile
      $copyitm | Add-Member -MemberType NoteProperty -Name "Destination" -Value $rmtfolder
      if($force -or $h.Status -eq "Up"){
        try
        {
          if(!(Test-Path -path $rmtfolder)) {
            New-Item $rmtfolder -Type Directory
          }
          Copy-Item $sourcefile $rmtfolder -recurse -force
          $copyitm | Add-Member -MemberType NoteProperty -Name "Result" -Value "Copied"
        }
        catch {
          $copyitm | Add-Member -MemberType NoteProperty -Name "Result" -Value "Failed!"
        }
      }
      else {
        $copyitm | Add-Member -MemberType NoteProperty -Name "Result" -Value "Host is down."
      }
      $results += $copyitm
    }
    $PSTab.AddObjectGrid($results, "Copy File Results")
  }
  else {
    Write-Output "Unable to locate $sourcefile. Please check the path and try again."
  }
}
#End Script