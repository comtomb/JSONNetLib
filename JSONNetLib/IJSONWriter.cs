/*
 */
using System;
using System.IO;
using System.Text;

namespace TomB.Util.JSON
{
	/// <summary>
	/// Outout of a <see cref="IJSONDocument"/> 
	/// </summary>
	public interface IJSONWriter
	{
		/// <summary>
		/// store in a file
		/// </summary>
		/// <param name="doc">source</param>
		/// <param name="fname">filename</param>
		void WriteToFile(IJSONDocument doc, string fname);
		/// <summary>
		/// store in a string
		/// </summary>
		/// <param name="doc">source</param>
		/// <returns>JSON string</returns>
		string WriteToString(IJSONDocument doc);
		/// <summary>
		/// write to a StringBuilder
		/// </summary>
		/// <param name="doc">source</param>
		/// <param name="sb">StringBuilder</param>
		void WriteToStringBuilder(IJSONDocument doc, StringBuilder sb);
		/// <summary>
		/// writer to a Stream
		/// </summary>
		/// <param name="doc">source</param>
		/// <param name="stream"></param>
		void WriteToStream(IJSONDocument doc, Stream stream);
		/// <summary>
		/// write to a writer
		/// </summary>
		/// <param name="doc">source</param>
		/// <param name="writer"></param>
		void WriteToWriter(IJSONDocument doc, TextWriter writer);		
	}
}
