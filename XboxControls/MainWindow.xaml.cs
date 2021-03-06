using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
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
        readonly DispatcherTimer movementTimer;
        readonly Random r = new Random();
        Gamepad gamepad;
        readonly double maxSpeed = 0.1;
        TranslateTransform playerTransform = new TranslateTransform();
        double offsetX = 0;
        double offsetY = 0;
        int tickTracker = 0;
        List<Enemy> enemies = new List<Enemy>();
        DrawingVisual canvas = new DrawingVisual();
        public MainWindow()
        {
            InitializeComponent();
            movementTimer = new DispatcherTimer(new TimeSpan(200), DispatcherPriority.Input, MovementTimer_Tick, Dispatcher.CurrentDispatcher);
            movementTimer.Start();
        }

        private void MovementTimer_Tick(object? sender, EventArgs e)
        {
            if (XInput.GetState(0, out State state))
            {
                gamepad = state.Gamepad;
            }
            double speedX = NormalizeThumbstick(gamepad.RightThumbX) * maxSpeed;
            double speedY = NormalizeThumbstick(gamepad.RightThumbY) * maxSpeed;
            offsetX += speedX;
            offsetY += -speedY;
            playerTransform.X = offsetX;
            playerTransform.Y = offsetY;
            rctPlayer.RenderTransform = playerTransform;
            //lbltext.Content = $"X: {speedX} Y: {speedY}\nRaw X: {gamepad.LeftThumbX} Raw Y: {gamepad.LeftThumbY}\nNormX: {NormalizeThumbstick(gamepad.LeftThumbX)} NormY: {NormalizeThumbstick(gamepad.LeftThumbY)}";

            if (tickTracker % 10 == 0)
            {
                //TODO: use DrawingVisual
                enemies.Add(new Enemy((int)Application.Current.MainWindow.Width, (int)Application.Current.MainWindow.Height));
            }
            tickTracker++;
        }

        private double NormalizeThumbstick(short value)
        {
            int valueint = value;
            double result;
            result = valueint switch
            {
                < 0 => valueint / 32768.0,
                0 => 0,
                > 0 => valueint / 32767.0
            };


            return result;
        }

        private RectVisibility IsOffScreen(Rectangle rect)
        {
           RectVisibility vec = new RectVisibility(false, false);
            Point pos = rect.TransformToAncestor(Application.Current.MainWindow).Transform(new Point(0, 0));
            if (pos.X + rect.ActualWidth < 0 || pos.X + rect.ActualWidth > this.ActualWidth)
            {
                // off screen laterally
                vec.Lateral = true;
            }
            else vec.Lateral = false;
            if (pos.Y + rect.ActualHeight < 0 || pos.Y + rect.ActualHeight > this.ActualHeight)
            {
                // off screen laterally
                vec.Vertical = true;
            }
            else vec.Vertical = false;

            return vec;
        }

        private Point GetControlPosition(Rectangle control)
        {
            return control.TransformToAncestor(Application.Current.MainWindow).Transform(new Point(0, 0));
        }

        record struct RectVisibility
        {
            public bool Lateral { get; set; }
            public bool Vertical { get; set; }

            public RectVisibility(bool lateral, bool vertical)
            {
                Lateral = lateral;
                Vertical = vertical;
            }
        }

        enum ProjectileState
        {
            LOCKED,
            FLYING,
            OUTOFBOUNDS
        }
    }
}
