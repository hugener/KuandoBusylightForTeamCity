// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleTeamCityBusylightConnectorLogger.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace KuandoBusylightForTeamCity.Console
{
    public class ConsoleTeamCityBusylightConnectorLogger : ITeamCityBusylightConnectorReporter
    {
        public void Running()
        {
            System.Console.WriteLine("Running...");
        }
    }
}