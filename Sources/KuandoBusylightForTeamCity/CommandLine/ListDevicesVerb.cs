// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ListDevicesVerb.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace KuandoBusylightForTeamCity.CommandLine
{
    using Sundew.CommandLine;

    public class ListDevicesVerb : IVerb
    {
        public string HelpText => "Lists the connected Busylight devices";

        public string Name => "listdevices";

        public void Configure(IArgumentsBuilder argumentsBuilder)
        {
        }
    }
}