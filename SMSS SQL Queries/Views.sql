USE DVLD;
GO

SELECT * FROM People;
GO


CREATE VIEW People_View AS
SELECT [Person ID] = p.PersonID, [National No] = p.NationalNo, [First Name] = p.FirstName, [Second Name] = p.SecondName, 
	   [Third Name] = p.ThirdName, [Last Name] = p.LastName, [Date Of Birth] = p.DateOfBirth, [Nationality] = c.CountryName, 
	   Gender = CASE p.Gender WHEN 0 THEN 'Male' ELSE 'Female' END, p.Phone, p.Email
FROM People p INNER JOIN Countries c ON p.NationalityCountryID = c.CountryID;
GO

SELECT * FROM People_View;
GO


CREATE VIEW UsersWithFullInfo_View AS
SELECT u.UserID, u.UserName, u.Password, p.NationalNo, p.FirstName, p.SecondName, p.ThirdName, p.LastName, 
	   p.DateOfBirth, p.Gender, p.Address, p.Phone, p.Email, Nationality = c.CountryName, p.ImagePath 
FROM Users u 
INNER JOIN People p ON u.PersonID = p.PersonID
INNER JOIN Countries c ON p.NationalityCountryID = c.CountryID;
GO

SELECT * FROM UsersWithFullInfo_View;
GO


CREATE VIEW Users_View AS
SELECT u.UserID, u.PersonID, FullName = p.FirstName + ' ' + p.SecondName + ' ' + (
		CASE 
		WHEN TRIM(p.ThirdName) != '' OR p.ThirdName IS NOT NULL THEN p.ThirdName + ' '
		ELSE '' END ) + p.LastName, u.UserName, u.IsActive
FROM Users u
INNER JOIN People p ON u.PersonID = p.PersonID;
GO

SELECT * FROM Users_View;
GO
