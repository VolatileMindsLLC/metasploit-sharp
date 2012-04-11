using System;
using System.Collections.Generic;

namespace metasploitsharp
{
	public class MetasploitManager : IDisposable
	{
		private MetasploitSession _session;
		
		public MetasploitManager (MetasploitSession session)
		{
			_session = session;
		}
		
		public Dictionary<object, object> GetCoreModuleStats()
		{
			return _session.Execute("core.module_stats");
		}
		
		public Dictionary<object, object> GetCoreVersionInformation()
		{
			return _session.Execute("core.version");
		}
		
		public Dictionary<object, object> AddCoreModulePath(string modulePath)
		{
			return _session.Execute("core.add_module_path", modulePath);
		}
		
		public Dictionary<object, object> ReloadCoreModules()
		{
			return _session.Execute("core.reload_modules");
		}
		
		public Dictionary<object, object> SaveCore()
		{
			return _session.Execute("core.save");
		}
		
		public Dictionary<object,object> SetCoreGlobalVariable(string optionName, string optionValue)
		{
			return _session.Execute("core.setg", optionName, optionValue);
		}
		
		public Dictionary<object, object> UnsetCoreGlobalVariable(string optionName)
		{
			return _session.Execute("core.unsetg", optionName);
		}
		
		public Dictionary<object, object> GetCoreThreadList()
		{
			return _session.Execute("core.thread_list");
		}
		
		public Dictionary<object, object> KillCoreThread(string threadID)
		{
			return _session.Execute("core.thread_kill", threadID);
		}
		
		public Dictionary<object, object> StopCore()
		{
			return _session.Execute("core.stop");
		}
		
		public Dictionary<object, object> CreateConsole()
		{
			return _session.Execute("console.create");
		}
		
		public Dictionary<object, object> DestroyConsole(string consoleID)
		{
			return _session.Execute("console.destroy", consoleID);
		}
		
		public Dictionary<object, object> ListConsoles()
		{
			return _session.Execute("console.list");
		}
		
		public Dictionary<object, object> WriteToConsole(string consoleID, string data)
		{
			return _session.Execute("console.write", consoleID, data);
		}
		
		public Dictionary<object, object> ReadConsole(string consoleID)
		{
			return _session.Execute("console.read", consoleID);
		}
		
		public Dictionary<object, object> DetachSessionFromConsole(string consoleID)
		{
			return _session.Execute("console.session_detach", consoleID);
		}
		
		public Dictionary<object, object> KillSessionFromConsole(string consoleID)
		{
			return _session.Execute("console.session_kill", consoleID);
		}
		
		public Dictionary<object, object> TabConsole(string consoleID, string input)
		{
			return _session.Execute("console.tabs", consoleID, input);
		}
		
		public Dictionary<object, object> ListJobs()
		{
			return _session.Execute("job.list");
		}
		
		public Dictionary<object, object> GetJobInfo(string jobID)
		{
			return _session.Execute("job.info", jobID);
		}
		
		public Dictionary<object, object> StopJob(string jobID)
		{
			return _session.Execute("job.stop", jobID);
		}
		
		public Dictionary<object, object> GetExploitModules()
		{
			return _session.Execute("module.exploits");
		}
		
		public Dictionary<object, object> GetAuxiliaryModules()
		{
			return _session.Execute("module.auxiliary");
		}
		
		public Dictionary<object, object> GetPostModules()
		{
			return _session.Execute("module.post");
		}
		
		public Dictionary<object, object> GetPayloads()
		{
			return _session.Execute("module.payloads");
		}
		
		public Dictionary<object, object> GetEncoders()
		{
			return _session.Execute("module.encoders");
		}
		
		public Dictionary<object, object> GetNops()
		{
			return _session.Execute("module.nops");
		}
		
		public Dictionary<object, object> GetModuleInformation(string moduleType, string moduleName)
		{
			return _session.Execute("module.info", moduleType, moduleName);
		}
		
		public Dictionary<object, object> GetModuleOptions(string moduleType, string moduleName)
		{
			return _session.Execute("module.options", moduleType,moduleName);
		}
		
		public Dictionary<object, object> GetModuleCompatiblePayloads(string moduleName)
		{
			return _session.Execute("module.compatible_payloads", moduleName);
		}
		
