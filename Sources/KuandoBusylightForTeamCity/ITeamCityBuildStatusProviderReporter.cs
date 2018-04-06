// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITeamCityBuildStatusProviderReporter.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace KuandoBusylightForTeamCity
{
    using System;

    public interface ITeamCityBuildStatusProviderReporter
    {
        void ConnectingTo(string hostName);

        void Authenticating();

        void Authenticated();

        void AuthenticationFailed();

        void RefreshBuildStatusFailed(int numberOfAttempts, Exception exception);
    }
}