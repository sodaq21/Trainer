using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Trainer.Models;
using System.Windows.Threading;

namespace Trainer.Windows
{
    /// <summary>
    /// Логика взаимодействия для Aim.xaml
    /// </summary>
    public partial class AimWindow : Window, INotifyPropertyChanged
    {
        private DispatcherTimer _timer = new DispatcherTimer();
        private SolidColorBrush btn_color = new SolidColorBrush();
        private BrushConverter converter = new BrushConverter();
        private GameSettings gameSettings = new GameSettings();
        private Random rnd = new Random();
        private int _score;
        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                OnPropertyChanged();
            }
        }

        private int _seconds;
        public int Seconds
        {
            get => _seconds;
            set
            {
                _seconds = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        public AimWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            _timer.Tick += _timer_Tick;
            StopButton.IsEnabled = false;

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
            if (!_timer.IsEnabled)
            {
                SettingsWindow settingsWindow = new SettingsWindow(this, gameSettings);
                settingsWindow.Owner = this;
                settingsWindow.ShowDialog();
            }
        }

        public void LoadSettings(GameSettings gs)
        {
            // setting window size
            gameSettings = gs;
            double w = double.Parse(gameSettings.windowSize.Substring(0, gameSettings.windowSize.IndexOf(' ')));
            double h = double.Parse(gameSettings.windowSize.Substring(gameSettings.windowSize.LastIndexOf(' ')));
            this.Width = w;
            this.Height = h;
            this.Left = (SystemParameters.PrimaryScreenWidth - w) / 2;
            this.Top = (SystemParameters.PrimaryScreenHeight - h) / 2;
        }

        private void TargetAdd()
        {
            Button target = new Button();
            double size = gameSettings.targetsSize * 10.0;
            Border border = new Border
            {
                CornerRadius = new CornerRadius(size / 2),
                Background = btn_color,
                Width = size,
                Height = size
            };
            target.Content = border;
            target.BorderThickness = new Thickness(0);
            target.Background = Brushes.Transparent;
            target.Click += TargetClick;
            PlayArea.Children.Add(target);
            double x = rnd.Next(0, (int)(PlayArea.ActualWidth - size));
            double y = rnd.Next(0, (int)(PlayArea.ActualHeight - size));
            Canvas.SetLeft(target, x);
            Canvas.SetTop(target, y);
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            // timer
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Start();
            Seconds = gameSettings.seconds;

            btn_color = (SolidColorBrush)converter.ConvertFromString(gameSettings.color.ToString());
            PlayArea.Children.Clear();
            Score = 0;
            for (int i = 0; i < gameSettings.count; i++)
            {
                TargetAdd();
            }
            StopButton.IsEnabled = true;
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            Seconds--;
            if (_seconds == 0)
            {
                _timer.Stop();
                EndGame();
            }
        }

        private void EndGame()
        {
            PlayArea.Children.Clear();
            MessageBox.Show($"Your score = {Score}!", "Trainer - Game Over", MessageBoxButton.OK, MessageBoxImage.Information);
            Seconds = 0;
        }

        private void TargetClick(object sender, RoutedEventArgs e)
        {
            Button trg = (Button)(sender);
            PlayArea.Children.Remove(trg);
            TargetAdd();
            Score++;
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            EndGame();
            StopButton.IsEnabled = false;
        }
    }
}
