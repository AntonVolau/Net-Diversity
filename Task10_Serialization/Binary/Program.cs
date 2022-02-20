using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Models;

namespace Binary
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
            Serialize(department, "department.bin");
            var deserialized = Deserialize("department.bin");
        }

        static void Serialize(Department department, string fileName)
        {
            var formatter = new BinaryFormatter();
            var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, department);
            stream.Close();
        }

        static Department Deserialize(string fileName)
        {
            var formatter = new BinaryFormatter();
            var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            var department = (Department) formatter.Deserialize(stream);
            stream.Close();
            return department;
        }
    }
}
