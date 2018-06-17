using System;
using System.IO;
using Newtonsoft.Json;

namespace PoshSec.Framework
{
    public class AppSettings<TSettings> where TSettings : new()
    {
        private const string DEFAULT_FILEPATH = "settings.json";

        public static TSettings Load(string path = DEFAULT_FILEPATH)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("Settings file not found.", path);
            var text = File.ReadAllText(path);
            var settings = JsonConvert.DeserializeObject<TSettings>(text);
            return settings;
        }

        public static void Save(TSettings settings, string path = DEFAULT_FILEPATH)
        {
            var text = JsonConvert.SerializeObject(settings);
            File.WriteAllText(path, text);
        }
    }
}
