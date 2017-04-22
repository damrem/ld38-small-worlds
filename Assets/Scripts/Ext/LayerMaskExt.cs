//using System;
using UnityEngine;

namespace Ext
{
	public static class LayerMaskExt
	{
		public static LayerMask GetMask(this GameObject go)
		{
			return (int)Mathf.Pow(2, go.layer);
		}
	}
}

