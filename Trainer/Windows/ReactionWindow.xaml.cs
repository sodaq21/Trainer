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
    /// Логика взаимодействия для ReactionWindow.xaml
    /// </summary>
    public partial class ReactionWindow : Window
    {

        public ReactionWindow()
        {
            InitializeComponent();

        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
            if (e.Key == Key.End)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
