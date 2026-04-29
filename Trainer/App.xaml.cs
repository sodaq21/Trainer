using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Trainer.Windows;

namespace Trainer
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_Startup(object sender, StartupEventArgs e)
        {
            var MenuWindow = new MenuWindow();
            MenuWindow.Show();
        }

        private static bool _isDark = false;

        public static void ToggleTheme()
        {
            _isDark = !_isDark;
            string themeName = _isDark ? "Dark" : "Light";

            var uri = new Uri($"Themes/{themeName}.xaml", UriKind.Relative);
            var appResources = Current.Resources.MergedDictionaries;
            appResources.Add(new ResourceDictionary { Source = uri });
        }
    }
}
