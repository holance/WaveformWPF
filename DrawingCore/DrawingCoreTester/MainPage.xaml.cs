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
using Windows.UI;
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
            mTimer.Interval = TimeSpan.FromMilliseconds(10);
        }

        private byte[] buffer = new byte[4];
        private void Timer_Tick(object sender, object e)
        {
            var rect = new Rect(mRnd.Next(0, (int)canvas.ActualWidth - 400), mRnd.Next(0, (int)canvas.ActualHeight - 400), 400, 400);
            for (int i=0; i<10; ++i)
            {
                var cmd = new SimpleDrawing();
                cmd.Width = mRnd.Next(5, (int)rect.Width);
                cmd.Height = cmd.Width;
                cmd.Center = new System.Numerics.Vector2((float)(rect.Width/2+rect.Left), (float)(rect.Height/2+rect.Top));
                mRnd.NextBytes(buffer);
                cmd.Color = Color.FromArgb(buffer[0], buffer[1], buffer[2], buffer[3]);
                mDrawingEngine.PushJob(cmd);
            }
            mDrawingEngine.Invalidate(rect);
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mRnd.NextBytes(buffer);
            mDrawingEngine.Clear(Color.FromArgb(buffer[0], buffer[1], buffer[2], buffer[3]));
        }
    }
}
