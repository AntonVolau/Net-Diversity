CREATE VIEW EmployeeInfo AS
SELECT Employee.Id AS EmployeeId, 
        CASE WHEN Employee.EmployeeName IS NOT NULL 
       THEN EmployeeName
       ELSE (Person.FirstName + ' ' + Person.LastName)
	   END AS EmployeeFullName,
        CONCAT((SELECT Address.ZipCode FROM Address WHERE Employee.AddressId = Address.Id), '_', 
		(SELECT Address.State FROM Address WHERE Employee.AddressId = Address.Id), ', ', 
		(SELECT Address.City FROM Address WHERE Employee.AddressId = Address.Id), '-',
		(SELECT Address.Street FROM Address WHERE Employee.AddressId = Address.Id)) As EmployeeFullAddress,
		CONCAT((SELECT Employee.CompanyName FROM Employee WHERE Employee.AddressId = Address.Id), '(', 
		(SELECT Employee.Position FROM Employee WHERE Employee.AddressId = Address.Id), ')') As EmployeeCompanyInfo
FROM Employee INNER JOIN Address ON Employee.AddressId = Address.Id
INNER JOIN Person ON Person.Id = Employee.PersonId