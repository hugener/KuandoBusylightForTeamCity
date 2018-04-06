// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BuildStatus.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace KuandoBusylightForTeamCity
{
    public class BuildStatus
    {
        public BuildStatus(bool hasPendingBuild, string lastBuildStatus)
        {
            this.HasPendingBuild = hasPendingBuild;
            this.LastBuildStatus = lastBuildStatus;
        }

        public bool HasPendingBuild { get; }

        public string LastBuildStatus { get; }
    }
}