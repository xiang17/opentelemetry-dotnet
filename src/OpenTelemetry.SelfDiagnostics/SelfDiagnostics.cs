// <copyright file="SelfDiagnostics.cs" company="OpenTelemetry Authors">
// Copyright The OpenTelemetry Authors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

using System;

namespace OpenTelemetry.SelfDiagnostics
{
    /// <summary>
    /// Self diagnostics module provides logging infrastructures for troubleshooting.
    /// </summary>
    public class SelfDiagnostics : IDisposable
    {
        private static readonly SelfDiagnostics Instance = new SelfDiagnostics();

        static SelfDiagnostics()
        {
            AppDomain.CurrentDomain.ProcessExit += (sender, eventArgs) =>
            {
                Instance.Dispose();
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelfDiagnostics"/> class.
        /// </summary>
        public SelfDiagnostics()
        {
        }

        /// <summary>
        /// This static method should be called so that static fields are initialized
        /// and static constructor is called by CLR.
        /// </summary>
        public static void EnsureInitialized()
        {
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                // TODO: dispose disposable class members
            }
        }
    }
}
