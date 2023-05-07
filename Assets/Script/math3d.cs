using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace math3d
{
    [Serializable]
    public class zQuaternion
    {
        public float x;
        public float y;
        public float z;
        public float w;
        public zQuaternion()
        {

        }

        public zQuaternion(float theta,zVector3 axis)
        {
            axis.Normalize();
            this.w = Mathf.Cos(theta / 2);
            this.x = Mathf.Sin(theta / 2) * axis.GetX();
            this.y = Mathf.Sin(theta / 2) * axis.GetY();
            this.z = Mathf.Sin(theta / 2) * axis.GetZ();
        }
        public zQuaternion(float theta,Vector3 temp)
        {
            zVector3 axis = new zVector3(temp.x, temp.y, temp.z);
            axis.Normalize();
            this.w = Mathf.Cos(theta / 2);
            this.x = Mathf.Sin(theta / 2) * axis.GetX();
            this.y = Mathf.Sin(theta / 2) * axis.GetY();
            this.z = Mathf.Sin(theta / 2) * axis.GetZ();
        }
        public zQuaternion(float x, float y, float z)
        {
            this.w = 0;
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public zQuaternion(Vector3 point)
        {
            this.w = 0;
            this.x = point.x;
            this.y = point.y;
            this.z = point.z;
        }
        public zQuaternion newInverse()
        {
            return new zQuaternion(-this.x, -this.y, -this.z,this.w);
        }
        public zQuaternion(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
        public void SetX(float x)
        {
            this.x = x;
        }
        public void SetY(float y)
        {
            this.y = y;
        }
        public void SetZ(float z)
        {
            this.z = z;
        }
        public void SetW(float w)
        {
            this.w = w;
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
        public RotateMatrix Quaternion2RotateMatrix()
        {
            RotateMatrix rotateMatrix = new RotateMatrix();
            rotateMatrix.SetR11(1 - 2 * y * y - 2 * z * z);
            rotateMatrix.SetR12(2 * x * y + 2 * w * z);
            rotateMatrix.SetR13(2 * x * z + 2 * w * y);
            rotateMatrix.SetR21(2 * x * y + 2 * w * z);
            rotateMatrix.SetR22(1 - 2 * x * x - 2 * z * z);
            rotateMatrix.SetR23(2 * y * z - 2 * w * x);
            rotateMatrix.SetR31(2 * x * z - 2 * w * y);
            rotateMatrix.SetR32(2 * y * z + 2 * w * x);
            rotateMatrix.SetR33(1 - 2 * x * x - 2 * y * y);
            return rotateMatrix;
        }
        public EulerAngle Quaternion2EulerAngle()
        {
            RotateMatrix rotateMatrix = Quaternion2RotateMatrix();
     
            EulerAngle eulerAngle = new EulerAngle();
            eulerAngle.SetAngleX(360 / (2 * Mathf.PI) * Mathf.Asin(-rotateMatrix.GetR23()));
            eulerAngle.SetAngleY(360 / (2 * Mathf.PI) * Mathf.Atan2(rotateMatrix.GetR13(), rotateMatrix.GetR33()));
            eulerAngle.SetAngleZ(360 / (2 * Mathf.PI) * Mathf.Atan2(rotateMatrix.GetR21(), rotateMatrix.GetR22()));

            return eulerAngle;
        }

        
        public zQuaternion newRightMultiply(zQuaternion other)
        {
            float w = this.w*other.w-this.x*other.x-this.y*other.y-this.z*other.z;
            float x = this.w*other.x+this.x*other.w+this.y*other.z-this.z*other.y;
            float y = this.w*other.y-this.x*other.z+this.y*other.w+this.z*other.x;
            float z = this.w*other.z+this.x*other.y-this.y*other.x+this.z*other.w;
            return new zQuaternion(x,y,z,w);

        }
        public zVector3 ExtractImaginaryPart()
        {
            return new zVector3(x,y,z);
        }
    }
    public class RotateMatrix
    {
        private float r11;
        private float r12;
        private float r13;
        private float r21;
        private float r22;
        private float r23;
        private float r31;
        private float r32;
        private float r33;
        public void SetR11(float r11)
        {
            this.r11 = r11;
        }
        public void SetR12(float r12)
        {
            this.r12 = r12;
        }
        public void SetR13(float r13)
        {
            this.r13 = r13;
        }
        public void SetR21(float r21)
        {
            this.r21 = r21;
        }
        public void SetR22(float r22)
        {
            this.r22 = r22;
        }
        public void SetR23(float r23)
        {
            this.r23 = r23;
        }
        public void SetR31(float r31)
        {
            this.r31 = r31;
        }
        public void SetR32(float r32)
        {
            this.r32 = r32;
        }
        public void SetR33(float r33)
        {
            this.r33 = r33;
        }
        public float GetR11()
        {
            return r11;
        }
        public float GetR12()
        {
            return r12;
        }
        public float GetR13()
        {
            return r13;
        }
        public float GetR21()
        {
            return r21;
        }
        public float GetR22()
        {
            return r22;
        }
        public float GetR23()
        {
            return r23;
        }
        public float GetR31()
        {
            return r31;
        }
        public float GetR32()
        {
            return r32;
        }
        public float GetR33()
        {
            return r33;
        }
    }
    [Serializable]
    public class Angle
    {
        public float rad;
        public float deg;

        public Angle()
        {

        }
        public Angle(float deg)
        {
            this.deg = deg;
            rad = deg * 2 * Mathf.PI / 360; 
        }
        public void SetRad(float rad)
        {
            this.rad = rad;
			this.deg=rad*180/ Mathf.PI;
        }
        public void SetDeg(float deg)
        {
            this.deg = deg;
            rad = deg * 2 * Mathf.PI / 360;
        }
        public float GetRad()
        {
            return rad;
        }
        public float GetDeg()
        {
            return deg;
        }
    }
    public class EulerAngle
    {
        private Angle angleX;
        private Angle angleY;
        private Angle angleZ;
        public EulerAngle()
        {
            angleX = new Angle();
            angleY = new Angle();
            angleZ = new Angle();
        }
        public EulerAngle(float degX, float degY, float degZ)
        {
            angleX = new Angle(degX);
            angleY = new Angle(degY);
            angleZ = new Angle(degZ);
        }
        public void SetAngleX(float deg)
        {
            angleX.SetDeg(deg);
        }
        public void SetAngleY(float deg)
        {
            angleY.SetDeg(deg);
        }
        public void SetAngleZ(float deg)
        {
            angleZ.SetDeg(deg);
        }
        public Angle GetAngleX()
        {
            return angleX;
        }
        public Angle GetAngleY()
        {
            return angleY;
        }
        public Angle GetAngleZ()
        {
            return angleZ;
        }
        public RotateMatrix EulerAngle2RotateMatrix()
        {
            RotateMatrix rotateMatrix = new RotateMatrix();
            float x = angleX.GetRad();
            float y = angleY.GetRad();
            float z = angleZ.GetRad();
            rotateMatrix.SetR11(Mathf.Cos(y) * Mathf.Cos(z) + Mathf.Sin(y) * Mathf.Sin(x) * Mathf.Sin(z));
            rotateMatrix.SetR12(-Mathf.Cos(y) * Mathf.Sin(z) + Mathf.Sin(y) * Mathf.Sin(x) * Mathf.Cos(z));
            rotateMatrix.SetR13(Mathf.Sin(y) * Mathf.Cos(x));
            rotateMatrix.SetR21(Mathf.Cos(x) * Mathf.Sin(z));
            rotateMatrix.SetR22(Mathf.Cos(x) * Mathf.Cos(z));
            rotateMatrix.SetR23(-Mathf.Sin(x));
            rotateMatrix.SetR31(-Mathf.Sin(y) * Mathf.Cos(z) + Mathf.Cos(y) * Mathf.Sin(x) * Mathf.Sin(z));
            rotateMatrix.SetR32(Mathf.Sin(y) * Mathf.Sin(z) + Mathf.Cos(y) * Mathf.Sin(x) * Mathf.Cos(z));
            rotateMatrix.SetR33(Mathf.Cos(y) * Mathf.Cos(x));
            return rotateMatrix;
        }
        public zQuaternion EulerAngle2Quaternion()
        {
            float x = angleX.GetRad();
            float y = angleY.GetRad();
            float z = angleZ.GetRad();
            zQuaternion q = new zQuaternion();
            q.SetX(Mathf.Cos(y / 2) * Mathf.Sin(x / 2) * Mathf.Cos(z / 2) + Mathf.Cos(x / 2) * Mathf.Sin(y / 2) * Mathf.Sin(z / 2));
            q.SetY(Mathf.Sin(y / 2) * Mathf.Cos(x / 2) * Mathf.Cos(z / 2) - Mathf.Sin(x / 2) * Mathf.Cos(y / 2) * Mathf.Sin(z / 2));
            q.SetZ(Mathf.Cos(y / 2) * Mathf.Cos(x / 2) * Mathf.Sin(z / 2) - Mathf.Sin(x / 2) * Mathf.Sin(y / 2) * Mathf.Cos(z / 2));
            q.SetW(Mathf.Cos(y/2)* Mathf.Cos(x / 2)* Mathf.Cos(z / 2)+ Mathf.Sin(x / 2)* Mathf.Sin(y / 2)* Mathf.Sin(z / 2));
            return q;
        }
    }
    [Serializable]
    public class zVector3
    {
        public float x;
        public float y;
        public float z;
        public zVector3() { }
        public zVector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public zVector3(Vector3 temp)
        {
            this.x = temp.x;
            this.y = temp.y;
            this.z = temp.z;
        }
        public zVector3 Rotate(Vector3 axis, Angle angle)
        {
            zQuaternion p=new zQuaternion(angle.rad, axis);
            zQuaternion inverseP = p.newInverse();
            zQuaternion r = new zQuaternion(this.x,this.y,this.z);
            return p.newRightMultiply(r).newRightMultiply(inverseP).ExtractImaginaryPart();
        }
		public Vector3 newVector3()
		{
			return new Vector3(this.x,this.y,this.z);	
		}
        public void Normalize()
        {
            float length = Mathf.Sqrt(x * x + y * y + z * z);
            x = x / length;
            y = y / length;
            z = z / length;
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
    }
    [Serializable]
    public class zVector2
    {
        public float x;
        public float z;
        public zVector2() { }
        public zVector2(float x, float z)
        {
            this.x = x;
            this.z = z;
        }
		public zVector2(Vector3 temp)
		{
			this.x=temp.x;
			this.z=temp.z;
		}
        public void Normalize()
        {
            float length = Mathf.Sqrt(x * x + z * z);
            x = x / length;
            z= z / length;
        }
        public float GetX()
        {
            return x;
        }
        public float GetZ()
        {
            return z;
        }

        public zVector2 newRight()
        {
            return new zVector2(-this.z, this.x);
        }
        public float newTheta()
        {
            float up = this.x;
            float down = Mathf.Sqrt(this.x * this.x+this.z*this.z);
            return Mathf.Acos(up/down);
        }
    }
}
