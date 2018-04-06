// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BuildStatusUpdaterService.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace KuandoBusylightForTeamCity.Service
{
    using System.ServiceProcess;
    using Serilog;

    public partial class BuildStatusUpdaterService : ServiceBase
    {
        private readonly TeamCityBusylightConnector teamCityBusylightConnector;

        public BuildStatusUpdaterService(IRunOptions runOptions, ILogger logger)
        {
            this.InitializeComponent();
            this.teamCityBusylightConnector = new TeamCityBusylightConnector(
                runOptions,
                new SerilogTeamCityBusylightConnectorLogger(logger),
                new SerilogTeamCityBuildStatusProviderLogger(logger));
            this.ServiceName += " " + runOptions.BuildTypeId;
        }

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
            this.teamCityBusylightConnector.Start();
        }

        protected override void OnStop()
        {
            base.OnStop();
            this.teamCityBusylightConnector.Dispose();
        }
    }
}