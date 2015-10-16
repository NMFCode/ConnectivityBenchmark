using System;
using System.Collections.Generic;

namespace ConnectivityBenchmark.ScenarioGeneration
{
    /// <summary>
    /// Generates scenarios for the example query
    /// </summary>
    class ScenarioGenerator
    {
        private static EvaluateQueryAction eval = new EvaluateQueryAction();

        public static Func<string> Evaluation
        {
            get
            {
                return eval.QueryAction;
            }
            set
            {
                eval.QueryAction = value;
            }
        }

        /// <summary>
        /// Generate a scenario with the given set of parameters
        /// </summary>
        /// <param name="ratio">The ratio of model queries to model manipulation elements</param>
        /// <param name="teamCount">The amount of generated teams</param>
        /// <param name="actions">The amount of model manipulation actions</param>
        /// <param name="employees">The amount of employees generated</param>
        /// <returns>The generated workload</returns>
        public static List<WorkloadAction> GenerateScenario(float ratio, int teamCount, int actions, out int employees)
        {
            var workload = new List<WorkloadAction>();
            var rand = new Random();
            var idx = 0;
            for (int i = 1; i <= teamCount; i++)
            {
                for (int j = 0; j < rand.Next(5, 100); j++)
                {
                    var knows = new List<int>(); 
                    while (knows.Count < idx - 2 && rand.NextDouble() * knows.Count < 10)
                    {
                        int nextIdx;
                        do
                        {
                            nextIdx = rand.Next(idx);
                        } while (knows.Contains(nextIdx));
                        knows.Add(nextIdx);
                    }
                    workload.Add(new AddEmployeeAction()
                    {
                        Name = "Employee" + idx.ToString(),
                        Team = "Team" + i.ToString(),
                        Knows = knows.ToArray()
                    });
                    idx++;
                }
            }
            employees = idx;
            var counter = 0f;
            for (int i = 0; i < actions; i++)
            {
                counter += ratio;
                while (counter > 1.0f)
                {
                    counter -= 1.0f;
                    workload.Add(eval);
                }
                var result = rand.NextDouble();
                if (result < 0.4)
                {
                    workload.Add(new RemoveKnownEmployeeAction()
                    {
                        Employee = rand.Next(idx)
                    });
                }
                else if (result < 0.8)
                {
                    var empIdx = rand.Next(idx);
                    var knownIdx = rand.Next(idx);
                    while (knownIdx == empIdx)
                    {
                        knownIdx = rand.Next(idx);
                    }
                    workload.Add(new AddKnownEmployeeAction()
                    {
                        Employee1 = empIdx,
                        Employee2 = knownIdx
                    });
                }
                else
                {
                    var empIdx = rand.Next(idx);
                    idx--;
                    workload.Add(new DeleteEmployeeAction()
                    {
                        Index = rand.Next(empIdx)
                    });
                }
            }
            return workload;
        }

    }
}
