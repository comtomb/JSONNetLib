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
	/// The attribute describes the detailed behaviour on how to load a single field/property
	/// 
	/// </summary>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class JSONLoadableElementAttribute : Attribute
	{
		/// <summary>
		/// specifies if the field needs a corresponding JSON item in the document
		/// </summary>
		public bool IsMandatory 
		{
			get;
			private set;
		}
		/// <summary>
		/// specifies the name of the item in the JSON Document. This allows a name conversion. 
		/// If not specified the name of the field/property is used to find the value in the source object
		/// </summary>
		public string ExternalName 
		{
			get;
			private set;
		}
		/// <summary>
		/// ctor
		/// </summary>
		/// <param name="mandatory"></param>
		/// <param name="externalName"></param>
		public JSONLoadableElementAttribute(bool mandatory = true, string externalName = null)
		{
			IsMandatory = mandatory;
			ExternalName = externalName;
		}
	}
}




