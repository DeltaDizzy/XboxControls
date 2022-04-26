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
        DispatcherTimer timer;
        Gamepad gamepad;
        bool ok;
        List<Rectangle> rectangles;
        public MainWindow()
        {
            InitializeComponent();
            rectangles = new List<Rectangle>()
            {
                rctA,
                rctB,
                rctX,
                rctY
            };
            timer = new DispatcherTimer(new TimeSpan(200), DispatcherPriority.Input, Timer_Tick, Dispatcher.CurrentDispatcher);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (XInput.GetState(0, out State state))
            {
                gamepad = state.Gamepad;
            }

            // A B X Y
            if (gamepad.Buttons.HasFlag(GamepadButtons.A))
            {
                setButtonPressed(rctA, true);
            }
            else
            {
                setButtonPressed(rctA, false);
            }
            if (gamepad.Buttons.HasFlag(GamepadButtons.B))
            {
                setButtonPressed(rctB, true);
            }
            else
            {
                setButtonPressed(rctB, false);
            }
            if (gamepad.Buttons.HasFlag(GamepadButtons.X))
            {
                setButtonPressed(rctX, true);
            }
            else
            {
                setButtonPressed(rctX, false);
            }
            if (gamepad.Buttons.HasFlag(GamepadButtons.Y))
            {
                setButtonPressed(rctY, true);
            }
            else
            {
                setButtonPressed(rctY, false);
            }

            // LT RT
            pbLT.Value = gamepad.LeftTrigger / 255;
        }

        private void setButtonPressed(Rectangle rect, bool pressed)
        {
            if (pressed)
            {
                rect.Fill = Brushes.Green;
            }
            else
            {
                rect.Fill = Brushes.Red;
            }
        }
    }
}
