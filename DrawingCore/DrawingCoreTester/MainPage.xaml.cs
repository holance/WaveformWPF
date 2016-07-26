using DrawingCore.Drawing;
using DrawingCore.Engine;
using DrawingCore.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DrawingCoreTester
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private IEngine mDrawingEngine = new DrawingEngine();
        private DispatcherTimer mTimer = new DispatcherTimer();
        private Random mRnd = new Random();
        public MainPage()
        {
            this.InitializeComponent();
            Loaded += MainPage_Loaded;
            Unloaded += MainPage_Unloaded;
            mTimer.Tick += Timer_Tick;
            mTimer.Interval = TimeSpan.FromMilliseconds(20);
        }

        private void Timer_Tick(object sender, object e)
        {
            for(int i=0; i<1000; ++i)
            {
                var cmd = new SimpleDrawing();
                cmd.Width = mRnd.Next(5, 100);
                cmd.Height = mRnd.Next(5, 100);
                cmd.Center = new System.Numerics.Vector2(mRnd.Next(0, 1000), mRnd.Next(0, 1000));
                mDrawingEngine.PushJob(cmd);
            }
            mDrawingEngine.Invalidate();
        }

        private void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
            mTimer.Stop();
            mDrawingEngine.DetachCanvas();
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            mDrawingEngine.AttachCanvas(canvas);
            mTimer.Start();
        }
    }
}
