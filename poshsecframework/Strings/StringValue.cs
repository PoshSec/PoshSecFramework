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
        public const string ScriptPathError = "The specified script directory does not exist. Please check the path.";
        public const string FrameworkFileError = "The specified Framework file does not exist. Please check the path.";
        public const string ModulePathError = "The specified module directory does not exist. Please check the path.";
        public const string ImportError = "There was an error when importing the PoshSec Framework file.";
        public const string ClearAlerts = "Are you sure you want to clear all of the alerts?";
        public const string UnhandledException = "Unhandled exception in script function.";
        public const string ConfirmScheduleDelete = "Are you sure you want to delete the selected schedules?";
        public const string SelectWeekdays = "Please select the days of the week to schedule this script.";
        public const string SelectMonths = "Please select the months of the year and the dates to schedule this script.";
        public const string SelectSystems = "Please select some systems first.";

        public const string TNSetExecutionPolicy = "\r\n\r\nFor more information, please visit\r\nhttp://technet.microsoft.com/en-us/library/ee176961.aspx";
        public const string TNUpdateHelp = "\r\n\r\nFor more information, please visit\r\nhttp://technet.microsoft.com/en-us/library/hh849720.aspx";
        public const string TNUnblockFile = "\r\n\r\nFor more information, please visit\r\nhttp://technet.microsoft.com/en-us/library/ee176841.aspx\r\nhttp://technet.microsoft.com/en-us/library/hh849924.aspx";

        public const string FTCheckSettings = "This will ensure that the settings are pointing to the proper directories and files necessary for the PoshSec Framework to function properly.";
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

        public const string ScriptCancelled = "Script cancelled by user.";
        public const string CommandCancelled = "Command cancelled by user.";

        public const string ActiveScripts = "Active Scripts";
        public const string SelectActiveScript = "Please select an active script.";

        public const string psf = "psf > ";
        public const string Ready = "Ready";
        //public const string ImportPSFramework = "Import-Module \"$PSFramework\"";
        public const string NotImplemented = "Not implemented yet. Soon!";
        public const string WriteError = "\r\nWrite-Output $error";

        public const string LocalNetwork = "Local Network";
        public const string Up = "Up";
        public const string Down = "Down";
        public const string NotInstalled = "Not Installed";
        public const string TimeFormat = "MM/dd/yyyy hh:mm tt";
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
        public const string FileFormat = "repos/{0}/{1}/{2}";
        public const string ReadmeFormat = "repos/{0}/{1}/readme";
        public const string ContentTypeJSON = "application/json; charset=utf-8";

        public const string ExportFormats = "Extensible Markup Language (*.xml)|*.xml|Comma Separate Values (*.csv)|*.csv|Tabbed Delimited (*.txt)|*.txt";
    }
}
