using System;
using UnityEngine;

namespace Ext
{
	public static class GameObjectExt
	{
		public static void SetAlpha(this GameObject gameObject, float alpha)
		{
//			Dbg.Log (typeof(GameObjectHelpers), "SetAlpha" + gameObject);
			SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> ();
			Color color = renderer.color;
			color.a = alpha;
			renderer.color = color;
		}

//		public static void SetLuminosity(GameObject gameObject, float luminosity)
//		{
//			SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> ();
//			Color color = renderer.color;
//			color.r *= luminosity;
//			color.g *= luminosity;
//			color.b *= luminosity;
//			renderer.color = color;
//		}
	}
}

