using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace BeGreen.Helpers
{
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;

        private const string SettingsShowIntroKey = "SettingsShowIntroKey";
        private static readonly bool SettingsShowIntroDefault = false;

        #endregion


        public static string GeneralSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsKey, value);
            }
        }

        public static bool isShowIntro
        {
            get
            {
                return AppSettings.GetValueOrDefault(SettingsShowIntroKey, SettingsShowIntroDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsShowIntroKey, value);
            }
        }

    }
}
