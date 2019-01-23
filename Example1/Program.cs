/*
 */
using System;
using TomB.Util.JSON;

namespace Example1
{
	/// <summary>
	/// demonstration of
	/// 	- create and populate a JSONDocument
	/// 	- save JSONDocument to a string
	/// </summary>
	class Program
	{
		public static void Main(string[] args)
		{
			// create a document and root
			var doc=JSONDocument.CreateDocument();			
			doc.Root=doc.CreateItemObject();
			
			// add some items to root: use explicit creation of items
			doc.RootAsObject.Add("FirstName",doc.CreateItemString("Hans"));
			doc.RootAsObject.Add("LastName",doc.CreateItemString("Wurst"));
			doc.RootAsObject.Add("age",doc.CreateItemNumber(42));
			
			// create and add an object and fill, use implicit creation of items
			var address=doc.CreateItemObject();
			address.Add("street","infinite loop 0");
			address.Add("city","Duckburg");
			doc.RootAsObject.Add("Address",address);
			
			// create and add an array
			var childAge=doc.CreateItemArray();
			childAge.Add(3);
			childAge.Add(10);
			childAge.Add(12);
			doc.RootAsObject.Add("AgeOfChildren",childAge);
			
			// write to a JSON string
			var jsonString=JSONWriter.CreateUnformattedWriter().WriteToString(doc);
			
			// output
			Console.WriteLine(jsonString);

			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}