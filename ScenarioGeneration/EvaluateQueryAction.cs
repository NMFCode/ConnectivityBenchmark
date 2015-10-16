using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnectivityBenchmark.ScenarioGeneration
{
    /// <summary>
    /// Represents a workload action to evaluate the query and measure its count
    /// </summary>
    class EvaluateQueryAction : WorkloadAction
    {
        public Func<string> QueryAction { get; set; }

        /// <summary>
        /// Evaluate the query count and write the result to the log
        /// </summary>
        public override void Perform(IList<Employee> employees, StringBuilder log)
        {
            log.AppendLine("Evaluating Query: Result = " + QueryAction());
        }
    }
}
