using CaseStudyService.Interface;
using CaseStudyService.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyService
{
    public class EmployeeService : IEmployeeService
    {
        HttpClient client = new HttpClient();
        public EmployeeService()
        {
            string authorizationtoken = "fa114107311259f5f33e70a5d85de34a2499b4401da069af0b1d835cd5ec0d56";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorizationtoken);
            client.BaseAddress = new Uri("https://gorest.co.in/public-api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }       
        public string DeleteEmployee(int employeeid)
        {
            var response =  client.DeleteAsync("users/" + employeeid);            
            return response.Result.StatusCode.ToString();
        }
        public string SaveEmployee(Employee employee)
        {
            var myContent = JsonConvert.SerializeObject(employee);            
            StringContent httpContent = new StringContent(myContent, Encoding.UTF8, "application/json");
            var response =  client.PostAsync("users", httpContent).Result;           
            return response.StatusCode.ToString();            
        }

        public Employee UpdateEmployee(Employee employee)
        {
            var myContent = JsonConvert.SerializeObject(employee);
            StringContent httpContent = new StringContent(myContent, Encoding.UTF8, "application/json");
            var response =  client.PutAsync("users/" + employee.id, httpContent).Result;
            var output = response.Content.ReadAsStringAsync();
            var employees = JsonConvert.DeserializeObject<wrapperEmployeeByID>(output.Result.ToString());
            return employees.data;            
        }
        public List<Employee> GetEmployees()
        {
            wrapperEmployee employees;
            var response =  client.GetAsync("users").Result;
            var output =  response.Content.ReadAsStringAsync();
            employees = JsonConvert.DeserializeObject<wrapperEmployee>(output.Result.ToString());
            return employees.data;
        }            
        public Employee GetEmployeebyID(int employeeid)
        {
            wrapperEmployeeByID employees;
            var response = client.GetAsync("users/" + employeeid).Result;
            var output= response.Content.ReadAsStringAsync();
            employees =JsonConvert.DeserializeObject<wrapperEmployeeByID>(output.Result.ToString());
            return employees.data;
        }

    }
}
