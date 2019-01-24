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
	/// JSON Number
	/// 
	/// the number is internally represented as a string (following the JSON conventions)
	/// 
	/// several methods are provided to get/set the value as int32,int64, double
	/// </summary>
	public interface IJSONItemNumber : IJSONItemAtomic<string>
	{
		/// <summary>
		/// set as Int32
		/// </summary>
		/// <param name="v">value</param>
		void Set(int v);

		/// <summary>
		/// set as Int64
		/// </summary>
		/// <param name="v">value</param>
		void Set(long v);
		/// <summary>
		/// set as double
		/// </summary>
		/// <param name="v">value</param>
		void Set(double v);

		/// <summary>
		/// true if the number can be converted to Int32
		/// </summary>
		bool IsInt 
		{
			get;
		}

		/// <summary>
		/// true if the number can be converted to long
		/// </summary>
		bool IsLong 
		{
			get;
		}
		/// <summary>
		/// true if the number can be converted to double
		/// </summary>
		bool IsDouble 
		{
			get;
		}

		/// <summary>
		/// return the number as int
		/// </summary>
		/// <returns>int</returns>
		/// <exception cref="InvalidOperationException"> if the number is not an int </exception>
		int GetAsInt();

		/// <summary>
		/// return the number as long
		/// </summary>
		/// <returns>int</returns>
		/// <exception cref="InvalidOperationException"> if the number is not a long</exception>
		long GetAsLong();
		/// <summary>
		/// return the number as double
		/// </summary>
		/// <returns>int</returns>
		/// <exception cref="InvalidOperationException"> if the number is not a double</exception>
		double GetAsDouble();
		/// <summary>
		/// try to get the number as int
		/// </summary>
		/// <param name="v">when this method returns contains the int value of this number</param>
		/// <returns>true if success</returns>
		bool TryGetAsInt(out int v);

		/// <summary>
		/// try to get the number as long
		/// </summary>
		/// <param name="v">when this method returns contains the long value of this number</param>
		/// <returns>true if success</returns>
		bool TryGetAsLong(out long v);

		/// <summary>
		/// try to get the number as double
		/// </summary>
		/// <param name="v">when this method returns contains the double value of this number</param>
		/// <returns>true if success</returns>
		bool TryGetAsDouble(out double v);
	}
}


