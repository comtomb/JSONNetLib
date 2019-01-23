/*
 */
using System;

namespace TomB.Util.JSON
{
	public static class JSONWriter
	{
		public static IJSONWriter CreateUnformattedWriter()
		{
			return new JSONUnformattedWriter();
		}

	}
}
