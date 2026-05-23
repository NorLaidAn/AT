using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace AT_Work6.Reporting
{
    internal static class MyEnvironment
    {
        public static string OS = Environment.OSVersion.ToString();
        public static string Machine = Environment.MachineName;
        public static string User = Environment.UserName;
        public static string DotNet = Environment.Version.ToString();

        public static bool Is64BitOS = Environment.Is64BitOperatingSystem;
        public static bool Is64BitProcess = Environment.Is64BitProcess;

        public static string CpuName =>
            new ManagementObjectSearcher("select * from Win32_Processor")
                .Get()
                .Cast<ManagementObject>()
                .First()["Name"].ToString();

        public static ulong TotalRam =>
            Convert.ToUInt64(
                new ManagementObjectSearcher("select * from Win32_ComputerSystem")
                    .Get()
                    .Cast<ManagementObject>()
                    .First()["TotalPhysicalMemory"]);

        public static void CreateReportEnviroment()
        {
            File.WriteAllText(
            "allure-results/environment.properties",
            $"""
            OS={MyEnvironment.OS}
            Machine={MyEnvironment.Machine}
            User={MyEnvironment.User}
            DotNet={MyEnvironment.DotNet}
            Is64BitOS={MyEnvironment.Is64BitOS}
            Is64BitProcess={MyEnvironment.Is64BitProcess}
            CPU={MyEnvironment.CpuName}
            RAM={MyEnvironment.TotalRam}
            """);
        }
    }
}
