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
	/// Document implementation
	/// </summary>
	internal class JSONDocumentImpl : IJSONDocument
	{
		public IJSONItem Root {
			get;
			set;
		}

		public IJSONItemObject RootAsObject {
			get {
				return Root as IJSONItemObject;
			}
		}

		public JSONItemType RootType {
			get {
				return Root != null ? Root.ItemType : JSONItemType.Undefined;
			}
		}
		public JSONDocumentImpl(IJSONItem root=null)
		{
			this.Root=root;
		}

		public IJSONItemArray CreateItemArray()
		{
			return new JSONItemArray();
		}

		public IJSONItemObject CreateItemObject()
		{
			return new JSONItemObject();
		}

		public IJSONItemString CreateItemString(string val)
		{
			return new JSONItemString(val);
		}

		public IJSONItemNumber CreateItemNumber(string val)
		{
			return new JSONItemNumber(val);
		}

		public IJSONItemNumber CreateItemNumber(int val)
		{
			return new JSONItemNumber(val);
		}
		public IJSONItemNumber CreateItemNumber(long val)
		{
			return new JSONItemNumber(val);
		}
		public IJSONItemNumber CreateItemNumber(double val)
		{
			return new JSONItemNumber(val);
		}
		public IJSONItemNull CreateItemNull()
		{
			return new JSONItemNull();
		}

		public IJSONItemBool CreateItemBool(bool val)
		{
			return new JSONItemBool(val);
		}

		public IJSONItem RetrieveItem(string jpath)
		{
			// split the jpath, and treat each sub-string as a "level" in the document
			// TODO handle mask of '.'
			var parts=jpath.Split('.');
			var run=Root;
			for(int i=0;i<parts.Length;i++)
			{
				var cp=parts[i];
				if( run.ItemType==JSONItemType.Object )
				{
					IJSONItem subItem;
					if( ((IJSONItemObject)run).TryGetValue(cp,out subItem ))
				   	{
						run=subItem;
				   	}
					else
						throw new ArgumentException("item not found: " + cp);
				}
				else
				{
					if( run.ItemType==JSONItemType.Array )
					{
						int idx;
						if( Int32.TryParse(cp,out idx) )
						{
							if( idx>=0 && idx<((IJSONItemArray)run).Count )
							{
								run=((IJSONItemArray)run)[idx];
							}
							else
								throw new ArgumentOutOfRangeException("illegal index " + cp );
						}
						else
							throw new ArgumentException("can't find " + cp);
							
						     
					}
					else
						throw new ArgumentException("not an array/object: " + cp);
				}
			}
			
			return run;
		}

		public IJSONItemObject RetrieveItemObject(string jpath)
		{
			var s=RetrieveItem(jpath) as IJSONItemObject;
			if( s==null)
				throw new InvalidOperationException("no object");
			return s;			
		}

		public IJSONItemArray RetrieveItemArray(string jpath)
		{
			var s=RetrieveItem(jpath) as IJSONItemArray;
			if( s==null)
				throw new InvalidOperationException("no array");
			return s;			
		}

		public string RetrieveItemString(string jpath)
		{
			var s = RetrieveItem(jpath) as IJSONItemString;
			if( s==null)
				throw new InvalidOperationException("no string");
			return s.Value;
		}

		public IJSONItemNumber RetrieveItemNumber(string jpath)
		{
			var item=RetrieveItem(jpath) as IJSONItemNumber;
			if(item==null)
				throw new InvalidOperationException("no number");
			return item;
		}
		public bool RetrieveItemBool(string jpath)
		{
			var item=RetrieveItem(jpath) as IJSONItemBool;
			if( item==null)
				throw new InvalidOperationException("no bool");
			return item.Value;
			
		}
		public int RetrieveItemInt(string jpath)
		{
			var item=RetrieveItemNumber(jpath);
			int v;
			if( item.TryGetAsInt(out v ) )
				return v;
			throw new InvalidOperationException("no int");			
		}
	}
}


