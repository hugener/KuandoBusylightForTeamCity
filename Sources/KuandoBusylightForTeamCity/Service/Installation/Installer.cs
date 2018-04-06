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
        public bool Install(IInstallOptions installOptions)
        {
            ManagedInstallerClass.InstallHelper(new[]
            {
                $@"/a=-h {installOptions.RunOptions.HostName} -b {installOptions.RunOptions.BuildTypeId}{GetCredentials(installOptions.RunOptions)} -ri {installOptions.RunOptions.RefreshInterval}",
                $"/start={(installOptions.Start ? "true" : "false")}",
                "/LogFile=",
                Assembly.GetExecutingAssembly().Location,
            });

            return true;
        }

        public bool Uninstall(string buildTypeId)
        {
            ManagedInstallerClass.InstallHelper(new[] { "/u", $@"/b={buildTypeId}", "/LogFile=", Assembly.GetExecutingAssembly().Location });
            return true;
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