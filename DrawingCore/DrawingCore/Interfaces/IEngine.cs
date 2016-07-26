using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace DrawingCore.Interfaces
{
    public interface IEngine
    {
        void AttachCanvas(UserControl canvas);

        void DetachCanvas();

        void Invalidate();

        bool PushJob(IDrawCommand cmd);
    }
}
