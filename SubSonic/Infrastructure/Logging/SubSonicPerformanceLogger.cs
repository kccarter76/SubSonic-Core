﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SubSonic.Infrastructure.Logging
{
    public class SubSonicPerformanceLogger<TCategoryName>
        : IPerformanceLogger<TCategoryName>
        , IDisposable
    {
        private DateTime start;
        private DateTime end;
        private readonly ILogger logger;
        private string name;

        public SubSonicPerformanceLogger(ILogger logger, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException(SubSonicErrorMessages.MissingNameArgumentException, nameof(name));
            }

            this.logger = logger;// ?? throw new ArgumentNullException(nameof(logger));
            
            StartClock(name);
        }

        public bool IsPerformanceLoggingEnabled => logger.IsNotNull() && logger.IsEnabled(LogLevel.Debug);

        public string NameOfScope => $"{typeof(TCategoryName).Name}::{name}";

        public double TotalMilliseconds => (end - start).TotalMilliseconds;

        public double TotalSeconds => (end - start).TotalSeconds;

        public double TotalMinutes => (end - start).TotalMinutes;

        public void StartClock(string name)
        {
            this.name = name;
            this.start = DateTime.Now;

            if (IsPerformanceLoggingEnabled)
            {
                logger.LogDebug(SubSonicLogging.PerformanceStart, NameOfScope, start);
            }
        }

        public void EndClock()
        {
            this.end = DateTime.Now;

            if (IsPerformanceLoggingEnabled)
            {
                logger.LogDebug(SubSonicLogging.PerformanceEnd, NameOfScope, TotalSeconds);
            }
        }

        //public async Task EndClockAsync()
        //{
        //    EndClock();
        //}

        //public async Task DisposeAsync()
        //{
        //    await EndClockAsync().ConfigureAwait(true);
        //}

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    EndClock();
                    //Task.Factory.StartNew(async () => await DisposeAsync().ConfigureAwait(true), default(CancellationToken), TaskCreationOptions.AttachedToParent, TaskScheduler.Default);
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~SubSonicPerformanceLogger()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}

