using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectivityBenchmark.ScenarioGeneration
{
    class AddKnownEmployeeAction : WorkloadAction
    {
        public int Employee1 { get; set; }

        public int Employee2 { get; set; }

        public override void Perform(IList<Employee> employees, StringBuilder log)
        {
            var employee1 = employees[Employee1];
            var employee2 = employees[Employee2];
            employee1.Knows.Add(employee2);
            employee2.Knows.Add(employee1);
            log.AppendFormat("Connect {0} and {1}", employee1.Name, employee2.Name);
            log.AppendLine();
        }
    }
}
