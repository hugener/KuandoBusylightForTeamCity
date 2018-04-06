// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectInstaller.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace KuandoBusylightForTeamCity.Service.Installation
{
    using System.Collections;
    using System.ComponentModel;
    using System.ServiceProcess;
    using KuandoBusylightForTeamCity.CommandLine;
    using Sundew.Base.Computation;
    using Sundew.CommandLine;

    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            this.InitializeComponent();
        }

        protected override void OnBeforeInstall(IDictionary savedState)
        {
            var arguments = this.Context.Parameters["a"];
            var assemblyPath = @"""" + this.Context.Parameters["assemblyPath"] + @""" " + arguments;
            this.Context.Parameters["assemblypath"] = assemblyPath;
            this.ParseAndSetServiceName(arguments);

            base.OnBeforeInstall(savedState);
        }

        protected override void OnBeforeUninstall(IDictionary savedState)
        {
            this.SetServiceName(this.Context.Parameters["b"]);
            if (this.serviceController.Status == ServiceControllerStatus.Running)
            {
                this.serviceController.Stop();
            }

            base.OnBeforeUninstall(savedState);
        }

        protected override void OnAfterInstall(IDictionary savedState)
        {
            if (this.serviceController.Status == ServiceControllerStatus.Stopped)
            {
                this.serviceController.Start();
            }

            base.OnAfterInstall(savedState);
        }

        private void ParseAndSetServiceName(string arguments)
        {
            var commandLineParser = new CommandLineParser<int, int>();
            commandLineParser.WithArguments(new RunOptions(null, null), options =>
            {
                var buildTypeId = options.BuildTypeId;
                this.SetServiceName(buildTypeId);
                return Result.Success(0);
            });
            commandLineParser.Parse(arguments);
        }

        private void SetServiceName(string buildTypeId)
        {
            var servicePostfix = $": {buildTypeId}";
            this.serviceInstaller.ServiceName += servicePostfix;
            this.serviceInstaller.DisplayName += servicePostfix;
            this.serviceController.ServiceName = this.serviceInstaller.ServiceName;
        }
    }
}
