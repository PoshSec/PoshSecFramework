 function Get-SecRunningProcess
{
  Param(
    [Parameter(Mandatory=$false,Position=1)]
    [string]$computer="",
    
    [Parameter(Mandatory=$false,Position=2)]
    [string]$procname=""
  )
  
  $runproc = $null
  
  if($computer -eq "") {
    $computer = Get-Content env:ComputerName
    if($procname -eq "") {
      $runproc = Get-Process
    }
    else {
      $runproc = Get-Process -name $procname
    }
  }
  else {
    if($procname -eq "") {
      $runproc = Get-Process -computername $computer
    }
    else {
      $runproc = Get-Process -computername $computer -name $procname
    }
  }
   
  $properties = @()
  
  if($runproc) {
    $runproc | ForEach-Object {
        $proc = New-Object PSObject
        $proc | Add-Member -MemberType NoteProperty -Name "Computer" -Value $computer
        $proc | Add-Member -MemberType NoteProperty -Name "ProcessName" -Value $_.ProcessName
        $proc | Add-Member -MemberType NoteProperty -Name "PID" -Value $_.Id
        $proc | Add-Member -MemberType NoteProperty -Name "MemoryUsage" -Value "$($_.WS / 1KB) K"
        $properties += $proc
    }
  } 
  
  Write-Output $properties
} 