/*
 */
using System;
using System.Diagnostics;
using TomB.Util.JSON;

namespace Runner
{
	
	class PersonChildren	
	{
		public string childname;
		public int age;
	}
	[JSONLoadableClass(tpe:JSONLoadableType.Fields)]
	class Partner
	{
		string name;
	}
	
	[JSONLoadableClass(tpe:JSONLoadableType.Fields)]
	class Person
	{
		string lastname;
		string firstname;
		int age;
		int[] someNumbers;
		[JSONLoadableElement(externalName:"children")]
		PersonChildren[] childrenX;
		
		Partner partner;
	}
	
	class Program
	{
		
		public static void Main(string[] args)
		{			
			
			var doc=JSONParser.CreateParser().LoadFromFile("TestData\\object.json");
			//JSONObjectLoader.CreateLoader().LoadObject( person, doc );
			var person=JSONObjectLoader.CreateLoader().CreateObject<Person>(doc);
			
			Console.WriteLine(person);
		}
	}
}