// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IInstallOptions.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace KuandoBusylightForTeamCity
{
    public interface IInstallOptions
    {
        IRunOptions RunOptions { get; }

        bool Start { get; }
    }
}