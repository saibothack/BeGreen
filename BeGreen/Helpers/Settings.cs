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

        private const string SettingsIsLoginKey = "SettingsIsLoginKey";
        private static readonly bool SettingsIsLoginDefault = false;

        private const string SettingsNameKey = "SettingsNameKey";
        private static readonly string SettingsNameDefault = string.Empty;

        private const string SettingsEmailKey = "SettingsEmailKey";
        private static readonly string SettingsEmailDefault = string.Empty;

        private const string SettingsAddressKey = "SettingsAddressKey";
        private static readonly string SettingsAddressDefault = string.Empty;

        private const string SettingsIdCustomerKey = "SettingsIdCustomerKey";
        private static readonly string SettingsIdCustomerDefault = string.Empty;

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

        public static bool isLogin
        {
            get
            {
                return AppSettings.GetValueOrDefault(SettingsIsLoginKey, SettingsIsLoginDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsIsLoginKey, value);
            }
        }

        public static string UserName
        {
            get
            {
                return AppSettings.GetValueOrDefault(SettingsNameKey, SettingsNameDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsNameKey, value);
            }
        }

        public static string UserEmail
        {
            get
            {
                return AppSettings.GetValueOrDefault(SettingsEmailKey, SettingsEmailDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsEmailKey, value);
            }
        }

        public static string UserAddress
        {
            get
            {
                return AppSettings.GetValueOrDefault(SettingsAddressKey, SettingsAddressDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsAddressKey, value);
            }
        }

        public static string IdCustomer
        {
            get
            {
                return AppSettings.GetValueOrDefault(SettingsIdCustomerKey, SettingsIdCustomerDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsIdCustomerKey, value);
            }
        }
    }
}
