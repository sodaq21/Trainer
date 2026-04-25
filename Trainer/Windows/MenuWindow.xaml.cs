using System.Windows;

namespace Trainer.Windows
{
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
        }

        private void AimBtn_Click(object sender, RoutedEventArgs e)
        {
            var aimWindow = new AimWindow { Owner = this };
            Hide();
            aimWindow.ShowDialog();
            Show();
        }

        private void ReactionBtn_Click(object sender, RoutedEventArgs e)
        {
            var reactionWindow = new ReactionWindow { Owner = this };
            Hide();
            reactionWindow.ShowDialog();
            Show();
        }

        private void ThemeToggle_Click(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).ToggleTheme();
        }
    }
}
