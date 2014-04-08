using System;

namespace ArUXV.Math
{
    public struct Quaternion
    {
        public double X, Y, Z, W;

        public static Quaternion IDENTITY()
        {
            return new Quaternion(1, 0, 0, 0);
        }
        public static Quaternion ZERO()
        {
            return new Quaternion(0, 0, 0, 0);
        }

        public Quaternion(double w, double x, double y, double z)
        {
            W = w; X = x; Y = y; Z = z;
        }

        public Quaternion(double w, Vector3d v)
        {
            W = w; X = v.X; Y = v.Y; Z = v.Z;
        }

        public Vector3d V
        {
            set { X = value.X; Y = value.Y; Z = value.Z; }
            get { return new Vector3d(X, Y, Z); }
        }

        public double Magnitude()
        {
            return System.Math.Sqrt(W * W + X * X + Y * Y + Z * Z);
        }

        public void Normalise()
        {
            double m = Magnitude();
            if (m > 0.0001)
            {
                W /= m;
                X /= m;
                Y /= m;
                Z /= m;
            }
            else
            {
                W = 1; X = 0; Y = 0; Z = 0;
            }
        }

        public Quaternion Conjugate()
        {
            X = -X; Y = -Y; Z = -Z;
            return this;
        }

        public void FromAxisAngle(Vector3d axis, double angleRadian)
        {
            double sinAngle;

            double halfangleRadian = angleRadian * 0.5f;
            axis.Normalise();

            double m = axis.Magnitude;
            if (m > 0.0001)
            {
                sinAngle = System.Math.Sin(halfangleRadian);
                this.X = (axis.X * sinAngle);
                this.Y = (axis.Y * sinAngle);
                this.Z = (axis.Z * sinAngle);
                this.W = System.Math.Cos(halfangleRadian);


                double ca = System.Math.Cos(angleRadian / 2);
                double sa = System.Math.Sin(angleRadian / 2);
                X = axis.X / m * sa;
                Y = axis.Y / m * sa;
                Z = axis.Z / m * sa;
                W = ca;
            }
            else
            {
                W = 1; X = 0; Y = 0; Z = 0;
            }
        }

        public Quaternion Add(Quaternion q)
        {
            W = q.W + W;
            X = q.X + X;
            Y = q.Y + Y;
            Z = q.Z + Z;
            return this;
        }

        public Quaternion MultiplyByScalar(double c)
        {
            W = c * W;
            X = c * X;
            Y = c * Y;
            Z = c * Z;
            return this;
        }

        // http://glprogramming.com/codedump/godecho/quaternion.html
        /// <summary>
        /// Return a Quaternion Matrix 4x4 representation
        /// </summary>
        /// <param name="pMatrix"></param>
        /// <returns>double[16] -> 4x4 matrix 
        /// 0   1   2   3
        /// 4   5   6   7   
        /// 8   9   10  11
        /// 12  13  14  15
        /// ---------------------
        /// x   x   x   0
        /// x   x   x   0   
        /// x   x   x   0
        /// 0   0   0   1
        /// </returns>
        public double[] ToMatrix()
        {
            this.Invert();
            double[] pMatrix = new double[16];

		    // First row
            pMatrix[0] = 1.0f - 2.0f * (Y * Y + Z * Z);     
            pMatrix[1] = 2.0f * (X * Y - W * Z);            
	        pMatrix[ 2] = 2.0f * ( X * Z + W * Y );  
	        pMatrix[ 3] = 0.0f;  
	
	        // Second row
	        pMatrix[ 4] = 2.0f * ( X * Y + W * Z );  
	        pMatrix[ 5] = 1.0f - 2.0f * ( X * X + Z * Z );  
	        pMatrix[ 6] = 2.0f * ( Y * Z - W * X );  
	        pMatrix[ 7] = 0.0f;  
	
	        // Third row
	        pMatrix[ 8] = 2.0f * ( X * Z - W * Y );  
	        pMatrix[ 9] = 2.0f * ( Y * Z + W * X );  
	        pMatrix[10] = 1.0f - 2.0f * ( X * X + Y * Y );  
	        pMatrix[11] = 0.0f;  
	
	        // Fourth row
	        pMatrix[12] = 0;  
	        pMatrix[13] = 0;  
	        pMatrix[14] = 0;  
	        pMatrix[15] = 1.0f;

            return pMatrix;
        }



