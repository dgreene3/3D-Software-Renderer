using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Timers;


namespace _3D_Software_Renderer_CSharp_WinForms
{
    public partial class Form1 : Form
    {
        Bitmap backBuffer;
        RenderContext target;

        DateTime previousTime;
        double timeDelta = 0.0;


        public Form1()
        {
            InitializeComponent();

            Application.Idle += HandleApplicaitonIdle;

            backBuffer = new Bitmap(Width, Height);
            target = new RenderContext(backBuffer); // set render target bitmap backbuffer

            previousTime = DateTime.Now;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;           
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            e.Graphics.DrawImage(target.backBuffer, 0, 0);
        }

        private void HandleApplicaitonIdle(object sender, EventArgs e)
        {
            while(IsApplicationIdle())
            {
                UpdateGame();
                RenderGame();
            }
        }
        private bool IsApplicationIdle()
        {
            NativeMessage result;
            return PeekMessage(out result, IntPtr.Zero, (uint)0, (uint)0, (uint)0) == 0;
        }
        private void UpdateGame()
        {
            // Do any updating
            DateTime currentTime = DateTime.Now;
            long numTicks = (currentTime - previousTime).Ticks;
            TimeSpan span = new TimeSpan(numTicks);
            timeDelta = (float)span.TotalSeconds;
            previousTime = currentTime;
        }
        private void RenderGame()
        {
            target.BeginRender();
            target.Clear();

            for (int j = 100; j < 200; ++j)
            {
                target.DrawScanBuffer(j, 300 - j, 300 + j);
            }
            target.FillShape(100, 200);

            target.EndRender();
            Refresh();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {

        }
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
           
        }





        /*
        private void SetPixel(Bitmap ProcessedBitmap, int x, int y, Color color)
        {
            unsafe
            {
                BitmapData bitmapData = ProcessedBitmap.LockBits(new Rectangle(0, 0, ProcessedBitmap.Width, ProcessedBitmap.Height),
                    ImageLockMode.ReadWrite, ProcessedBitmap.PixelFormat);

                int BytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(ProcessedBitmap.PixelFormat) / 8;
                int HeightInPixels = bitmapData.Height;
                int WidthInPixels = bitmapData.Width * BytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                byte* CurrentLine = PtrFirstPixel + (y * bitmapData.Stride);    // get pixel line
                int pixelIndex = x * BytesPerPixel; // Get Pixel offset 

                // BGR
                CurrentLine[pixelIndex] = (byte)color.B;
                CurrentLine[pixelIndex + 1] = (byte)color.G;
                CurrentLine[pixelIndex + 2] = (byte)color.R;

                ProcessedBitmap.UnlockBits(bitmapData);
            }
        }

        private void ClearBitmap(Bitmap ProcessedBitmap)
        {
            unsafe
            {
                BitmapData bitmapData = ProcessedBitmap.LockBits(new Rectangle(0, 0, ProcessedBitmap.Width, ProcessedBitmap.Height), 
                    ImageLockMode.ReadWrite, ProcessedBitmap.PixelFormat);

                int BytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(ProcessedBitmap.PixelFormat) / 8;
                int HeightInPixels = bitmapData.Height;
                int WidthInPixels = bitmapData.Width * BytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                Parallel.For(0, HeightInPixels, y =>
                    {
                        byte* CurrentLine = PtrFirstPixel + (y * bitmapData.Stride);
                        for(int x = 0; x < WidthInPixels; x = x + BytesPerPixel)
                        {
                            CurrentLine[x] = (byte)0;     
                            CurrentLine[x + 1] = (byte)0;   
                            CurrentLine[x + 2] = (byte)0;
                        }
                    });
                ProcessedBitmap.UnlockBits(bitmapData);
            }
        }
        */



        /// <summary>
        /// /////////////////////////////////////////////////////////////////
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NativeMessage
        {
            public IntPtr Handle;
            public uint Message;
            public IntPtr WParameter;
            public IntPtr LParameter;
            public uint Time;
            public Point Location;

        }
        [DllImport("user32.dll")]
        public static extern int PeekMessage(out NativeMessage message, IntPtr window, uint filterMin, uint filterMax, uint remove);
    }
}
