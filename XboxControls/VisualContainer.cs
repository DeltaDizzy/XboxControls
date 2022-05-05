using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace XboxControls
{
    public class VisualContainer : FrameworkElement
    {
        private VisualCollection children;

        public VisualContainer()
        {
            children = new VisualCollection(this);
        }

        public void Clear()
        {
            children.RemoveRange(0, children.Count);
        }

        public void CreateDrawingVisualRectangle(Enemy enemy)
        {
            DrawingVisual drawingVisual = new DrawingVisual();

            // Retrieve the DrawingContext in order to create new drawing content.
            DrawingContext drawingContext = drawingVisual.RenderOpen();


            // Create a rectangle and draw it in the DrawingContext.
            Rect rect = new Rect(new Point(enemy.Pos.X, enemy.Pos.Y), new Size(enemy.Dimensions.Width, enemy.Dimensions.Height));
            drawingContext.DrawRectangle(Brushes.Red, null, rect);

            // Persist the drawing content.
            drawingContext.Close();

            children.Add(drawingVisual);
        }
    }
}
