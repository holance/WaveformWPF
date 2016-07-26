using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingCore.Interfaces
{
    public interface IDrawCommand
    {
        void Draw(CanvasControl canvas, CanvasDrawingSession session);
    }
}
