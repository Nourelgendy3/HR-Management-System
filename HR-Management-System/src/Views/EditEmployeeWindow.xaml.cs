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
    /// <summary>
    /// Interaction logic for EditEmployee.xaml
    /// </summary>
    public partial class EditEmployeeWindow : Window
    {
        string BeforeEditId;

        public EditEmployeeWindow()
        {
            InitializeComponent();
        }

        private void EditLoad(object sender, RoutedEventArgs e)
        {
            BeforeEditId = EditIdBox.Text;
            Department[] deps = FileControls.getArrayDep();
            if (deps != null)
            {
                for (int i = 0; i < deps.Length; i++)
                {
                    EditDepartmentBox.Items.Add(deps[i].departmentName.Trim('\0'));
                }
            }
        }
       
        private void SubmitEdit_Click(object sender, RoutedEventArgs e)
        {
            string editId = EditIdBox.Text;
            string editName = EditNameBox.Text;
            string editDate = EditDateBox.Text;
            Department dep = FileControls.getDepartment(EditDepartmentBox.Text);
            string editDep = dep.departmentId;
           
            bool done = FileControls.editEmployee(editId, BeforeEditId, editName, editDate, editDep);
            if (!done)
            {
                System.Windows.MessageBox.Show("Id already used");
            }
            else
            {
                HomeWindow.reload(FileControls.getArrayEmp());
                this.Close();
            }
        }
    }
}
