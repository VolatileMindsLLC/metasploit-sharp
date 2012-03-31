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
					System.Text.Encoding enc = System.Text.Encoding.UTF8;
					Dictionary<object, object> modules = manager.GetCoreModuleStats();
					
					Console.WriteLine("Module stats:");
					foreach (KeyValuePair<object, object> pair in modules)
						Console.WriteLine(pair.Key + ": " + pair.Value );
					
					Dictionary<object, object> version = manager.GetCoreVersionInformation();
					
					Console.WriteLine("\n\nVersion information:");
					foreach (KeyValuePair<object, object> pair in version)
						Console.WriteLine(pair.Key + ": " + pair.Value);
					
					Console.WriteLine("\n\nCreating console....");
					Dictionary<object, object> consoleResponse = manager.CreateConsole();
					foreach (KeyValuePair<object, object> pair in consoleResponse)
						Console.WriteLine(pair.Key + ": " + pair.Value);
					
					Console.WriteLine("\n\nConsole created, getting list of consoles...");
					Dictionary<object, object> consoleList = manager.ListConsoles();
					foreach (KeyValuePair<object, object> pair in consoleList)
					{
						Console.WriteLine("\n" + pair.Key + ":");
						
						foreach (object obj in (pair.Value as object[]))
						{
							//each obj is a Dictionary<object, object>
							foreach (KeyValuePair<object, object> p in obj as Dictionary<object, object>)
							{
								string pkType = p.Key.GetType().ToString();
								string pvType = p.Value.GetType().ToString();
								
								if (p.Value.GetType() == typeof(byte[]))
									Console.WriteLine(enc.GetString(p.Key as byte[]) + ": " + enc.GetString(p.Value as byte[]));
								else if (p.Value.GetType() == typeof(bool))
									Console.WriteLine(enc.GetString(p.Key as byte[]) + ": " + ((bool)p.Value).ToString());
								else
									throw new Exception(pkType + ": " + pvType);
							}
						}
					}
					
				}
			}
		}
	}
}
