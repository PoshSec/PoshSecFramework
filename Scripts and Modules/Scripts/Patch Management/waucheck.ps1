<#
.DESCRIPTION
Windows Automatic Update Checker
Written by Ben0xA
With Help from mwjcomputing!

.PARAMETER kbs
Comma separated values of KB numbers.

.PARAMETER outputFile
The output file to save results. This will override showintab.

.PARAMETER omitInstalled
Omits output if the KB is installed.

.PARAMETER computer
Specifies a single computer to scan.

.PARAMETER showintab
Specifies whether to show the results in a PoshSec Framework Tab.

.NOTES
  pshosts=storedhosts
#>

Param(
	[Parameter(Mandatory=$true,Position=1)]
	[string]$kbs,
	
	[Parameter(Mandatory=$false,Position=2)]
	[string]$outputFile,
	
	[Parameter(Mandatory=$false,Position=3)]
	[boolean]$omitInstalled,
	
	[Parameter(Mandatory=$false,Position=4)]
	[string]$computer,
	
	[Parameter(Mandatory=$false,Position=5)]
	[boolean]$showintab,
  
  [Parameter(Mandatory=$false,Position=6)]
	[string]$storedhosts
)

Function Get-Pcs{
	$domain = New-Object System.DirectoryServices.DirectoryEntry
	
	$ds = New-Object System.DirectoryServices.DirectorySearcher
	$ds.SearchRoot = $domain
	$ds.Filter = ("(objectCategory=computer)")
	$ds.PropertiesToLoad.Add("name")
	
	$rslts = $ds.FindAll()
	return $rslts
}

Function Get-KBs([string]$pcname){
	$rslts = @()
	$qfe = Get-WmiObject -Class Win32_QuickFixEngineering -Computer $pcname -ErrorVariable myerror -ErrorAction SilentlyContinue
	if($myerror.count -eq 0) {
		foreach($kb in $kbItems){
			$installed = $false
			$kbentry = $qfe | Select-String $kb
			if($kbentry){
				$installed = $true
			}
			if($omitInstalled){
				if(-not $installed){
          $rslt = New-Object PSObject
          $rslt | Add-Member -MemberType NoteProperty -Name "PC_Name" -Value $pcname
          $rslt | Add-Member -MemberType NoteProperty -Name "KB" -Value $kb
          $rslt | Add-Member -MemberType NoteProperty -Name "Installed" -Value $installed
          $rslts += $rslt
				}
			}
			else {
        $rslt = New-Object PSObject
        $rslt | Add-Member -MemberType NoteProperty -Name "PC_Name" -Value $pcname
        $rslt | Add-Member -MemberType NoteProperty -Name "KB" -Value $kb
        $rslt | Add-Member -MemberType NoteProperty -Name "Installed" -Value $installed
        $rslts += $rslt
			}		
		}
	}
	else{
    $rslt = New-Object PSObject
    $rslt | Add-Member -MemberType NoteProperty -Name "PC_Name" -Value $pcname
    $rslt | Add-Member -MemberType NoteProperty -Name "KB" -Value $kb
    $rslt | Add-Member -MemberType NoteProperty -Name "Installed" -Value "RPC_Error"
    $rslts += $rslt
    $PSAlert.Add("$pcname is inaccessible. RPC_Error", 1)
	}
	return $rslts
}

# Begin Program Flow
Write-Output "WAUCheck"
Write-Output "Written By: @Ben0xA"
Write-Output "Huge thanks to @mwjcomputing!`r`n"
Write-Output "Looking for KBs $kbs"
$results = @()
$pcs = @()
if($omitInstalled){
	Write-Output "Omitting entries where the KB is installed."
}

if(-not $outputFile){
	Write-Output "Sending output to the screen. Use -outputFile name to save to a file.`r`n"
}
else {
	Write-Output "Will save csv results to $outputFile. Query messages will only appear on the screen.`r`n"
}

$wumaster = ""
$kbItems = $kbs.Split(",")
[PSObject]$hosts = $null

if(-not $computer){
  if($storedhosts) {
    #The storedhosts have been serialized as a string
    #Before we use them we need to deserialize.
    $hosts = $PSHosts.DeserializeHosts($storedhosts)
  }
  else {
    $hosts = $PSHosts.GetHosts()
  }
  
  if(!$hosts) {
    $hosts = Get-PCs    
    foreach($h in $hosts) {
      $pcs += $h.Properties.name
    }
  }
  else {
    foreach($h in $hosts) {
      $pcs += $h.Name
    }
  }
  
  $idx = 0
  $len = $pcs.length
	foreach($pc in $pcs){
    $idx += 1
		$pcname = $pc
		
		if($pcname){
      $PSStatus.Update("Querying $pcname [$idx of $len]")
      if($showintab) {
        $rsp = Get-KBs($pcname)
        if($rsp -and $rsp -ne "") {
          $results += $rsp
        }
      }
			else {
        $wumaster += Get-KBs($pcname) | Out-String
      }
		}	
	}
}
else{
  $PSStatus.Update("Querying $computer, please wait...")
  if($showintab) {
    $rsp = Get-KBs($computer)
    if($rsp -and $rsp -ne "") {
      $results += $rsp
    }    
  }
	else {
    $wumaster += Get-KBs($computer) | Out-String 
  }
}

if(-not $outputFile){
  if($showintab) {
    $PSTab.AddObjectGrid($results, "Windows KB ($kbs) Results")
  }
  else {
    $wumaster | Out-String
  }	
}
else {
	$wumaster| Out-File $outputFile
	Write-Output "Output saved to $outputFile"
}

#End Program