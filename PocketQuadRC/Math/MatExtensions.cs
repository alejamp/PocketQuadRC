using System;

namespace ArUXV.Math
{
    public static class MatExtensions
    {
        public static double m3x3(this double[] m, int row, int col)
        { 
            return m[col*3 + row];
        }
    }
}
