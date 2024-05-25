using System;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Presentation;
using Modulo.Dashboard.Views;
using System.Windows;
using Modulo.Dashboard.Settings;
using Modulo.BusinessSingletons;
using System.Linq;
using Modulo.Dashboard.Constants;

namespace Modulo.Dashboard
{
    public partial class Shell : ModernWindow
    {
        public Shell()
        {
            InitializeComponent();
            UiAppSetting.SetCurrentTheme();
        }

        public void AddLinkGroups(LinkGroupCollection linkGroupCollection)
        {
            this.MenuLinkGroups.Clear();
            foreach (LinkGroup linkGroup in linkGroupCollection)
            {
                this.MenuLinkGroups.Add(linkGroup);
            }

            CreateMenuLinkGroup();
            AddSettings();
            this.ContentSource = this.MenuLinkGroups.FirstOrDefault().Links.FirstOrDefault().Source;
            this.lnkHome.Source= this.MenuLinkGroups.FirstOrDefault().Links.FirstOrDefault().Source;
            //this.ContentSource = new Uri($"/Views/UsersView.xaml", UriKind.Relative);
        }

        private void CreateMenuLinkGroup()
        {          
            LinkGroup linkGroup;
            if (UserAuthenticator.CurrentUser.UserRoles.Where(r => r.Name == CommonSettings.ITAdminRole).Any())
            {
                linkGroup = new LinkGroup
                {
                    DisplayName = "IT Admin",
                    GroupKey = "Admin"
                };

                linkGroup.Links.Add(new Link
                {
                    DisplayName = "Users",
                    Source = GetUri(typeof(UsersView))
                });

                linkGroup.Links.Add(new Link
                {
                    DisplayName = "Roles",
                    Source = GetUri(typeof(MUIView))
                });

                linkGroup.Links.Add(new Link
                {
                    DisplayName = "Registered Modules",
                    Source = GetUri(typeof(RegisteredModules))
                });

                this.MenuLinkGroups.Add(linkGroup);
                //this.ContentSource = new Uri($"/Views/UsersView.xaml", UriKind.Relative);
            }

            if (UserAuthenticator.CurrentUser.UserModules?.Count == 0)
            {
                linkGroup = new LinkGroup
                {
                    DisplayName = "Accessibility"
                };
                linkGroup.Links.Add(new Link
                {
                    DisplayName = "Permission Denied",
                    Source = GetUri(typeof(AccessView))
                });
                this.MenuLinkGroups.Add(linkGroup);
                this.ContentSource = new Uri($"/Views/AccessView.xaml", UriKind.Relative);
            }
        }

        private void AddSettings()
        {
            LinkGroup linkGroupSettings = new LinkGroup
            {
                DisplayName = "Settings",
                GroupKey = "settings"
            };

            linkGroupSettings.Links.Add(new Link
            {
                DisplayName = "Settings options",
                Source = GetUri(typeof(SettingsView))
            });
            this.MenuLinkGroups.Add(linkGroupSettings);
        }

        private Uri GetUri(Type viewType)
        {
            return new Uri($"/Views/{viewType.Name}.xaml", UriKind.Relative);
        }

        private void ModernWindow_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ModernWindow_Activated(object sender, EventArgs e)
        {
            App.LoginForm.Close();
        }
    }
}