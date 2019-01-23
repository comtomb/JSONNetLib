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
	[DebuggerDisplay("{Count}")]
	internal class JSONItemArray : JSONItemGroup, IJSONItemArray
	{
		private List<IJSONItem> items = new List<IJSONItem>();

		public JSONItemArray() : base(JSONItemType.Array)
		{
		}

		public int IndexOf(IJSONItem item)
		{
			return items.IndexOf(item);
		}

		public void Insert(int index, IJSONItem item)
		{
			items.Insert(index,item);
		}

		public void RemoveAt(int index)
		{
			items.RemoveAt(index);
		}

		public IJSONItem this[int index] 
		{
			get 
			{
				return items[index];
			}
			set 
			{
				items[index]=value;
			}
		}

		public void Add(IJSONItem item)
		{
			items.Add(item);
		}

		public void Clear()
		{
			items.Clear();
		}

		public bool Contains(IJSONItem item)
		{
			return items.Contains(item);
		}

		public void CopyTo(IJSONItem[] array, int arrayIndex)
		{
			items.CopyTo(array,arrayIndex);
		}

		public bool Remove(IJSONItem item)
		{
			return items.Remove(item);
		}

		public int Count 
		{
			get 
			{
				return items.Count;
			}
		}

		public bool IsReadOnly 
		{
			get 
			{
				return false;
			}
		}

		public IEnumerator<IJSONItem> GetEnumerator()
		{
			return items.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return items.GetEnumerator();
		}

		public void Add(string value)
		{
			Add(new JSONItemString(value));
		}

		public void Add(int value)
		{
			Add(new JSONItemNumber(value));
		}

		public void Add(long value)
		{
			Add(new JSONItemNumber(value));
		}

		public void Add(double value)
		{
			Add(new JSONItemNumber(value));
		}

		public void Add(bool value)
		{
			Add(new JSONItemBool(value));
		}
	}
}


