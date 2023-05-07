using CaseStudyService.Model;
using System;
using System.Collections.Generic;

namespace CaseStudyService.Interface
{   
    public interface IEmployeeService
    {
        string SaveEmployee(Employee employee);
        Employee UpdateEmployee(Employee employee);
        string DeleteEmployee(int employeeid);
        List<Employee> GetEmployees();        
        Employee GetEmployeebyID(int id);
    }
}
