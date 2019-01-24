/*
 */
using System;
using System.IO;
using System.Text;

namespace TomB.Util.JSON
{
	/// <summary>
	/// simple writer without any formatting
	/// </summary>
	internal class JSONUnformattedWriter : AbstractJSONWriter
	{
		public JSONUnformattedWriter()
		{
			
		}
		public override void WriteToWriter(IJSONDocument doc, TextWriter writer)
		{
			WriteElement(doc.Root, writer );
		}
		
		private void WriteElement( IJSONItem item , TextWriter writer )
		{
			switch (item.ItemType) 
			{
				case JSONItemType.String:
					writer.Write('"');
					writer.Write(EscapeString( ((IJSONItemString)item).Value));
					writer.Write('"');
					break;
				case JSONItemType.Array:
					writer.Write('[');
					for(int i=0;i<((IJSONItemArray)item).Count;i++)
					{
						if(i>0)
							writer.Write(',');
						WriteElement( ((IJSONItemArray)item)[i],writer);
					}
					writer.Write(']');
					break;
				case JSONItemType.Object:
					writer.Write('{');
					int c=0;
					foreach( var kv in ((IJSONItemObject)item) )
					{
						if(c++>0)
							writer.Write(',');
						writer.Write('"');
						writer.Write( EscapeString(kv.Key) );
						writer.Write("\":");
						WriteElement(kv.Value,writer);
					}
					writer.Write('}');
					break;
				case JSONItemType.Number:
					writer.Write( ((IJSONItemNumber)item).Value);
					break;
				case JSONItemType.Null:
					writer.Write( "null");
					break;
				case JSONItemType.Bool:
					if( ((IJSONItemBool)item).IsTrue )
						writer.Write("true");
					else
						writer.Write("false");
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}
