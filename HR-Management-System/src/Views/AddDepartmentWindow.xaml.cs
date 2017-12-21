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
            string depid = DepNoBox.Text;
            string depName = DepNameBox.Text;
            FileControls.addDepartment(depid, depName);           
            this.Close();

        }

        private void DepNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
