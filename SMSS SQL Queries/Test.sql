
USE DVLD;
GO

--///////////////////////////////////
--///////////////////////////////////
-- Views

EXEC sp_helptext 'Drivers_View';
GO

SELECT * FROM DVLD.dbo.Drivers_View;
GO

SELECT * FROM DVLD.dbo.LocalDrivingLicenseApplications_View;
GO

SELECT * FROM DVLD.dbo.TestAppointments_View;
GO




--///////////////////////////////////
--///////////////////////////////////
-- Tables


SELECT * FROM DVLD.dbo.Applications;
GO

SELECT * FROM DVLD.dbo.ApplicationTypes;
GO

SELECT * FROM DVLD.dbo.Countries;
GO

SELECT * FROM DVLD.dbo.DetainedLicenses;
GO

SELECT * FROM DVLD.dbo.Drivers;
GO


SELECT * FROM DVLD.dbo.InternationalLicenses;
GO

SELECT * FROM DVLD.dbo.LicenseClasses;
GO

SELECT * FROM DVLD.dbo.Licenses;
GO

SELECT * FROM DVLD.dbo.LocalDrivingLicenseApplications;
GO

SELECT * FROM DVLD.dbo.People;
GO


SELECT * FROM DVLD.dbo.TestAppointments;
GO

SELECT * FROM DVLD.dbo.Tests;
GO

SELECT * FROM DVLD.dbo.TestTypes;
GO

SELECT * FROM DVLD.dbo.Users;
GO

--///////////////////////////////////
--///////////////////////////////////
go



SELECT app.ApplicationID, app.ApplicantPersonID, app.ApplicationDate, appT.ApplicationTypeTitle, 
	   app.ApplicationStatus, app.LastStatusDate, app.PaidFees, CreatedByUser = u.UserName 
FROM Applications app
INNER JOIN People p ON app.ApplicantPersonID = p.PersonID
INNER JOIN ApplicationTypes appT ON app.ApplicationTypeID = appT.ApplicationTypeID
INNER JOIN Users u ON app.CreatedByUserID = u.UserID;
GO

SELECT * FROM Applications;
GO

SELECT * FROM ApplicationTypes;
GO

SELECT * FROM LicenseClasses;
SELECT * FROM Applications;
SELECT * FROM LocalDrivingLicenseApplications;
SELECT * FROM LocalDrivingLicenseApplications_View;
GO


DECLARE @ApplicantPersonID INT, @ApplicationDate DATETIME, @ApplicationTypeID INT, @ApplicationStatus TINYINT, 
@LastStatusDate DATETIME, @PaidFees SMALLMONEY, @CreatedByUserID INT, @ApplicationID INT, @LicenseClassID INT;

INSERT INTO Applications (ApplicantPersonID, ApplicationDate, ApplicationTypeID,
			ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID)
VALUES (@ApplicantPersonID, @ApplicationDate, @ApplicationTypeID,
		@ApplicationStatus, @LastStatusDate, @PaidFees, @CreatedByUserID);

INSERT INTO LocalDrivingLicenseApplications (ApplicationID, LicenseClassID)
VALUES (@ApplicationID, @LicenseClassID);
GO


-- Me
SET STATISTICS IO, TIME ON;
SELECT ldla.LocalDrivingLicenseApplicationID, lc.ClassName, p.NationalNo, 
	   FullName = CONCAT(p.FirstName, ' ', p.SecondName, ' ', COALESCE(p.ThirdName + ' ', ''), p.LastName),
	   app.ApplicationDate, 
	   PassedTestCount = CASE WHEN app.ApplicationStatus = 3 THEN 3 
						 ELSE (
							 SELECT COUNT(tapp.TestTypeID)
							 FROM Tests t INNER JOIN TestAppointments tapp ON t.TestAppointmentID = tapp.TestAppointmentID
							 WHERE ldla.LocalDrivingLicenseApplicationID = tapp.LocalDrivingLicenseApplicationID AND t.TestResult = 1
						 )
						 END,
	   Status = CASE app.ApplicationStatus WHEN 1 THEN 'New' WHEN 2 THEN 'Cancelled' ELSE 'Completed' END
FROM LocalDrivingLicenseApplications ldla
INNER JOIN Applications app ON ldla.ApplicationID = app.ApplicationID
INNER JOIN People p ON app.ApplicantPersonID = p.PersonID
INNER JOIN LicenseClasses lc ON ldla.LicenseClassID = lc.LicenseClassID;
SET STATISTICS IO, TIME OFF;
GO

SET STATISTICS IO, TIME ON;
WITH TestCounts AS (
    SELECT tapp.LocalDrivingLicenseApplicationID, COUNT(tapp.TestTypeID) AS PassedTestCount
    FROM Tests t
    INNER JOIN TestAppointments tapp ON t.TestAppointmentID = tapp.TestAppointmentID
    WHERE t.TestResult = 1
    GROUP BY tapp.LocalDrivingLicenseApplicationID
)
SELECT ldla.LocalDrivingLicenseApplicationID, 
       lc.ClassName, 
       p.NationalNo, 
       FullName = CONCAT(p.FirstName, ' ', p.SecondName, ' ', COALESCE(p.ThirdName + ' ', ''), p.LastName),
       app.ApplicationDate, 
       PassedTestCount = CASE WHEN app.ApplicationStatus = 3 THEN 3 ELSE COALESCE(tc.PassedTestCount, 0) END,
       Status = CASE app.ApplicationStatus 
                   WHEN 1 THEN 'New' 
                   WHEN 2 THEN 'Cancelled' 
                   ELSE 'Completed' 
                END
