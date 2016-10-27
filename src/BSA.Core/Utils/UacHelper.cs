// <copyright file="UacHelper.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Core.Utils
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Reflection;
    using System.Security.Principal;

    public static class UacHelper
    {
        public static bool IsElevated()
        {
            var id = WindowsIdentity.GetCurrent();
            var p = new WindowsPrincipal(id);
            return p.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public static bool RestartWithAdminRights(string arguments = "")
        {
            var startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = true;
            startInfo.WorkingDirectory = Environment.CurrentDirectory;
            startInfo.FileName = Assembly.GetEntryAssembly().Location;
            startInfo.Arguments = arguments;
            startInfo.Verb = "runas";
            try
            {
                var p = Process.Start(startInfo);
                return true;
            }
            catch (Win32Exception ex)
            {
                return false;
            }
        }
    }
}