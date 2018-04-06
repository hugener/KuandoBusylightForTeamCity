// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RunOptions.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace KuandoBusylightForTeamCity.CommandLine
{
    using System;
    using Sundew.Base.Numeric;
    using Sundew.CommandLine;

    public class RunOptions : IArguments, IRunOptions
    {
        public RunOptions(string hostName, string buildTypeId, CredentialOptions credentials = null, TimeSpan? refreshInterval = null)
        {
            this.HostName = hostName;
            this.BuildTypeId = buildTypeId;
            this.Credentials = credentials;
            this.RefreshInterval = refreshInterval ?? TimeSpan.FromMilliseconds(1000);
        }

        public string HostName { get; private set; }

        public string BuildTypeId { get; private set; }

        public CredentialOptions Credentials { get; private set; }

        ICredentials IRunOptions.Credentials => this.Credentials;

        public TimeSpan RefreshInterval { get; private set; }

        public void Configure(IArgumentsBuilder argumentsBuilder)
        {
            argumentsBuilder.AddRequired("h", "host-name", () => this.HostName, hostName => this.HostName = hostName, "Specifies the team city host name");
            argumentsBuilder.AddRequired("b", "build-id", () => this.BuildTypeId, buildId => this.BuildTypeId = buildId, "Specifies the team city buildTypeId");
            argumentsBuilder.AddOptional("c", "credentials", this.Credentials, () => new CredentialOptions(null, null), options => this.Credentials = options, "Specifies the credentials to connect to TeamCity");
            var refreshIntervalRange = new Range<TimeSpan>(TimeSpan.FromMilliseconds(200), TimeSpan.FromMinutes(10));
            argumentsBuilder.AddOptional(
                "ri",
                "refresh-interval",
                () => refreshIntervalRange.Limit(this.RefreshInterval).ToString(),
                value => this.RefreshInterval = refreshIntervalRange.Limit(TimeSpan.Parse(value)),
                $"The build status refresh interval within the: {refreshIntervalRange}");
        }
    }
}