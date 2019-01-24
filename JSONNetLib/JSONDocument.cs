/*
 */
using System;

namespace TomB.Util.JSON
{
	/// <summary>
	/// Factory class to create an (empty) <see cref="IJSONDocument"/>
	/// </summary>
	public static class JSONDocument
	{
		/// <summary>
		/// create a document
		/// </summary>
		/// <param name="root">optional root item</param>
		/// <returns></returns>
		public static IJSONDocument CreateDocument(IJSONItem root=null)
		{
			return new JSONDocumentImpl(root);
		}
	}
}
