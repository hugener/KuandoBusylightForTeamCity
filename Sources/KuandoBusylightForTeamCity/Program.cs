// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace KuandoBusylightForTeamCity
{
    using System;
    using System.ServiceProcess;
    using KuandoBusylightForTeamCity.CommandLine;
    using KuandoBusylightForTeamCity.Console;
    using KuandoBusylightForTeamCity.Service;
    using KuandoBusylightForTeamCity.Service.Installation;
    using Serilog;
    using Sundew.Base.Computation;
    using Sundew.Base.Text;
    using Sundew.CommandLine;

    public class Program
    {
        public static int Main()
        {
            var isConsole = Environment.UserInteractive;
            var logger = GetLogger(isConsole);
            LogMessage(isConsole, logger, "Kuando Busylight for TeamCity");

            var commandLineParser = new CommandLineParser<int, int>();
            commandLineParser.AddVerb(new InstallVerb(), installVerb => Result.From(new Installer().Install(installVerb.RunOptions), 0, new ParserError<int>(2)));
            commandLineParser.AddVerb(new UninstallVerb(), uninstallVerb => Result.From(new Installer().Uninstall(uninstallVerb.RunOptions), 0, new ParserError<int>(3)));
            commandLineParser.WithArguments(new RunOptions(null, null), options => Run(isConsole, options, logger));

            var result = commandLineParser.Parse(Environment.CommandLine, 1);
            if (result)
            {
                return result.Value;
            }

            LogMessage(isConsole, logger, result.Error.ToString((info, indent) => $"{' '.Repeat(indent)}Error code: {info}"));
            return result.Error.Info;
        }

        private static Result<int, ParserError<int>> Run(bool isConsole, IRunOptions runOptions, ILogger logger)
        {
            if (isConsole)
            {
                return Result.From(new ConsoleBuildStatusUpdater().Run(runOptions), 0, new ParserError<int>(1));
            }

            try
            {
                ServiceBase.Run(new BuildStatusUpdaterService(runOptions, logger));
            }
            catch (Exception e)
            {
                logger.Error(e, "Could not run service.");
                return Result.Error(new ParserError<int>(1));
            }

            return Result.Success(0);
        }

        private static ILogger GetLogger(bool isConsole)
        {
            var loggerConfiguration = new LoggerConfiguration();
            if (isConsole)
            {
                loggerConfiguration.WriteTo.Console();
            }
            else
            {
                loggerConfiguration.WriteTo.RollingFile("log-{Date}.txt", fileSizeLimitBytes: 5_000_000L, retainedFileCountLimit: 10);
            }

            return loggerConfiguration.CreateLogger();
        }

        private static void LogMessage(bool isConsole, ILogger logger, string message)
        {
            if (isConsole)
            {
                System.Console.WriteLine(message);
            }
            else
            {
                logger.Information(message);
            }
        }
    }
}
