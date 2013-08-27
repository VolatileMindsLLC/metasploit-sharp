using System;
using metasploitsharp;
using System.Collections.Generic;

namespace CreateWorkspace
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			using (MetasploitSession session = new MetasploitSession("metasploit", "P@ssw0rd!", "https://192.168.1.5:3790/api/1.1"))
			{
				using (MetasploitProManager manager = new MetasploitProManager(session))
				{
					Dictionary<string, object> modules = manager.GetCoreModuleStats();
					
					Console.WriteLine("Module stats:");
					foreach (KeyValuePair<string, object> pair in modules)
						Console.WriteLine(pair.Key + ": " + pair.Value);
					
					Dictionary<string, object> version = manager.GetCoreVersionInformation();
					
					Console.WriteLine("\n\nVersion information:");
					foreach (KeyValuePair<string, object> pair in version)
						Console.WriteLine(pair.Key + ": " + pair.Value);
					
					Console.WriteLine("\n\nCreating console...");
					Dictionary<string, object> consoleResponse = manager.CreateConsole();
					foreach (KeyValuePair<string, object> pair in consoleResponse)
						Console.WriteLine(pair.Key + ": " + pair.Value);
					
					string consoleID = consoleResponse["id"] as string;
					
					Console.WriteLine("\n\nConsole created, getting list of consoles...");
					Dictionary<string, object> consoleList = manager.ListConsoles();
					foreach (KeyValuePair<string, object> pair in consoleList)
					{
						Console.WriteLine(pair.Value.GetType().Name);
						
						foreach (var obj in pair.Value as IList<object>) 
						{
							//each obj is a Dictionary<string, object> in this response
							if (obj is IDictionary<string, object>)
							{
								foreach (var p in obj as IDictionary<string, object>)
								{
									Console.WriteLine(p.Key + ": " + p.Value);
								}
							}
							else
								Console.WriteLine(obj);
						}
					}
					
					Console.WriteLine("\n\nDestroying our console: " + consoleID);
					Dictionary<string, object> destroyResponse = manager.DestroyConsole(consoleID);
					foreach (KeyValuePair<string, object> pair in destroyResponse)
						Console.WriteLine(pair.Key + ": " + pair.Value);
					
					if (destroyResponse.ContainsKey("result") && ((string)destroyResponse["result"]) == "success")
						Console.WriteLine("Destroyed.");
					else
						Console.WriteLine("Failed!");
					
					Dictionary<string, object> proVersion = manager.AboutPro();
					
					Console.WriteLine("\n\nInformation about pro:");
					foreach (KeyValuePair<string, object> pair in proVersion)
						Console.WriteLine(pair.Key + ": " + pair.Value);
					
					Dictionary<string, object> updateStatus = manager.ProUpdateStatus();
					Console.WriteLine("\n\nUpdate status:");
					
					foreach(KeyValuePair<string, object> pair in updateStatus)
						Console.WriteLine(pair.Key + ": " + pair.Value);
					
				}
			}
		}
	}
}