        //http://www.cg.info.hiroshima-cu.ac.jp/~miyazaki/knowledge/teche52.html
        public void FromMatrix(double[] m)
        { 
            W = (m.m3x3(1, 1) + m.m3x3(2, 2) + m.m3x3(3, 3) + 1.0f) / 4.0f;
            X = (m.m3x3(1, 1) - m.m3x3(2, 2) - m.m3x3(3, 3) + 1.0f) / 4.0f;
            Y = (-m.m3x3(1, 1) + m.m3x3(2, 2) - m.m3x3(3, 3) + 1.0f) / 4.0f;
            Z = (-m.m3x3(1, 1) - m.m3x3(2, 2) + m.m3x3(3, 3) + 1.0f) / 4.0f;
            if(W < 0.0f) W = 0.0f;
            if(X < 0.0f) X = 0.0f;
            if(Y < 0.0f) Y = 0.0f;
            if(Z < 0.0f) Z = 0.0f;
            W = System.Math.Sqrt(W);
            X = System.Math.Sqrt(X);
            Y = System.Math.Sqrt(Y);
            Z = System.Math.Sqrt(Z);
            if(W >= X && W >= Y && W >= Z) {
                W *= +1.0f;
                X *= System.Math.Sign(m.m3x3(3, 2) - m.m3x3(2, 3));
                Y *= System.Math.Sign(m.m3x3(1, 3) - m.m3x3(3, 1));
                Z *= System.Math.Sign(m.m3x3(2, 1) - m.m3x3(1, 2));
            } else if(X >= W && X >= Y && X >= Z) {
                W *= System.Math.Sign(m.m3x3(3, 2) - m.m3x3(2, 3));
                X *= +1.0f;
                Y *= System.Math.Sign(m.m3x3(2, 1) + m.m3x3(1, 2));
                Z *= System.Math.Sign(m.m3x3(1, 3) + m.m3x3(3, 1));
            } else if(Y >= W && Y >= X && Y >= Z) {
                W *= System.Math.Sign(m.m3x3(1, 3) - m.m3x3(3, 1));
                X *= System.Math.Sign(m.m3x3(2, 1) + m.m3x3(1, 2));
                Y *= +1.0f;
                Z *= System.Math.Sign(m.m3x3(3, 2) + m.m3x3(2, 3));
            } else if(Z >= W && Z >= X && Z >= Y) {
                W *= System.Math.Sign(m.m3x3(2, 1) - m.m3x3(1, 2));
                X *= System.Math.Sign(m.m3x3(3, 1) + m.m3x3(1, 3));
                Y *= System.Math.Sign(m.m3x3(3, 2) + m.m3x3(2, 3));
                Z *= +1.0f;
            } else {
                throw new Exception("coding error.");
            }
            this.Normalise();
        }

        // http://glprogramming.com/codedump/godecho/quaternion.html
        /// <summary>
        /// Return a Quaternion Matrix 3x3 representation
        /// </summary>
        /// <param name="pMatrix"></param>
        /// <returns>double[16] -> 4x4 matrix 
        /// 0   1   2   
        /// 3   4   5   
        /// 6   7   8  
        /// ---------------------
        /// x   x   x   0
        /// x   x   x   0   
        /// x   x   x   0
        /// 0   0   0   1
        /// </returns>
        public double[] ToRotationMatrix()
        {
            //this.Normalise();
            this.Invert();
            double[] pMatrix = new double[9];

            // First row
            pMatrix[0] = 1.0f - 2.0f * (Y * Y + Z * Z);
            pMatrix[1] = 2.0f * (X * Y - W * Z);
            pMatrix[2] = 2.0f * (X * Z + W * Y);

            // Second row
            pMatrix[3] = 2.0f * (X * Y + W * Z);
            pMatrix[4] = 1.0f - 2.0f * (X * X + Z * Z);
            pMatrix[5] = 2.0f * (Y * Z - W * X);

            // Third row
            pMatrix[6] = 2.0f * (X * Z - W * Y);
            pMatrix[7] = 2.0f * (Y * Z + W * X);
            pMatrix[8] = 1.0f - 2.0f * (X * X + Y * Y);

            return pMatrix;
        }


        //https://svn.code.sf.net/p/irrlicht/code/trunk/include/quaternion.h
        // set this quaternion to the result of the linear interpolation between two quaternions
        public Quaternion Lerp(Quaternion q, float time)
        {
	        float scale = 1.0f - time;
            return this.MultiplyByScalar(scale).Add(q.Copy().MultiplyByScalar(scale));
        }

        public double toAxisAngle(Vector3d axis)
        {
            double sinAngle;

            this.Normalise();
            sinAngle = System.Math.Sqrt(1.00 - (W * W));
            if (System.Math.Abs(sinAngle) < 0.0005f) sinAngle = 1.0f;

            axis.X = (this.X / sinAngle);
            axis.Y = (this.Y / sinAngle);
            axis.Z = (this.Z / sinAngle);
            return (System.Math.Acos(W) * 2.0f);
        }

        public void Invert()
        {
            double length;

            length = 1.0f / ((this.X * this.X) +
                             (this.Y * this.Y) +
                             (this.Z * this.Z) +
                             (this.W * this.W));
            this.X *= -length;
            this.Y *= -length;
            this.Z *= -length;
            this.W *= length;
        }

        public Quaternion CopyInverted()
        {
            var q = this.Copy();
            q.Invert();
            return q;
        }

        public Quaternion Copy()
        {
            return new Quaternion(W, X, Y, Z);
        }

        public Quaternion Multiply(Quaternion q)
        {
            this *= q;
            return this;
        }

