USE [SQL Fundamentials]
GO
CREATE TRIGGER On_Employee_INSERT
ON Employee
AFTER INSERT
AS
BEGIN
INSERT INTO Company(Name, AddressId)
SELECT CompanyName, AddressId 
FROM INSERTED
END