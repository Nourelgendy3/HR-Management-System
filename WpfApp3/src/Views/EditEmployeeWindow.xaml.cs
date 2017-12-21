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
        }
       
        private void SubmitEdit_Click(object sender, RoutedEventArgs e)
        {
            string editId = EditIdBox.Text;
            string editName = EditNameBox.Text;
            string editDate = EditDateBox.Text;
            string editDep = EditDepartmentBox.Text;
            Department checkdep = FileControls.getDepartmentName(editDep);
            if (checkdep != null)
            {
                FileControls.editEmployee(editId, BeforeEditId, editName, editDate, editDep);
                this.Close();
            }
            else
                System.Windows.MessageBox.Show(" department isn't valid ");

            
        }
    }
}
