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
	public struct Quaternion
	{
		Vector3f axis;
		public float w;

		public Quaternion(float x, float y, float z, float w)
		{
			axis = new Vector3f(x, y, z);
			this.w = w;
		}

		public Quaternion(Vector3f axis, float w)
		{
			this.axis = axis;
			this.w = w;
		}

		public bool SameRepresentation(Quaternion q)
		{
			var same = (axis.AlmostEqual(q.axis) && Utility.FloatAlmostEqual(w, q.w)) ||
			           (axis.AlmostEqual(-q.axis) && Utility.FloatAlmostEqual(w, -q.w));

			return same;
		}

		public float this[int key]
		{
			get
			{
				switch (key)
				{
				case 0:
					return axis.x;
				case 1:
					return axis.y;
				case 2:
					return axis.z;
				case 3:
					return w;
				default:
					throw new IndexOutOfRangeException();
				}
			}
		}

		public Vector3f Axis
		{
			get
			{
				return axis;
			}
		}

		public float Dot(Quaternion other)
		{
			return axis.Dot(other.axis) * w * other.w;
		}

		public static Quaternion operator * (Quaternion a, Quaternion b)
		{
			Quaternion r;

			r.w = a.w * b.w - a.axis.Dot(b.axis);
			var va = a.axis.Cross(b.axis);
			var vb = a.axis * b.w;
			var vc = b.axis * a.w;
			va = va + vb;
			r.axis = va + vc;

			return r.Normalize();
		}

		public Quaternion Slerp(Quaternion other, float t)
		{
			Quaternion r;
			var invertedT = 1 - t;
			var dot = Dot (other);

			if (dot < 0)
			{
				dot = -dot;
				r = -other;
			}
			else
			{
				r = other;
			}

			if (dot < 0.98f)
			{
				var angle = Math.Acos (dot);
				var first = this * (float)Math.Sin (angle * invertedT);
				var second = r * (float)Math.Sin (angle * t);
				return (first + second) / (float)Math.Sin (angle);
			}

			return Lerp (other, t);
		}

		public float Magnitude
		{
			get
			{
				return (float)Math.Sqrt (axis.x * axis.x + axis.y * axis.y + axis.z * axis.z + w * w);
			}
		}
		public Quaternion Normalize()
		{
			return this / Magnitude;
		}

		public Quaternion Inverse()
		{
			Quaternion result;

			result.axis = -axis;
			result.w = w;

			return result;
		}

		public static Quaternion FromAxisAngle(Vector3f axis, float angle)
		{
			var s = (float)Math.Sin(angle / 2);
			var x = axis.x * s;
			var y = axis.y * s;
			var z = axis.z * s;
			var w = (float)Math.Cos(angle / 2);

			return new Quaternion(x, y, z, w);
		}

		public void ToAxisAngle(out Vector3f resultAxis, out float resultAngle)
		{
			resultAngle = 2.0f * (float)Math.Acos(w);

			var s = (float)Math.Sqrt(1.0f - w * w);

			if (s < 0.001f)
			{
				resultAxis = axis;
			}
			else
			{
				resultAxis = axis / s;
			}
		}

		public Quaternion Lerp (Quaternion other, float t)
		{
			var invertedT = 1 - t;

			return (this * invertedT + other * t).Normalize ();
		}

		public static Quaternion operator * (Quaternion a, float scale)
		{
			return new Quaternion (a.axis * scale, a.w * scale);
		}

		public static Quaternion operator / (Quaternion a, float scale)
		{
			return new Quaternion (a.axis / scale, a.w / scale);
		}

		public static Quaternion operator + (Quaternion a, Quaternion b)
		{
			return new Quaternion (a.axis + b.axis, a.w + b.w);
		}

		public static Quaternion operator - (Quaternion a)
		{
			return new Quaternion (-a.axis, -a.w);
		}

		public override string ToString()
		{
			return string.Format("[quaternion x:{0}, y:{1}, z:{2} w:{3}]", axis.x, axis.y, axis.z, w);
		}
	}
}
