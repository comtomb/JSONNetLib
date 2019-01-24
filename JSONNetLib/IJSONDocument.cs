// MIT License
//
// Copyright (c) 2019 comtomb
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// 

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
namespace TomB.Util.JSON
{
	/// <summary>
	/// Interface for a JSONDocument.
	/// A document provides access to the root element of the JSON structure. 
	/// also there are methods to access and modify the content
	/// 
	/// the JSON data are stored in a tree-like object consisting of <see cref="IJSONItem"/>
	/// the items are either Objects, Arrays or Atomic values (true/false/null/string/number)
	/// 
	/// see https://json.org
	/// 
	/// </summary>
	public interface IJSONDocument
	{
		/// <summary>
		/// Root Element of the JSON data. this is usually a <see cref="IJSONItemObject"/>
		/// </summary>
		IJSONItem Root 
		{
			get;
			set;
		}
		/// <summary>
		/// convenience: Root element as <see cref="IJSONItemObject"/> 
		/// </summary>
		IJSONItemObject RootAsObject 
		{
			get;
		}
		/// <summary>
		/// the JSON data type of the Root
		/// </summary>
		JSONItemType RootType 
		{
			get;
		}

		/// <summary>
		/// create a new JSON Array to be dded somewhere in this document
		/// </summary>
		/// <returns>new array</returns>
		IJSONItemArray CreateItemArray();
		/// <summary>
		/// create a new JSON Object to be added somewhere in this document
		/// </summary>
		/// <returns>new object</returns>
		IJSONItemObject CreateItemObject();
		/// <summary>
		/// create a new JSON string to be added somewhere in this document
		/// </summary>
		/// <param name="val">content</param>
		/// <returns>new string</returns>
		IJSONItemString CreateItemString(string val);
		/// <summary>
		/// create a new JSON number to be added somewhere in this document
		/// </summary>
		/// <param name="val">needs to be a number in valid JSON format</param>
		/// <returns>new number</returns>
		IJSONItemNumber CreateItemNumber(string val);
		/// <summary>
		/// create a new JSON number to be added somewhere in this document
		/// </summary>
		/// <param name="val"></param>
		/// <returns>new number</returns>
		IJSONItemNumber CreateItemNumber(int val);
		/// <summary>
		/// create a new JSON number to be added somewhere in this document
		/// </summary>
		/// <param name="val"></param>
		/// <returns>new number</returns>
		IJSONItemNumber CreateItemNumber(long val);
		/// <summary>
		/// create a new JSON number to be added somewhere in this document
		/// </summary>
		/// <param name="val"></param>
		/// <returns>new number</returns>
		IJSONItemNumber CreateItemNumber(double val);
		/// <summary>
		/// create a new JSON null to be added somewhere in this document
		/// </summary>
		/// <returns>new null</returns>
		IJSONItemNull CreateItemNull();
		/// <summary>
		/// create a new JSON bool to be added somewhere in this document
		/// </summary>
		/// <param name="val"></param>
		/// <returns>new bool</returns>
		IJSONItemBool CreateItemBool(bool val);
		/// <summary>
		/// retrieve an item in the document by using a path-like string. the levels in the document are separated by '.'
		/// 		
		/// example
		/// 	object_key0.array_index1.object_key2.object_key3
		/// </summary>
		/// <param name="jpath">path description</param>
		/// <returns>element at the described position</returns>
		IJSONItem RetrieveItem(string jpath);
		/// <summary>
		/// retrieve an object
		/// </summary>
		/// <param name="jpath">path description</param>
		/// <returns>item at jpath position</returns>
		/// <exception cref="ArgumentException">the specified path can't be resolved to an item"></exception>
		/// <exception cref="InvalidOperationException">not an object"></exception>
		IJSONItemObject RetrieveItemObject(string jpath);
		/// <summary>
		/// retrieve an array
		/// </summary>
		/// <param name="jpath">path description</param>
		/// <returns>array at jpath position</returns>
		/// <exception cref="ArgumentException">the specified path can't be resolved to an item"></exception>
		/// <exception cref="InvalidOperationException">not an array"></exception>
		IJSONItemArray RetrieveItemArray(string jpath);
		/// <summary>
		/// retrieve a bool
		/// </summary>
		/// <param name="jpath">path description</param>
		/// <returns>bool at jpath position</returns>
		/// <exception cref="ArgumentException">the specified path can't be resolved to an item"></exception>
		/// <exception cref="InvalidOperationException">not a bool"></exception>
		bool RetrieveItemBool(string jpath);
		/// <summary>
		/// retrieve a int from a number item
		/// </summary>
		/// <param name="jpath">path description</param>
		/// <returns>int at jpath position</returns>
		/// <exception cref="ArgumentException">the specified path can't be resolved to an item"></exception>
		/// <exception cref="InvalidOperationException">not a number, or not a int-number"></exception>
		int RetrieveItemInt(string jpath);
		/// <summary>
		/// retrieve a string
		/// </summary>
		/// <param name="jpath">path description</param>
		/// <returns>string at jpath position</returns>
		/// <exception cref="ArgumentException">the specified path can't be resolved to an item"></exception>
		/// <exception cref="InvalidOperationException">not a string"></exception>		
		string RetrieveItemString(string jpath);
		/// <summary>
		/// retrieve a number item
		/// <seealso cref="RetrieveItem"/>
		/// </summary>
		/// <param name="jpath">path description</param>
		/// <returns>number at jpath position</returns>
		/// <exception cref="ArgumentException">the specified path can't be resolved to an item"></exception>
		/// <exception cref="InvalidOperationException">not a number"></exception>
		IJSONItemNumber RetrieveItemNumber(string jpath);
	}
}


