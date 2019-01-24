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
	/// Representation of a JSON Array
	/// </summary>
	public interface IJSONItemArray : IJSONItemGroup, IList<IJSONItem>
	{		
		/// <summary>
		/// Add a String Item
		/// </summary>
		/// <param name="value">string</param>
		void Add(string value);
		/// <summary>
		/// Add a number item
		/// </summary>
		/// <param name="value">int</param>
		void Add(int value);
		/// <summary>
		/// Add a number item
		/// </summary>
		/// <param name="value">long</param>
		void Add(long value);
		/// <summary>
		/// Add a number item
		/// </summary>
		/// <param name="value">double</param>
		void Add(double value);
		/// <summary>
		/// Add a bool item
		/// </summary>
		/// <param name="value">bool</param>
		void Add(bool value);
	}
}


