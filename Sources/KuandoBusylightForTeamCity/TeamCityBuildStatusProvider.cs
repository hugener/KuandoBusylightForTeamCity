// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TeamCityBuildStatusProvider.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace KuandoBusylightForTeamCity
{
    using System;
    using System.Linq;
    using System.Threading;
    using Sundew.Base.ControlFlow;
    using TeamCitySharp;
    using TeamCitySharp.DomainEntities;
    using TeamCitySharp.Fields;
    using TeamCitySharp.Locators;

    public class TeamCityBuildStatusProvider : IDisposable
    {
        private readonly ITeamCityBuildStatusProviderReporter teamCityBuildStatusProviderReporter;
        private readonly Thread refreshThread;
        private readonly CancellationTokenSource cancellationTokenSource;
        private readonly ManualResetEvent manualResetEvent;
        private TeamCityClient teamCityClient;
        private TimeSpan refreshInterval;
        private string buildTypeId;
        private BuildStatus lastBuildStatus;

        public TeamCityBuildStatusProvider(ITeamCityBuildStatusProviderReporter teamCityBuildStatusProviderReporter)
        {
            this.teamCityBuildStatusProviderReporter = teamCityBuildStatusProviderReporter;
            this.refreshThread = new Thread(this.RefreshBuildStatus);
            this.cancellationTokenSource = new CancellationTokenSource();
            this.manualResetEvent = new ManualResetEvent(false);
        }

        public event EventHandler<BuildStatus> BuildStatusChanged;

        public bool Connect(IRunOptions runOptions)
        {
            this.teamCityClient = new TeamCityClient(runOptions.HostName);
            this.refreshInterval = runOptions.RefreshInterval;
            this.buildTypeId = runOptions.BuildTypeId;
            this.teamCityBuildStatusProviderReporter?.ConnectingTo(runOptions.HostName);
            if (runOptions.Credentials != null)
            {
                this.teamCityClient.Connect(runOptions.Credentials.UserName, runOptions.Credentials.Password);
            }
            else
            {
                this.teamCityClient.ConnectAsGuest();
            }

            this.teamCityBuildStatusProviderReporter?.Authenticating();
            if (this.teamCityClient.Authenticate())
            {
                this.lastBuildStatus = new BuildStatus(false, "None");
                this.teamCityBuildStatusProviderReporter?.Authenticated();
                this.refreshThread.Start();
                return true;
            }

            this.teamCityBuildStatusProviderReporter?.AuthenticationFailed();
            return false;
        }

        public void Dispose()
        {
            this.manualResetEvent.Set();
            this.cancellationTokenSource.Cancel();
            this.refreshThread.Join();
            this.cancellationTokenSource.Dispose();
            this.manualResetEvent.Dispose();
        }

        private static bool GetHasPendingChanges(Change pendingChange, Build lastBuild)
        {
            return pendingChange != null && lastBuild.Changes.Change.All(x => x.Id != pendingChange.Id);
        }

        private BuildStatus GetStatus(string buildTypeId)
        {
            var pendingChanges = this.teamCityClient.Changes.ByBuildConfigId(buildTypeId).FirstOrDefault();
            var queuedBuild = this.teamCityClient.BuildQueue.ByBuildTypeLocator(BuildTypeLocator.WithId(buildTypeId)).FirstOrDefault();
            var runningBuild = this.teamCityClient.Builds.ByBuildLocator(BuildLocator.WithDimensions(BuildTypeLocator.WithId(buildTypeId), running: true)).FirstOrDefault();
            var lastBuild = this.teamCityClient.Builds
                .GetFields(BuildsField
                    .WithFields(BuildField.WithFields(
                        changes: ChangesField.WithFields(ChangeField.WithFields(true)), status: true))
                    .ToString()).LastBuildByBuildConfigId(buildTypeId);

            return new BuildStatus(queuedBuild != null | runningBuild != null | GetHasPendingChanges(pendingChanges, lastBuild), lastBuild.Status);
        }

        private void RefreshBuildStatus(object state)
        {
            var attempter = new Attempter(5);
            var cancellationToken = this.cancellationTokenSource.Token;
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var currentBuildStatus = this.GetStatus(this.buildTypeId);
                        if (currentBuildStatus.HasPendingBuild != this.lastBuildStatus.HasPendingBuild ||
                            currentBuildStatus.LastBuildStatus != this.lastBuildStatus.LastBuildStatus)
                        {
                            this.BuildStatusChanged?.Invoke(this, currentBuildStatus);
                        }

                        this.lastBuildStatus = currentBuildStatus;
                        attempter.Reset(5);
                        this.manualResetEvent.WaitOne(this.refreshInterval);
                    }
                    catch (OperationCanceledException)
                    {
                        throw;
                    }
                    catch (Exception e)
                    {
                        if (!attempter.Attempt())
                        {
                            this.teamCityBuildStatusProviderReporter?.RefreshBuildStatusFailed(attempter.MaxAttempts, e);
                            this.manualResetEvent.WaitOne(TimeSpan.FromSeconds(20));
                            attempter.Reset();
                        }
                        else
                        {
                            this.manualResetEvent.WaitOne(this.refreshInterval);
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
            }
        }
    }
}