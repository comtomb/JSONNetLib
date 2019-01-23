/*
 */
using System;
using System.IO;
using System.Text;

namespace TomB.Util.JSON
{
	public interface IJSONWriter
	{
		void WriteToFile(IJSONDocument doc, string fname);
		string WriteToString(IJSONDocument doc);
		void WriteToStringBuilder(IJSONDocument doc, StringBuilder sb);
		void WriteToStream(IJSONDocument doc, Stream stream);
		void WriteToWriter(IJSONDocument doc, TextWriter writer);		
	}
}
