﻿//using System;
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
		}

		public static Vector2 Clone(this Vector2 vector)
		{
			return new Vector2 (vector.x, vector.y);
		}

		public static Vector2 GetNormal(this Vector2 vector)
		{
			return vector.Clone ().Rotate (90);
		}

		public static Vector2 Normalized(this Vector2 vector, float length=1.0f)
		{
			Vector2 clone = vector.Clone ();
			clone.Scale (new Vector2 (length, length));
			return clone;
		}
	}
}

