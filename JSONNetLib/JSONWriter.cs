/*
 */
using System;

namespace TomB.Util.JSON
{
	/// <summary>
	/// factory class to create <see cref="IJSONWriter"/>
	/// </summary>
	public static class JSONWriter
	{
		/// <summary>
		/// create a simple JSONWriter without any formatting whitespaces
		/// </summary>
		/// <returns></returns>
		public static IJSONWriter CreateUnformattedWriter()
		{
			return new JSONUnformattedWriter();
		}

	}
}
