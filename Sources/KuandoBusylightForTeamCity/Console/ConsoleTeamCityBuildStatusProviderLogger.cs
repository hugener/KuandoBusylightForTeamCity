// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleTeamCityBuildStatusProviderLogger.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace KuandoBusylightForTeamCity.Console
{
    using System;

    public class ConsoleTeamCityBuildStatusProviderLogger : ITeamCityBuildStatusProviderReporter
    {
        public void ConnectingTo(string hostName)
        {
            System.Console.WriteLine($"{nameof(this.ConnectingTo)}: {hostName}");
        }

        public void Authenticating()
        {
            System.Console.WriteLine($"{nameof(this.Authenticating)}");
        }

        public void Authenticated()
        {
            System.Console.WriteLine($"{nameof(this.Authenticated)}");
        }

        public void AuthenticationFailed()
        {
            System.Console.WriteLine($"{nameof(this.AuthenticationFailed)}");
        }

        public void RefreshBuildStatusFailed(int numberOfAttempts, Exception exception)
        {
            System.Console.WriteLine($"{nameof(this.RefreshBuildStatusFailed)}: after {numberOfAttempts} attempts, with exception {exception}");
        }
    }
}