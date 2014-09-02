using System;
using metasploitsharp;
using System.Collections.Generic;

namespace metasploitsharp
{
	public class MetasploitProManager : MetasploitManager
	{
		MetasploitSession _session;
		
		public MetasploitProManager (MetasploitSession session) : base(session)
		{
			_session = session;
		}
		
		public Dictionary<string, object> AboutPro()
		{
			return _session.Execute("pro.about");
		}
		
		public Dictionary<string, object> ListProWorkspaces()
		{
			return _session.Execute("pro.workspaces");
		}
		
		public Dictionary<string, object> ListProProjects()
		{
			return _session.Execute("pro.projects");
		}
		
		public Dictionary<string, object> AddProWorkspace(Dictionary<string, object> options)
		{
			return _session.Execute("pro.workspace_add", options);
		}
		
		public Dictionary<string, object> AddProProject(Dictionary<string, object> options)
		{
			return _session.Execute("pro.project_add", options);
		}
		
		public Dictionary<string, object> DeleteProWorkspace(string workspaceName)
		{
			return _session.Execute("pro.workspace_del", workspaceName);
		}
		
		public Dictionary<string, object> DeleteProProject(string workspaceName)
		{
			return _session.Execute("pro.project_del", workspaceName);
		}
		
		public Dictionary<string, object> GetProUsers()
		{
			return _session.Execute("pro.users");
		}
		
		public Dictionary<string, object> RegisterPro(string productKey)
		{
			return _session.Execute("pro.register", productKey);
		}
		
		public Dictionary<string, object> ActivatePro(Dictionary<string, object> options)
		{
			return _session.Execute("pro.activate", options);
		}
		
		public Dictionary<string, object> ActivateProOffline(string activationPath)
		{
			return _session.Execute("pro.activate_offline", activationPath );
		}
		
		public Dictionary<string, object> GetProLicense()
		{
			return _session.Execute("pro.license");
		}
		
		public Dictionary<string, object> RevertProLicense()
		{
			return _session.Execute("pro.revert_license");
		}
		
		public Dictionary<string, object> ProUpdatesAvailable(Dictionary<string, object> options)
		{
			return _session.Execute("pro.update_available", options);
		}
		
		public Dictionary<string, object> ProInstallUpdates(Dictionary<string, object> options)
		{
			return _session.Execute("pro.update_install", options);
		}
		
		public Dictionary<string, object> ProInstallOfflineUpdates(string updatePath)
		{
			return _session.Execute("pro.update_install_offline", updatePath);
		}
		
		public Dictionary<string, object> ProUpdateStatus()
		{
			return _session.Execute("pro.update_status");
		}
		
		public Dictionary<string, object> ProStopUpdates()
		{
			return _session.Execute("pro.update_stop");
		}
		
		public Dictionary<string, object> RestartPro()
		{
			return _session.Execute("pro.restart_service");
		}
		
		public Dictionary<string, object> GetProTasks()
		{
			return _session.Execute("pro.task_list");
		}
		
		public Dictionary<string, object> GetProTaskStatus(string taskID)
		{
			return _session.Execute("pro.task_status", taskID);
		}
		
		public Dictionary<string, object> StopProTask(string taskID)
		{
			return _session.Execute("pro.task_stop", taskID);
		}
		
		public Dictionary<string, object> GetProTaskLog(string taskID)
		{
			return _session.Execute("pro.task_log", taskID);
		}
		
		public Dictionary<string, object> DeleteProTaskLog(string taskID)
		{
			return _session.Execute("pro.task_delete_log");
		}
		
		public Dictionary<string, object> StartDiscover(Dictionary<string, object> options)
		{
			return _session.Execute("pro.start_discover", options);
		}
		
		public Dictionary<string, object> StartImport(Dictionary<string, object> options)
		{
			return _session.Execute("pro.start_import", options);
		}
		
		public Dictionary<string, object> StartCredentialImport(Dictionary<string, object> options)
		{
			return _session.Execute("pro.start_import_creds", options);
		}
		
		public Dictionary<string, object> StartNexposeAssessment(Dictionary<string, object> options)
		{
			return _session.Execute("pro.start_nexpose", options);
		}
		
