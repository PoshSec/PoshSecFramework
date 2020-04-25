using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PoshSec.Framework.Test
{
    public class AppSettingsTest
    {
        class MySettings
        {
            public MySettings()
            {
                ListOfStringsSetting = new List<string>();
            }
            public int IntegerSetting { get; set; }
            public string StringSetting { get; set; }
            public List<string> ListOfStringsSetting { get; }
        }

        [TestClass]
        public class Save
        {
            [TestMethod]
            public void SaveSettings_ToDefaultPath_FileExists()
            {
                var settings = new MySettings
                {
                    IntegerSetting = 3,
                    StringSetting = "four"
                };
                settings.ListOfStringsSetting.AddRange(new[] { "five", "six" });

                AppSettings<MySettings>.Save(settings);
                Assert.IsTrue(File.Exists(AppSettings<MySettings>.Path));
            }

            [TestMethod]
            public void SaveSettings_ToSpecificPath_FileExists()
            {
                var settings = new MySettings
                {
                    IntegerSetting = 3,
                    StringSetting = "four"
                };
                settings.ListOfStringsSetting.AddRange(new[] { "five", "six" });

                AppSettings<MySettings>.Save(settings, "mysettings.json");
                Assert.IsTrue(File.Exists(AppSettings<MySettings>.Path));
            }
        }

        [TestClass]
        public class Load
        {
            [TestMethod]
            public void LoadSettings_FromDefaultPath_SettingsNotNull()
            {
                var settings = AppSettings<MySettings>.Load();
                Assert.IsNotNull(settings);
            }

            [TestMethod]
            public void LoadSettings_FromSpecificPath_SettingsNotNull()
            {
                var settings = AppSettings<MySettings>.Load("mysettings.json");
                Assert.IsNotNull(settings);
            }
        }
    }
}
