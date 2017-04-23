using System;

namespace Helpers
{
	public class Rnd
	{
		static Random random = new Random ();

		public static int Int (int min, int max)
		{
			return random.Next (min, max);
		}

		public static float Float()
		{
			return (float)random.NextDouble ();
		}

		public static float Float(float min, float max)
		{
			return (float)(min + random.NextDouble () * (max - min));
		}
	}
}

