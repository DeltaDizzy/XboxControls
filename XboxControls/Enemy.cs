using System.Windows.Shapes;
using System.Numerics;
using System.Drawing;
using System.Windows.Media;
using System;

namespace XboxControls
{
    public class Enemy
    {
        public Vector2 Pos { get; private set; }
        public Size Dimensions { get; private set; }

        public Enemy(int width, int height)
        {
            Random r = new Random();
            Pos = new Vector2(r.Next(width), r.Next(height));
            Dimensions = new Size(10, 10);
        }
    }
}
