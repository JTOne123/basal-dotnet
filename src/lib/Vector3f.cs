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
		public static bool FloatAlmostEqual(float a, float b)
		{
			return Math.Abs(a - b) < 0.0005f;
		}
	}

	public struct Vector3f
	{
		public float x;
		public float y;
		public float z;

		public Vector3f(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public override string ToString()
		{
			return string.Format("[vector3f x:{0}, y:{1}, z:{2}]", x, y, z);
		}

		public static Vector3f operator - (Vector3f a, Vector3f b)
		{
			return new Vector3f (a.x - b.x, a.y - b.y, a.z - b.z);
		}

		public static Vector3f operator + (Vector3f a, Vector3f b)
		{
			return new Vector3f (a.x + b.x, a.y + b.y, a.z + b.z);
		}

		public static Vector3f operator * (Vector3f a, float magnitude)
		{
			return new Vector3f (a.x * magnitude, a.y * magnitude, a.z * magnitude);
		}

		public static Vector3f operator /(Vector3f a, float magnitude)
		{
			return new Vector3f(a.x / magnitude, a.y / magnitude, a.z / magnitude);
		}

		public static Vector3f Interpolate(Vector3f a, Vector3f b, float factor)
		{
			return a + (b - a) * factor;
		}

		public float Dot(Vector3f other)
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

		public bool AlmostEqual(Vector3f other)
		{
			return Utility.FloatAlmostEqual (x, other.x)
			       && Utility.FloatAlmostEqual (y, other.y)
			       && Utility.FloatAlmostEqual (z, other.z);
		}
	}
}
