using System;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using TomB.Util.JSON;

namespace Tester
{
	[TestFixture]
	public class Testcases
	{
		
	class Wheel
	{
		public string location;
		public bool available;
	}
	[JSONLoadableClass(tpe:JSONLoadableType.NonPublicFields)]
	class Engine
	{
		internal int heads;
		internal string fuel;
		public int ignored=-1;
	}
	[JSONLoadableClass(tpe:JSONLoadableType.Loadable)]
	class Car
	{
		[JSONLoadableElement(externalName:"model")]
		internal string Model;
		
		[JSONLoadableElement]
		internal Engine engine;
		
		[JSONLoadableElement]
		public Wheel[] wheels;
		
		[JSONLoadableElement]
		private int[] somenumbers;
		
		
		[JSONLoadableElement]
		internal int age;
		
		[JSONLoadableElement(externalName:"color")]
		internal string Color {get;set;}
		
	}
		
		
		
		
		
		
		[Test]
		public void TestParseBootstrap()
		{
			var doc=JSONParser.CreateParser().LoadFromFile("TestData\\bootstrap.json");
			var root=doc.RootAsObject;
			Assert.NotNull(root);
			var lastScan=root["last_scan"] as IJSONItemNumber;
			Assert.NotNull(lastScan);
			
			var nodeArray=root["nodes"] as IJSONItemArray;
			Assert.NotNull(nodeArray);
			
		}
		[Test]
		public void TestParseGenerated()
		{
			var doc=JSONParser.CreateParser().LoadFromFile("TestData\\generated.json");
			Assert.NotNull(doc.Root);
			Assert.True( doc.RootType==JSONItemType.Array);
		}
		[Test]
		public void TestParseElements()
		{
			var doc=JSONParser.CreateParser().LoadFromFile("TestData\\elements.json");
			var root=doc.RootAsObject;
			Assert.NotNull(root);

			Assert.True(doc.RetrieveItemString("key0")=="value0");
			Assert.True(doc.RetrieveItemInt("key8.sk2.sk2b.1.sk3")==42);
			Assert.True(doc.RetrieveItemInt("key7.1")==2);
			Assert.True(doc.RetrieveItemInt("key8.sk3.0")==1);
		}
		[Test]
		public void TestCreate()
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

			Assert.NotNull( doc.Root );
			Assert.NotNull( doc.RootAsObject["FirstName"] as IJSONItemString );
			
			Assert.True( ((IJSONItemString)doc.RootAsObject["FirstName"]).Value!=null );
		}
		[Test]
		public void TestWrite()
		{
			var doc=JSONDocument.CreateDocument();
			doc.Root=doc.CreateItemNumber(42);
			var json=JSONWriter.CreateUnformattedWriter().WriteToString(doc);
			
			Assert.True( json.IndexOf("42") >= 0);
		}
		[Test]
		public void TestLoadObject()
		{
			var doc=JSONParser.CreateParser().LoadFromFile("TestData\\car.json");
			var car=JSONObjectLoader.CreateLoader().CreateObject<Car>(doc);
			
			Assert.True( car.Color=="red" );
			Assert.True( car.wheels.Length==4);					
			
		}
		
	}
}