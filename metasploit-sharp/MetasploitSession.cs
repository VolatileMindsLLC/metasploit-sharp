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
			return this.Execute ("auth.login", new object[] { username, password });
		}
		
		//Yay, fun method!
		public Dictionary<object, object> Execute (string method, object[] args)
		{
			if (string.IsNullOrEmpty (_host))
				throw new Exception ("Host null or empty");
			
			if (method != "auth.login" && string.IsNullOrEmpty (_token))
				throw new Exception ("Not authenticated.");
		
			BoxingPacker boxingPacker = new BoxingPacker ();
			CompiledPacker compiledPacker = new CompiledPacker (false);
			ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => {return true;}; //dis be bad, no ssl check
			
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create (_host);
			request.ContentType = "binary/message-pack";
			request.Method = "POST";
			
			Stream requestStream = request.GetRequestStream ();
			MsgPackWriter msgpackWriter = new MsgPackWriter (requestStream);
			
			msgpackWriter.WriteArrayHeader (args.Length + 1 + (string.IsNullOrEmpty (_token) ? 0 : 1));
			
			msgpackWriter.Write (method);
			
			if (!string.IsNullOrEmpty (_token))
				msgpackWriter.Write (_token);
			
			foreach (object arg in args) {
				//any applicable types should be here...
				if (arg is string)
					msgpackWriter.Write (arg as string);
				else if (arg is int)
					msgpackWriter.Write ((int)arg);
				else if (arg is Dictionary<object, object>) {
					msgpackWriter.WriteMapHeader ((arg as Dictionary<object, object>).Count);
					foreach (var e in arg as IDictionary<object, object>) {
						Pack (msgpackWriter, e.Key);
						Pack (msgpackWriter, e.Value);
					}
				} else if (arg == null)
					msgpackWriter.WriteNil ();
				else
					throw new Exception ("I need a definition for type " + arg.GetType ().ToString ());
			}
			
			requestStream.Close ();
			
			Stream responseStream = request.GetResponse ().GetResponseStream ();
			
			//everything is a bunch of bytes, needs to be typed
			Dictionary<object, object > resp = boxingPacker.Unpack (responseStream) as Dictionary<object, object>;
			
			//This is me trying to type the response for the user....
			Dictionary<object, object > returnDictionary = TypifyDictionary(resp);

			return returnDictionary;
		}
		
		Dictionary<object, object> TypifyDictionary(Dictionary<object, object> dict)
		{
			Dictionary<object, object> returnDictionary = new Dictionary<object, object>();
			System.Text.Encoding enc = System.Text.Encoding.ASCII;
			foreach (var pair in dict)
			{
				if (pair.Value != null) {
				
					if (pair.Value.GetType () == typeof(bool))
						returnDictionary.Add (enc.GetString (pair.Key as byte[]), ((bool)pair.Value).ToString ());
					else if (pair.Value.GetType () == typeof(byte[]))
						returnDictionary.Add (enc.GetString (pair.Key as byte[]), enc.GetString (pair.Value as byte[]));
					else if (pair.Value.GetType () == typeof(object[]))
						returnDictionary.Add (enc.GetString (pair.Key as byte[]), pair.Value);
					else if (pair.Value.GetType () == typeof(UInt32))
						returnDictionary.Add (enc.GetString (pair.Key as byte[]), ((UInt32)pair.Value).ToString ());
					else if (pair.Value.GetType () == typeof(Int32))
						returnDictionary.Add (enc.GetString (pair.Key as byte[]), ((Int32)pair.Value).ToString ());
					else if (pair.Value.GetType () == typeof(Dictionary<object, object>))
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
 	 	
			Type t = o.GetType ();
 	 	
			if (t.IsPrimitive) {
				if (t.Equals (typeof(int)))
					writer.Write ((int)o);
				else if (t.Equals (typeof(uint)))
					writer.Write ((uint)o);
				else if (t.Equals (typeof(float)))
					writer.Write ((float)o);
				else if (t.Equals (typeof(double)))
					writer.Write ((double)o);
				else if (t.Equals (typeof(long)))
					writer.Write ((long)o);
				else if (t.Equals (typeof(ulong)))
					writer.Write ((ulong)o);
				else if (t.Equals (typeof(bool)))
					writer.Write ((bool)o);
				else if (t.Equals (typeof(byte)))
					writer.Write ((byte)o);
				else if (t.Equals (typeof(sbyte)))
					writer.Write ((sbyte)o);
				else if (t.Equals (typeof(short)))
					writer.Write ((short)o);
				else if (t.Equals (typeof(ushort)))
					writer.Write ((ushort)o);
				else
					throw new NotSupportedException ();  // char?
 	 	
				return;
			}
			else
			{
				if (t == typeof(string))
					writer.Write(o as string);
			}
		}
		
		public void Dispose ()
		{
			this.Execute ("auth.logout", new object[] {});
		}
	}
}

