using DrawingCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using GalaSoft.MvvmLight;
using Windows.UI;

namespace DrawingCore.Drawing
{
    public class TraceDrawing : ObservableObject, IDrawCommand
    {
        private float[] mBuffer = null;
        public float[] Buffer
        {
            set
            {
                if(Set<float[]>(ref mBuffer, value))
                {
                    if (mBuffer != null)
                    {
                        DrawStart = 0;
                        DrawEnd = mBuffer.Length;
                    }
                }
            }
            get
            {
                return mBuffer;
            }
        }
        public int BufferLength
        {
            get
            {
                if (mBuffer == null)
                {
                    return -1;
                }
                return mBuffer.Length;
            }
        }

        private int mDrawStart = 0;
        public int DrawStart
        {
            set
            {
                if(Set<int>(ref mDrawStart, value))
                {
                    if(mDrawStart>=BufferLength || mDrawStart < 0)
                    {
                        throw new ArgumentOutOfRangeException("DrawStart Index out of bound.");
                    }
                }
            }
            get
            {
                return mDrawStart;
            }
        }

        private int mDrawEnd = 0;
        public int DrawEnd
        {
            set
            {
                if(Set<int>(ref mDrawEnd, value))
                {
                    if (mDrawEnd > BufferLength || mDrawEnd < 0)
                    {
                        throw new ArgumentOutOfRangeException("DrawStart Index out of bound.");
                    }
                }
            }
            get
            {
                return mDrawEnd;
            }
        }

        public float Scale { set; get; }

        private Color mColor = Colors.LightBlue;
        public Color Color
        {
            set
            {
                Set<Color>(ref mColor, value);
            }
            get
            {
                return mColor;
            }
        }

        public float AxisX { set; get; }
        public void Draw(CanvasControl canvas, CanvasDrawingSession session)
        {
            if (DrawStart >= DrawEnd)
            {
                throw new ArgumentOutOfRangeException("DrawStart >= DrawEnd");
            }
            int dataLength = DrawEnd - DrawStart;
            var dataRange = Buffer.Skip(DrawStart).Take(dataLength);
            for(int i=0; i<dataLength; ++i)
            {

            }
        }
    }
}
