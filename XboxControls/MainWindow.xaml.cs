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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Vortice.XInput;

namespace XboxControls
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly DispatcherTimer timer;
        Gamepad gamepad;
        readonly double maxSpeed = 0.1;
        TranslateTransform playerTransform = new TranslateTransform();
        TranslateTransform
        double offsetX = 0;
        double offsetY = 0;
        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer(new TimeSpan(200), DispatcherPriority.Input, Timer_Tick, Dispatcher.CurrentDispatcher);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (XInput.GetState(0, out State state))
            {
                gamepad = state.Gamepad;
            }
            double speedX = NormalizeThumbstick(gamepad.LeftThumbX) * maxSpeed;
            double speedY = NormalizeThumbstick(gamepad.LeftThumbY) * maxSpeed;
            offsetX += speedX;
            offsetY += -speedY;
            playerTransform.X = offsetX;
            playerTransform.Y = offsetY;
            rctPlayer.RenderTransform = playerTransform;
            lbltext.Content = $"X: {speedX} Y: {speedY}";
        }

        private double NormalizeThumbstick(short value)
        {
            double result;
            result = value switch
            {
                < 0 => value / 32768,
                0 => 0,
                > 0 => value / 32767
            };


            return result;
        }
    }
}
