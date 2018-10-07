// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BusylightController.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace KuandoBusylightForTeamCity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using Busylight;

    public class BusylightController : IDisposable
    {
        private readonly SDK busylightSdk;
        private readonly Timer timer;
        private readonly IReadOnlyList<string> hidDeviceIds;
        private BusylightColor busylightColor;
        private bool timerOn = false;
        private bool isAlternateColor;
        private object busylightHandler;
        private ICollection<BusylightDevice> busylightDeviceList;

        public BusylightController(IReadOnlyList<string> hidDeviceIds)
        {
            this.busylightSdk = new SDK();
            this.timer = new Timer(_ => this.OnTimerTick());
            this.hidDeviceIds = hidDeviceIds;
        }

        public bool Initialize()
        {
            if (this.hidDeviceIds.Any())
            {
                this.busylightHandler = this.busylightSdk.GetType()
                    .GetField("busylightHandler", BindingFlags.Instance | BindingFlags.NonPublic)
                    .GetValue(this.busylightSdk);
                this.busylightDeviceList = (List<BusylightDevice>)this.busylightHandler.GetType()
                    .GetField("mylist", BindingFlags.Instance | BindingFlags.NonPublic)
                    .GetValue(this.busylightHandler);
                this.busylightSdk.BusyLightChanged += this.OnBusylightSdkBusyLightChanged;
                this.RefreshDeviceList();
            }
            else
            {
                this.busylightDeviceList = this.busylightSdk.GetAttachedBusylightDeviceList();
            }

            var busylightDevice = this.busylightDeviceList.FirstOrDefault();
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
            this.busylightSdk.BusyLightChanged -= this.OnBusylightSdkBusyLightChanged;
            this.timerOn = false;
            this.timer.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
            this.timer?.Dispose();
            this.busylightSdk.Terminate();
        }

        private void OnBusylightSdkBusyLightChanged(object sender, EventArgs e)
        {
            this.RefreshDeviceList();
        }

        private void RefreshDeviceList()
        {
            foreach (var availableBusylightDevice in this.busylightDeviceList.ToArray())
            {
                var hidDevicePath = (string)availableBusylightDevice.GetType()
                    .GetField("HIDDevicePath", BindingFlags.Instance | BindingFlags.NonPublic)
                    .GetValue(availableBusylightDevice);
                if (!this.hidDeviceIds.Any(x => hidDevicePath.Contains(x)))
                {
                    this.busylightDeviceList.Remove(availableBusylightDevice);
                }
            }
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