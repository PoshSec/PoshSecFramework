<#
.DESCRIPTION
Windows Automatic Update Checker
Written by Ben0xA
With Help from mwjcomputing!

.PARAMETER kbs
Comma separated values of KB numbers.

.PARAMETER outputFile
The output file to save results.

.PARAMETER omitInstalled
Omits output if the KB is installed.

.PARAMETER computer
Specifies a single computer to scan.
#>

Param(
	[Parameter(Mandatory=$true,Position=1)]
	[string]$kbs,
	
	[Parameter(Mandatory=$false,Position=2)]
	[string]$outputFile,
	
	[Parameter(Mandatory=$false,Position=3)]
	[boolean]$omitInstalled,
	
	[Parameter(Mandatory=$false,Position=4)]
	[string]$computer
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

Function Get-KBs($pcname){
	$rslt = ""
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
					$rslt += "$pcname,$kb,$installed`r`n"
				}
			}
			else {
				$rslt += "$pcname,$kb,$installed`r`n"
			}		
		}
	}
	else{
		$rslt += "$pcname,$kb,RPC_Error`r`n"
    $PSAlert.Add("$pcname is inaccessible. RPC_Error", 1)
	}
	return $rslt
}

# Begin Program Flow

#Clear-Host
Write-Output "WAUCheck"
Write-Output "Written By: @Ben0xA"
Write-Output "Huge thanks to @mwjcomputing!`r`n"
Write-Output "Looking for KBs $kbs"
if($omitInstalled){
	Write-Output "Omitting entries where the KB is installed."
}

if(-not $outputFile){
	Write-Output "Sending output to the screen. Use -outputFile name to save to a file.`r`n"
}
else {
	Write-Output "Will save csv results to $outputFile. Query messages will only appear on the screen.`r`n"
}

$wumaster = "PC Name,KB,Installed`r`n"
$kbItems = $kbs.Split(",")
if(-not $computer){
	$pcs = Get-PCs
	
  $idx = 0
  $len = $pcs.length
	foreach($pc in $pcs){
    $idx += 1
		$pcname = $pc.Properties.name
		
		if($pcname){
			#Write-Output "Querying $pcname, please wait..."
      $PSStatus.Update("Querying $pcname [$idx of $len]")
			$wumaster += Get-KBs($pcname)
		}	
	}
}
else{
	#Write-Output "Querying $computer, please wait..."
  $PSStatus.Update("Querying $computer, please wait...")
	$wumaster += Get-KBs($computer)
}

if(-not $outputFile){
	#Clear-Host
	$wumaster
}
else {
	$wumaster| Out-File $outputFile
	Write-Output "Output saved to $outputFile"
}

#End Program