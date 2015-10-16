using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectivityBenchmark.ScenarioGeneration
{
    class RemoveKnownEmployeeAction : WorkloadAction
    {
        public int Employee { get; set; }

        public override void Perform(IList<Employee> employees, StringBuilder log)
        {
            var employee = employees[Employee];
            if (employee.Knows.Count > 0)
            {
                var other = employee.Knows[0];
                employee.Knows.RemoveAt(0);
                other.Knows.Remove(employee);

                log.AppendFormat("Remove connection between {0} and {1}", employee.Name, other.Name);
                log.AppendLine();
            }
        }
    }
}
