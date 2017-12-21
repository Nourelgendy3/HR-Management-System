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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace HRMS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void CreatePanel(string EmpName, string EmpId, string HireDate, string DepName)
        {
            //inputs should be from file :
            string id = EmpId;
            string name = EmpName;
            string departmentName = DepName;
            string HDate = HireDate;

            // creating new controls
            StackPanel mainSp = new StackPanel();
            StackPanel empSp = new StackPanel();
            StackPanel depSp = new StackPanel();
            StackPanel btnSp = new StackPanel();
            Image image = new Image();
            Label ID = new Label();
            Label depID = new Label();
            Label Name = new Label();
            Label depName = new Label();
            Button editBtn = new Button();
            editBtn.Content = image;
            Department dep = Functions.getDepartment(departmentName);
            editBtn.Click += delegate (object sender, RoutedEventArgs e)
            {
                edit_btn(sender, e, EmpId,dep.departmentId);
            }; // function of edit button

            depName.Content = "Department Name : " + departmentName;
            depID.Content = "Hire Date : " + HDate;
            Name.Content = "Name:" + name;
            ID.Content = "Id:" + id;
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            path += "\\edit-icon.png";
            image.Source = new BitmapImage(new Uri(path));

            //style of created controls
            mainSp.Width = 750;
            mainSp.MaxWidth = 750;
            depSp.MinWidth = 400;
            empSp.MinWidth = 250;
            empSp.Orientation = Orientation.Vertical;
            mainSp.Orientation = Orientation.Horizontal;
            depSp.HorizontalAlignment = HorizontalAlignment.Right;
            editBtn.VerticalAlignment = VerticalAlignment.Top;
            editBtn.HorizontalAlignment = HorizontalAlignment.Right;
            depSp.Margin = new Thickness(40, 0, 0, 0);
            mainSp.Margin = new Thickness(70, 0, 0, 0);
            editBtn.Margin = new Thickness(18, 0, 0, 0);
            editBtn.BorderThickness = new Thickness(0, 0, 0, 0);
            editBtn.Background = Brushes.Transparent;
            mainSp.Background = Brushes.White;
            depName.FontSize = 20;
            depID.FontSize = 20;
            Name.FontSize = 20;
            ID.FontSize = 20;

            // listbox < mainsp<empsp , depsp , editbtn> >
            empSp.Children.Add(ID);
            empSp.Children.Add(Name);
            depSp.Children.Add(depName);
            depSp.Children.Add(depID);
            mainSp.Children.Add(empSp);
            mainSp.Children.Add(depSp);
            mainSp.Children.Add(editBtn);
            listBox.Items.Add(mainSp);
            // listBox.Items.Add(new ListBoxItem { Content=mainsp , Background = Brushes.White  });
        }
        private void AddDepartment_Click(object sender, RoutedEventArgs e)
        {
            department addDepartment = new department();
            addDepartment.Show();
        }
        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            employee addEmployee = new employee();
            addEmployee.Show();
        }
        private void Search_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Search.Text == "Search Employee Name")
            {
                Search.Text = "";

            }
        }

        private void edit_btn(object sender, RoutedEventArgs e, string empID, string depNo)
        {
            EditEmployee editPage = new EditEmployee();

            editPage.EditIdBox.Text = empID;
            editPage.EditDepartmentBox.Text = depNo.Trim('\0');
            editPage.Show();
        }

        private void srchbtn_Click(object sender, RoutedEventArgs e)
        {
            List<Employee> tmpEmployeesList = new List<Employee>();
            listBox.Items.Clear();
            Employee TmpEmployee = new Employee();
            Department tmpDep = new Department();
            string srch4 = Search.Text;
            if (srch4 != "")
            {
                tmpEmployeesList = Functions.getEmployee(srch4);
                int i = tmpEmployeesList.Count() - 1;
                Console.WriteLine(tmpEmployeesList.Count());
                if (i >= 0)
                {
                    while (i != -1)
                    {
                        tmpDep = Functions.getDepartmentName(tmpEmployeesList[i].departmentNumber);
                        string srchEmpName = tmpEmployeesList[i].employeeName.Trim('\0');
                        string srchDepName = tmpDep.departmentName.Trim('\0');
                        string srchEmpid = tmpEmployeesList[i].id.Trim('\0');
                        string srchHireDate = tmpEmployeesList[i].hireDate.Trim('\0');
                        CreatePanel(srchEmpName, srchEmpid, srchHireDate, srchDepName);
                        i--;
                    }
                }
                else
                    System.Windows.MessageBox.Show("Employee Not Found ");
            }
            else
                System.Windows.MessageBox.Show("Search is empty");

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Employee[] arr = Functions.getArrayEmp();
            Department tmpDep = new Department();
            if (arr!=null)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    tmpDep = Functions.getDepartmentName(arr[i].departmentNumber);

                    CreatePanel(arr[i].employeeName.Trim('\0'), arr[i].id.Trim('\0'), arr[i].hireDate.Trim('\0'), tmpDep.departmentName.Trim('\0'));

                }
            }
            Department[] deps = Functions.getArrayDep();
            if (deps != null)
            {
                for (int i = 0; i < deps.Length; i++)
                {
                    DepcomboBox.Items.Add(deps[i].departmentName.Trim('\0'));
                }
            }
        }



        private void button_Click(object sender, RoutedEventArgs e)
        {
            listBox.Items.Clear();
            Employee[] arr = Functions.getArrayEmp();
            Department tmpDep = new Department();
            if (arr != null)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    tmpDep = Functions.getDepartmentName(arr[i].departmentNumber);
                    CreatePanel(arr[i].employeeName.Trim('\0'), arr[i].id.Trim('\0'), arr[i].hireDate.Trim('\0'), tmpDep.departmentName.Trim('\0'));
                }
            }

            DepcomboBox.Items.Clear();
            Department[] deps = Functions.getArrayDep();
            if (deps != null)
            {
                for (int i = 0; i < deps.Length; i++)
                {
                    DepcomboBox.Items.Add(deps[i].departmentName.Trim('\0'));
                }
            }
        }
    

        private void DepcomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Department[] deps = Functions.getArrayDep();
            if (deps != null)
            {
                if (DepcomboBox.SelectedItem != null)
                {
                    for (int i = 0; i < deps.Length; i++)
                    {
                        if (DepcomboBox.SelectedItem.ToString() == deps[i].departmentName.Trim('\0'))
                        {
                            Console.WriteLine("gwa el check ");
                            Department tmpDep = new Department();
                            List<Employee> tmpEmployeesList = Functions.getEmpInDep(deps[i].departmentName);
                            int j = tmpEmployeesList.Count() - 1;
                            Console.WriteLine(tmpEmployeesList.Count());
                            if (j >= 0)
                            {
                                listBox.Items.Clear();
                                while (j != -1)
                                {
                                    tmpDep = Functions.getDepartmentName(tmpEmployeesList[j].departmentNumber);
                                    string srchEmpName = tmpEmployeesList[j].employeeName.Trim('\0');
                                    string srchDepName = tmpDep.departmentName.Trim('\0');
                                    string srchEmpid = tmpEmployeesList[j].id.Trim('\0');
                                    string srchHireDate = tmpEmployeesList[j].hireDate.Trim('\0');
                                    CreatePanel(srchEmpName, srchEmpid, srchHireDate, srchDepName);
                                    j--;
                                }
                            }
                            else
                                listBox.Items.Clear();
                        }
                    }
                }
            }
        }

        
        private void Search_KeyUp_1(object sender, KeyEventArgs e)
        {
            List<Employee> tmpEmployeesList = new List<Employee>();
            listBox.Items.Clear();
            Employee TmpEmployee = new Employee();
            Department tmpDep = new Department();
            string srch4 = Search.Text;
            if (srch4 != "")
            {
                tmpEmployeesList = Functions.getEmployee(srch4);
                if (tmpEmployeesList != null)
                {
                    int i = tmpEmployeesList.Count() - 1;
                    if (i >= 0)
                    {
                        while (i != -1)
                        {
                            tmpDep = Functions.getDepartmentName(tmpEmployeesList[i].departmentNumber);
                            string srchEmpName = tmpEmployeesList[i].employeeName.Trim('\0');
                            string srchDepName = tmpDep.departmentName.Trim('\0');
                            string srchEmpid = tmpEmployeesList[i].id.Trim('\0');
                            string srchHireDate = tmpEmployeesList[i].hireDate.Trim('\0');
                            CreatePanel(srchEmpName, srchEmpid, srchHireDate, srchDepName);
                            i--;
                        }
                    }
                    // else
                    // System.Windows.MessageBox.Show("Employee Not Found ");
                }
            }

        }
    }
}