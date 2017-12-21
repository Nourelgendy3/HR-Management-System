using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HRMS
{
    public partial class AddEmployeeWindow : Window
    {
        public AddEmployeeWindow()
        {
            InitializeComponent();
        }
        private void addEmpLoad(object sender, RoutedEventArgs e)
        {
            Department[] deps = FileControls.getArrayDep();
            if (deps != null)
            {
                for (int i = 0; i < deps.Length; i++)
                {
                    DepartmentBox.Items.Add(deps[i].departmentName.Trim('\0'));
                }
            }
        }

        private void Addbtn_Click(object sender, RoutedEventArgs e)
        {
            Department deps = FileControls.getDepartment(DepartmentBox.Text);
            string id = IdBox.Text;
            string name = nameBox.Text;
            string date = DateBox.Text;
            string dep = deps.departmentId;
            Department checkdep = FileControls.getDepartmentName(dep);
            if (checkdep != null)
            {
                FileControls.addEmployee(id, name, date, dep);
                HomeWindow.reload(FileControls.getArrayEmp());
                this.Close();
            }
            else
                System.Windows.MessageBox.Show("Department Is not valid Employee Will Not Be Added");
               
        }
    }
}
