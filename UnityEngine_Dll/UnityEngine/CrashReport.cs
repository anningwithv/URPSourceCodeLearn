using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Export/CrashReport/CrashReport.bindings.h")]
	public sealed class CrashReport
	{
		private static List<CrashReport> internalReports;

		private static object reportsLock = new object();

		private readonly string id;

		public readonly DateTime time;

		public readonly string text;

		public static CrashReport[] reports
		{
			get
			{
				CrashReport.PopulateReports();
				object obj = CrashReport.reportsLock;
				CrashReport[] result;
				lock (obj)
				{
					result = CrashReport.internalReports.ToArray();
				}
				return result;
			}
		}

		public static CrashReport lastReport
		{
			get
			{
				CrashReport.PopulateReports();
				object obj = CrashReport.reportsLock;
				CrashReport result;
				lock (obj)
				{
					bool flag = CrashReport.internalReports.Count > 0;
					if (flag)
					{
						result = CrashReport.internalReports[CrashReport.internalReports.Count - 1];
						return result;
					}
				}
				result = null;
				return result;
			}
		}

		private static int Compare(CrashReport c1, CrashReport c2)
		{
			long ticks = c1.time.Ticks;
			long ticks2 = c2.time.Ticks;
			bool flag = ticks > ticks2;
			int result;
			if (flag)
			{
				result = 1;
			}
			else
			{
				bool flag2 = ticks < ticks2;
				if (flag2)
				{
					result = -1;
				}
				else
				{
					result = 0;
				}
			}
			return result;
		}

		private static void PopulateReports()
		{
			object obj = CrashReport.reportsLock;
			lock (obj)
			{
				bool flag = CrashReport.internalReports != null;
				if (!flag)
				{
					string[] reports = CrashReport.GetReports();
					CrashReport.internalReports = new List<CrashReport>(reports.Length);
					string[] array = reports;
					for (int i = 0; i < array.Length; i++)
					{
						string text = array[i];
						double value;
						string reportData = CrashReport.GetReportData(text, out value);
						DateTime dateTime = new DateTime(1970, 1, 1).AddSeconds(value);
						CrashReport.internalReports.Add(new CrashReport(text, dateTime, reportData));
					}
					CrashReport.internalReports.Sort(new Comparison<CrashReport>(CrashReport.Compare));
				}
			}
		}

		public static void RemoveAll()
		{
			CrashReport[] reports = CrashReport.reports;
			for (int i = 0; i < reports.Length; i++)
			{
				CrashReport crashReport = reports[i];
				crashReport.Remove();
			}
		}

		private CrashReport(string id, DateTime time, string text)
		{
			this.id = id;
			this.time = time;
			this.text = text;
		}

		public void Remove()
		{
			bool flag = CrashReport.RemoveReport(this.id);
			if (flag)
			{
				object obj = CrashReport.reportsLock;
				lock (obj)
				{
					CrashReport.internalReports.Remove(this);
				}
			}
		}

		[FreeFunction(Name = "CrashReport_Bindings::GetReports", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetReports();

		[FreeFunction(Name = "CrashReport_Bindings::GetReportData", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetReportData(string id, out double secondsSinceUnixEpoch);

		[FreeFunction(Name = "CrashReport_Bindings::RemoveReport", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RemoveReport(string id);
	}
}
