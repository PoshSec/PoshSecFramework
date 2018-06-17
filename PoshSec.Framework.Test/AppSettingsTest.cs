using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PoshSec.Framework.Test
{
    [TestClass]
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

        [TestMethod]
        public void SaveSettingsTest()
        {
            var settings = new MySettings();
            settings.IntegerSetting = 3;
            settings.StringSetting = "four";
            settings.ListOfStringsSetting.AddRange(new[] { "five", "six" });

            AppSettings<MySettings>.Save(settings);
            Assert.IsTrue(File.Exists("settings.json"));
        }

        [TestMethod]
        public void LoadSettingsTest()
        {
            var settings = AppSettings<MySettings>.Load();
            Assert.IsNotNull(settings);
        }
    }
}
