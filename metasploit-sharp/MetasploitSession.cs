using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Net.Sockets;
using MsgPack.Serialization;
using MsgPack;
using System.Collections.Specialized;
using System.Text;
using System.Collections;

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
			
			Dictionary<string, object > response = this.Authenticate (username, password);
			
			bool loggedIn = !response.ContainsKey ("error");

			if (!loggedIn)
				throw new Exception (response ["error_message"] as string);
			
			if ((response ["result"] as string) == "success")
				_token = response ["token"] as string;
		} 

		public MetasploitSession (string token, string host)
		{
			_token = token;
			_host = host;
		}
		
		public string Token { 
			get { return _token; }
		}
		
		public Dictionary<string, object> Authenticate (string username, string password)
		{
			return this.Execute ("auth.login", username, password);
		}
		
		public Dictionary<string, object> Execute (string method, params object[] args)
		{
			if (string.IsNullOrEmpty (_host))
				throw new Exception ("Host null or empty");
			
			if (method != "auth.login" && string.IsNullOrEmpty (_token))
				throw new Exception ("Not authenticated.");
		
			ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => {return true;}; //dis be bad, no ssl check
			
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create (_host);

			request.ContentType = "binary/message-pack";
			request.Method = "POST";
			request.KeepAlive = true;
			
			Stream requestStream = request.GetRequestStream ();
			Packer msgpackWriter = Packer.Create(requestStream);
			
			msgpackWriter.PackArrayHeader (args.Length + 1 + (string.IsNullOrEmpty (_token) ? 0 : 1));
			
			msgpackWriter.PackString (method);
			
			if (!string.IsNullOrEmpty (_token) && method != "auth.login")
				msgpackWriter.Pack (_token);
			
			foreach (object arg in args) 
				Pack(msgpackWriter, arg);
			
			requestStream.Close();

			byte[] buffer = new byte[4096];
			MemoryStream mstream = new MemoryStream();

			try {
			using (WebResponse response = request.GetResponse ())
			{
				using (Stream rstream = response.GetResponseStream())
				{
					int count = 0;
					
					do
					{
						count = rstream.Read(buffer, 0, buffer.Length);
						mstream.Write(buffer, 0, count);
					} while (count != 0);
					
				}
			}
			}
			catch (WebException ex) {
				string res = string.Empty;
				using (StreamReader rdr = new StreamReader(ex.Response.GetResponseStream()))
					res = rdr.ReadToEnd();

				Console.WriteLine(res) ;
			}
			
			mstream.Position = 0;
			
			//everything is a bunch of bytes, needs to be typed
			MessagePackObjectDictionary resp = Unpacking.UnpackObject(mstream).AsDictionary();
			//Hashtable resp = Unpacking.UnpackDictionary(mstream);
			
			//This is me trying to type the response for the user....
			Dictionary<string, object > returnDictionary = TypifyDictionary(resp);
			
			return returnDictionary;
		}
		
		Dictionary<string, object> TypifyDictionary(MessagePackObjectDictionary dict)
		{
			Dictionary<string, object> returnDictionary = new Dictionary<string, object>();
			
			foreach (var pair in dict)
			{
				MessagePackObject obj = (MessagePackObject)pair.Value;
				
				if (obj.UnderlyingType == null)
					continue;
				
				if (obj.IsRaw)
				{
					if (obj.IsTypeOf(typeof(string)).Value)
						returnDictionary[pair.Key.ToString()] = obj.ToString();
					else if (obj.IsTypeOf(typeof(int)).Value)
						returnDictionary[pair.Key.ToString()] = (int)obj.ToObject();
					else 
						throw new Exception("I don't know type: " + pair.Value.GetType().Name);
				}
				else if (obj.IsArray)
				{
					List<object> arr = new List<object>();
					foreach (var o in obj.ToObject() as MessagePackObject[])
					{
						if (o.IsDictionary)
							arr.Add(TypifyDictionary(o.AsDictionary()));
						else if (o.IsRaw)
							arr.Add(o.ToString());
						else if (o.IsArray)
						{
							var enu = o.AsEnumerable();
							List<object> array = new List<object>();
							foreach (var blah in enu)
								array.Add(blah as object);

							arr.Add(array.ToArray());
						}else if (o.ToObject().GetType() == typeof(Byte)) //this is a hack because I don't know what type you are...
							arr.Add(o.ToString());
					}
					
					returnDictionary.Add(pair.Key.AsString(), arr);
				}
				else if (obj.IsDictionary)
					returnDictionary[pair.Key.ToString()] = TypifyDictionary(obj.AsDictionary());
				else if (obj.IsTypeOf(typeof(UInt16)).Value)
					returnDictionary[pair.Key.ToString()] = obj.AsUInt16();
				else if (obj.IsTypeOf(typeof(UInt32)).Value)
					returnDictionary[pair.Key.ToString()] = obj.AsUInt32();
				else if (obj.IsTypeOf(typeof(bool)).Value)
					returnDictionary[pair.Key.ToString()] = obj.AsBoolean();
				else 
					throw new Exception("Don't know type: " + obj.ToObject().GetType().Name);
			}
			
			return returnDictionary;
		}
		
		void Pack (Packer packer, object o)
		{
 	 	
			if (o == null) {
				packer.PackNull();
				return;
			}
 	
			if (o is int)
				packer.Pack ((int)o);
			else if (o is uint)
				packer.Pack ((uint)o);
			else if (o is float)
				packer.Pack ((float)o);
			else if (o is double)
				packer.Pack ((double)o);
			else if (o is long)
				packer.Pack ((long)o);
			else if (o is ulong)
				packer.Pack ((ulong)o);
			else if (o is bool)
				packer.Pack ((bool)o);
			else if (o is byte)
				packer.Pack ((byte)o);
			else if (o is sbyte)
				packer.Pack ((sbyte)o);
			else if (o is short)
				packer.Pack ((short)o);
			else if (o is ushort)
				packer.Pack ((ushort)o);
			else if (o is string)
				packer.PackString((string)o, Encoding.ASCII);
			else if (o is Dictionary<string, object>)
			{
				packer.PackMapHeader((o as Dictionary<string, object>).Count);
				
				foreach (var pair in (o as Dictionary<string, object>))
				{
					Pack(packer, pair.Key);
					Pack(packer, pair.Value);
				}
				
			}
			else if (o is string[])
			{
				packer.PackArrayHeader((o as string[]).Length);
				
				foreach (var obj in (o as string[]))
					packer.Pack(obj as string);
			}
			else
				throw new Exception("Cant handle type: " + o.GetType().Name);; 
		
		}
		
		public void Dispose ()
		{
			this.Execute ("auth.logout", new object[] {});
		}
	}
}

