/*
 */
using System;
using TomB.Util.JSON;

namespace Example2
{
	/// <summary>
	/// Demo
	/// 	load a JSON File
	/// 	extract some values using a path syntax
	/// </summary>
	class Program
	{
		public static void Main(string[] args)
		{
			var doc=JSONParser.CreateParser().LoadFromFile("elements.json");
			
			var v0=doc.RetrieveItemString("key0");
			var v1=doc.RetrieveItemInt("key8.sk2.sk2b.1.sk3");
			var v2=doc.RetrieveItemInt("key7.1");
			var v3=doc.RetrieveItemInt("key8.sk3.0");
			
			Console.WriteLine("retrieved values: " + v0 + " " + v1 + " " + v2 + " " + v3);
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}