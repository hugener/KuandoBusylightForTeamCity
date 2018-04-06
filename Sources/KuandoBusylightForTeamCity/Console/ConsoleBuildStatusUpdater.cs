// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleBuildStatusUpdater.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace KuandoBusylightForTeamCity.Console
{
    public class ConsoleBuildStatusUpdater
    {
        public bool Run(IRunOptions runOptions)
        {
            using (var buildStatusUpdater = new TeamCityBusylightConnector(
                runOptions,
                new ConsoleTeamCityBusylightConnectorLogger(),
                new ConsoleTeamCityBuildStatusProviderLogger()))
            {
                if (!buildStatusUpdater.Start())
                {
                    return false;
                }

                System.Console.WriteLine("Press any key to quit");
                System.Console.ReadKey();
            }

            return true;
        }
    }
}