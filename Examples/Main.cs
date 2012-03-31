using System;
using metasploitsharp;
using System.Collections.Generic;

namespace Examples
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			using (MetasploitSession session = new MetasploitSession("metasploit", "P[.=~v5Y", "https://192.168.1.141:3790/api/1.1"))
			{
				if (string.IsNullOrEmpty(session.Token))
					throw new Exception("Login failed. Check credentials");
				
				using (MetasploitManager manager = new MetasploitManager(session))
				{
					Dictionary<string, string> modules = manager.GetCoreModuleStats();
					
					Console.WriteLine("Module stats:");
					foreach (KeyValuePair<string, string> pair in modules)
						Console.WriteLine(pair.Key + ": " + pair.Value);
					
					Dictionary<string, string> version = manager.GetCoreVersionInformation();
					
					Console.WriteLine("\n\nVersion information:");
					foreach (KeyValuePair<string, string> pair in version)
						Console.WriteLine(pair.Key + ": " + pair.Value);
					
					Console.WriteLine("Creating console....");
					Dictionary<string, string> consoleResponse = manager.CreateConsole();
					foreach (KeyValuePair<string, string> pair in consoleResponse)
						Console.WriteLine(pair.Key + ": " + pair.Value);
					
					Console.WriteLine("Console created, getting list of consoles...");
					Dictionary<string, string> consoleList = manager.ListConsoles();
					foreach (KeyValuePair<string, string> pair in consoleList)
						Console.WriteLine(pair.Key + ": " + pair.Value);
					
				}
			}
		}
	}
}
