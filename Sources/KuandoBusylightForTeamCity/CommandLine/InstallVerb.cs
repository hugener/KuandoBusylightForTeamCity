// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InstallVerb.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace KuandoBusylightForTeamCity.CommandLine
{
    using Sundew.CommandLine;

    public class InstallVerb : IInstallOptions, IVerb
    {
        public InstallVerb(RunOptions runOptions = null)
        {
            this.RunOptions = runOptions;
        }

        public RunOptions RunOptions { get; private set; }

        IRunOptions IInstallOptions.RunOptions => this.RunOptions;

        public bool Start { get; private set; }

        public string HelpText => "Installs Busylight for TeamCity as a Windows Service.";

        public string Name => "install";

        public void Configure(IArgumentsBuilder argumentsBuilder)
        {
            argumentsBuilder.AddRequired("a", "arguments", this.RunOptions, () => new RunOptions(null, null), value => this.RunOptions = value, "The arguments to use for the installed service.");
            argumentsBuilder.AddSwitch("s", "start", this.Start, value => this.Start = value, "Starts the service after installation.");
        }
    }
}