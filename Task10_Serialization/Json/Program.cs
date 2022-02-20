using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Models;

namespace Json
{
    class Program
    {
        static void Main(string[] args)
        {
            var employee = new Employee
            {
                EmployeeName = "Anton"
            };
            var employee1 = new Employee
            {
                EmployeeName = "NeAnton"
            };
            var employees = new List<Employee>();
            employees.Add(employee);
            employees.Add(employee1);
            var department = new Department
            {
                DepartmentName = "epam",
                Employees = employees
            };
            Serialize(department, "department.json");
            var deserialized = Deserialize("department.json");
        }

        static void Serialize(Department department, string fileName)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(department, options);
            File.WriteAllText(fileName, jsonString);
        }

        static Department Deserialize(string fileName)
        {
            var jsonString = File.ReadAllText(fileName);
            var department = JsonSerializer.Deserialize<Department>(jsonString)!;
            return department;
        }
    }
}
