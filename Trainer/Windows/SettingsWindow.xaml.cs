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
using Trainer.Models;

namespace Trainer.Windows
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private GameSettings _gameSettings;
        private AimWindow _aimWindow;
        public SettingsWindow(AimWindow aw, GameSettings gs)
        {
            InitializeComponent();
            _gameSettings = new GameSettings();
            this._aimWindow = aw;
            this._gameSettings = gs;
            WindowSize_cb.SelectedValue = gs.windowSize;
            TargetSize_cb.SelectedValue = gs.targetsSize;
            //Sensitivity_slider.Value = gs.sensitivity;
            TargetColor_cb.SelectedValue = gs.color;
            TargetCount_cb.SelectedValue = gs.count;
            Time_cb.SelectedValue = gs.seconds;

        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.End)
            {
                Application.Current.Shutdown();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _gameSettings.windowSize = WindowSize_cb.SelectedValue.ToString();
            _gameSettings.targetsSize = int.Parse(TargetSize_cb.SelectedValue.ToString());
            //_gameSettings.sensitivity = Sensitivity_slider.Value;
            _gameSettings.color = TargetColor_cb.SelectedValue.ToString();
            _gameSettings.count = int.Parse(TargetCount_cb.SelectedValue.ToString());
            _gameSettings.seconds = int.Parse(Time_cb.SelectedValue.ToString());
            _aimWindow.LoadSettings(_gameSettings);
            this.Close();
            
        }
    }
}
