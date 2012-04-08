using System;
using metasploitsharp;
using System.Collections.Generic;

namespace StartImport
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			using (MetasploitSession session = new MetasploitSession("metasploit", "2c8X|a2!", "https://192.168.1.148:3790/api/1.1"))
			{
				using (MetasploitProManager manager = new MetasploitProManager(session))
				{
					Dictionary<object, object> options = new Dictionary<object, object>();
					options.Add("workspace", "default");
					options.Add("DS_PATH", "/tmp/efc63839-ae8d-4caf-92f5-3f3ff7b6e306");
					
					Dictionary<object, object> response = manager.StartImport(options);
					
					foreach (var pair in response)
						Console.WriteLine(pair.Key + ": " + pair.Value);
					
					response = manager.GetProTaskStatus(response["task_id"] as string);
					
					foreach (var pair in response)
					{
						string stat = (pair.Value as Dictionary<object, object>)[(object)"status"] as string;
						
						while (stat == "running")
						{
							System.Threading.Thread.Sleep(500);
							
							response = manager.GetProTaskStatus(response["task_id"] as string);
							
							foreach (var p in response)
								stat = (p.Value as Dictionary<object, object>)["status"] as string;
						}
					}
				}
			}
		}
	}
}
