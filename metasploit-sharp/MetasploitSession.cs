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
			System.Text.Encoding enc = System.Text.Encoding.UTF8;
			_host = host;
			_token = null;
			
			Dictionary<object, object> response = this.Authenticate(username, password);
			
			bool loggedIn = !response.ContainsKey("error");
			
			if (!loggedIn)
				throw new Exception(response["error_message"] as string);
			
			if (((string)response[((object)"result")]) == "success")
				_token = response["token"] as string;
		}
		
		public string Token { 
			get { return _token; }
		}
		
		
		public Dictionary<object, object> Authenticate(string username, string password)
		{
			return this.Execute("auth.login", new object[] { username, password });
		}
		
		//Yay, fun method!
		public Dictionary<object, object> Execute(string method, object[] args)
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
					msgpackWriter.Write(compiledPacker.Pack<Dictionary<object, object>>(arg as Dictionary<object, object>));
			}
			
			requestStream.Close();
			
			Stream responseStream = request.GetResponse().GetResponseStream();
			
			//everything is a bunch of bytes, needs to be typed
			Dictionary<object, object> resp = boxingPacker.Unpack(responseStream) as Dictionary<object, object>;
			
			//This is me trying to type the response for the user....
			Dictionary<object, object> returnDictionary = new Dictionary<object, object>();
			
			System.Text.Encoding enc = System.Text.Encoding.UTF8;
			foreach (KeyValuePair<object, object> pair in resp)
			{
				string keyType = pair.Key.GetType().ToString();
				string valueType = pair.Value.GetType().ToString();
				
				if (pair.Value.GetType() == typeof(bool))
					returnDictionary.Add(enc.GetString(pair.Key as byte[]), ((bool)pair.Value).ToString());
				else if (pair.Value.GetType() == typeof(byte[]))
					returnDictionary.Add(enc.GetString(pair.Key as byte[]), enc.GetString(pair.Value as byte[]));
				else if (pair.Value.GetType() == typeof(object[]))
					returnDictionary.Add(enc.GetString(pair.Key as byte[]), pair.Value);
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

