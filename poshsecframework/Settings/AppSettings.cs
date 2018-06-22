using System;
using System.IO;
using System.Management.Automation;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace PoshSec.Framework
{
    public class AppSettings<TSettings> where TSettings : class, new() 
    {
        private static string _settingsDirectory = Application.LocalUserAppDataPath;
        private static string _settingsFilename = "settings.json";

        private static JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.Indented
        };

        public static string Path => System.IO.Path.Combine(_settingsDirectory, _settingsFilename);

        public static TSettings Load(string path = "")
        {
            path = HandlePath(path);

            if (!File.Exists(path))
                return new TSettings();

            var text = File.ReadAllText(path);
            var settings = JsonConvert.DeserializeObject<TSettings>(text, _serializerSettings) ?? new TSettings();
            SavePath(path);

            return settings;
        }

        public static void Save(TSettings settings, string path = "")
        {
            path = HandlePath(path);
            var text = JsonConvert.SerializeObject(settings, _serializerSettings);
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
            var filename = System.IO.Path.GetFileName(path);
            if (string.IsNullOrWhiteSpace(filename))
                filename = _settingsFilename;
            var directory = System.IO.Path.GetDirectoryName(path) ?? _settingsDirectory;
            if (string.IsNullOrWhiteSpace(directory))
                directory = _settingsDirectory;
            path = System.IO.Path.Combine(directory, filename);
            return path;
        }
    }
}
