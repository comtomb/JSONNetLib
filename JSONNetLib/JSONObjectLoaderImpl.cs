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
using System.Reflection;

namespace TomB.Util.JSON
{
	
	
	
	internal class JSONObjectLoaderImpl : IJSONObjectLoader
	{
		
		public bool AllMandatory {get;set;}
		public JSONLoadableType Loadable {get;set;}

		public JSONObjectLoaderImpl()
		{
			AllMandatory=JSONLoadableClassAttribute.DefaultAllMandatory;
			Loadable=JSONLoadableClassAttribute.DefaultLoadable;
		}
		
		public void LoadObject(object dest, IJSONDocument src)
		{
			if( src.RootType!=JSONItemType.Object )
				throw new ArgumentException("no object as root");
			LoadObject( dest, src.RootAsObject );
		}
		private void LoadObject(object dest, IJSONItemObject src)
		{
			bool clsMandatory=AllMandatory;
			var clsLoadable=Loadable;
			var clsAttr=(JSONLoadableClassAttribute)Attribute.GetCustomAttribute(dest.GetType(),typeof( JSONLoadableClassAttribute ) ) ;
			if( clsAttr!=null)
			{
				clsMandatory=clsAttr.AllMandatory;
				clsLoadable=clsAttr.Loadable;
			}
			var fields=dest.GetType().GetFields(BindingFlags.NonPublic|BindingFlags.Public|BindingFlags.Instance);
			foreach( var field in fields )
			{
				var fieldAttr=(JSONLoadableElementAttribute)field.GetCustomAttribute( typeof(JSONLoadableElementAttribute) );
				var fieldLoadable=clsLoadable;
				string extName=field.Name;;
				bool fieldMandatory=clsMandatory;
				
				if( fieldAttr!=null )
				{
					fieldMandatory=fieldAttr.IsMandatory;
					if( fieldAttr.ExternalName!=null)
						extName=fieldAttr.ExternalName;
				}
				Debug.WriteLine(field.IsPublic );
				if( !(((clsLoadable & JSONLoadableType.Loadable)!=0 && fieldAttr!=null)					// skip loadable
				   || ( (clsLoadable & JSONLoadableType.PublicFields)!=0 && field.IsPublic )			// skip public field
				   || ( (clsLoadable & JSONLoadableType.NonPublicFields)!=0 && !field.IsPublic)))		// skip a nonPublic field
					continue;
				IJSONItem item;
				if( !src.TryGetValue(extName,out item) )
				{
					if( fieldMandatory)
						throw new ArgumentException("missing mandatory field '"+extName+"'");
					continue;
				}
				var fieldValue=MapItem(item, field.FieldType );
				field.SetValue(dest,fieldValue);
			}
			var properties=dest.GetType().GetProperties(BindingFlags.NonPublic|BindingFlags.Public|BindingFlags.Instance);
			foreach(var property in properties )
			{
				var fieldAttr=(JSONLoadableElementAttribute)property.GetCustomAttribute( typeof(JSONLoadableElementAttribute) );
				var fieldLoadable=clsLoadable;
				string extName=property.Name;;
				bool fieldMandatory=clsMandatory;
				
				if( fieldAttr!=null )
				{
					fieldMandatory=fieldAttr.IsMandatory;
					if( fieldAttr.ExternalName!=null)
						extName=fieldAttr.ExternalName;
				}
				bool isPublic=property.GetMethod.IsPublic;
				if( !(((clsLoadable & JSONLoadableType.Loadable)!=0 && fieldAttr!=null)					// skip loadable
				   || ( (clsLoadable & JSONLoadableType.PublicProperties)!=0 && isPublic )			// skip public field
				   || ( (clsLoadable & JSONLoadableType.NonPublicProperties)!=0 && !isPublic)))		// skip a nonPublic field
					continue;
				IJSONItem item;
				if( !src.TryGetValue(extName,out item) )
				{
					if( fieldMandatory)
						throw new ArgumentException("missing mandatory field '"+extName+"'");
					continue;
				}
				var fieldValue=MapItem(item, property.PropertyType );
				property.SetValue(dest,fieldValue);
				
			}
			
			
		}
		
		private object MapItem( IJSONItem item ,Type expectedType )
		{
			switch (item.ItemType) 
			{
				case JSONItemType.String:
					if( expectedType==typeof(string) )
					{
						return ((IJSONItemString)item).Value;
					}
					throw new InvalidOperationException();
				case JSONItemType.Array:
					{
						var itemArr=(IJSONItemArray)item;
						if( expectedType.IsArray && expectedType.GetArrayRank()==1)
						{
							var ret=Array.CreateInstance( expectedType.GetElementType(), itemArr.Count);
							for(int i=0;i<itemArr.Count;i++)
							{
								var subItem=MapItem(itemArr[i],expectedType.GetElementType() );
								ret.SetValue(subItem,i);
							}
							return ret;
						}
						else
							throw new InvalidOperationException();
					}
				case JSONItemType.Object:
					{
						var destObj=Activator.CreateInstance(expectedType);
						LoadObject(destObj,(IJSONItemObject)item);
						return destObj;
					}
				case JSONItemType.Number:
					if( expectedType==typeof(Int32) && ((IJSONItemNumber)item).IsInt )
						return ((IJSONItemNumber)item).GetAsInt();
					throw new InvalidOperationException();
				case JSONItemType.Null:
					return null;
				case JSONItemType.Bool:
					return ((IJSONItemBool)item).Value;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		
		public T CreateObject<T>(IJSONDocument src) where T : new()
		{
			return (T)MapItem(src.Root, typeof(T) );
		}
		
	}
	
	
	
	
	
	
	
	
	
	
	
}
