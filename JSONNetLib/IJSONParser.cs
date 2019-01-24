﻿// MIT License
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
	/// Parser for JSON Sources
	/// Load <see cref="IJSONDocument"/> from several sources
	/// </summary>
	public interface IJSONParser
	{
		/// <summary>
		/// Load a <see cref="IJSONDocument"/> from a stream
		/// </summary>
		/// <param name="s">source</param>
		/// <returns>document read from the source</returns>
		/// <exception cref="JSONException">parsing error</exception>
		IJSONDocument LoadFromStream(Stream s);

		/// <summary>
		/// Load a <see cref="IJSONDocument"/> from a file
		/// </summary>
		/// <param name="fname">filename</param>
		/// <returns>document read from the source</returns>
		/// <exception cref="JSONException">parsing error</exception>
		IJSONDocument LoadFromFile(string fname);
		/// <summary>
		/// Load a <see cref="IJSONDocument"/> from a string
		/// </summary>
		/// <param name="json">JSON string</param>
		/// <returns>document read from the source</returns>
		/// <exception cref="JSONException">parsing error</exception>
		IJSONDocument LoadFromString(string json);

		/// <summary>
		/// Load a <see cref="IJSONDocument"/> from a URI using HTTP
		/// </summary>
		/// <param name="uri">URI where the JSON text is location</param>
		/// <returns>document read from the source</returns>
		/// <exception cref="JSONException">parsing error</exception>
		IJSONDocument LoadFromHttp(string uri);
		/// <summary>
		/// Load a <see cref="IJSONDocument"/> from a TextReader
		/// </summary>
		/// <param name="s">source</param>
		/// <returns>document read from the source</returns>
		/// <exception cref="JSONException">parsing error</exception>		
		IJSONDocument LoadFromReader(TextReader s);
	}
}


