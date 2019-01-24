/*
 */
using System;
using System.Reflection;

namespace TomB.Util.JSON
{
	/// <summary>
	/// interface to interact with <see cref="IJSONObjectLoader"/>
	/// 
	/// An object to be loaded/created with an <see cref="IJSONObjectLoader"></see> can implement this interface to 
	/// control the loading processes. 
	/// the methods are called before load starts (setup the object), after load finishes  and before a field/property is set (conversions).
	/// 
	/// 
	/// </summary>
	public interface IJSONLoadableObject
	{
		/// <summary>
		/// called before the object is loaded
		/// </summary>
		void OnBeforeLoad();
		/// <summary>
		/// called after the object is fully loaded
		/// </summary>
		void OnAfterLoad();
		/// <summary>
		/// called before a Field is set to a value
		/// can be used to convert/manipulate a value before it es set
		/// </summary>
		/// <param name="field">field to be set</param>
		/// <param name="valueIn">value from source</param>
		/// <param name="valueOut">value to actually set</param>
		/// <returns>returns true if the field should be set to valueOut, false if the field should remain unchanged</returns>
		bool OnSetField( FieldInfo field, object valueIn, out object valueOut );
		/// <summary>
		/// called before a Property is set to a value
		/// can be used to convert/manipulate a value before it es set
		/// </summary>
		/// <param name="field">field to be set</param>
		/// <param name="valueIn">value from source</param>
		/// <param name="valueOut">value to actually set</param>
		/// <returns>returns true if the property should be set to valueOut, false if the property should remain unchanged</returns>
		bool OnSetProperty( PropertyInfo field, object valueIn, out object valueOut );
		
	}
}
