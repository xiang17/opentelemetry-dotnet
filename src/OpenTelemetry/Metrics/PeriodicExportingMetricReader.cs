// <copyright file="PeriodicExportingMetricReader.cs" company="OpenTelemetry Authors">
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
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OpenTelemetry.Metrics
{
    public class PeriodicExportingMetricReader : MetricReader
    {
        private MetricExporter exporter;
        private Task exportTask;
        private CancellationTokenSource token;

        public PeriodicExportingMetricReader(MetricExporter exporter, int exportIntervalMs)
        {
            this.exporter = exporter;
            this.token = new CancellationTokenSource();
            this.exportTask = new Task(() =>
            {
                while (!this.token.IsCancellationRequested)
                {
                    Task.Delay(exportIntervalMs).Wait();
                    this.Collect();
                }
            });

            this.exportTask.Start();
        }

        public override void OnCollect(IEnumerable<Metric> metrics)
        {
            this.exporter.Export(metrics);
        }

        public override AggregationTemporality GetAggregationTemporality()
        {
            return this.exporter.GetAggregationTemporality();
        }
    }
}