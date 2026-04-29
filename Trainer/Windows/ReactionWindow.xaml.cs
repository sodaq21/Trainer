using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Trainer.Windows
{
    /// <summary>
    /// Логика взаимодействия для ReactionWindow.xaml
    /// </summary>
    public partial class ReactionWindow : Window, INotifyPropertyChanged
    {
        private bool isPressed;
        private long ms = 0;
        Random rnd = new Random();
        Stopwatch stopwatch = new Stopwatch();
        CancellationTokenSource cts;
        private int? _besttime = null;
        public int? BestTime
        {
            get => _besttime;
            set
            {
                _besttime = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ReactionWindow()
        {
            InitializeComponent();
            isPressed = false;
            this.DataContext = this;
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

        private void ReactionClick(object sender, RoutedEventArgs e)
        {
            if (!isPressed)
            {
                isPressed = true;
                StartReactionTest();
            }
            else
            {
                if (stopwatch.IsRunning)
                {
                    stopwatch.Stop();
                    ms = stopwatch.ElapsedMilliseconds;
                    border.SetResourceReference(Control.BackgroundProperty, "AccentColor");
                    txt.Text = $"{ms} milliseconds!";
                    stopwatch.Reset();
                    isPressed = false;
                    if (ms < BestTime || BestTime == null)
                        BestTime = (int)ms;
                }
                else
                    cts?.Cancel();
            }
        }
        private async void StartReactionTest()
        {
            cts?.Cancel();
            cts = new CancellationTokenSource();
            border.Background = (Brush)new BrushConverter().ConvertFrom("#dc3545");
            txt.Text = "Wait...";
            try
            {
                await Task.Delay(rnd.Next(1000, 5000), cts.Token);
                border.Background = (Brush)new BrushConverter().ConvertFrom("#57c470");
                txt.Text = "Click!";
                stopwatch.Start();
            }
            catch (OperationCanceledException)
            {
                border.SetResourceReference(Control.BackgroundProperty, "AccentColor");
                txt.Text = "False start! Try again!";
                stopwatch.Stop();
                stopwatch.Reset();
                isPressed = false;
            }
            finally
            {
                cts?.Dispose();
                cts = null;
            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
            stopwatch.Stop();
            stopwatch.Reset();
        }
    }
}
