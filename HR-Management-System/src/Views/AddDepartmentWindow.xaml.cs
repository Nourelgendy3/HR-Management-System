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
    /// Interaction logic for department.xaml
    /// </summary>
    public partial class AddDepartmentWindow : Window
    {
        public AddDepartmentWindow()
        {
            InitializeComponent();
        }

        private void AddDepBtn_Click(object sender, RoutedEventArgs e)
        {
            bool done;
            string depid = DepNoBox.Text;
            string depName = DepNameBox.Text;
             if (depid == "")
                System.Windows.MessageBox.Show("you must enter ID");
            else if (depName == "")
                System.Windows.MessageBox.Show("you must enter a name");
            if (depName != "" && depid != "")
            {
                done = FileControls.addDepartment(depid, depName);
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
}
