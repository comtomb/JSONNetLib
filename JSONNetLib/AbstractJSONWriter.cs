/*
 */
using System;
using System.IO;
using System.Text;
namespace TomB.Util.JSON
{
	internal abstract class AbstractJSONWriter : IJSONWriter
	{
		public AbstractJSONWriter()
		{
		}

		public void WriteToFile(IJSONDocument doc, string fname)
		{
			using (var fs = new FileStream(fname, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None, 8192)) {
				WriteToStream(doc, fs);
			}
		}

		public string WriteToString(IJSONDocument doc)
		{
			var sb = new StringBuilder();
			WriteToStringBuilder(doc, sb);
			return sb.ToString();
		}

		public void WriteToStringBuilder(IJSONDocument doc, System.Text.StringBuilder sb)
		{
			using (var writer = new StringWriter(sb)) {
				WriteToWriter(doc, writer);
			}
		}

		public void WriteToStream(IJSONDocument doc, System.IO.Stream stream)
		{
			using (var writer = new StreamWriter(stream)) {
				WriteToWriter(doc, writer);
			}
		}

		public abstract void WriteToWriter(IJSONDocument doc, System.IO.TextWriter writer);
		
		protected string EscapeString(string txt)
		{
			var sb = new StringBuilder();
			foreach (var c in txt) {
				switch (c) {
					case '\b':
						sb.Append("\\b");
						break;
					case '\t':
						sb.Append("\\t");
						break;
					case '\n':
						sb.Append("\\n");
						break;
					case '\r':
						sb.Append("\\r");
						break;
					case '\\':
						sb.Append("\\\\");
						break;
					case '/':
						sb.Append("\\/");
						break;
					case '"':
						sb.Append("\\\"");
						break;
					default:
						sb.Append(c);
						break;
				}
			}
			return sb.ToString();
		}

	}
}