		public Dictionary<object, object> GetModuleTargetCompatiblePayloads(string moduleName, int targetIndex)
		{
			return _session.Execute("module.target_compatible_payloads", moduleName, targetIndex);
		}
		
		public Dictionary<object, object> GetModuleCompatibleSessions(string moduleName)
		{
			return _session.Execute("module.compatible_sessions", moduleName);
		}
		
		public Dictionary<object, object> Encode(string data, string encoderModule, Dictionary<object, object> options)
		{
			return _session.Execute("module.encode", data, encoderModule, options);
		}
		
		public Dictionary<object, object> ExecuteModule(string moduleType, string moduleName, Dictionary<object, object> options)
		{
			return _session.Execute("module.execute", moduleType, moduleName, options);
		}
		
		public Dictionary<object, object> LoadPlugin(string pluginName, Dictionary<object, object> options)
		{
			return _session.Execute("plugin.load", pluginName, options);
		}
		
		public Dictionary<object, object> UnloadPlugin(string pluginName)
		{
			return _session.Execute("plugin.unload", pluginName);
		}
		
		public Dictionary<object, object> ListLoadedPlugins()
		{
			return _session.Execute("plugin.loaded");
		}
		
		public Dictionary<object, object> ListSessions()
		{
			return _session.Execute("session.list");
		}
		
		public Dictionary<object, object> StopSession(string sessionID)
		{
			return _session.Execute("session.stop", sessionID);
		}
		
		public Dictionary<object, object> ReadSessionShell(string sessionID)
		{
			return this.ReadSessionShell(sessionID, null);
		}
		
		public Dictionary<object, object> ReadSessionShell(string sessionID, int? readPointer)
		{
			if (readPointer.HasValue)
				return _session.Execute("session.read_shell", sessionID, readPointer.Value);
			else
				return _session.Execute("session.read_shell", sessionID);
		}
		
		public Dictionary<object, object> WriteToSessionShell(string sessionID, string data)
		{
			return _session.Execute("session.shell_write", sessionID, data);
		}
		
		public Dictionary<object, object> WriteToSessionMeterpreter(string sessionID, string data)
		{
			return _session.Execute("session.meterpreter_write", sessionID, data);
		}
		
		public Dictionary<object, object> ReadSessionMeterpreter(string sessionID)
		{
			return _session.Execute("session.meterpreter_read", sessionID);
		}
		
		public Dictionary<object, object> RunSessionMeterpreterSingleCommand(string sessionID, string command)
		{
			return _session.Execute("session.meterpreter_run_single", sessionID, command);
		}
		
		public Dictionary<object, object> RunSessionMeterpreterScript(string sessionID, string scriptName)
		{
			return _session.Execute("session.meterpreter_script", sessionID, scriptName);
		}
		
		public Dictionary<object, object> DetachMeterpreterSession(string sessionID)
		{
			return _session.Execute("session.meterpreter_session_detach", sessionID);
		}
		
		public Dictionary<object, object> KillMeterpreterSession(string sessionID)
		{
			return _session.Execute("session.meterpreter_session_kill", sessionID);
		}
		
		public Dictionary<object, object> TabMeterpreterSession(string sessionID, string input)
		{
			return _session.Execute("session.meterpreter_tabs", sessionID, input);
		}
		
		public Dictionary<object, object> CompatibleModuleForSession(string sessionID)
		{
			return _session.Execute("session.compatible_modules", sessionID);
		}
		
		public Dictionary<object, object> UpgradeShellToMeterpreter(string sessionID, string host, string port)
		{
			return _session.Execute("session.shell_upgrade", sessionID, host, port);
		}
		
		public Dictionary<object, object> ClearSessionRing(string sessionID)
		{
			return _session.Execute("session.ring_clear", sessionID);
		}
		
		public Dictionary<object, object> LastSessionRing(string sessionID)
		{
			return _session.Execute("session.ring_last", sessionID);
		}
		
		public Dictionary<object, object> WriteToSessionRing(string sessionID, string data)
		{
			return _session.Execute("session.ring_put", sessionID, data);
		}
		
		public Dictionary<object, object> ReadSessionRing(string sessionID, int? readPointer)
		{
			if (readPointer.HasValue)
				return _session.Execute("session.ring_read", sessionID, readPointer.Value);
			else
				return _session.Execute("session.ring_read", sessionID);
		}
		
		
		
		public void Dispose()
		{
		}
	}
}

