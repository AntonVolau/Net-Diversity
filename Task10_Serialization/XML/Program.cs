using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Models;

namespace XML
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
            Serialize(department, "department.xml");
            var deserialized = Deserialize("department.xml");
        }

        static void Serialize(Department department, string fileName)
        {
            var serializer = new XmlSerializer(typeof(Department));
            var writer = new StreamWriter(fileName);
            serializer.Serialize(writer, department);
            writer.Close();
        }

        static Department Deserialize(string fileName)
        {
            var serializer = new XmlSerializer(typeof(Department));
            var fileStream = new FileStream(fileName, FileMode.Open);
            var department = (Department) serializer.Deserialize(fileStream);
            fileStream.Close();
            return department;
        }
    }
}
