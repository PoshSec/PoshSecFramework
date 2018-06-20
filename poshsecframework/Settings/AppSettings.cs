using System;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace PoshSec.Framework
{
    public class AppSettings<TSettings> where TSettings : new()
    {
        private static string _settingsDirectory = Application.LocalUserAppDataPath;
        private static string _settingsFilename = "settings.json";

        public static string Path => System.IO.Path.Combine(_settingsDirectory, _settingsFilename);

        public static TSettings Load(string path = "")
        {
            path = HandlePath(path);

            if (!File.Exists(path))
                throw new FileNotFoundException("Settings file not found.", path);

            var text = File.ReadAllText(path);
            var settings = JsonConvert.DeserializeObject<TSettings>(text);

            SavePath(path);

            return settings;
        }

        public static void Save(TSettings settings, string path = "")
        {
            path = HandlePath(path);

            var text = JsonConvert.SerializeObject(settings);
            File.WriteAllText(path, text);

            SavePath(path);
        }

        private static void SavePath(string path)
        {
            _settingsDirectory = System.IO.Path.GetDirectoryName(path);
            _settingsFilename = System.IO.Path.GetFileName(path);
        }

        private static string HandlePath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                path = System.IO.Path.Combine(_settingsDirectory, _settingsFilename);
            return path;
        }
    }
}
