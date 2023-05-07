using System;
using System.Windows;
using System.Windows.Data;
using CaseStudyService.Interface;
using Newtonsoft.Json;
using CaseStudyService.Model;
using PagedList;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace UPSCaseStudy
{    
    public partial class MainWindow : Window
    {
        private readonly IEmployeeService _service;
        IPagedList<Employee> list;
        int pageNumber = 1; string messageBoxText = "", caption = "";
        public MainWindow(IEmployeeService service)
        {           
            _service = service;
            InitializeComponent();
        }
        private void LoadEmployees(object sender, RoutedEventArgs e)
        {
            if (txtSearchEmployee.Text == null || txtSearchEmployee.Text =="")                           
                GetEmployees();           
            else            
                GetEmployeeById(Convert.ToInt32(txtSearchEmployee.Text));            
        }

        private void GetEmployeeById(int id)
        {            
            List<Employee> employeelist = new List<Employee>();
            try {
                var response = _service.GetEmployeebyID(id);
                if (response != null) {
                    employeelist.Add(response);
                    dgEmployee.DataContext = employeelist;
                    btnNextPage.IsEnabled = false;
                    btnprevPage.IsEnabled = false;
                    lblpageNumber.Content = string.Format("Page {0}", pageNumber);
                }
            }
            catch {
                messageBoxText = "Please try again after sometime.";
                caption = "Message";
            }
        }
        private async void GetEmployees()
        {
            try {
                list = await GetPageListData();
                btnprevPage.IsEnabled = list.HasPreviousPage;
                btnNextPage.IsEnabled = list.HasNextPage;
                dgEmployee.DataContext = list;
                lblpageNumber.Content = string.Format("Page {0}/{1}", pageNumber, list.PageCount);
            }
            catch {
                messageBoxText = "Please try again after sometime.";
                caption = "Message";
            }            
        }
       
        public void SaveEmployee(Employee employee)
        {           
            try {
                var response = _service.SaveEmployee(employee);
                if (response == "OK") {
                    messageBoxText = "User added successfully";
                    caption = "Message";
                }                
            }
            catch(Exception ex) {
                messageBoxText = "Please try again after sometime.";
                caption = "Message";
            }
            finally {                
                MessageBox.Show(messageBoxText, caption);
            }
        }
        public void UpdateEmployee(Employee emp)
        {            
            try {
                var response = _service.UpdateEmployee(emp);
                if (response != null) {
                    messageBoxText = "User deatils Modified successfully";
                    caption = "Message";
                }
            }
            catch (Exception ex) {
                messageBoxText = "Please try again after sometime.";
                caption = "Message";
            }
            finally {
                GetEmployees();                
                MessageBox.Show(messageBoxText, caption);
            }            
        }
        private void DeleteEmployee(int empid)
        {
            try {
                var response = _service.DeleteEmployee(empid);
                if (response == "OK") {
                    messageBoxText = "User removed successfully";
                    caption = "Message";
                    MessageBox.Show(messageBoxText, caption);
                    GetEmployees();
                }                
            }
            catch (Exception ex) {
                messageBoxText = "Please try again after sometime.";
                caption = "Message";
            }
            finally {
                GetEmployees();
                MessageBox.Show(messageBoxText, caption);
            }
        }
        private void SaveEmployeeDetails(object sender, RoutedEventArgs e)
        {
            var employee = new Employee()
            {
                //id = Convert.ToInt32(txtEmployeeId.Text),
                id = txtEmployeeId.Text == "" ? 0 : Convert.ToInt32(txtEmployeeId.Text),
                name = txtName.Text.ToString(),
                email = txtEmail.Text.ToString(),
                gender = txtGender.Text.ToString(),
                status = txtStatus.Text.ToString()
            };
            if (employee.id == 0)
            {
                SaveEmployee(employee);
                GetEmployees();
            }
            else
            {
                UpdateEmployee(employee);
                GetEmployees();
            }
            txtEmployeeId.Text = "";
            txtName.Text = "";
            txtEmail.Text = "";
            txtGender.Text = "";
            txtStatus.Text = "";
            GetEmployees();
        }
        private void EditEmployeeDetails(object sender, RoutedEventArgs e)
        {
            Employee employee = ((FrameworkElement)sender).DataContext as Employee;
            txtEmployeeId.Text = employee.id.ToString();
            txtName.Text = employee.name;
            txtEmail.Text = employee.email;
            txtGender.Text = employee.gender;
            txtStatus.Text = employee.status;
        }
        void DeleteEmployeeDetails(object sender, RoutedEventArgs e)
        {
            Employee employee = ((FrameworkElement)sender).DataContext as Employee;
            this.DeleteEmployee(employee.id);
            GetEmployees();            
        }        

        public async Task<IPagedList<Employee>> GetPageListData(int pageNumber =1, int pageSize = 5)
        {            
            var response = _service.GetEmployees();            
            return await Task.Factory.StartNew(() => { return response.ToPagedList(pageNumber, pageSize); });
        }

        private async void PreviousPage(object sender, RoutedEventArgs e)
        {
            if (list.HasPreviousPage) {
                list = await GetPageListData(--pageNumber);
                btnprevPage.IsEnabled = list.HasPreviousPage;
                btnNextPage.IsEnabled = list.HasNextPage;
                dgEmployee.DataContext = list;
                lblpageNumber.Content = string.Format("Page {0}/{1}", pageNumber, list.PageCount);
            }
        }

        private async void NextPage(object sender, RoutedEventArgs e)
        {
            if (list.HasNextPage) {
                list = await GetPageListData(++pageNumber);
                btnprevPage.IsEnabled = list.HasPreviousPage;
                btnNextPage.IsEnabled = list.HasNextPage;
                dgEmployee.DataContext = list;
                lblpageNumber.Content = string.Format("Page {0}/{1}", pageNumber, list.PageCount);
            }
        }
    }
}
