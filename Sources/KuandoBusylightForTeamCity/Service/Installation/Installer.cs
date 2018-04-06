// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Installer.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace KuandoBusylightForTeamCity.Service.Installation
{
    using System.Configuration.Install;
    using System.Reflection;

    public class Installer
    {
        public bool Install(IRunOptions runOptions)
        {
            ManagedInstallerClass.InstallHelper(new[] { GetServiceCommandLine(runOptions), "/LogFile=", Assembly.GetExecutingAssembly().Location, });
            return true;
        }

        public bool Uninstall(IRunOptions runOptions)
        {
            ManagedInstallerClass.InstallHelper(new[] { "/u", GetServiceCommandLine(runOptions), "/LogFile=", Assembly.GetExecutingAssembly().Location });
            return true;
        }

        private static string GetServiceCommandLine(IRunOptions runOptions)
        {
            return $@"/a=-h {runOptions.HostName} -b {runOptions.BuildTypeId}{GetCredentials(runOptions)} -r {runOptions.RefreshInterval}";
        }

        private static string GetCredentials(IRunOptions runOptions)
        {
            if (runOptions.Credentials == null)
            {
                return string.Empty;
            }

            return $@" -c ""-u {runOptions.Credentials.UserName} -p {runOptions.Credentials.Password}""";
        }
    }
}