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
    /// Логика взаимодействия для Aim.xaml
    /// </summary>
    public partial class AimWindow : Window
    {
        private GameSettings gameSettings;
        public int Score { get; set; }
        private Random rnd;
        //private Point _lastMousePosition;
        //private double _virtualX;
        //private double _virtualY;
        public AimWindow()
        {
            InitializeComponent();
            gameSettings = new GameSettings();
            rnd = new Random();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
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

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow(this, gameSettings);
            settingsWindow.Owner = this;
            settingsWindow.ShowDialog();
        }

        public void LoadSettings(GameSettings gs)
        {
            gameSettings = gs;
            double w = double.Parse(gameSettings.windowSize.Substring(0, gameSettings.windowSize.IndexOf(' ')));
            double h = double.Parse(gameSettings.windowSize.Substring(gameSettings.windowSize.LastIndexOf(' ')));
            this.Width = w;
            this.Height = h;
        }

        private void TargetAdd()
        {
            Button target = new Button();
            double size = gameSettings.targetsSize * 10;
            // converting string color to solidcolorbrush
            Color clr = (Color)ColorConverter.ConvertFromString(gameSettings.color);
            SolidColorBrush btn_color = new SolidColorBrush(clr);
            target.Background = btn_color;
            target.Width = size;
            target.Height = size;
            PlayArea.Children.Add(target);
            double x = rnd.Next(Convert.ToInt32(0 + size), Convert.ToInt32(PlayArea.Width - size));
            double y = rnd.Next(Convert.ToInt32(0 + size), Convert.ToInt32(PlayArea.Height - size));
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            PlayArea.Children.Clear();
            Score = 0;
            for (int i = 0; i < gameSettings.count; i++)
            {
                TargetAdd();
            }
        }

        //private void SensitivityChange(double sens)
        //{

        //}

        //private void Window_MouseMove(object sender, MouseEventArgs e)
        //{
        //    Point currentRealPos = e.GetPosition(MainCanvas);

        //    // Если это первое движение, просто запоминаем позицию и выходим
        //    if (_lastMousePosition == new Point(0, 0))
        //    {
        //        _lastMousePosition = currentRealPos;
        //        return;
        //    }

        //    // Считаем дельту (на сколько сдвинулась физическая мышка)
        //    double deltaX = currentRealPos.X - _lastMousePosition.X;
        //    double deltaY = currentRealPos.Y - _lastMousePosition.Y;

        //    // Применяем чувствительность
        //    _virtualX += deltaX * gameSettings.sensitivity;
        //    _virtualY += deltaY * gameSettings.sensitivity;

        //    // Ограничиваем прицел рамками Canvas (Способ без Math.Clamp)
        //    _virtualX = Math.Min(Math.Max(_virtualX, 0), MainCanvas.ActualWidth);
        //    _virtualY = Math.Min(Math.Max(_virtualY, 0), MainCanvas.ActualHeight);

        //    // Сдвигаем нарисованный прицел (смещаем на половину ширины/высоты, чтобы центр был на координатах)
        //    Canvas.SetLeft(Crosshair, _virtualX - (Crosshair.Width / 2));
        //    Canvas.SetTop(Crosshair, _virtualY - (Crosshair.Height / 2));

        //    _lastMousePosition = currentRealPos;
        //}

        //private void MainCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{

        //}
    }
}
