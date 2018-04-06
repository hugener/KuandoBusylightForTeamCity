// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerilogTeamCityBuildStatusProviderLogger.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace KuandoBusylightForTeamCity.Service
{
    using System;
    using Serilog;

    public class SerilogTeamCityBuildStatusProviderLogger : ITeamCityBuildStatusProviderReporter
    {
        private readonly ILogger logger;

        public SerilogTeamCityBuildStatusProviderLogger(ILogger logger)
        {
            this.logger = logger;
        }

        public void ConnectingTo(string hostName)
        {
            this.logger.Information($"{nameof(this.ConnectingTo)}: {hostName}");
        }

        public void Authenticating()
        {
            this.logger.Information($"{nameof(this.Authenticating)}");
        }

        public void Authenticated()
        {
            this.logger.Information($"{nameof(this.Authenticated)}");
        }

        public void AuthenticationFailed()
        {
            this.logger.Information($"{nameof(this.AuthenticationFailed)}");
        }

        public void RefreshBuildStatusFailed(int numberOfAttempts, Exception exception)
        {
            this.logger.Information("{nameof(this.RefreshBuildStatusFailed)}: after {numberOfAttempts} attempts, with exception {exception}", numberOfAttempts, exception);
        }
    }
}