		public Dictionary<string, object> StartBruteforce(Dictionary<string, object> options)
		{
			return _session.Execute("pro.start_bruteforce", options);
		}
		
		public Dictionary<string, object> StartExploit(Dictionary<string, object> options)
		{
			return _session.Execute("pro.start_exploit", options);
		}
		
		public Dictionary<string, object> StartWebscan(Dictionary<string, object> options)
		{
			return _session.Execute("pro.start_webscan", options);
		}
		
		public Dictionary<string, object> StartWebAudit(Dictionary<string, object> options)
		{
			return _session.Execute("pro.start_webaudit", options);
		}
		
		public Dictionary<string, object> StartWebSploit(Dictionary<string, object> options)
		{
			return _session.Execute("pro.start_websploit", options);
		}
		
		public Dictionary<string, object> StartCleanup(Dictionary<string, object> options)
		{
			return _session.Execute("pro.start_cleanup", options);
		}
		
		public Dictionary<string, object> StartLootCollection(Dictionary<string, object> options)
		{
			return _session.Execute("pro.start_collect", options);
		}

		public Dictionary<string, object> StartSingle (Dictionary<string, object> options)
		{
			return _session.Execute("pro.start_single", options);
		}

		public Dictionary<string, object> StartReport(Dictionary<string, object> options)
		{
			return _session.Execute("pro.start_report", options);
		}
		
		public Dictionary<string, object> ImportData(string workspace, string data, Dictionary<string, object> options)
		{
			return _session.Execute("pro.import_data", workspace, data, options);
		}
		
		public Dictionary<string, object> ImportFile(string workspace, string path, Dictionary<string, object> options)
		{
			return _session.Execute("pro.import_file", workspace, path, options);
		}
		
		public Dictionary<string, object> ValidateImportFile(string filepath)
		{
			return _session.Execute("pro.validate_import_file", filepath);
		}
		
		public Dictionary<string, object> DownloadLoot(int lootID)
		{
			return _session.Execute("pro.loot_download", lootID);
		}
		
		public Dictionary<string, object> ListLoot(string workspace)
		{
			return _session.Execute("pro.loot_list", workspace);
		}
		
		public Dictionary<string, object> SearchProModules(string query)
		{
			return _session.Execute("pro.module_search", query);
		}
		
		public Dictionary<string, object> ValidateProModule(string moduleName, Dictionary<string, object> options)
		{
			return _session.Execute("pro.module_validate", moduleName, options);
		}
		
		public Dictionary<string, object> ListProModules()
		{
			return _session.Execute("pro.report_list");
		}
		
		public Dictionary<string, object> DownloadReport(string reportID)
		{
			return _session.Execute("pro.report_download");
		}
		
		public Dictionary<string, object> DownloadReportByTask(string taskID)
		{
			return _session.Execute("pro.report_download_by_task", taskID);
		}
		
		public Dictionary<string, object> GetReportList(string workspace)
		{
			return _session.Execute("pro.report_list", workspace);
		}
		
		public Dictionary<string, object> MeterpreterChDir(string sessionID, string path)
		{
			return _session.Execute("pro.meterpreter_chdir", sessionID, path);
		}
		
		public Dictionary<string, object> MeterpreterGetCWD(string sessionID)
		{
			return _session.Execute("pro.meterpreter_getcwd", sessionID);
		}
		
		public Dictionary<string, object> MeterpreterListDirectory(string sessionID, string path)
		{
			return _session.Execute("pro.meterpreter_list", sessionID, path);
		}
		
		public Dictionary<string, object> MeterpreterRemoveFileOrDirectory(string sessionID, string path)
		{
			return _session.Execute("pro.meterpreter_rm", sessionID, path);
		}
		
		public Dictionary<string, object> MeterpreterRootPaths(string sessionID)
		{
			return _session.Execute("pro.meterpreter_root_paths", sessionID);
		}
		
		public Dictionary<string, object> MeterpreterSearch(string sessionID, string query)
		{
			return _session.Execute("pro.meterpreter_search", sessionID, query);
		}
		
		public Dictionary<string, object> MeterpreterTunnelInterfaces(string sessionID)
		{
			return _session.Execute("pro.meterpreter_tunnel_interfaces", sessionID);
		}
	}
}

