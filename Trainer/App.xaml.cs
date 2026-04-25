using System;
using System.Windows;
using Trainer.Windows;

namespace Trainer
{
    public partial class App : Application
    {
        private const string LightThemePath = "Themes/Light.xaml";
        private const string DarkThemePath = "Themes/Dark.xaml";

        public bool IsDarkTheme { get; private set; }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            ApplyTheme(false);
            var menuWindow = new MenuWindow();
            menuWindow.Show();
        }

        public void ToggleTheme()
        {
            ApplyTheme(!IsDarkTheme);
        }

        public void ApplyTheme(bool useDarkTheme)
        {
            var dictionaries = Current.Resources.MergedDictionaries;
            if (dictionaries.Count < 2)
            {
                return;
            }

            dictionaries[1] = new ResourceDictionary
            {
                Source = new Uri(useDarkTheme ? DarkThemePath : LightThemePath, UriKind.Relative)
            };

            IsDarkTheme = useDarkTheme;
        }
    }
}
