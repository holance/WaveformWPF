using DrawingCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;
using System.Numerics;
using Microsoft.Graphics.Canvas.Brushes;
using Windows.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;

namespace DrawingCore.Drawing
{
    public class SimpleDrawing : IDrawCommand
    {
        public float Width { set; get; }
        public float Height { set; get; }

        public Vector2 Center { set; get; }

        public Color Color { set; get; }

        public SimpleDrawing()
        {
            Color = Colors.Red;
            Width = 10;
            Height = 10;
            Center = new Vector2(5, 5);
        }
        public void Draw(CanvasControl canvas, CanvasDrawingSession session)
        {
            session.DrawEllipse(Center, Width, Height, Color, 2);
        }
    }
}
