// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRunOptions.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace KuandoBusylightForTeamCity
{
    using System;

    public interface IRunOptions
    {
        string HostName { get; }

        string BuildTypeId { get; }

        ICredentials Credentials { get; }

        TimeSpan RefreshInterval { get; }
    }
}