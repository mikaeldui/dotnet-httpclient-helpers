using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;

namespace System.Net.Http
{
    public partial class UserAgent
    {
        public static UserAgent From(Assembly assembly)
        {
            var assemblyName = assembly.GetName();
            UserAgent userAgent = new(assemblyName.Name);

            if (Debugger.IsAttached)
                userAgent.Version = "Debug";
            else
                userAgent.Version = assembly.GetProductVersion();

            if (assembly == Assembly.GetEntryAssembly())
            {
                userAgent.Comments.Add(RuntimeInformation.OSDescription);
                userAgent.Comments.Add(RuntimeInformation.OSArchitecture.ToString());
                userAgent.Comments.Add(RuntimeInformation.FrameworkDescription);
            }

            userAgent.Comments.Add(assemblyName.ProcessorArchitecture.ToString());

            var targetFramework = assembly.GetCustomAttribute<TargetFrameworkAttribute>();
            var frameworkVersion = targetFramework?.FrameworkName.Replace(",Version=", " ");
            if(frameworkVersion != null)
                userAgent.Comments.Add(frameworkVersion);

            return userAgent;
        }
    }
}
