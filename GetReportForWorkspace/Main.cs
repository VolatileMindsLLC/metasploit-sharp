using System;
using metasploitsharp;
using System.Collections.Generic;

namespace GetReportForWorkspace
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			using (MetasploitSession session = new MetasploitSession("metasploit", "-LG.\"9U5", "https://192.168.1.148:3790/api/1.1"))
			{
				using (MetasploitProManager manager = new MetasploitProManager(session))
				{
					Dictionary<object, object> options = new Dictionary<object, object>();
					options.Add("DS_WHITELIST_HOSTS", string.Empty);
					options.Add("DS_BLACKLIST_HOSTS", string.Empty);
					options.Add("workspace", "default");
					options.Add("DS_MaskPasswords", false);
					options.Add("DS_IncludeTaskLog", false);
					options.Add("DS_JasperDisplaySession", true);
					options.Add("DS_JasperDisplayCharts", true);
					options.Add("DS_LootExcludeScreenshots", false);
					options.Add("DS_LootExcludePasswords", false);
					options.Add("DS_JasperTemplate", "msfxv3.jrxml");
					options.Add("DS_REPORT_TYPE", "PDF");
					options.Add("DS_UseJasper", true);
					options.Add("DS_UseCustomReporting", true);
					options.Add("DS_JasperProductName", "AutoAssess");
					options.Add("DS_JasperDbEnv", "production");
					options.Add("DS_JasperLogo", string.Empty);
					options.Add("DS_JasperDisplaySections", "1,2,3,4,5,6,7,8");
					options.Add("DS_EnabelPCIReport", true);
					options.Add("DS_EnableFISMAReport", true);
					options.Add("DS_JasperDIsplayWeb", true);
					
					Dictionary<object, object> response = manager.StartReport(options);
					
					foreach (var pair in response)
						Console.WriteLine(pair.Key + ": " + pair.Value);
					
					string taskID = response["task_id"] as string;
					
					response = manager.GetProTaskStatus(taskID);
					
					bool done = false;
					while (!done)
					{
						System.Text.Encoding enc = System.Text.Encoding.ASCII;
						string status = string.Empty;
						foreach (var pair in response)
						{
							Console.WriteLine(enc.GetString(pair.Key as byte[]) + ":");
							foreach (var p in pair.Value as Dictionary<object, object>)
								Console.WriteLine(p.Key + ": " + p.Value);
							
							status = (pair.Value as Dictionary<object, object>)["status"] as string;
						}
													
						if (status != "running")
						{
							done = true;
							Console.WriteLine("Done!");
						}
						else
						{
							response = manager.GetProTaskStatus(taskID);
							Console.WriteLine("Not done yet...");
						}
					}
				}
			}
		}
	}
}
