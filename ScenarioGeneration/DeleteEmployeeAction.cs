using System.Collections.Generic;
using System.Text;

namespace ConnectivityBenchmark.ScenarioGeneration
{
    /// <summary>
    /// Represents the deletion of an employee
    /// </summary>
    class DeleteEmployeeAction : WorkloadAction
    {
        /// <summary>
        /// The index of the empoloyee that should be removed
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Deletes the employee
        /// </summary>
        public override void Perform(IList<Employee> employees, StringBuilder log)
        {
            var employee = employees[Index];
            foreach (var known in employee.Knows)
            {
                known.Knows.Remove(employee);
            }
            employee.Knows.Clear();
            employees.RemoveAt(Index);
            log.AppendFormat("Delete {0}", employee.Name);
            log.AppendLine();
        }
    }
}
