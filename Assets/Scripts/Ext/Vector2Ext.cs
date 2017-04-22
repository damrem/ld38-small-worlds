//using System;
using UnityEngine;

namespace Ext
{
	public static class Vector2Ext
	{
		public static Vector2 UnitVector(this Vector2 vector)
		{
			Vector2 unitVector = new Vector2 ();

			float dx = Mathf.Abs (vector.x);
			float dy = Mathf.Abs (vector.y);

			if (dx > dy){
				unitVector.x = vector.x / dx;
				unitVector.y = 0;
			}
			else{
				unitVector.x = 0;
				unitVector.y = vector.y / dy;
			}

			return unitVector;
		}

		public static Vector2 Rotate(this Vector2 vector, float degrees)
		{
			return Quaternion.Euler (0, 0, degrees) * vector;
//			float radians = degrees * Mathf.Deg2Rad;
//			float sin = Mathf.Sin(radians);
//			float cos = Mathf.Cos(radians);
//
//			float tx = vector.x;
//			float ty = vector.y;
//
//			return new Vector2(cos * tx - sin * ty, sin * tx + cos * ty);
		}

		public static Vector2 Clone(this Vector2 vector)
		{
			return new Vector2 (vector.x, vector.y);
		}

		public static Vector2 GetNormal(this Vector2 vector)
		{
			return vector.Clone ().Rotate (90);
		}
	}
}

