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
		
		public Dictionary<string, string> GetCoreModuleStats()
		{
			return _session.Execute("core.module_stats", new object[] {});
		}
		
		public Dictionary<string, string> GetCoreVersionInformation()
		{
			return _session.Execute("core.version", new object[] {});
		}
		
		public Dictionary<string, string> AddCoreModulePath(string modulePath)
		{
			return _session.Execute("core.add_module_path", new object[] { modulePath });
		}
		
		public Dictionary<string, string> ReloadCoreModules()
		{
			return _session.Execute("core.reload_modules", new object[] {});
		}
		
		public Dictionary<string, string> SaveCore()
		{
			return _session.Execute("core.save", new object[] {});
		}
		
		public Dictionary<string,string> SetCoreGlobalVariable(string optionName, string optionValue)
		{
			return _session.Execute("core.setg", new object[] {optionName, optionValue});
		}
		
		public Dictionary<string, string> UnsetCoreGlobalVariable(string optionName)
		{
			return _session.Execute("core.unsetg", new object[] {optionName});
		}
		
		public Dictionary<string, string> GetCoreThreadList()
		{
			return _session.Execute("core.thread_list", new object[] {});
		}
		
		public Dictionary<string, string> KillCoreThread(string threadID)
		{
			return _session.Execute("core.thread_kill", new object[]{threadID});
		}
		
		public Dictionary<string, string> StopCore()
		{
			return _session.Execute("core.stop", new object[]{});
		}
		
		public Dictionary<string, string> CreateConsole()
		{
			return _session.Execute("console.create", new object[]{});
		}
		
		public Dictionary<string, string> DestroyConsole(string consoleID)
		{
			return _session.Execute("console.destroy", new object[]{consoleID});
		}
		
		public Dictionary<string, string> ListConsoles()
		{
			return _session.Execute("console.list", new object[]{});
		}
		
		public Dictionary<string, string> WriteToConsole(string consoleID, string data)
		{
			return _session.Execute("console.write", new object[] { consoleID, data });
		}
		
		public Dictionary<string, string> ReadConsole(string consoleID)
		{
			return _session.Execute("console.read", new object[] { consoleID });
		}
		
		public Dictionary<string, string> DetachSessionFromConsole(string consoleID)
		{
			return _session.Execute("console.session_detach", new object[] { consoleID });
		}
		
		public Dictionary<string, string> KillSessionFromConsole(string consoleID)
		{
			return _session.Execute("console.session_kill", new object[] { consoleID });
		}
		
		public Dictionary<string, string> TabConsole(string consoleID, string input)
		{
			return _session.Execute("console.tabs", new object[] {consoleID, input});
		}
		
		public Dictionary<string, string> ListJobs()
		{
			return _session.Execute("job.list", new object[]{});
		}
		
		public Dictionary<string, string> GetJobInfo(string jobID)
		{
			return _session.Execute("job.info", new object[] { jobID });
		}
		
		public Dictionary<string, string> StopJob(string jobID)
		{
			return _session.Execute("job.stop", new object[] { jobID });
		}
		
		public Dictionary<string, string> GetExploitModules()
		{
			return _session.Execute("module.exploits", new object[] {});
		}
		
		public Dictionary<string, string> GetAuxiliaryModules()
		{
			return _session.Execute("module.auxiliary", new object[] {});
		}
		
		public Dictionary<string, string> GetPostModules()
		{
			return _session.Execute("module.post", new object[] {});
		}
		
		public Dictionary<string, string> GetPayloads()
		{
			return _session.Execute("module.payloads", new object[] {});
		}
		
		public Dictionary<string, string> GetEncoders()
		{
			return _session.Execute("module.encoders", new object[] {});
		}
		
		public Dictionary<string, string> GetNops()
		{
			return _session.Execute("module.nops", new object[] {});
		}
		
		public Dictionary<string, string> GetModuleInformation(string moduleType, string moduleName)
		{
			return _session.Execute("module.info", new object[]{moduleType, moduleName});
		}
		
		public Dictionary<string, string> GetModuleOptions(string moduleType, string moduleName)
		{
			return _session.Execute("module.options", new object[] {moduleType,moduleName});
		}
		
		public Dictionary<string, string> GetModuleCompatiblePayloads(string moduleName)
		{
			return _session.Execute("module.compatible_payloads", new object[] {moduleName});
		}
		
		public Dictionary<string, string> GetModuleTargetCompatiblePayloads(string moduleName, int targetIndoex)
		{
			return _session.Execute("module.target_compatible_payloads", new object[] { moduleName, targetIndoex});
		}
		
		public Dictionary<string, string> GetModuleCompatibleSessions(string moduleName)
		{
			return _session.Execute("module.compatible_sessions", new object[] { moduleName } );
		}
		
		public Dictionary<string, string> Encode(string data, string encoderModule, Dictionary<object, object> options)
		{
			return _session.Execute("module.encode", new object[] {data, encoderModule, options } );
		}
		
		public Dictionary<string, string> ExecuteModule(string moduleType, string moduleName, Dictionary<object, object> options)
		{
			return _session.Execute("module.execute", new object[] {moduleType, moduleName, options});
		}
		
		public Dictionary<string, string> LoadPlugin(string pluginName, Dictionary<object, object> options)
		{
			return _session.Execute("plugin.load", new object[] { pluginName, options });
		}
		
		public Dictionary<string, string> UnloadPlugin(string pluginName)
		{
			return _session.Execute("plugin.unload", new object[] { pluginName });
		}
		
		public Dictionary<string, string> ListLoadedPlugins()
		{
			return _session.Execute("plugin.loaded", new object[] {} );
		}
		
		public Dictionary<string, string> ListSessions()
		{
			return _session.Execute("session.list", new object[] {});
		}
		
		public Dictionary<string, string> StopSession(string sessionID)
		{
			return _session.Execute("session.stop", new object[] { sessionID});
		}
		
		public Dictionary<string, string> ReadSessionShell(string sessionID, int? readPointer)
		{
			if (readPointer.HasValue)
				return _session.Execute("session.read_shell", new object[]{sessionID, readPointer.Value});
			else
				return _session.Execute("session.read_shell", new object[] { sessionID });
		}
		
		public Dictionary<string, string> WriteToSessionShell(string sessionID, string data)
		{
			return _session.Execute("session.shell_write", new object[] { sessionID, data } );
		}
		
		public Dictionary<string, string> WriteToSessionMeterpreter(string sessionID, string data)
		{
			return _session.Execute("session.meterpreter_write", new object[] { sessionID, data});
		}
		
		public Dictionary<string, string> ReadSessionMeterpreter(string sessionID)
		{
			return _session.Execute("session.meterpreter_read", new object[] { sessionID } );
		}
		
		public Dictionary<string, string> RunSessionMeterpreterSingleCommand(string sessionID, string command)
		{
			return _session.Execute("session.meterpreter_run_single", new object[] { sessionID, command });
		}
		
		public Dictionary<string, string> RunSessionMeterpreterScript(string sessionID, string scriptName)
		{
			return _session.Execute("session.meterpreter_script", new object[] { sessionID, scriptName } );
		}
		
		public Dictionary<string, string> DetachMeterpreterSession(string sessionID)
		{
			return _session.Execute("session.meterpreter_session_detach", new object[] { sessionID });
		}
		
		public Dictionary<string, string> KillMeterpreterSession(string sessionID)
		{
			return _session.Execute("session.meterpreter_session_kill", new object[] { sessionID});
		}
		
		public Dictionary<string, string> TabMeterpreterSession(string sessionID, string input)
		{
			return _session.Execute("session.meterpreter_tabs", new object[] { sessionID, input });
		}
		
		public Dictionary<string, string> CompatibleModuleForSession(string sessionID)
		{
			return _session.Execute("session.compatible_modules", new object[] { sessionID });
		}
		
		public Dictionary<string, string> UpgradeShellToMeterpreter(string sessionID, string host, string port)
		{
			return _session.Execute("session.shell_upgrade", new object[] { sessionID, host, port });
		}
		
		public Dictionary<string, string> ClearSessionRing(string sessionID)
		{
			return _session.Execute("session.ring_clear", new object[] { sessionID } );
		}
		
		public Dictionary<string, string> LastSessionRing(string sessionID)
		{
			return _session.Execute("session.ring_last", new object[] { sessionID});
		}
		
		public Dictionary<string, string> WriteToSessionRing(string sessionID, string data)
		{
			return _session.Execute("session.ring_put", new object[] { sessionID, data } );
		}
		
		public Dictionary<string, string> ReadSessionRing(string sessionID, int? readPointer)
		{
			if (readPointer.HasValue)
				return _session.Execute("session.ring_read", new object[] { sessionID, readPointer.Value } );
			else
				return _session.Execute("session.ring_read", new object[] { sessionID });
		}
		
		
		
		public void Dispose()
		{
		}
	}
}

