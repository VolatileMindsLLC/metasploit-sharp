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
		
		public void Dispose()
		{
		}
	}
}

