/*

MIT License

Copyright (c) 2017 Peter Bjorklund

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

*/
using System;

namespace Piot.Basal
{

    public class Utility
    {
        public const int FixedPointFactor = 1000;
        public static bool FloatAlmostEqual(int a, int b)
        {
            return Math.Abs(a - b) < 5f;
        }

        public static float FixedPointToFloat(int x)
        {
            return x / (float)Utility.FixedPointFactor;
        }
        public static int FloatToFixedPoint(float x)
        {
            return (int)(x * Utility.FixedPointFactor);
        }
    }

    public struct Vector3f
    {
        public int x;
        public int y;
        public int z;

        public Vector3f(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Vector3f FromFloats(float x, float y, float z)
        {
            return new Vector3f((int)(x * Utility.FixedPointFactor), (int)(y * Utility.FixedPointFactor), (int)(z * Utility.FixedPointFactor));
        }

        public void ToFloats(out float outX, out float outY, out float outZ)
        {
            outX = Utility.FixedPointToFloat(x);
            outY = Utility.FixedPointToFloat(y);
            outZ = Utility.FixedPointToFloat(z);
        }


        public override string ToString()
        {
            float x, y, z;
            ToFloats(out x, out y, out z);
            return string.Format("[vector3f x:{0}, y:{1}, z:{2}]", x, y, z);
        }

        public string DebugString()
        {
            return string.Format("[vector3f fixed x:{0}, y:{1}, z:{2}]", x, y, z);
        }
        public static Vector3f operator -(Vector3f a, Vector3f b)
        {
            return new Vector3f(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static Vector3f operator +(Vector3f a, Vector3f b)
        {
            return new Vector3f(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static Vector3f operator *(Vector3f a, int magnitude)
        {
            return new Vector3f(a.x * magnitude, a.y * magnitude, a.z * magnitude);
        }

        public static Vector3f operator /(Vector3f a, int magnitude)
        {
            return new Vector3f(a.x / magnitude, a.y / magnitude, a.z / magnitude);
        }

        public static Vector3f Interpolate(Vector3f a, Vector3f b, int factor)
        {
            return a + (b - a) * factor / Utility.FixedPointFactor;
        }

        public int Dot(Vector3f other)
        {
            return x * other.x + y * other.y + z * other.z;
        }

        public Vector3f Cross(Vector3f other)
        {
            Vector3f result;

            result.x = y * other.z - z * other.y;
            result.y = z * other.x - x * other.z;
            result.z = x * other.y - y * other.x;

            return result;
        }

        public static Vector3f operator -(Vector3f a)
        {
            return new Vector3f(-a.x, -a.y, -a.z);
        }

        public bool IsEqual(Vector3f other)
        {
            return x == other.x
                   && y == other.y
                   && z == other.z;
        }
    }
}
