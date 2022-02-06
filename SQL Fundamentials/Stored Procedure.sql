USE [SQL Fundamentials];
GO
CREATE PROCEDURE InsertEmployee
                        @employee_name NVARCHAR(50) = NULL, 
                        @employee_first_name NVARCHAR(50) = NULL, 
                        @employee_last_name NVARCHAR(50) = NULL,
                        @company_name NVARCHAR(20),
                        @position NVARCHAR(50) = NULL, 
                        @street NVARCHAR(50),
                        @city NVARCHAR(50) = NULL,
						@state NVARCHAR(50) = NULL,
                        @zip_code NVARCHAR(50) = NULL
AS
BEGIN
BEGIN TRANSACTION
IF (LTRIM(RTRIM(@employee_name)) IS NULL and LTRIM(RTRIM(@employee_first_name)) IS NULL and LTRIM(RTRIM(@employee_last_name)) is null)
BEGIN
RAISERROR ('Employee Name cannot be null',
               18, -- Severity.  
               1 -- State.  
               );
				ROLLBACK TRANSACTION
				RETURN
END
IF (@employee_first_name IS NULL)
	SET @employee_first_name = 'Unknown';
IF (@employee_last_name IS NULL)
	SET @employee_last_name = 'Unknown';
IF (@employee_name IS NULL)
	SET @employee_name = @employee_first_name + ' ' + @employee_last_name;
	 INSERT INTO Person(FirstName, LastName)
	 VALUES(@employee_first_name, @employee_last_name);
	 INSERT INTO Address(Street, City, State, ZipCode)
	 VALUES(@street, @state, @city, @zip_code);
	 INSERT INTO Employee(EmployeeName, CompanyName, Position, AddressId, PersonId)
     VALUES(@employee_name, @company_name, @position, (SELECT Address.Id FROM Address WHERE Id=(SELECT max(Id) FROM Address)), 
	 (SELECT Person.Id FROM Person WHERE Id=(SELECT max(Id) FROM Person)));
		
	 COMMIT TRANSACTION	   
END