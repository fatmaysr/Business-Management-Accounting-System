using System.IO;
using System.Web;
using Nexus.Models;
using Newtonsoft.Json;

namespace Nexus.Helpers
{
    public static class SettingsStore
    {
        private static string FilePath =>
            HttpContext.Current.Server.MapPath("~/App_Data/settings.json");

        public static GeneralSettingsViewModel Load()
        {
            if (!File.Exists(FilePath))
                return new GeneralSettingsViewModel();

            var json = File.ReadAllText(FilePath);
            return JsonConvert.DeserializeObject<GeneralSettingsViewModel>(json)
                   ?? new GeneralSettingsViewModel();
        }

        public static void Save(GeneralSettingsViewModel settings)
        {
            var folder = HttpContext.Current.Server.MapPath("~/App_Data");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }
    }
}