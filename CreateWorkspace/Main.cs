using System;
using metasploitsharp;
using System.Collections.Generic;

namespace CreateWorkspace
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			using (MetasploitSession session = new MetasploitSession("metasploit", "2c8X|a2!", "https://192.168.1.148:3790/api/1.1"))
			{
				using (MetasploitProManager manager = new MetasploitProManager(session))
				{
					string workspace = Guid.NewGuid().ToString();
					
					Dictionary<object, object> options = new Dictionary<object, object>();
					options.Add("name", workspace);
					
					Dictionary<object, object> response = manager.AddProProject(options);
					
					foreach (var pair in response)
						Console.WriteLine(pair.Key + ": " + pair.Value);
					
					response = manager.DeleteProWorkspace(workspace);
					
					foreach (var pair in response)
						Console.WriteLine(pair.Key + ": " + pair.Value);
					
					response = manager.CreateConsole();
					
					workspace = Guid.NewGuid().ToString();
					string consoleID = response["id"] as string;
					
					response = manager.WriteToConsole(consoleID, "workspace -a " + workspace + "\n");
					response = manager.WriteToConsole(consoleID, "workspace\n");
					response = manager.WriteToConsole(consoleID, "workspace -d " + workspace + "\n");
					response = manager.ReadConsole(consoleID);
					
					foreach (var pair in response)
						Console.WriteLine(pair.Key + ": " + pair.Value);
					
					manager.DestroyConsole(consoleID);
				}
			}
		}
	}
}
