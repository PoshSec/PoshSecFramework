using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace poshsecframework.Strings
{
    internal static class StringValue
    {
        public const string psftitle = "PoshSec Framework";
        public const string ActiveScriptsRunning = "You have active scripts running. If you exit, all running scripts will be terminated. Are you sure you want to exit?";
        public const string ReloadScriptsRunning = "Can not reload the framework because there are scripts running. Please stop all scripts before issuing the reload command again.";
        public const string SettingScriptsRunning = "You can not change the settings while scripts or commands are running. Please stop any commands or scripts and then try again.";
        public const string CommandRunning = "A command is already running. Please wait, or cancel the command and try again.";
        public const string RequireParams = "There are required paramaters that are missing values. Please fill in all of the required parameters before proceeding.";
        public const string SelectIPScan = "Please select an IP to scan.";
        public const string SelectNetwork = "Please select a network first.";
        public const string ScriptPathError = "The specified script directory does not exist.";
        public const string ModulePathError = "The specified module directory does not exist.";
        public const string CreatePath = " Would you like to create the directory?";
        public const string ImportError = "There was an error when importing the PoshSec Framework file.";
        public const string ClearAlerts = "Are you sure you want to clear all of the alerts?";
        public const string ClearAlert = "Are you sure you want to clear the selected alerts?";
        public const string UnhandledException = "Unhandled exception in script function.";
        public const string ConfirmScheduleDelete = "Are you sure you want to delete the selected schedules?";
        public const string ConfirmNetworkDelete = "Are you sure you want to delete the selected network?";
        public const string ConfirmModuleDelete = "Are you sure you want to delete the selected module(s)?";
        public const string ConfirmScriptDelete = "There are existing scripts in the Scripts folder. Downloading the current scripts will delete any existing scripts in that folder. Proceed?";
        public const string SelectWeekdays = "Please select the days of the week to schedule this script.";
        public const string SelectMonths = "Please select the months of the year and the dates to schedule this script.";
        public const string SelectSystems = "Please select some systems first.";
        public const string InvalidNetworkName = "Please enter a unique and valid network name. (i.e. system.local)";
        public const string InvalidSyslog = "Please enter a valid host name or IP address for the syslog server.";
        public const string InvalidAlertType = "That is not a valid Alert Type. The valid alert types are ";
        public const string FileSavedSuccessfully = "File saved successfully.";
        public const string RestartRequired = "PoshSec Framework needs to be restarted. Would you like to restart now?";

        public const string TNSetExecutionPolicy = "\r\n\r\nFor more information, please visit\r\nhttp://technet.microsoft.com/en-us/library/ee176961.aspx";
        public const string TNUpdateHelp = "\r\n\r\nFor more information, please visit\r\nhttp://technet.microsoft.com/en-us/library/hh849720.aspx";
        public const string TNUnblockFile = "\r\n\r\nFor more information, please visit\r\nhttp://technet.microsoft.com/en-us/library/ee176841.aspx\r\nhttp://technet.microsoft.com/en-us/library/hh849924.aspx";

        public const string FTCheckSettings = "This will ensure that the settings are pointing to the proper directories and files necessary for the PoshSec Framework to function properly.";
        public const string FTInitialDownload = "This will download the PoshSec repository from github. This step can be skipped if you would prefer to use other repositories.";
        public const string FTUnblockFiles = "This will execute the command\r\n'Get-ChildItem -recurse <path to poshsecframework> | Unblock-File'. \r\n\r\nGet-ChildItem -recurse gets a listing of all of the files in the poshsecframework directory, and subdirectories, and sends that to the Unblock-File Cmdlet. This will remove the 'Blocked' file attibute on those files." + TNUnblockFile;
        public const string FTUpdateHelp = "This will execute the command\r\n'Update-Help -Force'.\r\n\r\nThis is required when using the Get-Help Cmdlet. This requires an active internet connection." + TNUpdateHelp;
        public const string FTSetExecutionPolicy = "This will execute the command\r\n'Set-ExecutionPolicy RemoteSigned -Force'.\r\n\r\nThis allows for downloaded scripts that are signed by a trusted publisher to run on your system.\r\n\r\nPoshSec Framework requires this ExecutionPolicy to work." + TNSetExecutionPolicy;
        public const string FTRunSampleScript = "This will run a sample powershell script after the configuration is complete to ensure that everything is configured properly.";
        public const string NoStepDescription = "Unable to find step description.";
        public const string RunAsAdministratorError = "You will need to run PoshSec Framework as an Administrator for this command to work, or resolve the errors listed. Would you like to attempt to run this step again?";
        public const string StepCompleteDescription = "Click a step to view any error, or status, messages.";
        public const string StepSelectDescription = "Select a step to the left to read the description.";
        public const string StepSuccessDescription = "The step executed successfully.";
        public const string StepIgnoredDescription = "The step was ignored.";
        public const string StepSuccess = "Success";
        public const string StepRunning = "Running...";
        public const string StepIgnored = "Ignored";
        public const string StepFailed = "Failed!";
        public const string MustSelectStep = "You must select at least 1 step to continue.";
        public const string AlertFormat = "Severity: {0}\\r\\nMessage: {1}\\r\\nTimestamp: {2}\\r\\nScript: {3}\\r\\n";
        public const string ScriptBlockNoFile = "\r\nat <ScriptBlock>, <No file>: line 1";

        public const string ScriptCancelled = "Script cancelled by user.";
        public const string CommandCancelled = "Command cancelled by user.";

        public const string ActiveScripts = "Active Scripts";
        public const string SelectActiveScript = "Please select an active script.";

        public const string psf = "psf > ";
        public const string Ready = "Ready";
        public const string NotImplemented = "Not implemented yet. Soon!";
        public const string WriteError = "\r\nWrite-Output $error";
        public const string ModRestartFilename = "modrestart.psf";

        public const string LocalNetwork = "Local Network";
        public const string Up = "Up";
        public const string Down = "Down";
        public const string NotInstalled = "Not Installed";
        public const string TimeFormat = "MM/dd/yyyy hh:mm tt";
        public const string LogDateFormat = "MM/dd/yyyy";
        public const string LogTimeFormat = "hh:mm:ss tt";
        public const string SyslogTimeFormat = "MMM dd yyyy hh:mm:ss";
        public const string WaitingForHostResp = "Waiting for hostname responses, please wait...";
        public const string BlankMAC = "00-00-00-00-00-00";
        public const string NAHost = "N/A";
        public const string UnknownHost = "0.0.0.0";

        public const string CLS = "CLS";
        public const string Clear = "CLEAR";
        public const string AptGetUpdate = "APT-GET UPDATE";
        public const string Reload = "RELOAD";
        public const string Exit = "EXIT";

        public const string GetCommand = "Get-Command";
        public const string OutString = "Out-string";
        public const string GetHelpFull = "Get-Help {0} -full";
        public const string SetExecutionPolicy = "Set-ExecutionPolicy RemoteSigned -Force";
        public const string UpdateHelp = "Update-Help -Force";
        public const string UnblockFiles = "Unblock-File -Path ";

        public const string UpdateURI = "https://github.com/PoshSec/PoshSecFramework/commits/master";
        public const string WikiURI = "https://github.com/PoshSec/PoshSecFramework/wiki/_pages";
        public const string GithubURI = "https://api.github.com/";
        public const string GithubURL = "https://github.com/";
        public const string RateLimitURL = "http://developer.github.com/v3/#rate-limiting";
        public const string PSFScriptsPath = "repos/PoshSec/PoshSecScripts/zipball/master";
        public const string ArchiveFormat = "repos/{0}/{1}/zipball/{2}";
        public const string LastModifiedFormat = "repos/{0}/{1}";
        public const string BranchFormat = "repos/{0}/{1}/branches";
        public const string AccessToken = "?access_token={0}";
        public const string ModuleSaveFormat = "{0}|{1}|{2}|{3}";
        public const string ContentTypeJSON = "application/json; charset=utf-8";
        public const string DefaultBranch = "master";
        public const string RateLimitKey = "X-RateLimit-Remaining";
        public const string LastModifiedKey = "Last-Modified";
        public const string NotModified = "304 Not Modified";
        public const string IfModifiedSince = "If-Modified-Since";
        public const string InvalidPSModule = "The repository {0} in branch {1} does not have a valid .psd1 file. This is required for PowerShell modules. Please check the path, repository, and/or branch and try again.";
        public const string InvalidRepositoryURL = "The URL specified is not a valid Github repository URL. Please enter the URL again.";
        public const string BranchNotFound = "The specified branch of {0} was not found as a valid branch for this repository.";

        public const string ExportFormats = "Extensible Markup Language (*.xml)|*.xml|Comma Separate Values (*.csv)|*.csv|Tabbed Delimited (*.txt)|*.txt";
        public const string OutputLogFormat = "[{0}:{1}] - {2}";
        public const string SyslogFormat = "{0}{1} {2} : %{3}-{4}: {5}";
        public const string PriorityFormat = "<{0}>";
        public const string AlertLabelFormat = "{0} ({1})";
    }
}
