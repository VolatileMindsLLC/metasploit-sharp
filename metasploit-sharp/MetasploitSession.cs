using System;
using System.Collections.Generic;
using MsgPack;
using System.Net;
using System.IO;
using System.Net.Sockets;

namespace metasploitsharp
{
	public class MetasploitSession : IDisposable
	{
		string _host;
		string _token;
		
		public MetasploitSession (string username, string password, string host)
		{
			_host = host;
			_token = null;
			
			Dictionary<object, object > response = this.Authenticate (username, password);
			
			bool loggedIn = !response.ContainsKey ("error");
			
			if (!loggedIn)
				throw new Exception (response ["error_message"] as string);
			
			if ((response ["result"] as string) == "success")
				_token = response ["token"] as string;
		}
		
		public string Token { 
			get { return _token; }
		}
		
		public Dictionary<object, object> Authenticate (string username, string password)
		{
			return this.Execute ("auth.login", username, password);
		}
		
		public Dictionary<object, object> Execute (string method, params object[] args)
		{
			if (string.IsNullOrEmpty (_host))
				throw new Exception ("Host null or empty");
			
			if (method != "auth.login" && string.IsNullOrEmpty (_token))
				throw new Exception ("Not authenticated.");
		
			BoxingPacker boxingPacker = new BoxingPacker ();
			ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => {return true;}; //dis be bad, no ssl check
			
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create (_host);
			request.ContentType = "binary/message-pack";
			request.Method = "POST";
			request.KeepAlive = true;
			
			Stream requestStream = request.GetRequestStream ();
			MsgPackWriter msgpackWriter = new MsgPackWriter (requestStream);
			
			msgpackWriter.WriteArrayHeader (args.Length + 1 + (string.IsNullOrEmpty (_token) ? 0 : 1));
			
			msgpackWriter.Write (method);
			
			if (!string.IsNullOrEmpty (_token))
				msgpackWriter.Write (_token);
			
			foreach (object arg in args) 
				Pack(msgpackWriter, arg);
			
			requestStream.Close();
			
			byte[] results;
			byte[] buffer = new byte[4096];
			using (WebResponse response = request.GetResponse ())
			{
				using (Stream rstream = response.GetResponseStream())
				{
					using (MemoryStream mstream = new MemoryStream())
					{
						int count = 0;
						
						do
						{
							count = rstream.Read(buffer, 0, buffer.Length);
							mstream.Write(buffer, 0, count);
						} while (count != 0);
						
						results = mstream.ToArray();
					}
				}
			}

			
			//everything is a bunch of bytes, needs to be typed
			Dictionary<object, object > resp = boxingPacker.Unpack (results) as Dictionary<object, object>;
			
			//This is me trying to type the response for the user....
			Dictionary<object, object > returnDictionary = TypifyDictionary(resp);
			
			return returnDictionary;
		}
		
		Dictionary<object, object> TypifyDictionary(Dictionary<object, object> dict)
		{
			Dictionary<object, object> returnDictionary = new Dictionary<object, object>();
			System.Text.Encoding enc = System.Text.Encoding.UTF8;
			foreach (var pair in dict)
			{
				if (pair.Value != null) {
					if (pair.Value is bool)
						returnDictionary.Add (enc.GetString (pair.Key as byte[]), ((bool)pair.Value).ToString ());
					else if (pair.Value is byte[])
						returnDictionary.Add (enc.GetString (pair.Key as byte[]), enc.GetString (pair.Value as byte[]));
					else if (pair.Value is object[])
					{
						object[] ret = new object[(pair.Value as object[]).Length];
						int i = 0;
						foreach (object obj in pair.Value as object[])
						{
							if (obj is Dictionary<object, object>)
								ret[i] = TypifyDictionary(obj as Dictionary<object, object>);
							else if (obj is byte[])
								ret[i] = enc.GetString(obj as byte[]);
							else
								throw new Exception("Don't know how to do type: " + obj.GetType().ToString());
							
							i++;
						}
						
						returnDictionary.Add(enc.GetString(pair.Key as byte[]), ret);
					}
					else if (pair.Value is UInt32)
						returnDictionary.Add (enc.GetString (pair.Key as byte[]), ((UInt32)pair.Value).ToString ());
					else if (pair.Value is Int32)
						returnDictionary.Add (enc.GetString (pair.Key as byte[]), ((Int32)pair.Value).ToString ());
					else if (pair.Value is Dictionary<object, object>)
						returnDictionary.Add (pair.Key, TypifyDictionary(pair.Value as Dictionary<object, object>));
					else
						throw new Exception ("unknown type: " + pair.Value.GetType().ToString());
				} else
					returnDictionary.Add (enc.GetString (pair.Key as byte[]), string.Empty);
			}
			
			return returnDictionary;
		}
		
		void Pack (MsgPackWriter writer, object o)
		{
 	 	
			if (o == null) {
				writer.WriteNil ();
				return;
			}
 	
			if (o is int)
				writer.Write ((int)o);
			else if (o is uint)
				writer.Write ((uint)o);
			else if (o is float)
				writer.Write ((float)o);
			else if (o is double)
				writer.Write ((double)o);
			else if (o is long)
				writer.Write ((long)o);
			else if (o is ulong)
				writer.Write ((ulong)o);
			else if (o is bool)
				writer.Write ((bool)o);
			else if (o is byte)
				writer.Write ((byte)o);
			else if (o is sbyte)
				writer.Write ((sbyte)o);
			else if (o is short)
				writer.Write ((short)o);
			else if (o is ushort)
				writer.Write ((ushort)o);
			else if (o is string)
				writer.Write(o as string);
			else if (o is Dictionary<object, object>)
			{
				writer.WriteMapHeader((o as Dictionary<object, object>).Count);
				
				foreach (var pair in (o as Dictionary<object, object>))
				{
					Pack(writer, pair.Key);
					Pack(writer, pair.Value);
				}
				
			}
			else
				throw new NotSupportedException (); 
		
		}
		
		public void Dispose ()
		{
			this.Execute ("auth.logout", new object[] {});
		}
	}
}

