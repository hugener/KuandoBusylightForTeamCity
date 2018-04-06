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
        public UninstallVerb(RunOptions runOptions = null)
        {
            this.RunOptions = runOptions;
        }

        public RunOptions RunOptions { get; private set; }

        public string HelpText => "Uninstalls Busylight for TeamCity as a Windows Service.";

        public string Name => "uninstall";

        public void Configure(IArgumentsBuilder argumentsBuilder)
        {
            argumentsBuilder.AddRequired("a", "arguments", this.RunOptions, () => new RunOptions(null, null), value => this.RunOptions = value, "The arguments to use for the installed service.");
        }
    }
}