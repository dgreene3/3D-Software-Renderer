using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3D_Software_Renderer_CSharp_WinForms
{
    class Vector4f
    {
        private float x; 
 	    private float y; 
 	    private float z; 
 	    private float w; 
 
 
 	    public Vector4f(float x, float y, float z, float w) 
 	    { 
 		    this.x = x; 
 		    this.y = y; 
 		    this.z = z; 
 		    this.w = w; 
 	    } 
 
 
 	    public float Length() 
 	    { 
 		    return (float)Math.Sqrt(x * x + y * y + z * z + w * w); 
 	    } 
 
 
 	    public float Max() 
 	    { 
 		    return Math.Max(Math.Max(x, y), Math.Max(z, w)); 
 	    } 
 
 
 	    public float Dot(Vector4f r) 
 	    { 
 		    return x * r.GetX() + y * r.GetY() + z * r.GetZ() + w * r.GetW(); 
 	    } 
 
 
 	    public Vector4f Cross(Vector4f r) 
 	    { 
 		    float x_ = y * r.GetZ() - z * r.GetY(); 
 		    float y_ = z * r.GetX() - x * r.GetZ(); 
 		    float z_ = x * r.GetY() - y * r.GetX(); 
 
 
 		    return new Vector4f(x_, y_, z_, 0); 
 	    } 
 
 
 	    public Vector4f Normalized() 
	    { 
 		    float length = Length(); 
 
 
 		    return new Vector4f(x / length, y / length, z / length, w / length); 
	    } 
 
 
 	    public Vector4f Rotate(Vector4f axis, float angle) 
 	    { 
 		    float sinAngle = (float)Math.Sin(-angle); 
		    float cosAngle = (float)Math.Cos(-angle); 
 
 
 		    return this.Cross(axis.Mul(sinAngle)).Add(           //Rotation on local X 
 				    (this.Mul(cosAngle)).Add(                     //Rotation on local Z 
						    axis.Mul(this.Dot(axis.Mul(1 - cosAngle))))); //Rotation on local Y 
 	    } 
 
 	    public Vector4f Lerp(Vector4f dest, float lerpFactor) 
 	    { 
 		    return dest.Sub(this).Mul(lerpFactor).Add(this); 
 	    } 
 
 
 	    public Vector4f Add(Vector4f r) 
 	    { 
 		    return new Vector4f(x + r.GetX(), y + r.GetY(), z + r.GetZ(), w + r.GetW()); 
 	    } 
 
 
 	    public Vector4f Add(float r) 
 	    { 
 		    return new Vector4f(x + r, y + r, z + r, w + r); 
 	    } 
 
 
 	    public Vector4f Sub(Vector4f r) 
 	    { 
 		    return new Vector4f(x - r.GetX(), y - r.GetY(), z - r.GetZ(), w - r.GetW()); 
 	    } 
 
 
 	    public Vector4f Sub(float r) 
 	    { 
 		    return new Vector4f(x - r, y - r, z - r, w - r); 
 	    } 
 
 
 	    public Vector4f Mul(Vector4f r) 
	    { 
		    return new Vector4f(x * r.GetX(), y * r.GetY(), z * r.GetZ(), w * r.GetW()); 
 	    } 

 
 	    public Vector4f Mul(float r) 
	    { 
		    return new Vector4f(x * r, y * r, z * r, w * r); 
	    } 

 
	    public Vector4f Div(Vector4f r) 
 	    { 
 		    return new Vector4f(x / r.GetX(), y / r.GetY(), z / r.GetZ(), w / r.GetW()); 
 	    } 
 
 
 	    public Vector4f Div(float r) 
 	    { 
 		    return new Vector4f(x / r, y / r, z / r, w / r); 
 	    } 
 
 
 	    public Vector4f Abs() 
 	    { 
 		    return new Vector4f(Math.Abs(x), Math.Abs(y), Math.Abs(z), Math.Abs(w)); 
 	    } 
 
 
 	    public String toString() 
 	    { 
 		    return "(" + x + ", " + y + ", " + z + ", " + w + ")"; 
 	    } 
 
 
 	    public float GetX() 
	    { 
            return x; 
 	    } 
 
 
 	    public float GetY() 
 	    { 
 		    return y; 
 	    } 
 
 
 	    public float GetZ() 
 	    { 
 		    return z; 
 	    } 
 
 
 	    public float GetW() 
 	    { 
 		    return w; 
 	    } 
 
 
 	    public bool equals(Vector4f r) 
 	    { 
 		    return x == r.GetX() && y == r.GetY() && z == r.GetZ() && w == r.GetW(); 
	    } 

    }
}
