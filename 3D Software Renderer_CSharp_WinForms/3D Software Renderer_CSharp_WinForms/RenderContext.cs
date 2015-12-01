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

        public void FillTriangle(Vertex v1, Vertex v2, Vertex v3)
        {
            Matrix4f screenSpaceTransform =  
				new Matrix4f().InitScreenSpaceTransform(backBuffer.Width/2, backBuffer.Height/2); 
		    Vertex minYVert = v1.Transform(screenSpaceTransform).PerspectiveDivide(); 
 		    Vertex midYVert = v2.Transform(screenSpaceTransform).PerspectiveDivide(); 
 		    Vertex maxYVert = v3.Transform(screenSpaceTransform).PerspectiveDivide(); 

 
 
 		    if(maxYVert.GetY() < midYVert.GetY()) 
 		    { 
 			    Vertex temp = maxYVert; 
 			    maxYVert = midYVert; 
 			    midYVert = temp; 
 		    } 
 
 
 		    if(midYVert.GetY() < minYVert.GetY()) 
 		    { 
 			    Vertex temp = midYVert; 
 			    midYVert = minYVert; 
 			    minYVert = temp; 
 		    } 
 
 
 		    if(maxYVert.GetY() < midYVert.GetY()) 
 		    { 
 			    Vertex temp = maxYVert; 
 			    maxYVert = midYVert; 
 			    midYVert = temp; 
 		    } 
 
 
 		    float area = minYVert.TriangleAreaTimes2(maxYVert, midYVert); 
 		    int handedness = area >= 0 ? 1 : 0; 
 
 
 		    ScanConvertTriangle(minYVert, midYVert, maxYVert, handedness); 
 		    FillShape((int)minYVert.GetY(), (int)maxYVert.GetY()); 

        }

        public void ScanConvertTriangle(Vertex minYVert, Vertex midYVert, Vertex maxYVert, int handedness)
        {
            ScanConvertLine(minYVert, maxYVert, 0 + handedness); 
 		    ScanConvertLine(minYVert, midYVert, 1 - handedness); 
 		    ScanConvertLine(midYVert, maxYVert, 1 - handedness); 

        }
        public void ScanConvertLine(Vertex minYVert, Vertex maxYVert, int whichSide)
        {
            int yStart = (int)minYVert.GetY(); 
		    int yEnd   = (int)maxYVert.GetY(); 
 		    int xStart = (int)minYVert.GetX(); 
		    int xEnd   = (int)maxYVert.GetX(); 

 
 		    int yDist = yEnd - yStart; 
	        int xDist = xEnd - xStart; 

 
	        if(yDist <= 0) 
 		    { 
			    return; 
		    } 
 
 
 		    float xStep = (float)xDist/(float)yDist; 
 		    float curX = (float)xStart; 
 
 
 		    for(int j = yStart; j < yEnd; j++) 
 		    { 
 			    m_scanBuffer[j * 2 + whichSide] = (int)curX; 
 			    curX += xStep; 
 		    } 

        }

    }
}
