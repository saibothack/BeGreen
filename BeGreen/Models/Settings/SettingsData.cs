using System.Collections.Generic;

namespace BeGreen.Models.Settings
{
    public class SettingsData
    {
        public string success { get; set; }
        public string message { get; set; }
        public List<Settings> data { get; set; }
    }
}
