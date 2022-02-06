CREATE VIEW [dbo].[EmployeeInfo3]
	AS SELECT TOP(100) e.Id,
	COALESCE(EmployeeName, CONCAT(FirstName,' ',LastName)) EmployeeFullName,
	CONCAT(ZipCode, '_', State, ',', City, '-', Street) EmployeeFullAddress,
	CONCAT (CompanyName, '(', Position, ')') EmployeeCompanyInfo
	FROM Employee e
	JOIN Person p 
	ON p.Id = e.PersonId
	JOIN Address a 
	ON a.Id = e.AddressId
	ORDER BY CompanyName, City ASC