FROM LocalDrivingLicenseApplications ldla
INNER JOIN Applications app ON ldla.ApplicationID = app.ApplicationID
INNER JOIN People p ON app.ApplicantPersonID = p.PersonID
INNER JOIN LicenseClasses lc ON ldla.LicenseClassID = lc.LicenseClassID
LEFT JOIN TestCounts tc ON ldla.LocalDrivingLicenseApplicationID = tc.LocalDrivingLicenseApplicationID;
SET STATISTICS IO, TIME OFF;
GO

-- Dr
SELECT dbo.LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID, dbo.LicenseClasses.ClassName, dbo.People.NationalNo, dbo.People.FirstName + ' ' + dbo.People.SecondName + ' ' + ISNULL(dbo.People.ThirdName, '') 
                  + ' ' + dbo.People.LastName AS FullName, dbo.Applications.ApplicationDate,
                      (SELECT COUNT(dbo.TestAppointments.TestTypeID) AS PassedTestCount
                       FROM      dbo.Tests INNER JOIN
                                         dbo.TestAppointments ON dbo.Tests.TestAppointmentID = dbo.TestAppointments.TestAppointmentID
                       WHERE   (dbo.TestAppointments.LocalDrivingLicenseApplicationID = dbo.LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID) AND (dbo.Tests.TestResult = 1)) AS PassedTestCount, 
                  CASE WHEN Applications.ApplicationStatus = 1 THEN 'New' WHEN Applications.ApplicationStatus = 2 THEN 'Cancelled' WHEN Applications.ApplicationStatus = 3 THEN 'Completed' END AS Status
FROM     dbo.LocalDrivingLicenseApplications INNER JOIN
                  dbo.Applications ON dbo.LocalDrivingLicenseApplications.ApplicationID = dbo.Applications.ApplicationID INNER JOIN
                  dbo.LicenseClasses ON dbo.LocalDrivingLicenseApplications.LicenseClassID = dbo.LicenseClasses.LicenseClassID INNER JOIN
                  dbo.People ON dbo.Applications.ApplicantPersonID = dbo.People.PersonID;
GO


SELECT FullName = CONCAT(p.FirstName, ' ', p.SecondName, ' ', COALESCE(p.ThirdName + ' ', ''), p.LastName),
	   e = 'Came From',
	   FirstName, SecondName, ThirdName, LastName 
FROM People p;
GO


----////////////////////////

SET STATISTICS IO, TIME ON;
-- Run Query 1 (Using LEFT JOIN)
SELECT 
    ldla.LocalDrivingLicenseApplicationID, 
    lc.ClassName, 
    p.NationalNo, 
    FullName = p.FirstName + ' ' + p.SecondName + ' ' 
             + (CASE WHEN TRIM(p.ThirdName) = '' OR p.ThirdName IS NULL THEN '' ELSE p.ThirdName + ' ' END) 
             + p.LastName,
    app.ApplicationDate, 
    PassedTestCount = 
        CASE 
            WHEN app.ApplicationStatus = 3 THEN 3 
            ELSE R1.PassedTestCount
        END,
    Status = 
        CASE app.ApplicationStatus 
            WHEN 1 THEN 'New' 
            WHEN 2 THEN 'Cancelled' 
            ELSE 'Completed' 
        END
FROM LocalDrivingLicenseApplications ldla
INNER JOIN Applications app ON ldla.ApplicationID = app.ApplicationID
INNER JOIN People p ON app.ApplicantPersonID = p.PersonID
INNER JOIN LicenseClasses lc ON ldla.LicenseClassID = lc.LicenseClassID
LEFT JOIN (
    SELECT tapp.LocalDrivingLicenseApplicationID, COUNT(tapp.TestTypeID) AS PassedTestCount
    FROM Tests t
    INNER JOIN TestAppointments tapp ON t.TestAppointmentID = tapp.TestAppointmentID
    WHERE t.TestResult = 1
    GROUP BY tapp.LocalDrivingLicenseApplicationID
) R1 ON ldla.LocalDrivingLicenseApplicationID = R1.LocalDrivingLicenseApplicationID;

SET STATISTICS IO, TIME ON;
GO


SET STATISTICS IO, TIME ON;
-- Run Query 2 (Using OUTER APPLY)
SELECT 
    ldla.LocalDrivingLicenseApplicationID, 
    lc.ClassName, 
    p.NationalNo, 
    FullName = p.FirstName + ' ' + p.SecondName + ' ' 
             + (CASE WHEN TRIM(p.ThirdName) = '' OR p.ThirdName IS NULL THEN '' ELSE p.ThirdName + ' ' END) 
             + p.LastName,
    app.ApplicationDate, 
    PassedTestCount = 
        CASE 
            WHEN app.ApplicationStatus = 3 THEN 3 
            ELSE ISNULL(t.PassedTestCount, 0)
        END,
    Status = 
        CASE app.ApplicationStatus 
            WHEN 1 THEN 'New' 
            WHEN 2 THEN 'Cancelled' 
            ELSE 'Completed' 
        END
FROM LocalDrivingLicenseApplications ldla
INNER JOIN Applications app ON ldla.ApplicationID = app.ApplicationID
INNER JOIN People p ON app.ApplicantPersonID = p.PersonID
INNER JOIN LicenseClasses lc ON ldla.LicenseClassID = lc.LicenseClassID
OUTER APPLY (
    SELECT COUNT(tapp.TestTypeID) AS PassedTestCount
    FROM Tests t
    INNER JOIN TestAppointments tapp ON t.TestAppointmentID = tapp.TestAppointmentID
    WHERE tapp.LocalDrivingLicenseApplicationID = ldla.LocalDrivingLicenseApplicationID AND t.TestResult = 1
) t;
SET STATISTICS IO, TIME OFF;
GO



SELECT * FROM TestAppointments;
SELECT * FROM TestAppointments_View;
GO

