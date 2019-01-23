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
	internal class JSONItemObject : JSONItemGroup, IJSONItemObject
	{
		private IDictionary<string, IJSONItem> items = new Dictionary<string, IJSONItem>();

		public JSONItemObject() 
			: base(JSONItemType.Object)
		{
		}

		public bool ContainsKey(string key)
		{
			return items.ContainsKey(key);
		}

		public void Add(string key, IJSONItem value)
		{
			items.Add(key, value);
		}

		public bool Remove(string key)
		{
			return items.Remove(key);
		}

		public bool TryGetValue(string key, out IJSONItem value)
		{
			return items.TryGetValue(key,out value);
		}

		public IJSONItem this[string key] 
		{
			get 
			{
				return items[key];
			}
			set 
			{
				items[key]=value;
			}
		}

		public ICollection<string> Keys 
		{
			get 
			{
				return items.Keys;
			}
		}

		public ICollection<IJSONItem> Values 
		{
			get 
			{
				return items.Values;
			}
		}

		public void Add(KeyValuePair<string, IJSONItem> item)
		{
			items.Add(item);
		}

		public void Clear()
		{
			items.Clear();
		}

		public bool Contains(KeyValuePair<string, IJSONItem> item)
		{
			return items.Contains(item);
		}

		public void CopyTo(KeyValuePair<string, IJSONItem>[] array, int arrayIndex)
		{
			items.CopyTo(array,arrayIndex);
		}

		public bool Remove(KeyValuePair<string, IJSONItem> item)
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

		public IEnumerator<KeyValuePair<string, IJSONItem>> GetEnumerator()
		{
			return items.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return items.GetEnumerator();
		}

		public void Add(string key, string value)
		{
			Add(key, new JSONItemString(value));
		}

		public void Add(string key, int value)
		{
			Add(key,new JSONItemNumber(value));
		}

		public void Add(string key, long value)
		{
			Add(key,new JSONItemNumber(value));
		}

		public void Add(string key, double value)
		{
			Add(key,new JSONItemNumber(value));
		}

		public void Add(string key, bool value)
		{
			Add(key,new JSONItemBool(value));
		}
	}
}


