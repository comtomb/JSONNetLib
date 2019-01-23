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
	internal class JSONItemNumber : JSONItemAtomic, IJSONItemNumber
	{
		private JSONItemNumber()
			: base(JSONItemType.Number,null)
		{
			
		}
		public JSONItemNumber(string val) 
			:this()
		{
			Value=val;
		}
		public JSONItemNumber(int val)
			: this()
		{
			Set(val);
		}
		public JSONItemNumber(long val)
			: this()
		{
			Set(val);
		}
		public JSONItemNumber(double val)
			: this()
		{
			Set(val);
		}

		public string Value
		{
			get
			{
				return (string)ValueAsObject;
			}
			set
			{
				// TODO syntax check
				ValueAsObject=value;
			}
		}
		
		public void Set(int v)
		{
			Value = v.ToString();
		}

		public void Set(long v)
		{
			Value = v.ToString();
		}

		public void Set(double v)
		{
			Value = v.ToString();
			// TODO localization ('E', '.'... )
		}

		public int GetAsInt()
		{
			int v;
			if (TryGetAsInt(out v))
				return v;
			else
				throw new InvalidOperationException();
		}

		public long GetAsLong()
		{
			long v;
			if (TryGetAsLong(out v))
				return v;
			else
				throw new InvalidOperationException();
		}

		public double GetAsDouble()
		{
			double v;
			if (TryGetAsDouble(out v))
				return v;
			else
				throw new InvalidOperationException();
		}

		public bool TryGetAsInt(out int v)
		{
			return Int32.TryParse(Value, out v);
		}

		public bool TryGetAsLong(out long v)
		{
			return Int64.TryParse(Value, out v);
		}

		public bool TryGetAsDouble(out double v)
		{
			return Double.TryParse(Value, out v);
		}

		public bool IsInt {
			get {
				int v;
				return TryGetAsInt(out v);
			}
		}

		public bool IsLong {
			get {
				long v;
				return TryGetAsLong(out v);
			}
		}

		public bool IsIntDouble {
			get {
				double v;
				return TryGetAsDouble(out v);
			}
		}
	}
}


