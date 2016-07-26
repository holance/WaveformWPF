/*
The MIT License (MIT)

Copyright (c) 2016 Lunci

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.Graphics.Canvas;
using DrawingCore.Interfaces;
using Windows.UI.Xaml.Controls;
using Windows.UI;
using System.Collections.Concurrent;

namespace DrawingCore.Engine
{
    public class DrawingEngine: IEngine
    {
        private CanvasControl mCanvas = null;
        private ConcurrentQueue<IDrawCommand> mJobs = new ConcurrentQueue<IDrawCommand>();
        private bool mAttached = false;
        public DrawingEngine()
        {
        }

        public void AttachCanvas(UserControl canvas)
        {
            DetachCanvas();
            if(canvas is CanvasControl)
            {
                mCanvas = canvas as CanvasControl;
                mCanvas.CreateResources += MCanvas_CreateResources;
                mCanvas.Draw += MCanvas_Draw;
                mAttached = true;
            }
            else
            {
                throw new ArgumentException("Argument only accepts CanvasControl.");
            }
        }

        private void MCanvas_CreateResources(CanvasControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
        }

        private void MCanvas_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            IDrawCommand cmd = null;
            var session = args.DrawingSession;
            while(mJobs.TryDequeue(out cmd))
            {
                cmd.Draw(sender, session);
            }
        }

        public void DetachCanvas()
        {
            if (mCanvas != null)
            {
                mAttached = false;
                mCanvas.CreateResources -= MCanvas_CreateResources;
                mCanvas.Draw -= MCanvas_Draw;
                mCanvas = null;              
            }
        }

        public bool PushJob(IDrawCommand cmd)
        {
            if (!mAttached)
            {
                return false;
            }
            mJobs.Enqueue(cmd);
            return true;
        }

        public void Invalidate()
        {
            if (!mAttached)
            {
                return;
            }
            mCanvas.Invalidate();
        }
    }
}
