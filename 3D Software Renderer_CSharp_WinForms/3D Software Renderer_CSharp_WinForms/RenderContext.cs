using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace _3D_Software_Renderer_CSharp_WinForms
{
    class RenderContext : RenderTarget
    {
        private int[] m_scanBuffer;

        public RenderContext(Bitmap bmp) : base(bmp)
        {
            m_scanBuffer = new int[bmp.Height * 2];
        }

        public void DrawScanBuffer(int yCoord, int xMin, int xMax)
        {
            m_scanBuffer[yCoord * 2    ]    = xMin;
            m_scanBuffer[yCoord * 2 + 1]    = xMax;    
        }

        public void FillShape(int yMin, int yMax)
        {
            for(int j = yMin; j < yMax; j++) 
    		{ 
    			int xMin = m_scanBuffer[j * 2]; 
     			int xMax = m_scanBuffer[j * 2 + 1]; 
     
 
     			for(int i = xMin; i < xMax; i++) 
     			{ 
     				DrawPixel(i, j, Color.FromArgb(255, 255, 255)); 
     			} 
     		} 
        }
    }
}
