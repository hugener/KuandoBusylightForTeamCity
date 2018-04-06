// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UninstallVerb.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace KuandoBusylightForTeamCity.CommandLine
{
    using Sundew.CommandLine;

    public class UninstallVerb : IVerb
    {
        public UninstallVerb(string buildTypeId)
        {
            this.BuildTypeId = buildTypeId;
        }

        public string BuildTypeId { get; private set; }

        public string HelpText => "Uninstalls Busylight for TeamCity as a Windows Service.";

        public string Name => "uninstall";

        public void Configure(IArgumentsBuilder argumentsBuilder)
        {
            argumentsBuilder.AddRequired("b", "build-type-id", () => this.BuildTypeId, buildId => this.BuildTypeId = buildId, "Specifies the TeamCity build type id");
        }
    }
}