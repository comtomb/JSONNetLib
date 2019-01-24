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
	/// Object Loader
	/// Can load the fields/properties of an object from a <see cref="IJSONDocument"/>
	/// 
	/// the loader interacts with <see cref="IJSONLoadableObject"/> and can be controlled by the attributes
	/// <see cref="JSONLoadableClassAttribute"/> and <see cref="JSONLoadableElementAttribute"/>
	/// 
	/// if the class to be loaded is not declared with a <see cref="JSONLoadableClassAttribute"/> this attribute it's default values are used
	/// 	
	/// </summary>
	public interface IJSONObjectLoader
	{
		/// <summary>
		/// Flag if all Fields/Properties matched by Loadable are mandatory. Default: <see cref="JSONLoadableClassAttribute.DefaultAllMandatory"/>
		/// </summary>
		bool AllMandatory {get;set;}
		/// <summary>
		/// Flags which Fields/Properties should be loaded. Default: <see cref="JSONLoadableClassAttribute.DefaultLoadable"/>
		/// </summary>
		JSONLoadableType Loadable {get;set;}
		/// <summary>
		/// load the fields/properties of an object from a document
		/// </summary>
		/// <param name="dest">object to be loaded. this object can implement <see cref="IJSONLoadableObject"/></param>
		/// <param name="src">source document</param>
		void LoadObject(object dest, IJSONDocument src);
		/// <summary>
		/// create a new object and load it from a JSON Document
		/// </summary>
		/// <param name="src">source</param>
		/// <returns>created document</returns>
		T CreateObject<T>(IJSONDocument src) where T : new();
	}
}


