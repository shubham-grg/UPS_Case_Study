using CaseStudyService.Interface;
using CaseStudyService.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace CaseStudyTest
{
    
    [TestClass]
    public class EmployeeTest
    {        
        Mock<IEmployeeService> mock = new Mock<IEmployeeService>();
        Employee emp = new Employee { id = 1400565,  name = "Shubham",  email = "Shub@Garg.com",  gender = "male", status = "active"};
        string successCode = "OK";

        [TestMethod]
        public void GetEmployeeByIDTest()
        {                     
            mock.Setup(x => x.GetEmployeebyID(emp.id)).Returns(emp);
            Assert.AreEqual(emp, mock.Object.GetEmployeebyID(emp.id));
        }
        [TestMethod]
        public void GetEmployeesTest()
        {
            List<Employee> employeeList = new List<Employee>()
            {
                new Employee  { id = 1400565, name = "Shubham1", email = "Shub1@Garg.com",  gender = "male", status = "active" },
                new Employee  { id = 1400565, name = "Shubham2", email = "Shub2@Garg.com",  gender = "male", status = "active" },
                new Employee  { id = 1400565, name = "Shubham3", email = "Shub3@Garg.com",  gender = "male", status = "active" }
            };               
            mock.Setup(x => x.GetEmployees()).Returns(employeeList);
            Assert.AreEqual(employeeList, mock.Object.GetEmployees());
        }

        [TestMethod]
        public void SaveEmployeeTest()
        {                       
            mock.Setup(x => x.SaveEmployee(emp)).Returns(successCode);
            Assert.AreEqual(successCode, mock.Object.SaveEmployee(emp));
        }

        [TestMethod]
        public void UpdateEmployeeTest()
        {            
            mock.Setup(x => x.UpdateEmployee(emp)).Returns(emp);
            Assert.AreEqual(emp, mock.Object.UpdateEmployee(emp));
        }

        [TestMethod]
        public void DeleteEmployeeTest()
        {
            mock.Setup(x => x.DeleteEmployee(emp.id)).Returns(successCode);
            Assert.AreEqual(successCode, mock.Object.DeleteEmployee(emp.id));
        }
    }
}
