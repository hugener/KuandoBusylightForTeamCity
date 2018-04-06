// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerilogTeamCityBusylightConnectorLogger.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace KuandoBusylightForTeamCity.Service
{
    using Serilog;

    public class SerilogTeamCityBusylightConnectorLogger : ITeamCityBusylightConnectorReporter
    {
        private readonly ILogger logger;

        public SerilogTeamCityBusylightConnectorLogger(ILogger logger)
        {
            this.logger = logger;
        }

        public void Running()
        {
            this.logger.Information("Running...");
        }
    }
}