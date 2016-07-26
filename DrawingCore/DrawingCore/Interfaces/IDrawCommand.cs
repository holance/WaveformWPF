using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingCore.Interfaces
{
    public interface IDrawCommand
    {
        void Draw(CanvasDrawingSession session);
    }
}
