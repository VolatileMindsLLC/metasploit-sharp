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
			
			Dictionary<string, string> response = this.Authenticate(username, password);
			
			bool loggedIn = !response.ContainsKey("error");
			
			if (!loggedIn)
				throw new Exception(response["error_message"]);
			
			if (response["result"] == "success")
				_token = response["token"];
		}
		
		public string Token { 
			get { return _token; }
		}
		
		
		public Dictionary<string, string> Authenticate(string username, string password)
		{
			return this.Execute("auth.login", new object[] { username, password });
		}
		
		//Yay, fun method!
		public Dictionary<string, string> Execute(string method, object[] args)
		{
			if (string.IsNullOrEmpty(_host))
				throw new Exception("Host null or empty");
			
			if (method != "auth.login" && string.IsNullOrEmpty(_token))
				throw new Exception("Not authenticated.");
		
			BoxingPacker boxingPacker = new BoxingPacker();
			CompiledPacker compiledPacker = new CompiledPacker(true);
			ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => {return true;}; //dis be bad, no ssl check
			
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_host);
			request.ContentType = "binary/message-pack";
			request.Method = "POST";
			
			Stream requestStream = request.GetRequestStream();
			MsgPackWriter msgpackWriter = new MsgPackWriter(requestStream);
			
			msgpackWriter.WriteArrayHeader(args.Length + 1 + (string.IsNullOrEmpty(_token) ? 0 : 1));
			
			msgpackWriter.Write(method);
			
			if (!string.IsNullOrEmpty(_token))
				msgpackWriter.Write(_token);
			
			foreach (object arg in args)
			{
				if (arg is string)
					msgpackWriter.Write(arg as string);
				else if (arg is Dictionary<object, object>)
				{
					msgpackWriter.Write(compiledPacker.Pack<Dictionary<object, object>>(arg as Dictionary<object, object>));
					
//					msgpackWriter.WriteMapHeader((arg as Dictionary<object, object>).Count);
//					
//					object[] pairs = new object[(arg as Dictionary<object, object>).Count];
//					
//					int i = 0;
//					foreach (object pair in (arg as Dictionary<object, object>))
//						pairs[i++] = pair;
//					
					//packer.
				}
			}
			
			requestStream.Close();
			
			Stream responseStream = request.GetResponse().GetResponseStream();
			Dictionary<object, object> resp = boxingPacker.Unpack(responseStream) as Dictionary<object, object>;
			Dictionary<string, string> returnDictionary = new Dictionary<string, string>();
			
			System.Text.Encoding enc = System.Text.Encoding.UTF8;
			foreach (KeyValuePair<object, object> pair in resp)
			{
				string keyType = pair.Key.GetType().ToString();
				string valueType = pair.Value.GetType().ToString();
				
				if (pair.Value.GetType() == typeof(bool))
					returnDictionary.Add(enc.GetString((byte[])pair.Key), ((bool)pair.Value).ToString());
				else if (pair.Value.GetType() == typeof(byte[]))
					returnDictionary.Add(enc.GetString(pair.Key as byte[]), enc.GetString(pair.Value as byte[]));
				else if (pair.Value.GetType() == typeof(object[]))
				{
					string valyou = string.Empty;
					foreach (object obj in (pair.Value as object[]))
					{
						valyou = string.Empty;
						if (obj is Dictionary<object, object>)
						{
							foreach (KeyValuePair<object, object> p in (obj as Dictionary<object, object>))
							{
								string objKeyType = p.Key.GetType().ToString();
								string objValueType = p.Value.GetType().ToString();
								
								if (p.Value.GetType() == typeof(byte[]))
									valyou = valyou + enc.GetString(p.Key as byte[]) + ": " + enc.GetString(p.Value as byte[]) + "\n";
								else if (p.Value.GetType() == typeof(bool))
									valyou = valyou + enc.GetString(p.Key as byte[]) + ": " + ((bool)p.Value).ToString();
							}
						}
						else
							valyou = valyou + (enc.GetString(obj as byte[]));
					}
					returnDictionary.Add(enc.GetString(pair.Key as byte[]), valyou);
				}
				else if (pair.Value.GetType() == typeof(UInt32))
					returnDictionary.Add(enc.GetString(pair.Key as byte[]), ((UInt32)pair.Value).ToString());
				else if (pair.Value.GetType() == typeof(Int32))
					returnDictionary.Add(enc.GetString(pair.Key as byte[]), ((Int32)pair.Value).ToString());
				else
					throw new Exception("key type: " + keyType + ", value type: " + valueType);
			}	
			
			return returnDictionary;
		}
		
		public void Dispose()
		{
			this.Execute("auth.logout", new object[] {});
		}
	}
}

