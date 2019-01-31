// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TeamCityBusylightConnector.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace KuandoBusylightForTeamCity
{
    using System;
    using Busylight;

    public class TeamCityBusylightConnector : IDisposable
    {
        private readonly IRunOptions runOptions;
        private readonly ITeamCityBusylightConnectorReporter teamCityBusylightConnectorReporter;
        private readonly ITeamCityBuildStatusProviderReporter teamCityBuildStatusProviderReporter;
        private TeamCityBuildStatusProvider teamCityBuildStatusProvider;
        private BusylightController busylightController;

        public TeamCityBusylightConnector(
            IRunOptions runOptions,
            ITeamCityBusylightConnectorReporter teamCityBusylightConnectorReporter,
            ITeamCityBuildStatusProviderReporter teamCityBuildStatusProviderReporter)
        {
            this.runOptions = runOptions;
            this.teamCityBusylightConnectorReporter = teamCityBusylightConnectorReporter;
            this.teamCityBuildStatusProviderReporter = teamCityBuildStatusProviderReporter;
        }

        public bool Start()
        {
            this.teamCityBuildStatusProvider = new TeamCityBuildStatusProvider(this.teamCityBuildStatusProviderReporter);
            this.busylightController = new BusylightController(this.runOptions.HidDeviceIds);
            if (!this.busylightController.Initialize())
            {
                return false;
            }

            this.teamCityBuildStatusProvider.BuildStatusChanged += (sender, eventArgs) =>
                OnBuildStatusProviderBuildStatusChanged(this.busylightController, eventArgs);
            if (!this.teamCityBuildStatusProvider.Connect(this.runOptions))
            {
                return false;
            }

            this.teamCityBusylightConnectorReporter.Running();
            return true;
        }

        public void Dispose()
        {
            this.busylightController.Dispose();
            this.teamCityBuildStatusProvider.Dispose();
        }

        private static void OnBuildStatusProviderBuildStatusChanged(BusylightController busylightController, BuildStatus buildStatus)
        {
            var busylightColor = BusylightColor.Blue;
            switch (buildStatus.LastBuildStatus)
            {
                case "SUCCESS":
                    busylightColor = BusylightColor.Green;
                    break;
                case "FAILURE":
                case "ERROR":
                    busylightColor = BusylightColor.Red;
                    break;
            }

            busylightController.Signal(busylightColor, buildStatus.HasPendingBuild);
        }
    }
}