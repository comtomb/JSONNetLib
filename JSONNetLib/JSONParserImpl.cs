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
	/// implementation of a Parser
	/// 
	/// the parser uses a JSONSource as wrapper for the actual input sources (string, files...) to read the source char by char
	/// 	
	/// </summary>
	internal class JSONParserImpl : IJSONParser
	{
		#region sources
		/// <summary>
		/// read one char after the other...
		/// </summary>
		[DebuggerDisplay("'{CurrentCharAsChar}' ({currentChar}) @ {pos}")]
		abstract class JSONSource : IDisposable
		{
			protected abstract int FetchNextChar();

			protected char CurrentCharAsChar 
			{
				get 
				{
					return (char)currentChar;
				}
			}

			public int currentChar;

			public JSONParserPos pos = new JSONParserPos();

			/// <summary>
			/// next char, but fail on EOF
			/// </summary>
			/// <returns></returns>
			public int ReadNextCharNoEOF()
			{
				int c = ReadNextChar();
				if (c < 0)
					throw new JSONException("EOF met");
				return c;
			}
			/// <summary>
			/// read the next char.
			/// </summary>
			/// <returns>next char, or -1 for EOF</returns>
			public int ReadNextChar()
			{
				currentChar = FetchNextChar();
				if (currentChar < 0)
					return -1;
				pos.streamPos++;
				pos.column++;
				if (currentChar == '\n') {
					pos.row++;
					pos.column = 1;
				}
				return currentChar;
			}

			/// <summary>
			/// skip white space, fail if EOF is met
			/// </summary>
			/// <returns>next non-white character</returns>
			public int SkipWhiteWithException()
			{
				int c = currentChar;
				while (IsWhite(c))
					c = ReadNextCharNoEOF();
				return c;
			}
			
			protected bool IsWhite(int c)
			{
				return Char.IsWhiteSpace((char)c);
			}
			/// <summary>
			/// skip whitespace, expect the first "non-white" char to be a specifc one
			/// </summary>
			/// <param name="expect"></param>
			public void SkipWhiteExpect(int expect)
			{
				int c = SkipWhiteWithException();
				if (c != expect)
					throw new JSONUnexpectedCharException(c, expect, pos);
			}
			/// <summary>
			/// read next char, expect it to a specific one
			/// </summary>
			/// <param name="expect"></param>
			public void ReadNextExpect(int expect)
			{
				if (ReadNextChar() != expect)
					throw new JSONUnexpectedCharException(currentChar, expect, pos);
			}

			public void Dispose()
			{
			}
		}

		class JSONSourceStream : JSONSource
		{
			StreamReader reader;

			public JSONSourceStream(Stream s)
			{
				reader = new StreamReader(s);
			}

			protected override int FetchNextChar()
			{
				return reader.Read();
			}
		}

		class JSONSourceString : JSONSource
		{
			readonly StringReader reader;

			public JSONSourceString(string str)
			{
				reader = new StringReader(str);
			}

			protected override int FetchNextChar()
			{
				return reader.Read();
			}
		}

		class JSONSourceReader : JSONSource
		{
			readonly TextReader reader;

			public JSONSourceReader(TextReader reader)
			{
				this.reader = reader;
			}

			protected override int FetchNextChar()
			{
				return reader.Read();
			}
		}

		#endregion

		/// <summary>
		/// curren document
		/// </summary>
		private IJSONDocument currentDoc;
		/// <summary>
		/// current source
		/// </summary>
		private JSONSource currentSource;

		public JSONParserImpl()
		{
		}

		#region public interface
		public IJSONDocument LoadFromStream(Stream s)
		{
			using (var src = new JSONSourceStream(s)) {
				return LoadFromSource(src);
			}
		}

		public IJSONDocument LoadFromFile(string fname)
		{
			using (var fs = new FileStream(fname, FileMode.Open, FileAccess.Read, FileShare.Read, 8192)) {
				return LoadFromStream(fs);
			}
		}

		public IJSONDocument LoadFromString(string json)
		{
			using (var src = new JSONSourceString(json)) {
				return LoadFromSource(src);
			}
		}

		public IJSONDocument LoadFromHttp(string uri)
		{
			using (var client = new System.Net.Http.HttpClient()) 
			{
				using (var resp = client.GetAsync(uri).Result) 
				{
					resp.EnsureSuccessStatusCode();
					using (var stream = resp.Content.ReadAsStreamAsync().Result) 
					{
						return LoadFromStream(stream);
					}
				}
			}
		}

		public IJSONDocument LoadFromReader(TextReader s)
		{
			using (var src = new JSONSourceReader(s)) 
			{
				return LoadFromSource(src);
			}
		}

		#endregion
		private IJSONDocument LoadFromSource(JSONSource source)
		{
			currentSource = source;
			using( currentSource )
			{
				if (source.ReadNextChar() < 0)
					throw new JSONException("unexpected end met");				
				currentDoc = JSONDocument.CreateDocument();
				try
				{
					var root = ReadValue();
					// TODO read til end?
					currentDoc.Root = root;
					var cd=currentDoc;
					currentDoc=null;
					currentSource=null;
					return cd;
				} catch( InvalidOperationException e)
				{
					throw new JSONException("Invalid Operation @ " + currentSource.pos , e);
				}
				catch( ArgumentException e)
				{
					throw new JSONException("Invalid Argument @ " + currentSource.pos , e);
				}
			}
		}
		/// <summary>
		/// read a value
		/// </summary>
		/// <returns></returns>
		private IJSONItem ReadValue()
		{
			switch ((char)currentSource.SkipWhiteWithException()) 
			{
				case '"':
					return ReadItemString();
				case '{':
					return ReadItemObject();
				case '[':
					return ReadItemArray();
				case 't':
					return ReadItemTrue();
				case 'f':
					return ReadItemFalse();
				case 'n':
					return ReadItemNull();
				case '-':
				case '0':
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
					return ReadItemNumber();
				default:
					throw new JSONUnexpectedCharException(currentSource.currentChar, currentSource.pos);
			}
		}

		private IJSONItemString ReadItemString()
		{
			Debug.Assert(currentSource.currentChar == '"');
			bool escaped;
			return currentDoc.CreateItemString(ReadRawString(out escaped));
		}

		private IJSONItemNumber ReadItemNumber()
		{
			Debug.Assert(currentSource.currentChar == '-' || (currentSource.currentChar >= '0' && currentSource.currentChar <= '9'));
			var num = new StringBuilder();
			if (currentSource.currentChar == '-') {
				num.Append('-');
				currentSource.ReadNextCharNoEOF();
			}
			if (currentSource.currentChar == '0') {
				num.Append('0');
				currentSource.ReadNextChar();
			}
			else {
				if (currentSource.currentChar >= '1' && currentSource.currentChar <= '9') {
					while (currentSource.currentChar >= '0' && currentSource.currentChar <= '9') {
						num.Append((char)currentSource.currentChar);
						currentSource.ReadNextChar();
					}
				}
				else
					throw new JSONUnexpectedCharException(currentSource.currentChar, currentSource.pos);
			}
			if (currentSource.currentChar == '.') {
				num.Append('.');
				currentSource.ReadNextCharNoEOF();
				if (currentSource.currentChar >= '0' && currentSource.currentChar <= '9') {
					while (currentSource.currentChar >= '0' && currentSource.currentChar <= '9') {
						num.Append((char)currentSource.currentChar);
						currentSource.ReadNextChar();
					}
				}
				else
					throw new JSONUnexpectedCharException(currentSource.currentChar, currentSource.pos);
			}
			if (currentSource.currentChar == 'e' || currentSource.currentChar == 'E') {
				num.Append('E');
				currentSource.ReadNextCharNoEOF();
				if (currentSource.currentChar == '+' || currentSource.currentChar == '-') {
					if (currentSource.currentChar == '-')
						num.Append((char)currentSource.currentChar);
					currentSource.ReadNextCharNoEOF();
				}
				if (currentSource.currentChar >= '0' && currentSource.currentChar <= '9') {
					while (currentSource.currentChar >= '0' && currentSource.currentChar <= '9') {
						num.Append((char)currentSource.currentChar);
						currentSource.ReadNextChar();
					}
				}
				else
					throw new JSONUnexpectedCharException(currentSource.currentChar, currentSource.pos);
			}
			return currentDoc.CreateItemNumber(num.ToString());
		}

		private IJSONItemNull ReadItemNull()
		{
			Debug.Assert(currentSource.currentChar == 'n');
			currentSource.ReadNextExpect('u');
			currentSource.ReadNextExpect('l');
			currentSource.ReadNextExpect('l');
			currentSource.ReadNextChar();
			return currentDoc.CreateItemNull();
		}

		private IJSONItemBool ReadItemTrue()
		{
			Debug.Assert(currentSource.currentChar == 't');
			currentSource.ReadNextExpect('r');
			currentSource.ReadNextExpect('u');
			currentSource.ReadNextExpect('e');
			currentSource.ReadNextChar();
			return currentDoc.CreateItemBool(true);
		}

		private IJSONItemBool ReadItemFalse()
		{
			Debug.Assert(currentSource.currentChar == 'f');
			currentSource.ReadNextExpect('a');
			currentSource.ReadNextExpect('l');
			currentSource.ReadNextExpect('s');
			currentSource.ReadNextExpect('e');
			currentSource.ReadNextChar();
			return currentDoc.CreateItemBool(false);
		}

		private IJSONItemArray ReadItemArray()
		{
			Debug.Assert(currentSource.currentChar == '[');
			var arr = currentDoc.CreateItemArray();
			currentSource.ReadNextCharNoEOF();
			currentSource.SkipWhiteWithException();
			if (currentSource.currentChar != ']') {
				while (true) {
					var item = ReadValue();
					arr.Add(item);
					currentSource.SkipWhiteWithException();
					if (currentSource.currentChar == ']')
						break;
					if (currentSource.currentChar != ',')
						throw new JSONUnexpectedCharException(currentSource.currentChar, ',', currentSource.pos);
					currentSource.ReadNextCharNoEOF();
				}
			}
			currentSource.ReadNextChar();
			return arr;
		}

		private IJSONItemObject ReadItemObject()
		{
			Debug.Assert(currentSource.currentChar == '{');
			currentSource.ReadNextCharNoEOF();
			currentSource.SkipWhiteWithException();
			var obj = currentDoc.CreateItemObject();
			if (currentSource.currentChar != '}') {
				while (true) {
					currentSource.SkipWhiteExpect('"');
					bool escaped;
					var key = ReadRawString(out escaped);
					currentSource.SkipWhiteExpect(':');
					currentSource.ReadNextCharNoEOF();
					var val = ReadValue();
					obj.Add(key, val);
					currentSource.SkipWhiteWithException();
					if (currentSource.currentChar == '}')
						break;
					if (currentSource.currentChar != ',')
						throw new JSONUnexpectedCharException(currentSource.currentChar, ',', currentSource.pos);
					currentSource.ReadNextCharNoEOF();
				}
			}
			currentSource.ReadNextChar();
			return obj;
		}

		private string ReadRawString(out bool escaped)
		{
			Debug.Assert(currentSource.currentChar == '"');
			var sb = new StringBuilder();
			escaped = false;
			while (currentSource.ReadNextCharNoEOF() != '"') {
				if (currentSource.currentChar == '\\') {
					currentSource.ReadNextCharNoEOF();
					switch ((char)currentSource.currentChar) {
						case '"':
							escaped = true;
							sb.Append('"');
							break;
						case '\\':
							escaped = true;
							sb.Append('\\');
							break;
						case '/':
							sb.Append('/');
							escaped = true;
							break;
						case 'b':
							escaped = true;
							sb.Append('\b');
							break;
						case 'n':
							escaped = true;
							sb.Append('\n');
							break;
						case 'r':
							escaped = true;
							sb.Append('\r');
							break;
						case 't':
							escaped = true;
							sb.Append('\t');
							break;
						case 'u':
							var h = new StringBuilder();
							for (int i = 0; i < 4; i++) {
								currentSource.ReadNextCharNoEOF();
								if ((currentSource.currentChar >= '0' && currentSource.currentChar <= '9') || (currentSource.currentChar >= 'A' && currentSource.currentChar <= 'F') || (currentSource.currentChar >= 'a' && currentSource.currentChar <= 'f'))
									h.Append((char)currentSource.currentChar);
								else
									throw new JSONUnexpectedCharException(currentSource.currentChar, currentSource.pos);
							}
							int hex = Int32.Parse(h.ToString(), System.Globalization.NumberStyles.HexNumber);
							sb.Append((char)hex);
							break;
						default:
							throw new JSONUnexpectedCharException(currentSource.currentChar, currentSource.pos);
					}
				}
				else {
					sb.Append((char)currentSource.currentChar);
				}
			}
			currentSource.ReadNextChar();
			return sb.ToString();
		}
	}
}


