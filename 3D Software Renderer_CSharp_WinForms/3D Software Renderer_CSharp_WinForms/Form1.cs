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

        long frameCount;
        DateTime lastFPSTime = DateTime.Now;

        DateTime previousTime;
        double timeDelta = 0.0;

        Vertex minYVert = new Vertex(-1, -1, 0);
        Vertex midYVert = new Vertex(0, 1, 0);
        Vertex maxYVert = new Vertex(1, -1, 0);

        float rotCounter;
        Matrix4f projection;

        public Form1()
        {
            InitializeComponent();

            Application.Idle += HandleApplicaitonIdle;

            backBuffer = new Bitmap(Width, Height);
            target = new RenderContext(backBuffer); // set render target bitmap backbuffer

            previousTime = DateTime.Now;
            frameCount = 0;

            rotCounter = 0.0f;
            projection = new Matrix4f().InitPerspective((float)Math.PI / 2.0f, (float)(Width / Height), 0.1f, 1000.0f);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;           
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

            frameCount++;
        }


        private void RenderGame()
        {
            target.BeginRender();
            target.Clear();

            rotCounter += (float)timeDelta;
            Matrix4f translation = new Matrix4f().InitTranslation(0.0f, 0.0f, 3.0f);
            Matrix4f rotation = new Matrix4f().InitRotation(0.0f, rotCounter, 0.0f);
            Matrix4f transform = projection.Mul(translation.Mul(rotation));
            target.FillTriangle(maxYVert.Transform(transform), midYVert.Transform(transform), minYVert.Transform(transform));

            target.EndRender();
            Refresh();
        }


        /*****************************************************************************/
        //                      EVENT METHODS                   
           
        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            e.Graphics.DrawImage(target.backBuffer, 0, 0);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {

        }
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            
        }



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
            public System.Drawing.Point Location;

        }
        [DllImport("user32.dll")]
        public static extern int PeekMessage(out NativeMessage message, IntPtr window, uint filterMin, uint filterMax, uint remove);


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F)
            {
                DateTime currentTime = DateTime.Now;
                long numTicks = (currentTime - lastFPSTime).Ticks;
                TimeSpan span = new TimeSpan(numTicks);
                float numSeconds = (float)span.TotalSeconds;

                lastFPSTime = currentTime;
                float fps = (float)(frameCount / numSeconds);

                MessageBox.Show("FPS: " + fps.ToString());
            }
        }

       
    }
}
