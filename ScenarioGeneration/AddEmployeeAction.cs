using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ConnectivityBenchmark.ScenarioGeneration
{
    /// <summary>
    /// Represents a class to add an employee to the employees list
    /// </summary>
    class AddEmployeeAction : WorkloadAction
    {
        /// <summary>
        /// The name for the new employee
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The team for the new employee
        /// </summary>
        public string Team { get; set; }

        /// <summary>
        /// Array of employees that the new employee knows
        /// </summary>
        public int[] Knows { get; set; }

        /// <summary>
        /// Adds the employee to the employees collection
        /// </summary>
        public override void Perform(IList<Employee> employees, StringBuilder log)
        {
            var employee = new Employee()
            {
                Name = Name,
                Team = Team
            };
            if (Knows != null)
            {
                for (int i = 0; i < Knows.Length; i++)
                {
                    var otherEmployee = employees[Knows[i]];
                    employee.Knows.Add(otherEmployee);
                    otherEmployee.Knows.Add(employee);
                }
            }
            employees.Add(employee);
            log.AppendFormat("Appended {0}, knows {1}", Name, string.Join(", ", employee.Knows.Select(e => e.Name)));
            log.AppendLine();
        }
    }
}
