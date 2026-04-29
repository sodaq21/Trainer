using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Trainer.Windows
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
        }

        private void AimBtn_Click(object sender, RoutedEventArgs e)
        {
            AimWindow aimWindow = new AimWindow();
            aimWindow.Owner = this;
            this.Hide();
            aimWindow.ShowDialog();
            this.Show();
        }

        private void ReactionBtn_Click(object sender, RoutedEventArgs e)
        {
            ReactionWindow reactionWindow = new ReactionWindow();
            reactionWindow.Owner = this;
            this.Hide();
            reactionWindow.ShowDialog();
            this.Show();
        }

        private void SwitchTheme(object sender, RoutedEventArgs e)
        {
            App.ToggleTheme();
        }
    }
}
