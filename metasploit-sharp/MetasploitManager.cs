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
			return _session.Execute("core.module_stats", new object[] {});
		}
		
		public Dictionary<object, object> GetCoreVersionInformation()
		{
			return _session.Execute("core.version", new object[] {});
		}
		
		public Dictionary<object, object> AddCoreModulePath(string modulePath)
		{
			return _session.Execute("core.add_module_path", new object[] { modulePath });
		}
		
		public Dictionary<object, object> ReloadCoreModules()
		{
			return _session.Execute("core.reload_modules", new object[] {});
		}
		
		public Dictionary<object, object> SaveCore()
		{
			return _session.Execute("core.save", new object[] {});
		}
		
		public Dictionary<object,object> SetCoreGlobalVariable(string optionName, string optionValue)
		{
			return _session.Execute("core.setg", new object[] {optionName, optionValue});
		}
		
		public Dictionary<object, object> UnsetCoreGlobalVariable(string optionName)
		{
			return _session.Execute("core.unsetg", new object[] {optionName});
		}
		
		public Dictionary<object, object> GetCoreThreadList()
		{
			return _session.Execute("core.thread_list", new object[] {});
		}
		
		public Dictionary<object, object> KillCoreThread(string threadID)
		{
			return _session.Execute("core.thread_kill", new object[]{threadID});
		}
		
		public Dictionary<object, object> StopCore()
		{
			return _session.Execute("core.stop", new object[]{});
		}
		
		public Dictionary<object, object> CreateConsole()
		{
			return _session.Execute("console.create", new object[]{});
		}
		
		public Dictionary<object, object> DestroyConsole(string consoleID)
		{
			return _session.Execute("console.destroy", new object[]{consoleID});
		}
		
		public Dictionary<object, object> ListConsoles()
		{
			return _session.Execute("console.list", new object[]{});
		}
		
		public Dictionary<object, object> WriteToConsole(string consoleID, string data)
		{
			return _session.Execute("console.write", new object[] { consoleID, data });
		}
		
		public Dictionary<object, object> ReadConsole(string consoleID)
		{
			return _session.Execute("console.read", new object[] { consoleID });
		}
		
		public Dictionary<object, object> DetachSessionFromConsole(string consoleID)
		{
			return _session.Execute("console.session_detach", new object[] { consoleID });
		}
		
		public Dictionary<object, object> KillSessionFromConsole(string consoleID)
		{
			return _session.Execute("console.session_kill", new object[] { consoleID });
		}
		
		public Dictionary<object, object> TabConsole(string consoleID, string input)
		{
			return _session.Execute("console.tabs", new object[] {consoleID, input});
		}
		
		public Dictionary<object, object> ListJobs()
		{
			return _session.Execute("job.list", new object[]{});
		}
		
		public Dictionary<object, object> GetJobInfo(string jobID)
		{
			return _session.Execute("job.info", new object[] { jobID });
		}
		
		public Dictionary<object, object> StopJob(string jobID)
		{
			return _session.Execute("job.stop", new object[] { jobID });
		}
		
		public Dictionary<object, object> GetExploitModules()
		{
			return _session.Execute("module.exploits", new object[] {});
		}
		
		public Dictionary<object, object> GetAuxiliaryModules()
		{
			return _session.Execute("module.auxiliary", new object[] {});
		}
		
		public Dictionary<object, object> GetPostModules()
		{
			return _session.Execute("module.post", new object[] {});
		}
		
		public Dictionary<object, object> GetPayloads()
		{
			return _session.Execute("module.payloads", new object[] {});
		}
		
		public Dictionary<object, object> GetEncoders()
		{
			return _session.Execute("module.encoders", new object[] {});
		}
		
		public Dictionary<object, object> GetNops()
		{
			return _session.Execute("module.nops", new object[] {});
		}
		
		public Dictionary<object, object> GetModuleInformation(string moduleType, string moduleName)
		{
			return _session.Execute("module.info", new object[]{moduleType, moduleName});
		}
		
		public Dictionary<object, object> GetModuleOptions(string moduleType, string moduleName)
		{
			return _session.Execute("module.options", new object[] {moduleType,moduleName});
		}
		
		public Dictionary<object, object> GetModuleCompatiblePayloads(string moduleName)
		{
			return _session.Execute("module.compatible_payloads", new object[] {moduleName});
		}
		
		public Dictionary<object, object> GetModuleTargetCompatiblePayloads(string moduleName, int targetIndoex)
		{
			return _session.Execute("module.target_compatible_payloads", new object[] { moduleName, targetIndoex});
		}
		
		public Dictionary<object, object> GetModuleCompatibleSessions(string moduleName)
		{
			return _session.Execute("module.compatible_sessions", new object[] { moduleName } );
		}
		
		public Dictionary<object, object> Encode(string data, string encoderModule, Dictionary<object, object> options)
		{
			return _session.Execute("module.encode", new object[] {data, encoderModule, options } );
		}
		
		public Dictionary<object, object> ExecuteModule(string moduleType, string moduleName, Dictionary<object, object> options)
		{
			return _session.Execute("module.execute", new object[] {moduleType, moduleName, options});
		}
		
		public Dictionary<object, object> LoadPlugin(string pluginName, Dictionary<object, object> options)
		{
			return _session.Execute("plugin.load", new object[] { pluginName, options });
		}
		
		public Dictionary<object, object> UnloadPlugin(string pluginName)
		{
			return _session.Execute("plugin.unload", new object[] { pluginName });
		}
		
		public Dictionary<object, object> ListLoadedPlugins()
		{
			return _session.Execute("plugin.loaded", new object[] {} );
		}
		
		public Dictionary<object, object> ListSessions()
		{
			return _session.Execute("session.list", new object[] {});
		}
		
		public Dictionary<object, object> StopSession(string sessionID)
		{
			return _session.Execute("session.stop", new object[] { sessionID});
		}
		
		public Dictionary<object, object> ReadSessionShell(string sessionID, int? readPointer)
		{
			if (readPointer.HasValue)
				return _session.Execute("session.read_shell", new object[]{sessionID, readPointer.Value});
			else
				return _session.Execute("session.read_shell", new object[] { sessionID });
		}
		
		public Dictionary<object, object> WriteToSessionShell(string sessionID, string data)
		{
			return _session.Execute("session.shell_write", new object[] { sessionID, data } );
		}
		
		public Dictionary<object, object> WriteToSessionMeterpreter(string sessionID, string data)
		{
			return _session.Execute("session.meterpreter_write", new object[] { sessionID, data});
		}
		
		public Dictionary<object, object> ReadSessionMeterpreter(string sessionID)
		{
			return _session.Execute("session.meterpreter_read", new object[] { sessionID } );
		}
		
		public Dictionary<object, object> RunSessionMeterpreterSingleCommand(string sessionID, string command)
		{
			return _session.Execute("session.meterpreter_run_single", new object[] { sessionID, command });
		}
		
		public Dictionary<object, object> RunSessionMeterpreterScript(string sessionID, string scriptName)
		{
			return _session.Execute("session.meterpreter_script", new object[] { sessionID, scriptName } );
		}
		
		public Dictionary<object, object> DetachMeterpreterSession(string sessionID)
		{
			return _session.Execute("session.meterpreter_session_detach", new object[] { sessionID });
		}
		
		public Dictionary<object, object> KillMeterpreterSession(string sessionID)
		{
			return _session.Execute("session.meterpreter_session_kill", new object[] { sessionID});
		}
		
		public Dictionary<object, object> TabMeterpreterSession(string sessionID, string input)
		{
			return _session.Execute("session.meterpreter_tabs", new object[] { sessionID, input });
		}
		
		public Dictionary<object, object> CompatibleModuleForSession(string sessionID)
		{
			return _session.Execute("session.compatible_modules", new object[] { sessionID });
		}
		
		public Dictionary<object, object> UpgradeShellToMeterpreter(string sessionID, string host, string port)
		{
			return _session.Execute("session.shell_upgrade", new object[] { sessionID, host, port });
		}
		
		public Dictionary<object, object> ClearSessionRing(string sessionID)
		{
			return _session.Execute("session.ring_clear", new object[] { sessionID } );
		}
		
		public Dictionary<object, object> LastSessionRing(string sessionID)
		{
			return _session.Execute("session.ring_last", new object[] { sessionID});
		}
		
		public Dictionary<object, object> WriteToSessionRing(string sessionID, string data)
		{
			return _session.Execute("session.ring_put", new object[] { sessionID, data } );
		}
		
		public Dictionary<object, object> ReadSessionRing(string sessionID, int? readPointer)
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

