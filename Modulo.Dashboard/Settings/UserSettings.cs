
namespace Modulo.Dashboard.Settings
{
    public partial class UserSettings
    {

        private UISetting uISettingField;

        private CustomSettings customSettingsField;

        /// <remarks/>
        public UISetting UISetting
        {
            get
            {
                return this.uISettingField;
            }
            set
            {
                this.uISettingField = value;
            }
        }

        /// <remarks/>
        public CustomSettings CustomSettings
        {
            get
            {
                return this.customSettingsField;
            }
            set
            {
                this.customSettingsField = value;
            }
        }
    }

    public partial class UISetting
    {

        private string themeField;

        private string accentcolorField;

        private string fontsizeField;

        /// <remarks/>
        public string theme
        {
            get
            {
                return this.themeField;
            }
            set
            {
                this.themeField = value;
            }
        }

        /// <remarks/>
        public string accentcolor
        {
            get
            {
                return this.accentcolorField;
            }
            set
            {
                this.accentcolorField = value;
            }
        }

        /// <remarks/>
        public string fontsize
        {
            get
            {
                return this.fontsizeField;
            }
            set
            {
                this.fontsizeField = value;
            }
        }
    }

    public partial class CustomSettings
    {

        private string startGroupModuleField;

        private string startPageField;

        /// <remarks/>
        public string StartGroupModule
        {
            get
            {
                return this.startGroupModuleField;
            }
            set
            {
                this.startGroupModuleField = value;
            }
        }

        /// <remarks/>
        public string StartPage
        {
            get
            {
                return this.startPageField;
            }
            set
            {
                this.startPageField = value;
            }
        }
    }


}
