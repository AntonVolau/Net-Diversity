IF (NOT EXISTS(SELECT * FROM [dbo].[Address]))  
BEGIN  
    INSERT Address(Street, City, State, ZipCode) 
	VALUES 
	('Pushkina', 'St. Petersburg', NULL, 23555),
	('Lenina', 'Brest', NULL, 23467),
	('1st Avenue', 'Oklahoma-city', 'Oklahoma', 12001);
END

IF (NOT EXISTS(SELECT * FROM [dbo].[Company]))  
BEGIN  
    INSERT Company(Name, AddressId) 
	VALUES 
	('Epam', 1),
	('McDonalds', 2),
	('KFC', 3);
END

IF (NOT EXISTS(SELECT * FROM [dbo].[Person]))  
BEGIN  
    INSERT Person(FirstName, LastName) 
	VALUES 
	('Dexter', 'Morgan'),
	('Marty', 'McFly'),
	('Peter', 'Parker');
END

IF (NOT EXISTS(SELECT * FROM [dbo].[Employee]))  
BEGIN  
    INSERT Employee(AddressId, PersonId, CompanyName, Position, EmployeeName) 
	VALUES
	(1, (SELECT Person.Id FROM Person WHERE Person.FirstName = 'Dexter'), (SELECT Company.Name FROM Company WHERE Company.Id = 1), Null, CONCAT((SELECT Person.FirstName FROM Person WHERE Person.Id = 1), ' ', (SELECT Person.LastName FROM Person WHERE Person.Id = 1))),
	(2, 2, (SELECT Company.Name FROM Company WHERE Company.Id = 2), 'Director', (SELECT Person.FirstName FROM Person WHERE Person.Id = 2) + ' ' + (SELECT Person.LastName FROM Person WHERE Person.Id = 2)),
	(3, 3, (SELECT Company.Name FROM Company WHERE Company.Id = 3), 'Spider-Man', (SELECT Person.FirstName FROM Person WHERE Person.Id = 3) + ' ' + (SELECT Person.LastName FROM Person WHERE Person.Id = 3))
END