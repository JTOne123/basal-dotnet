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
    public struct Vector2f
    {
        public int x;
        public int y;

        public Vector2f(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2f FromFloats(float x, float y)
        {
            return new Vector2f((int)(x * Utility.FixedPointFactor), (int)(y * Utility.FixedPointFactor));
        }

        public void ToFloats(out float outX, out float outY)
        {
            outX = Utility.FixedPointToFloat(x);
            outY = Utility.FixedPointToFloat(y);
        }

        public override string ToString()
        {
            float x, y;
            ToFloats(out x, out y);
            return $"[vector2f x:{x}, y:{y}]";
        }

        public string DebugString()
        {
            return $"[vector2f fixed x:{x}, y:{y}]";
        }

        public static Vector2f operator -(Vector2f a, Vector2f b)
        {
            return new Vector2f(a.x - b.x, a.y - b.y);
        }

        public static Vector2f operator +(Vector2f a, Vector2f b)
        {
            return new Vector2f(a.x + b.x, a.y + b.y);
        }

        public static Vector2f operator *(Vector2f a, int magnitude)
        {
            return new Vector2f(a.x * magnitude, a.y * magnitude);
        }

        public static Vector2f operator /(Vector2f a, int magnitude)
        {
            return new Vector2f(a.x / magnitude, a.y / magnitude);
        }

        public static Vector2f Interpolate(Vector2f a, Vector2f b, int factor)
        {
            return a + (b - a) * factor / Utility.FixedPointFactor;
        }

        public int Dot(Vector2f other)
        {
            return x * other.x + y * other.y;
        }

        public static Vector2f operator -(Vector2f a)
        {
            return new Vector2f(-a.x, -a.y);
        }

        public bool IsEqual(Vector2f other)
        {
            return x == other.x
                   && y == other.y;
        }
    }
}
