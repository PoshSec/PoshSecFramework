using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace poshsecframework.Strings
{
    internal static class StringValue
    {
        public const String psftitle = "PoshSec Framework";
        public const String ActiveScriptsRunning = "You have active scripts running. If you exit, all running scripts will be terminated. Are you sure you want to exit?";
        public const String ReloadScriptsRunning = "Can not reload the framework because there are scripts running. Please stop all scripts before issuing the reload command again.";
        public const String SettingScriptsRunning = "You can not change the settings while scripts or commands are running. Please stop any commands or scripts and then try again.";
        public const String CommandRunning = "A command is already running. Please wait, or cancel the command and try again.";
        public const String RequireParams = "There are required paramaters that are missing values. Please fill in all of the required parameters before proceeding.";
        public const String SelectIPScan = "Please select an IP to scan.";
        public const String SelectNetwork = "Please select a network first.";
        public const String ScriptPathError = "The specified script directory does not exist. Please check the path.";
        public const String FrameworkFileError = "The specified Framework file does not exist. Please check the path.";
        public const string ModulePathError = "The specified module directory does not exist. Please check the path.";
        public const String ClearAlerts = "Are you sure you want to clear all of the alerts?";
        public const String UnhandledException = "Unhandled exception in script function.";
        public const String ConfirmScheduleDelete = "Are you sure you want to delete the selected schedules?";
        public const String SelectWeekdays = "Please select the days of the week to schedule this script.";
        public const String SelectMonths = "Please select the months of the year and the dates to schedule this script.";
        public const String SelectSystems = "Please select some systems first.";

        public const String ScriptCancelled = "Script cancelled by user.";
        public const String CommandCancelled = "Command cancelled by user.";

        public const String ActiveScripts = "Active Scripts";
        public const String SelectActiveScript = "Please select an active script.";

        public const String psf = "psf > ";
        public const String Ready = "Ready";
        public const String ImportPSFramework = "Import-Module \"$PSFramework\"";
        public const String NotImplemented = "Not implemented yet. Soon!";

        public const String LocalNetwork = "Local Network";
        public const String Up = "Up";
        public const String Down = "Down";
        public const String NotInstalled = "Not Installed";
        public const String TimeFormat = "MM/dd/yyyy hh:mm tt";
        public const String WaitingForHostResp = "Waiting for hostname responses, please wait...";
        public const String BlankMAC = "00-00-00-00-00-00";
        public const String NAHost = "N/A";
        public const String UnknownHost = "0.0.0.0 (unknown host)";

        public const String CLS = "CLS";
        public const String Clear = "CLEAR";
        public const String AptGetUpdate = "APT-GET UPDATE";
        public const String Reload = "RELOAD";
        public const String Exit = "EXIT";

        public const String GetCommand = "Get-Command";
        public const String OutString = "Out-String";
        public const String GetHelpFull = "Get-Help {0} -full";

        public const String UpdateURI = "https://github.com/PoshSec/PoshSecFramework/commits/master";
        public const String WikiURI = "https://github.com/PoshSec/PoshSecFramework/wiki/_pages";

        public const String ExportFormats = "Extensible Markup Language (*.xml)|*.xml|Comma Separate Values (*.csv)|*.csv|Tabbed Delimited (*.txt)|*.txt";
    }
}
