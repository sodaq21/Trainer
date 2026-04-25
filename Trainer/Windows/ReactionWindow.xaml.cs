using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Trainer.Windows
{
    public partial class ReactionWindow : Window, INotifyPropertyChanged
    {
        private bool isPressed;
        private long ms;
        private readonly Random rnd = new Random();
        private readonly Stopwatch stopwatch = new Stopwatch();
        private CancellationTokenSource cts;
        private int? _besttime;

        private Brush IdleBrush => (Brush)FindResource("Brush.SurfaceMuted");
        private Brush WaitBrush => (Brush)FindResource("Brush.Warning");
        private Brush ReadyBrush => (Brush)FindResource("Brush.Success");

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
            DataContext = this;
            border.Background = IdleBrush;
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
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
            else if (stopwatch.IsRunning)
            {
                stopwatch.Stop();
                ms = stopwatch.ElapsedMilliseconds;
                border.Background = IdleBrush;
                txt.Text = $"{ms} milliseconds!";
                stopwatch.Reset();
                isPressed = false;
                if (ms < BestTime || BestTime == null)
                {
                    BestTime = (int)ms;
                }
            }
            else
            {
                cts?.Cancel();
            }
        }

        private async void StartReactionTest()
        {
            cts?.Cancel();
            cts = new CancellationTokenSource();
            border.Background = WaitBrush;
            txt.Text = "Wait...";
            try
            {
                await Task.Delay(rnd.Next(1000, 5000), cts.Token);
                border.Background = ReadyBrush;
                stopwatch.Start();
            }
            catch (OperationCanceledException)
            {
                border.Background = IdleBrush;
                txt.Text = "False start! Try again";
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
            Close();
        }
    }
}
