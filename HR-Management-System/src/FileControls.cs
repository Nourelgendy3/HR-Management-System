using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS
{
    public class FileControls
    {
        //  Add employees to file and check if id is already used
        public static bool addEmployee(string employeeId, string employeeName, string hireDate, string departmentNumber, string fileName = "employees.txt")
        {
            if (File.Exists("employees.txt"))
            {
                FileStream fs = new FileStream("employees.txt", FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                while (sr.Peek() != -1)
                {
                    char[] eid = new char[5];
                    char[] en = new char[20];
                    char[] hr = new char[10];
                    char[] dn = new char[5];
                    sr.Read(eid, 0, 5);
                    sr.Read(en, 0, 20);
                    sr.Read(hr, 0, 10);
                    sr.Read(dn, 0, 5);
                    string id = new string(eid);
                    string name = new string(en);
                    string hireD = new string(hr);
                    string depnumber = new string(dn);
                    if (id.CompareTo(employeeId) == 0)
                    {
                        sr.Close();
                        fs.Close();
                        return false;
                    }
                }
                sr.Close();
                fs.Close();
            }
            FileStream f = new FileStream("employees.txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(f);
            char[] emp_id = new char[5];
            char[] emp_name = new char[20];
            char[] hire_date = new char[10];
            char[] departmentNum = new char[5];

            employeeId.CopyTo(0, emp_id, 0, employeeId.Length);
            employeeName.CopyTo(0, emp_name, 0, employeeName.Length);
            hireDate.CopyTo(0, hire_date, 0, hireDate.Length);
            departmentNumber.CopyTo(0, departmentNum, 0, departmentNumber.Length);

            sw.Write(emp_id, 0, 5);
            sw.Write(emp_name, 0, 20);
            sw.Write(hire_date, 0, 10);
            sw.Write(departmentNum, 0, 5);
            sw.Close();
            f.Close();
            return true;
        }

        //  Add departments to file and check if department number is already used
        public static bool addDepartment(string departmentId, string departmentName, string fileName = "departments.txt")
        {
            if (File.Exists("departments.txt"))
            {
                FileStream fs = new FileStream("departments.txt", FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                while (sr.Peek() != -1)
                {
                    char[] di = new char[5];
                    char[] dn = new char[20];
                    sr.Read(di, 0, 5);
                    sr.Read(dn, 0, 20);
                    string id = new string(di);
                    string name = new string(dn);

                    if (id.CompareTo(departmentId) == 0)
                    {
                        sr.Close();
                        fs.Close();
                        return false;
                    }

                }
                sr.Close();
                fs.Close();
            }
            FileStream f = new FileStream(fileName, FileMode.Append);
            StreamWriter sw = new StreamWriter(f);
            char[] departmentID = new char[5];
            char[] DepartmentName = new char[20];

            departmentId.CopyTo(0, departmentID, 0, departmentId.Length);
            departmentName.CopyTo(0, DepartmentName, 0, departmentName.Length);

            sw.Write(departmentID, 0, 5);
            sw.Write(DepartmentName, 0, 20);
            sw.Close();
            f.Close();
            return true;
        }

        // function return department<number,name> and takes department name as a parameter
        public static Department getDepartment(string reqDepName)
        {
            Department department = new Department();
            FileStream fs = new FileStream("departments.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            while (sr.Peek() != -1)
            {
                char[] ID = new char[5];
                char[] Name = new char[20];
                sr.Read(ID, 0, 5);
                sr.Read(Name, 0, 20);
                string id = new string(ID);
                string name = new string(Name);

                if (name.CompareTo(reqDepName) == 0)
                {
                    department.departmentName = reqDepName;
                    department.departmentId = id;
                    sr.Close();
                    fs.Close();
                    return department;
                }
            }
            sr.Close();
            fs.Close();
            return null;
        }

        // function return department<number,name> and takes department id as a parameter
        public static Department getDepartmentName(string reqDepId)
        {
            Department department = new Department();
            if (File.Exists("departments.txt"))
            {
                FileStream fs = new FileStream("departments.txt", FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                while (sr.Peek() != -1)
                {
                    char[] ID = new char[5];
                    char[] Name = new char[20];
                    sr.Read(ID, 0, 5);
                    sr.Read(Name, 0, 20);
                    string id = new string(ID).Trim('\0');
                    string name = new string(Name).Trim('\0');
                    if (id.CompareTo(reqDepId) == 0)
                    {
                        department.departmentId = reqDepId;
                        department.departmentName = name;
                        sr.Close();
                        fs.Close();
                        return department;
                    }
                }
                sr.Close();
                fs.Close();
                return null;
            }
            return null;
        }

        // function read emplyees file return list of employees
        public static List<Employee> getEmployee(string reqEmpName)
        {
            if (File.Exists("employees.txt"))
            {
                List<Employee> employeesList = new List<Employee>();
                FileStream fs = new FileStream("employees.txt", FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                while (sr.Peek() != -1)
                {
                    Employee employee = new Employee();
                    char[] empID = new char[5];
                    char[] employeeName = new char[20];
                    char[] hire_Date = new char[10];
                    char[] departmentNum = new char[5];
                    sr.Read(empID, 0, 5);
                    sr.Read(employeeName, 0, 20);
                    sr.Read(hire_Date, 0, 10);
                    sr.Read(departmentNum, 0, 5);

                    string id = new string(empID);
                    string name = new string(employeeName);
                    string hireD = new string(hire_Date);
                    string depnumber = new string(departmentNum);
                    if (improveSearch(reqEmpName, name))
                    {
                        employee.id = id;
                        employee.employeeName = name;
                        employee.hireDate = hireD;
                        employee.departmentNumber = depnumber;
                        employeesList.Add(employee);
                    }
                }
                sr.Close();
                fs.Close();
                return employeesList;
            }
            return null;
        }

        // Function to edit employee's information
        public static bool editEmployee(string reqID, string serID, string reqName, string reqHireDate, string reqDepnum, string fileName = "employees.txt")
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(fs);
            StreamWriter sw = new StreamWriter(fs);
            int recNum = 0;
            bool isFound = false;

            char[] rID = new char[5];
            char[] rname = new char[20];
            char[] rhiredate = new char[10];
            char[] rdepnum = new char[5];
            reqID.CopyTo(0, rID, 0, reqID.Length);
            reqName.CopyTo(0, rname, 0, reqName.Length);
            reqHireDate.CopyTo(0, rhiredate, 0, reqHireDate.Length);
            reqDepnum.CopyTo(0, rdepnum, 0, reqDepnum.Length);

            char[] repID = new char[5];
            char[] repName = new char[20];
            char[] repHireDate = new char[10];
            char[] repDepnum = new char[5];
            string record = " ";
            while (sr.Peek() != -1)
            {
                char[] ID = new char[5];
                char[] name = new char[20];
                char[] hiredate = new char[10];
                char[] depnum = new char[5];
                recNum++;
                char[] line = new char[40];
                sr.Read(ID, 0, 5);
                sr.Read(name, 0, 20);
                sr.Read(hiredate, 0, 10);
                sr.Read(depnum, 0, 5);

                string recname = new string(name);
                string recID = new string(ID);
                string rechiredate = new string(hiredate);
                string recdepnum = new string(depnum);

                if (recID.Trim('\0').CompareTo(reqID) == 0 && reqID.Trim('\0').CompareTo(serID) != 0)
                {
                    sw.Close();
                    sr.Close();
                    fs.Close();
                    return false;
                }
                if (recID.Trim('\0').CompareTo(serID) == 0)
                {
                    record = recID + recname + rechiredate + recdepnum;
                    recID.CopyTo(0, repID, 0, 5);
                    recname.CopyTo(0, repName, 0, 20);
                    rechiredate.CopyTo(0, repHireDate, 0, 10);
                    recdepnum.CopyTo(0, repDepnum, 0, 5);
                    isFound = true;
                }
            }
            sr.Close();
            if (isFound)
            {
                string strID = "", strname = "", strhiredate = "", strdepnum = "";
                if (reqID == "")
                {
                    strID = new string(repID);
                }
                else
                {
                    strID = new string(rID);
                }
                if (reqHireDate == "")
                {
                    strhiredate = new string(repHireDate);
                }
                else
                {
                    strhiredate = new string(rhiredate);
                }

                if (reqDepnum == "")
                {
                    strdepnum = new string(repDepnum);
                }
                else
                {
                    strdepnum = new string(rdepnum);
                }
                if (reqName == "")
                {
                    strname = new string(repName);
                }
                else
                {
                    strname = new string(rname);
                }
                string reqrec = strID + strname + strhiredate + strdepnum;
                string s = File.ReadAllText(fileName);
                s = s.Replace(record, reqrec);
                File.WriteAllText(fileName, s);
                return true;
            }

            return false;
        }

        //  function return all employees in required department
        public static List<Employee> getEmpInDep(string reqDepName)
        {
            Department dep = new Department();
            dep = getDepartment(reqDepName);
            string reqDepID = dep.departmentId;

            FileStream fs = new FileStream("employees.txt", FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);
            List<Employee> employeesList = new List<Employee>();

            while (sr.Peek() != -1)
            {
                Employee employee = new Employee();
                char[] empID = new char[5];
                char[] employeeName = new char[20];
                char[] hire_Date = new char[10];
                char[] departmentNum = new char[5];
                sr.Read(empID, 0, 5);
                sr.Read(employeeName, 0, 20);
                sr.Read(hire_Date, 0, 10);
                sr.Read(departmentNum, 0, 5);

                string id = new string(empID);
                string name = new string(employeeName);
                string hireD = new string(hire_Date);
                string depnumber = new string(departmentNum);
                if (depnumber.CompareTo(reqDepID) == 0)
                {
                    employee.id = id;
                    employee.employeeName = name;
                    employee.hireDate = hireD;
                    employee.departmentNumber = depnumber;
                    employeesList.Add(employee);
                }
            }
            sr.Close();
            fs.Close();
            return employeesList;
        }

        // Function return all departments in department file
        public static Department[] getArrayDep()
        {
            if (File.Exists("departments.txt"))
            {
                FileStream fs = new FileStream("departments.txt", FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                long line = fs.Length;
                int recordsnum = (int)line / 25;
                Department[] departments = new Department[recordsnum];
                int d = 0;

                while (sr.Peek() != -1)
                {
                    char[] depID = new char[5];
                    char[] depName = new char[20];
                    sr.Read(depID, 0, 5);
                    sr.Read(depName, 0, 20);
                    string id = new string(depID);
                    string name = new string(depName);
                    Department dep = new Department();

                    dep.departmentId = id;
                    dep.departmentName = name;
                    departments[d] = dep;
                    d++;
                }
                sr.Close();
                fs.Close();
                return departments;
            }
            return null;
        }

        // function return all employees in employees file
        public static Employee[] getArrayEmp()
        {
            if (File.Exists("employees.txt"))
            {
                FileStream fs = new FileStream("employees.txt", FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                long line = fs.Length;
                int recordsnum = (int)line / 40;
                Employee[] employees = new Employee[recordsnum];
                int c = 0;

                while (sr.Peek() != -1)
                {

                    char[] empID = new char[5];
                    char[] employeeName = new char[20];
                    char[] hire_Date = new char[10];
                    char[] departmentNum = new char[5];
                    sr.Read(empID, 0, 5);
                    sr.Read(employeeName, 0, 20);
                    sr.Read(hire_Date, 0, 10);
                    sr.Read(departmentNum, 0, 5);
                    string ID = new string(empID);
                    string name = new string(employeeName);
                    string hireD = new string(hire_Date);
                    string depnumber = new string(departmentNum);
                    Employee emp = new Employee();

                    emp.id = ID;
                    emp.employeeName = name;
                    emp.hireDate = hireD;
                    emp.departmentNumber = depnumber;
                    employees[c] = emp;
                    c++;
                }
                sr.Close();
                fs.Close();
                return employees;
            }
            return null;
        }

        public static bool improveSearch(string query, string name)
        {
            if (query.Length > name.Length)
                return false;
            for (int i = 0; i < query.Length; i++)
            {
                if (query[i] != name[i])
                    return false;
            }
            return true;
        }
    }
}
