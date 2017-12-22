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
using MaterialDesignThemes.Wpf;

namespace HRMS
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        private static StackPanel mainPanel;
        private static ComboBox departmentFilter;

        public HomeWindow()
        {
            mainPanel = new StackPanel();
            departmentFilter = new ComboBox();
            mainPanel.HorizontalAlignment = HorizontalAlignment.Center;
            InitializeComponent();
            logo.PreviewMouseDown += delegate
            {
                departmentFilter.SelectedIndex = 0;
                Search.Text = null;
                reload(FileControls.getArrayEmp());
            };
            content.Content = mainPanel;
            departmentFilter = DepcomboBox;
            reload(FileControls.getArrayEmp());
        }

        public static void reload(Employee[] employees)
        {
            mainPanel.Children.Clear();
            Department tmpDep = new Department();
            if (employees != null)
            {
                for (int i = 0; i < employees.Length; i++)
                {
                    tmpDep = FileControls.getDepartmentName(employees[i].departmentNumber);
                    CreatePanel(employees[i].employeeName.Trim('\0'), employees[i].id.Trim('\0'), employees[i].hireDate.Trim('\0'), tmpDep.departmentName.Trim('\0'));
                }
            }

            int currentChoice = departmentFilter.SelectedIndex;
            departmentFilter.Items.Clear();
            ComboBoxItem defaultChoice = new ComboBoxItem();
            defaultChoice.IsSelected = true;
            defaultChoice.IsEnabled = false;
            defaultChoice.Content = "Department Filter";
            departmentFilter.Items.Add(defaultChoice);

            Department[] deps = FileControls.getArrayDep();
            if (deps != null)
            {
                for (int i = 0; i < deps.Length; i++)
                {
                    departmentFilter.Items.Add(deps[i].departmentName.Trim('\0'));
                }
            }

            if (currentChoice == -1) currentChoice = 0;
            departmentFilter.SelectedIndex = currentChoice;
        }

        public static void CreatePanel(string EmpName, string EmpId, string HireDate, string DepName)
        {
            Card main = new Card();
            main.Padding = new Thickness(10);
            main.Margin = new Thickness(4);
            main.Width = 550;
            StackPanel innerContent = new StackPanel();
            innerContent.Orientation = Orientation.Horizontal;

            StackPanel left = new StackPanel();
            TextBlock idText = new TextBlock();
            idText.Text = "ID: " + EmpId;
            idText.Width = 180;
            idText.Margin = new Thickness(5);
            TextBlock nameText = new TextBlock();
            nameText.Text = "Name: " + EmpName;
            nameText.Width = 180;
            nameText.Margin = new Thickness(5);
            left.Children.Add(idText);
            left.Children.Add(nameText);

            StackPanel right = new StackPanel();
            TextBlock depText = new TextBlock();
            depText.Text = "Department: " + DepName;
            depText.Width = 240;
            depText.Margin = new Thickness(5);
            TextBlock dateText = new TextBlock();
            dateText.Text = "Hire Date: " + HireDate;
            dateText.Width = 240;
            dateText.Margin = new Thickness(5);
            right.Children.Add(depText);
            right.Children.Add(dateText);

            Button editBtn = new Button();
            editBtn.Margin = new Thickness(20, 0, 0, 0);
            editBtn.Height = double.NaN;
            editBtn.Style = Application.Current.FindResource("btnStyle") as Style;
            editBtn.Click += delegate
            {
                editEmployee(EmpId, FileControls.getDepartment(DepName).departmentId, EmpName, HireDate);
            };
            //editBtn.Background = new SolidColorBrush(Color.FromRgb(21, 206, 60));
            PackIcon penIcon = new PackIcon();
            penIcon.Kind = PackIconKind.Pencil;
            penIcon.Width = 24;
            penIcon.Height = 24;
            penIcon.Foreground = new SolidColorBrush(Color.FromRgb(45, 204, 112));
            editBtn.Content = penIcon;

            innerContent.Children.Add(left);
            innerContent.Children.Add(right);
            innerContent.Children.Add(editBtn);
            main.Content = innerContent;
            mainPanel.Children.Add(main);
        }

        private void openAddDepartment(object sender, RoutedEventArgs e)
        {
            AddDepartmentWindow addDepartment = new AddDepartmentWindow();
            addDepartment.Show();
        }

        private void openAddEmployee(object sender, RoutedEventArgs e)
        {
            AddEmployeeWindow addEmployee = new AddEmployeeWindow();
            addEmployee.Show();
        }

        private static void editEmployee(string empID, string depNo, string name, string date)
        {
            EditEmployeeWindow editPage = new EditEmployeeWindow();
            editPage.EditIdBox.Text = empID;
            Department deps = FileControls.getDepartmentName(depNo);
            ComboBoxItem defaultChoice = new ComboBoxItem();
            defaultChoice.IsSelected = true;
            defaultChoice.IsEnabled = false;
            defaultChoice.Content = deps.departmentName;
            editPage.EditDepartmentBox.Items.Add(defaultChoice);
            editPage.EditDateBox.Text = date;
            editPage.EditNameBox.Text = name;
            editPage.Show();
        }

        // Implementation Needs to be changed for a better looking code
        private void DepcomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search.Clear();
            Department[] deps = FileControls.getArrayDep();
            if (deps != null)
            {
                if (DepcomboBox.SelectedItem != null)
                {
                    for (int i = 0; i < deps.Length; i++)
                    {
                        if (DepcomboBox.SelectedItem.ToString() == deps[i].departmentName.Trim('\0'))
                        {
                            Department tmpDep = new Department();
                            List<Employee> tmpEmployeesList = FileControls.getEmpInDep(deps[i].departmentName);
                            int j = tmpEmployeesList.Count() - 1;
                            Console.WriteLine(tmpEmployeesList.Count());
                            if (j >= 0)
                            {
                                mainPanel.Children.Clear();
                                while (j != -1)
                                {
                                    tmpDep = FileControls.getDepartmentName(tmpEmployeesList[j].departmentNumber);
                                    string srchEmpName = tmpEmployeesList[j].employeeName.Trim('\0');
                                    string srchDepName = tmpDep.departmentName.Trim('\0');
                                    string srchEmpid = tmpEmployeesList[j].id.Trim('\0');
                                    string srchHireDate = tmpEmployeesList[j].hireDate.Trim('\0');
                                    CreatePanel(srchEmpName, srchEmpid, srchHireDate, srchDepName);
                                    j--;
                                }
                            }
                            else
                            {
                                mainPanel.Children.Clear();
                            }
                        }
                    }
                }
            }
        }

        // Implementation Needs to be changed for a better looking code
        private void OnSearch(object sender, KeyEventArgs e)
        {
            List<Employee> tmpEmployeesList = new List<Employee>();
            Employee TmpEmployee = new Employee();
            Department tmpDep = new Department();
            string srchQuery = Search.Text;
            srchQuery = srchQuery.ToLower();
            mainPanel.Children.Clear();
            if (srchQuery != "")
            {
                tmpEmployeesList = FileControls.getEmployee(srchQuery);
                if (tmpEmployeesList != null)
                {
                    int i = tmpEmployeesList.Count() - 1;
                    if (i >= 0)
                    {
                        while (i != -1)
                        {

                            tmpDep = FileControls.getDepartmentName(tmpEmployeesList[i].departmentNumber);

                            if (tmpDep.departmentName == departmentFilter.SelectedItem.ToString() || departmentFilter.SelectedIndex == 0)
                            {
                                string srchEmpName = tmpEmployeesList[i].employeeName.Trim('\0');
                                string srchDepName = tmpDep.departmentName.Trim('\0');
                                string srchEmpid = tmpEmployeesList[i].id.Trim('\0');
                                string srchHireDate = tmpEmployeesList[i].hireDate.Trim('\0');
                                CreatePanel(srchEmpName, srchEmpid, srchHireDate, srchDepName);
                                i--;
                            }
                            else
                            {
                                i--;
                            }
                        }

                    }
                }
            }
            else
            {
                reload(FileControls.getArrayEmp());
            }
        }
    }
}