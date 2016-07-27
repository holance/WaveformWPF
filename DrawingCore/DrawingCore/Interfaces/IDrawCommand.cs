using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace DrawingCore.Interfaces
{
    public interface IDrawCommand
    {
        void Draw(double canvasWidth, double canvasHeight, CanvasDrawingSession session, Rect rect);
    }
}
