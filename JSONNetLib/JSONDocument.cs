/*
 */
using System;

namespace TomB.Util.JSON
{
	public static class JSONDocument
	{
		public static IJSONDocument CreateDocument(IJSONItem root=null)
		{
			return new JSONDocumentImpl(root);
		}
	}
}
