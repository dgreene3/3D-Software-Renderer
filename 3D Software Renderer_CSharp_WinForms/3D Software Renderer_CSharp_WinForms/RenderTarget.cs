using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace _3D_Software_Renderer_CSharp_WinForms
{
    class RenderTarget
    {
        public Bitmap backBuffer;

        private BitmapData bitmapData;
        private int BytesPerPixel;
        private int HeightInPixels;
        private int WidthInPixels;

        public RenderTarget(Bitmap bmp)
        {
            backBuffer = bmp;

            // One-time initialization
            for (int y = 0; y < bmp.Height; ++y)
            {
                for (int x = 0; x < bmp.Width; ++x)
                {
                    backBuffer.SetPixel(x, y, Color.Black);
                    
                }
            }

            BitmapData bitmapData = backBuffer.LockBits(new Rectangle(0, 0, backBuffer.Width, backBuffer.Height),
                ImageLockMode.ReadWrite, backBuffer.PixelFormat);
            BytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(backBuffer.PixelFormat) / 8;
            HeightInPixels = bitmapData.Height;
            WidthInPixels = bitmapData.Width * BytesPerPixel;
            backBuffer.UnlockBits(bitmapData);
        }

        public void Clear()
        {
            unsafe
            {
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                Parallel.For(0, HeightInPixels, y =>
                {
                    byte* CurrentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < WidthInPixels; x = x + BytesPerPixel)
                    {
                        // BGR
                        CurrentLine[x] = (byte)0;
                        CurrentLine[x + 1] = (byte)0;
                        CurrentLine[x + 2] = (byte)0;
                    }
                });
            }
        }

        public void BeginRender()
        {
            unsafe
            {
                bitmapData = backBuffer.LockBits(new Rectangle(0, 0, backBuffer.Width, backBuffer.Height),
                        ImageLockMode.ReadWrite, backBuffer.PixelFormat);
            }
        }
        public void EndRender()
        {
            unsafe
            {
                backBuffer.UnlockBits(bitmapData);
            }
        }

        public void DrawPixel(int x, int y, Color color)
        {
            unsafe
            {
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                byte* CurrentLine = PtrFirstPixel + (y * bitmapData.Stride);    // get pixel line
                int pixelIndex = x * BytesPerPixel; // Get Pixel offset 

                // BGR
                CurrentLine[pixelIndex] = (byte)color.B;
                CurrentLine[pixelIndex + 1] = (byte)color.G;
                CurrentLine[pixelIndex + 2] = (byte)color.R;
            }
        }
    }
}
