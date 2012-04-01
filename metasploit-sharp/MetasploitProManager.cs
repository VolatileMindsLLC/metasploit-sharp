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
			return _session.Execute("pro.about", new object[] {});
		}
		
		public Dictionary<object, object> ListProWorkspaces()
		{
			return _session.Execute("pro.workspaces", new object[] {});
		}
		
		public Dictionary<object, object> ListProProjects()
		{
			return _session.Execute("pro.projects", new object[] {});
		}
		
		public Dictionary<object, object> AddProWorkspace(Dictionary<object, object> options)
		{
			return _session.Execute("pro.workspace_add", new object[]{options});
		}
		
		public Dictionary<object, object> AddProProject(Dictionary<object, object> options)
		{
			return _session.Execute("pro.project_add", new object[] {options});
		}
		
		public Dictionary<object, object> DeleteProWorkspace(string workspaceName)
		{
			return _session.Execute("pro.workspace_del", new object[]{ workspaceName});
		}
		
		public Dictionary<object, object> DeleteProProject(string workspaceName)
		{
			return _session.Execute("pro.project_del", new object[] { workspaceName } );
		}
		
		public Dictionary<object, object> GetProUsers()
		{
			return _session.Execute("pro.users", new object[]{});
		}
		
		public Dictionary<object, object> RegisterPro(string productKey)
		{
			return _session.Execute("pro.register", new object[] { productKey } );
		}
		
		public Dictionary<object, object> ActivatePro(Dictionary<object, object> options)
		{
			return _session.Execute("pro.activate", new object[] { options } );
		}
		
		public Dictionary<object, object> ActivateProOffline(string activationPath)
		{
			return _session.Execute("pro.activate_online", new object[] { activationPath });
		}
		
		public Dictionary<object, object> GetProLicense()
		{
			return _session.Execute("pro.license", new object[] {});
		}
		
		public Dictionary<object, object> RevertProLicense()
		{
			return _session.Execute("pro.revert_license", new object[]{});
		}
		
		public Dictionary<object, object> ProUpdatesAvailable(Dictionary<object, object> options)
		{
			return _session.Execute("pro.update_available", new object[]{options});
		}
		
		public Dictionary<object, object> ProInstallUpdates(Dictionary<object, object> options)
		{
			return _session.Execute("pro.update_install", new object[] { options } );
		}
		
		public Dictionary<object, object> ProInstallOfflineUpdates(string updatePath)
		{
			return _session.Execute("pro.update_instalL_offline", new object[] { updatePath });
		}
		
		public Dictionary<object, object> ProUpdateStatus()
		{
			return _session.Execute("pro.update_status", new object[] {});
		}
		
		public Dictionary<object, object> ProStopUpdates()
		{
			return _session.Execute("pro.update_stop", new object[]{});
		}
		
		public Dictionary<object, object> RestartPro()
		{
			return _session.Execute("pro.restart_service", new object[] {});
		}
		
		public Dictionary<object, object> GetProTasks()
		{
			return _session.Execute("pro.task_list", new object[]{});
		}
		
		public Dictionary<object, object> GetProTaskStatus(string taskID)
		{
			return _session.Execute("pro.task_status", new object[] { taskID} );
		}
		
		public Dictionary<object, object> StopProTask(string taskID)
		{
			return _session.Execute("pro.task_stop", new object[]{taskID});
		}
		
		public Dictionary<object, object> GetProTaskLog(string taskID)
		{
			return _session.Execute("pro.task_log", new object[] { taskID } );
		}
		
		public Dictionary<object, object> DeleteProTaskLog(string taskID)
		{
			return _session.Execute("pro.task_delete_log", new object[]{});
		}
		
		public Dictionary<object, object> StartDiscover(Dictionary<object, object> options)
		{
			return _session.Execute("pro.start_discover", new object[] { options } );
		}
		
		public Dictionary<object, object> StartImport(Dictionary<object, object> options)
		{
			return _session.Execute("pro.start_import", new object[] { options });
		}
		
		public Dictionary<object, object> StartCredentialImport(Dictionary<object, object> options)
		{
			return _session.Execute("pro.start_import_creds", new object[] { options });
		}
		
		public Dictionary<object, object> StartNexposeAssessment(Dictionary<object, object> options)
		{
			return _session.Execute("pro.start_nexpose", new object[]{ options});
		}
		
		public Dictionary<object, object> StartBruteforce(Dictionary<object, object> options)
		{
			return _session.Execute("pro.start_bruteforce", new object[] { options });
		}
		
		public Dictionary<object, object> StartExploit(Dictionary<object, object> options)
		{
			return _session.Execute("pro.start_exploit", new object[]{options});
		}
		
		public Dictionary<object, object> StartWebscan(Dictionary<object, object> options)
		{
			return _session.Execute("pro.start_webscan", new object[] { options });
		}
		
		public Dictionary<object, object> StartWebAudit(Dictionary<object, object> options)
		{
			return _session.Execute("pro.start_webaudit", new object[] { options });
		}
		
		public Dictionary<object, object> StartWebSploit(Dictionary<object, object> options)
		{
			return _session.Execute("pro.start_websploit", new object[] { options } );
		}
		
		public Dictionary<object, object> StartCleanup(Dictionary<object, object> options)
		{
			return _session.Execute("pro.start_cleanup", new object[] { options } );
		}
		
		public Dictionary<object, object> StartLootCollection(Dictionary<object, object> options)
		{
			return _session.Execute("pro.start_collect", new object[] { options } );
		}
		
		public Dictionary<object, object> StartReport(Dictionary<object, object> options)
		{
			return _session.Execute("pro.start_report", new object[] { options});
		}
		
		public Dictionary<object, object> ImportData(string workspace, string data, Dictionary<object, object> options)
		{
			return _session.Execute("pro.import_data", new object[] { workspace, data, options });
		}
		
		public Dictionary<object, object> ImportFile(string workspace, string path, Dictionary<object, object> options)
		{
			return _session.Execute("pro.import_file", new object[] { workspace, path, options } );
		}
		
		public Dictionary<object, object> ValidateImportFile(string filepath)
		{
			return _session.Execute("pro.validate_import_file", new object[] { filepath });
		}
		
		public Dictionary<object, object> DownloadLoot(int lootID)
		{
			return _session.Execute("pro.loot_download", new object[] { lootID } );
		}
		
		public Dictionary<object, object> ListLoot(string workspace)
		{
			return _session.Execute("pro.loot_list", new object[] { workspace });
		}
		
		public Dictionary<object, object> SearchProModules(string query)
		{
			return _session.Execute("pro.module_search", new object[] { query } );
		}
		
		public Dictionary<object, object> ValidateProModule(string moduleName, Dictionary<object, object> options)
		{
			return _session.Execute("pro.module_validate", new object[] { moduleName, options});
		}
		
		public Dictionary<object, object> ListProModules()
		{
			return _session.Execute("pro.report_list", new object[]{});
		}
		
		public Dictionary<object, object> DownloadReport(string reportID)
		{
			return _session.Execute("pro.report_download", new object[] { reportID } );
		}
		
		public Dictionary<object, object> DownloadReportByTask(string taskID)
		{
			return _session.Execute("pro.report_download_by_task", new object[] {taskID});
		}
		
		public Dictionary<object, object> GetReportList(string workspace)
		{
			return _session.Execute("pro.report_list", new object[] { workspace } );
		}
		
		public Dictionary<object, object> MeterpreterChDir(string sessionID, string path)
		{
			return _session.Execute("pro.meterpreter_chdir", new object[] { sessionID, path});
		}
		
		public Dictionary<object, object> MeterpreterGetCWD(string sessionID)
		{
			return _session.Execute("pro.meterpreter_getcwd", new object[] { sessionID } );
		}
		
		public Dictionary<object, object> MeterpreterListDirectory(string sessionID, string path)
		{
			return _session.Execute("pro.meterpreter_list", new object[] { sessionID, path});
		}
		
		public Dictionary<object, object> MeterpreterRemoveFileOrDirectory(string sessionID, string path)
		{
			return _session.Execute("pro.meterpreter_rm", new object[] { sessionID, path });
		}
		
		public Dictionary<object, object> MeterpreterRootPaths(string sessionID)
		{
			return _session.Execute("pro.meterpreter_root_paths", new object[] { sessionID});
		}
		
		public Dictionary<object, object> MeterpreterSearch(string sessionID, string query)
		{
			return _session.Execute("pro.meterpreter_search", new object[] { sessionID, query } );
		}
		
		public Dictionary<object, object> MeterpreterTunnelInterfaces(string sessionID)
		{
			return _session.Execute("pro.meterpreter_tunnel_interfaces", new object[] { sessionID});
		}
	}
}

