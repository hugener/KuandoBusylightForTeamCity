// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BusylightController.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace KuandoBusylightForTeamCity
{
    using System;
    using System.Linq;
    using System.Threading;
    using Busylight;

    public class BusylightController : IDisposable
    {
        private readonly SDK busylightSdk;
        private readonly Timer timer;
        private BusylightColor busylightColor;
        private bool timerOn = false;
        private bool isAlternateColor;

        public BusylightController()
        {
            this.busylightSdk = new SDK();
            this.timer = new Timer(_ => this.OnTimerTick());
        }

        public bool Initialize()
        {
            var busylightDevice = this.busylightSdk.GetAttachedBusylightDeviceList().FirstOrDefault();
            if (busylightDevice == null || !busylightDevice.IsLightSupported)
            {
                return false;
            }

            return true;
        }

        public void Signal(BusylightColor busylightColor, bool isIndeterminate)
        {
            this.busylightColor = busylightColor;
            if (isIndeterminate)
            {
                this.timerOn = true;
                this.timer.Change(0, 800);
            }
            else
            {
                this.timerOn = false;
                this.timer.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
                if (busylightColor == BusylightColor.Red)
                {
                    this.busylightSdk.Pulse(busylightColor);
                }
                else
                {
                    this.busylightSdk.Light(busylightColor);
                }
            }
        }

        public void Dispose()
        {
            this.timerOn = false;
            this.timer.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
            this.timer?.Dispose();
            this.busylightSdk.Terminate();
        }

        private void OnTimerTick()
        {
            if (this.timerOn)
            {
                this.busylightSdk.Light(this.isAlternateColor ? this.busylightColor : BusylightColor.Yellow);
                this.isAlternateColor = !this.isAlternateColor;
            }
        }
    }
}