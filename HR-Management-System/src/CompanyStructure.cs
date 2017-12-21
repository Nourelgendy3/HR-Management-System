using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS
{
    public class Department
    {
        public string departmentId;
        public string departmentName;

        public Department()
        {
            departmentId = "";
            departmentName = "";
        }

        public Department(string departmentId, string departmentName)
        {
            this.departmentId = departmentId;
            this.departmentName = departmentName;
        }
    }

    public class Employee
    {
        public string id;
        public string employeeName;
        public string hireDate;
        public string departmentNumber;

        public Employee()
        {
            this.id = "";
            this.employeeName = "";
            this.hireDate = "";
            this.departmentNumber = "";
        }
        public Employee(string id, string employeeName, string hireDate, string departmentNumber)
        {
            this.id = id;
            this.employeeName = employeeName;
            this.hireDate = hireDate;
            this.departmentNumber = departmentNumber;
        }
    }
}
