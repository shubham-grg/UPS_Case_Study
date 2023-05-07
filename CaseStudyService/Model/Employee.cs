using System;
using System.Collections.Generic;

namespace CaseStudyService.Model
{
    public class Employee
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public string status { get; set; }
    }
    public class Meta
    {
        public Pagination pagination { get; set; }
    }
    public class Pagination
    {
        public int total { get; set; }
        public int pages { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
    }
    public class wrapperEmployee
    {
        public int code { get; set; }
        public Meta meta { get; set; }
        public List<Employee> data { get; set; }        
    }
    public class wrapperEmployeeByID
    {
        public int code { get; set; }
        public Meta meta { get; set; }
        public Employee data { get; set; }
    }
}