        public Quaternion FromEulerAngles(double x, double y, double z)
        {
            //Heading = rotation about y axis
            //Attitude = rotation about z axis
            //Bank = rotation about x axis

            // Assuming the angles are in radians.
            double c1 = System.Math.Cos(y/2);
            double s1 = System.Math.Sin(y/2);
            double c2 = System.Math.Cos(z/2);
            double s2 = System.Math.Sin(z/2);
            double c3 = System.Math.Cos(x/2);
            double s3 = System.Math.Sin(x/2);
            double c1c2 = c1*c2;
            double s1s2 = s1*s2;
            W =c1c2*c3 - s1s2*s3;
  	        X =c1c2*s3 + s1s2*c3;
	        Y =s1*c2*c3 + c1*s2*s3;
	        Z =c1*s2*c3 - s1*c2*s3;
            return this;
        }

        //http://www.euclideanspace.com/maths/geometry/rotations/conversions/quaternionToEuler/
        public Vector3d ToEulerAngles() {
	        double test = X * Y + Z * W;
            Vector3d res = new Vector3d();

	        if (test > 0.499) { // singularity at north pole
		        res.Y = 2 * System.Math.Atan2(X,W);
		        res.Z = System.Math.PI / 2;
		        res.X = 0;
		        return res;
	        }
	        if (test < -0.499) { // singularity at south pole
		        res.Y = -2 * System.Math.Atan2(X,W);
		        res.Z = - System.Math.PI/2;
		        res.X = 0;
		        return res;
	        }
            double sqx = X * X;
            double sqy = Y * Y;
            double sqz = Z * Z;
            res.Y = System.Math.Atan2(2 * Y * W-2 * X * Z , 1 - 2*sqy - 2*sqz);
	        res.Z = System.Math.Asin (2*test);
            res.X = System.Math.Atan2(2 * X * W - 2 * Y * Z, 1 - 2 * sqx - 2 * sqz);
            return res;
        }

        //http://diydrones.com/forum/topics/using-quaternions
        // See Sebastian O.H. Madwick report 
        // "An efficient orientation filter for inertial and intertial/magnetic sensor arrays" Chapter 2 Quaternion representation
        public Vector3d ToEulerAngles2()
        {
            Vector3d res = new Vector3d();
            res.Z = -System.Math.Atan2(2 * X * Y - 2 * W * Z, 2 * W * W + 2 * X * X - 1); // psi
            res.Y = System.Math.Asin(2 * X * Z + 2 * W * Y); // theta
            res.X = -System.Math.Atan2(2 * Y * Z - 2 * W * X, 2 * W * W + 2 * Z * Z - 1); // phi
            return res;
        }

        public Vector3d ToEulerAnglesFreeIMU()
        {
            Vector3d res = new Vector3d();
            double gx, gy, gz; // estimated gravity direction

            gx = 2 * (X * Z - W * Y);
            gy = 2 * (W * X + Y * Z);
            gz = W * W - X * X - Y * Y + Z * Z;

            res.Z = -System.Math.Atan2(2 * X * Y - 2 * W * Z, 2 * W*W + 2 * X * X - 1);
            res.Y = -System.Math.Atan(gx / System.Math.Sqrt(gy * gy + gz * gz));
            res.X = System.Math.Atan(gy / System.Math.Sqrt(gx * gx + gz * gz));

            return res;
        }
   
        public void Rotate(Point3d pt)
        {
            this.Normalise();
            Quaternion q1 = this.Copy();
            q1.Conjugate();

            Quaternion qNode = new Quaternion(0, pt.X, pt.Y, pt.Z);
            qNode = this * qNode * q1;
            pt.X = qNode.X;
            pt.Y = qNode.Y;
            pt.Z = qNode.Z;
        }

        public void Rotate(Point3d[] nodes)
        {
            this.Normalise();
            Quaternion q1 = this.Copy();
            q1.Conjugate();
            for (int i = 0; i < nodes.Length; i++)
            {
                Quaternion qNode = new Quaternion(0, nodes[i].X, nodes[i].Y, nodes[i].Z);
                qNode = this * qNode * q1;
                nodes[i].X = qNode.X;
                nodes[i].Y = qNode.Y;
                nodes[i].Z = qNode.Z;
            }
        }

        // Multiplying q1 with q2 is meaning of doing q2 firstly then q1
        public static Quaternion operator *(Quaternion q1, Quaternion q2)
        {
            double nw = q1.W * q2.W - q1.X * q2.X - q1.Y * q2.Y - q1.Z * q2.Z;
            double nx = q1.W * q2.X + q1.X * q2.W + q1.Y * q2.Z - q1.Z * q2.Y;
            double ny = q1.W * q2.Y + q1.Y * q2.W + q1.Z * q2.X - q1.X * q2.Z;
            double nz = q1.W * q2.Z + q1.Z * q2.W + q1.X * q2.Y - q1.Y * q2.X;
            return new Quaternion(nw, nx, ny, nz);
        }

        // Multiplying q1 with q2 is meaning of doing q2 firstly then q1
        public static Quaternion operator +(Quaternion q1, Quaternion q2)
        {
            return q1.Add(q2);
        }
    }
}
