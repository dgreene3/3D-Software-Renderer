using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace _3D_Software_Renderer_CSharp_WinForms
{
    class Stars3D : Random
    {
        private float m_spread;
        private float m_speed;

        private float[] m_starX;
        private float[] m_starY;
        private float[] m_starZ;


        public Stars3D(int numStars, float spread, float speed)
        {
            m_spread = spread;
            m_speed = speed;

            m_starX = new float[numStars];
            m_starY = new float[numStars];
            m_starZ = new float[numStars];

            for(int i = 0; i < numStars; ++i)
            {
                InitStar(i);
            }
        }
        private void InitStar(int i)
        {
            m_starX[i] = 2 * ((float)Sample() - 0.5f) * m_spread;
            m_starY[i] = 2 * ((float)Sample() - 0.5f) * m_spread;

            m_starZ[i] = ((float)Sample() + 0.00001f) * m_spread;
        }

        public void UpdateAndRender(RenderTarget target, double delta)
        {
            float halfWidth = (float)(target.backBuffer.Width / 2.0f);
            float halfHeight = (float)(target.backBuffer.Height / 2.0f);

            for(int i = 0; i < m_starX.Length; ++i)
            {
                m_starZ[i] -= (float)delta * m_speed;

                if(m_starZ[i] <= 0)
                {
                    InitStar(i);
                }

                int x = (int)((m_starX[i] / m_starZ[i]) * halfWidth + halfWidth);
                int y = (int)((m_starY[i] / m_starZ[i]) * halfHeight + halfHeight);

                if(x < 0 || x >= target.backBuffer.Width ||
                    y < 0 || y >= target.backBuffer.Height)
                {
                    InitStar(i);
                }
                else
                {
                    target.DrawPixel(x, y, Color.White);
                }
            }
        }
    }
}
