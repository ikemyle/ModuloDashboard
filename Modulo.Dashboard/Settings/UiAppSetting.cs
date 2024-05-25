using Modulo.Dashboard.Constants;
using FirstFloor.ModernUI.Presentation;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace Modulo.Dashboard.Settings
{
    public class UiAppSetting
    {
        public static UserSettings CurrentUserSettings;
        private static readonly string UiSettingFile = AppDomain.CurrentDomain.BaseDirectory + CommonSettings.UserFile;        

        public static void SetCurrentTheme()
        {
            using (StreamReader file = File.OpenText(UiSettingFile))
            {
                JsonSerializer serializer = new JsonSerializer();
                CurrentUserSettings = (UserSettings)serializer.Deserialize(file, typeof(UserSettings));
                SetTheme();
            }
        }

        public static void SetTheme()
        {
            var uri = new Uri(CurrentUserSettings.UISetting.theme, UriKind.Relative);
            var accentColor = (Color)ColorConverter.ConvertFromString(CurrentUserSettings.UISetting.accentcolor);

            try
            {
                AppearanceManager.Current.ThemeSource = uri;
                AppearanceManager.Current.AccentColor = accentColor;
                Application.Current.Resources[AppearanceManager.KeyDefaultFontSize] = CurrentUserSettings.UISetting.fontsize == ThemeFont.FontSmall ? 13D : 16D;
                Application.Current.Resources[AppearanceManager.KeyFixedFontSize] = CurrentUserSettings.UISetting.fontsize == ThemeFont.FontSmall ? 12.667D : 16.333D;
            }
            catch
            {
                AppearanceManager.Current.ThemeSource = new Uri(ThemesPath.Light, UriKind.Relative);
            }
        }

        internal static void SaveSetting()
        {
            //var usr = new UserSettings();
            //usr.CustomSettings = new CustomSettings();
            //usr.CustomSettings.StartGroupModule = "IT Admin";
            //usr.CustomSettings.StartPage = "Users";
            //usr.UISetting = new UISetting();
            //usr.UISetting.theme = @"/Themes/DarkTheme.xaml";
            //usr.UISetting.accentcolor = "blue";
            //usr.UISetting.fontsize = "small";

            using (StreamWriter file = File.CreateText(UiSettingFile))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, CurrentUserSettings);
            }
            SetTheme();
        }
    }
}
