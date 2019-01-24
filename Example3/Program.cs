/*
 */
using System;
using TomB.Util.JSON;

namespace Example3
{
	/// <summary>
	/// Demo
	/// 	create an object by loading  fields/property values from a JSON file
	/// 	Example is a car with engine and wheels
	/// 	demonstrate also use of "external" name
	/// 	demonstrate use of non-mandatory fields/properties
	/// </summary>
	class Program
	{


		public static void Main(string[] args)
		{
			var doc=JSONParser.CreateParser().LoadFromFile("car.json");
			var car=JSONObjectLoader.CreateLoader().CreateObject<Car>(doc);
			
			Console.WriteLine("loaded a " + car.age + " years old, " + car.Color + " car");
			Console.WriteLine( "car has " + car.wheels.Length + " wheels");
			foreach( var wheel in car.wheels )
				Console.WriteLine("wheel " + wheel.location + " available: " + wheel.available );
			Console.WriteLine("engine driven by " + car.engine.fuel);
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
			
		}
	}
	/// <summary>
	/// a wheel
	/// we use the default settings: load all public fields
	/// </summary>
	class Wheel
	{
		public string location=string.Empty;
		public bool available=false;		
	}
	/// <summary>
	/// the engine
	/// we load only NonPublicFields
	/// </summary>
	[JSONLoadableClass(tpe:JSONLoadableType.NonPublicFields)]
	class Engine
	{
		internal int heads=0;
		internal string fuel=string.Empty;
		public int ignored=-1;
	}
	/// <summary>
	/// the car itself
	/// we load only fields/properties marked as "Loadable"
	/// also an example for name mapping and a non-
	/// </summary>
	[JSONLoadableClass(tpe:JSONLoadableType.Loadable)]
	class Car
	{
		[JSONLoadableElement(externalName:"model")]
		internal string Model=string.Empty;
		
		[JSONLoadableElement]
		internal Engine engine=null;
		
		[JSONLoadableElement]
		public Wheel[] wheels=null;
		
		[JSONLoadableElement]
		private int[] somenumbers=null;
		
		
		[JSONLoadableElement]
		internal int age;
		
		[JSONLoadableElement(externalName:"color")]
		internal string Color {get;set;}		

		[JSONLoadableElement(mandatory:false)]
		internal int length;


	}

}