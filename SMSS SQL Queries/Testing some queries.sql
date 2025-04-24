
DECLARE @FirstName NVARCHAR(20) = 'Mohammed', @SecondName NVARCHAR(20) = 'Saqer', @ThirdName NVARCHAR(20) = 'Mussa', @LastName NVARCHAR(20) = 'Abu-Hadhoud';

IF TRIM(@ThirdName) != '' AND @ThirdName IS NOT NULL BEGIN
	SELECT * FROM People 
	WHERE FirstName  = @FirstName AND 
	      SecondName = @SecondName AND 
	      ThirdName  = @ThirdName AND 
	      LastName   = @LastName;
END;
GO

SELECT * FROM People;
GO

DECLARE @ImagePath NVARCHAR(250) = 'C:\DVLD-People-Images\eefc59c8-9471-43a5-b786-f476fe7843af.jpg';
IF TRIM(@ImagePath) != '' AND @ImagePath IS NOT NULL BEGIN
	SELECT * FROM People WHERE ImagePath = @ImagePath;
END;
GO

DECLARE @Nationality NVARCHAR(50) = 'Jordan';
SELECT * FROM People_View WHERE Nationality = @Nationality;
GO

SELECT PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, 
	   CASE Gender WHEN 0 THEN 'Male' ELSE 'Female' END AS Gender, Nationality, Phone, Email 
FROM People_View;
GO

DECLARE @ImagePath NVARCHAR(250) = NULL;

IF @ImagePath IS NOT NULL BEGIN 
	SELECT 1 FROM People WHERE ImagePath = @ImagePath;
END;

SELECT 1 FROM People WHERE (ImagePath IS NOT NULL) AND (ImagePath = @ImagePath);
GO

DECLARE @CountryName NVARCHAR(50) = 'Iraq';
SELECT TOP 1 CountryID FROM Countries WHERE CountryName = @CountryName;

SELECT NationalNo FROM People;
GO

--//////////////////
--//////////////////
GO

DECLARE @UserID INT = 15;
UPDATE Users SET IsActive = 0 WHERE UserID = @UserID; SELECT @@ROWCOUNT;
GO

SELECT * FROM USERS;
GO

SELECT u.UserID, u.PersonID,
	FullName = p.FirstName + ' ' + p.SecondName + ' ' + (CASE WHEN TRIM(p.ThirdName) = '' THEN '' ELSE p.ThirdName + ' ' END) + p.LastName,
	u.UserName, u.IsActive 
FROM Users u INNER JOIN People p ON u.PersonID = p.PersonID;
GO

SELECT u.UserID, u.PersonID,
	FullName = p.FirstName + ' ' + p.SecondName + ' ' + (CASE WHEN TRIM(p.ThirdName) = '' THEN '' ELSE p.ThirdName + ' ' END) + p.LastName,
	u.UserName, u.IsActive 
FROM Users u INNER JOIN People p ON u.PersonID = p.PersonID WHERE u.IsActive = 1;
GO

SELECT u.*, p.*, Nationality = c.CountryName
FROM Users u INNER JOIN People p ON u.PersonID = p.PersonID INNER JOIN Countries c ON c.CountryID = p.NationalityCountryID;
GO


SELECT u.UserID, u.PersonID, u.UserName, u.Password, u.IsActive, p.NationalNo, p.FirstName, p.SecondName, p.ThirdName, 
	   p.LastName, p.DateOfBirth, p.Gender, p.Address, p.Phone, p.Email, p.NationalityCountryID, p.ImagePath
FROM Users u INNER JOIN People p ON u.PersonID = p.PersonID
GO

SELECT u.UserID, u.PersonID,
    FullName = p.FirstName + ' ' + p.SecondName + ' ' 
			 + (CASE WHEN TRIM(p.ThirdName) = '' OR p.ThirdName IS NULL THEN '' ELSE p.ThirdName + ' ' END) + p.LastName,
    u.UserName, u.IsActive 
FROM Users u INNER JOIN People p ON u.PersonID = p.PersonID;
GO

--SELECT u.UserID, u.PersonID, -- logical Error: If the ThirdName field has a null it will make FullName field value is null.
--    FullName = p.FirstName + ' ' + p.SecondName + ' ' + (CASE WHEN TRIM(p.ThirdName) = '' THEN '' ELSE p.ThirdName + ' ' END) + p.LastName,
--    u.UserName, u.IsActive 
--FROM Users u INNER JOIN People p ON u.PersonID = p.PersonID;
--GO

SELECT * FROM People;
GO

SELECT * FROM LicenseClasses;
GO


DECLARE @ApplicantPersonID INT, @ApplicationDate DATETIME, @ApplicationTypeID INT, @ApplicationStatus TINYINT, @LastStatusDate DATETIME, @PaidFees SMALLMONEY, @CreatedByUserID INT;

INSERT INTO Applications (ApplicantPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID)
VALUES (@ApplicantPersonID, @ApplicationDate, @ApplicationTypeID, @ApplicationStatus, @LastStatusDate, @PaidFees, @CreatedByUserID);
SELECT SCOPE_IDENTITY();
GO

DECLARE @ApplicationID INT, @ApplicantPersonID INT, @ApplicationDate DATETIME, @ApplicationTypeID INT, @ApplicationStatus TINYINT, @LastStatusDate DATETIME, @PaidFees SMALLMONEY, @CreatedByUserID INT;
UPDATE Applications
SET ApplicantPersonID = @ApplicantPersonID,
    ApplicationDate   = @ApplicationDate,
	ApplicationTypeID = @ApplicationTypeID,
    ApplicationStatus = @ApplicationStatus,
	LastStatusDate    = @LastStatusDate,
    PaidFees          = @PaidFees,
    CreatedByUserID   = @CreatedByUserID
WHERE ApplicationID   = @ApplicationID;
GO

DECLARE @ApplicationID INT;
SELECT ApplicantPersonID, ApplicationDate, ApplicationTypeID, 
	   ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID
FROM Applications
WHERE ApplicationID = @ApplicationID;
GO
