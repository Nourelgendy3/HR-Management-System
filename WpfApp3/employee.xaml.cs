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
    public partial class employee : Window
    {
        public employee()
        {
            InitializeComponent();
        }

        private void Addbtn_Click(object sender, RoutedEventArgs e)
        {

            string id = IdBox.Text;
            string name = nameBox.Text;
            string date = DateBox.Text;
            string dep = DepartmentBox.Text;
            Department checkdep = Functions.getDepartmentName(dep);
            if (checkdep != null)
            {
                Functions.addEmployee(id, name, date, dep);
                this.Close();
            }
            else
                System.Windows.MessageBox.Show("Department Is not valid Employee Will Not Be Added");
               
        }
    }
}
