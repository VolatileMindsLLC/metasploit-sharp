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
		
		public Dictionary<object, object> AboutPro()
		{
			return _session.Execute("pro.about");
		}
		
		public Dictionary<object, object> ListProWorkspaces()
		{
			return _session.Execute("pro.workspaces");
		}
		
		public Dictionary<object, object> ListProProjects()
		{
			return _session.Execute("pro.projects");
		}
		
		public Dictionary<object, object> AddProWorkspace(Dictionary<object, object> options)
		{
			return _session.Execute("pro.workspace_add", options);
		}
		
		public Dictionary<object, object> AddProProject(Dictionary<object, object> options)
		{
			return _session.Execute("pro.project_add", options);
		}
		
		public Dictionary<object, object> DeleteProWorkspace(string workspaceName)
		{
			return _session.Execute("pro.workspace_del", workspaceName);
		}
		
		public Dictionary<object, object> DeleteProProject(string workspaceName)
		{
			return _session.Execute("pro.project_del", workspaceName);
		}
		
		public Dictionary<object, object> GetProUsers()
		{
			return _session.Execute("pro.users");
		}
		
		public Dictionary<object, object> RegisterPro(string productKey)
		{
			return _session.Execute("pro.register", productKey);
		}
		
		public Dictionary<object, object> ActivatePro(Dictionary<object, object> options)
		{
			return _session.Execute("pro.activate", options);
		}
		
		public Dictionary<object, object> ActivateProOffline(string activationPath)
		{
			return _session.Execute("pro.activate_offline", activationPath );
		}
		
		public Dictionary<object, object> GetProLicense()
		{
			return _session.Execute("pro.license");
		}
		
		public Dictionary<object, object> RevertProLicense()
		{
			return _session.Execute("pro.revert_license");
		}
		
		public Dictionary<object, object> ProUpdatesAvailable(Dictionary<object, object> options)
		{
			return _session.Execute("pro.update_available", options);
		}
		
		public Dictionary<object, object> ProInstallUpdates(Dictionary<object, object> options)
		{
			return _session.Execute("pro.update_install", options);
		}
		
		public Dictionary<object, object> ProInstallOfflineUpdates(string updatePath)
		{
			return _session.Execute("pro.update_instalL_offline", updatePath);
		}
		
		public Dictionary<object, object> ProUpdateStatus()
		{
			return _session.Execute("pro.update_status");
		}
		
		public Dictionary<object, object> ProStopUpdates()
		{
			return _session.Execute("pro.update_stop");
		}
		
		public Dictionary<object, object> RestartPro()
		{
			return _session.Execute("pro.restart_service");
		}
		
		public Dictionary<object, object> GetProTasks()
		{
			return _session.Execute("pro.task_list");
		}
		
		public Dictionary<object, object> GetProTaskStatus(string taskID)
		{
			return _session.Execute("pro.task_status", taskID);
		}
		
		public Dictionary<object, object> StopProTask(string taskID)
		{
			return _session.Execute("pro.task_stop", taskID);
		}
		
		public Dictionary<object, object> GetProTaskLog(string taskID)
		{
			return _session.Execute("pro.task_log", taskID);
		}
		
		public Dictionary<object, object> DeleteProTaskLog(string taskID)
		{
			return _session.Execute("pro.task_delete_log");
		}
		
		public Dictionary<object, object> StartDiscover(Dictionary<object, object> options)
		{
			return _session.Execute("pro.start_discover", options);
		}
		
		public Dictionary<object, object> StartImport(Dictionary<object, object> options)
		{
			return _session.Execute("pro.start_import", options);
		}
		
		public Dictionary<object, object> StartCredentialImport(Dictionary<object, object> options)
		{
			return _session.Execute("pro.start_import_creds", options);
		}
		
		public Dictionary<object, object> StartNexposeAssessment(Dictionary<object, object> options)
		{
			return _session.Execute("pro.start_nexpose", options);
		}
		
		public Dictionary<object, object> StartBruteforce(Dictionary<object, object> options)
		{
			return _session.Execute("pro.start_bruteforce", options);
		}
		
		public Dictionary<object, object> StartExploit(Dictionary<object, object> options)
		{
			return _session.Execute("pro.start_exploit", options);
		}
		
		public Dictionary<object, object> StartWebscan(Dictionary<object, object> options)
		{
			return _session.Execute("pro.start_webscan", options);
		}
		
		public Dictionary<object, object> StartWebAudit(Dictionary<object, object> options)
		{
			return _session.Execute("pro.start_webaudit", options);
		}
		
		public Dictionary<object, object> StartWebSploit(Dictionary<object, object> options)
		{
			return _session.Execute("pro.start_websploit", options);
		}
		
		public Dictionary<object, object> StartCleanup(Dictionary<object, object> options)
		{
			return _session.Execute("pro.start_cleanup", options);
		}
		
		public Dictionary<object, object> StartLootCollection(Dictionary<object, object> options)
		{
			return _session.Execute("pro.start_collect", options);
		}
		
		public Dictionary<object, object> StartReport(Dictionary<object, object> options)
		{
			return _session.Execute("pro.start_report", options);
		}
		
		public Dictionary<object, object> ImportData(string workspace, string data, Dictionary<object, object> options)
		{
			return _session.Execute("pro.import_data", workspace, data, options);
		}
		
		public Dictionary<object, object> ImportFile(string workspace, string path, Dictionary<object, object> options)
		{
			return _session.Execute("pro.import_file", workspace, path, options);
		}
		
		public Dictionary<object, object> ValidateImportFile(string filepath)
		{
			return _session.Execute("pro.validate_import_file", filepath);
		}
		
		public Dictionary<object, object> DownloadLoot(int lootID)
		{
			return _session.Execute("pro.loot_download", lootID);
		}
		
		public Dictionary<object, object> ListLoot(string workspace)
		{
			return _session.Execute("pro.loot_list", workspace);
		}
		
		public Dictionary<object, object> SearchProModules(string query)
		{
			return _session.Execute("pro.module_search", query);
		}
		
		public Dictionary<object, object> ValidateProModule(string moduleName, Dictionary<object, object> options)
		{
			return _session.Execute("pro.module_validate", moduleName, options);
		}
		
		public Dictionary<object, object> ListProModules()
		{
			return _session.Execute("pro.report_list");
		}
		
		public Dictionary<object, object> DownloadReport(string reportID)
		{
			return _session.Execute("pro.report_download");
		}
		
		public Dictionary<object, object> DownloadReportByTask(string taskID)
		{
			return _session.Execute("pro.report_download_by_task", taskID);
		}
		
		public Dictionary<object, object> GetReportList(string workspace)
		{
			return _session.Execute("pro.report_list", workspace);
		}
		
		public Dictionary<object, object> MeterpreterChDir(string sessionID, string path)
		{
			return _session.Execute("pro.meterpreter_chdir", sessionID, path);
		}
		
		public Dictionary<object, object> MeterpreterGetCWD(string sessionID)
		{
			return _session.Execute("pro.meterpreter_getcwd", sessionID);
		}
		
		public Dictionary<object, object> MeterpreterListDirectory(string sessionID, string path)
		{
			return _session.Execute("pro.meterpreter_list", sessionID, path);
		}
		
		public Dictionary<object, object> MeterpreterRemoveFileOrDirectory(string sessionID, string path)
		{
			return _session.Execute("pro.meterpreter_rm", sessionID, path);
		}
		
		public Dictionary<object, object> MeterpreterRootPaths(string sessionID)
		{
			return _session.Execute("pro.meterpreter_root_paths", sessionID);
		}
		
		public Dictionary<object, object> MeterpreterSearch(string sessionID, string query)
		{
			return _session.Execute("pro.meterpreter_search", sessionID, query);
		}
		
		public Dictionary<object, object> MeterpreterTunnelInterfaces(string sessionID)
		{
			return _session.Execute("pro.meterpreter_tunnel_interfaces", sessionID);
		}
	}
}

