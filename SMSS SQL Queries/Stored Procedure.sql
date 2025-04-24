
USE DVLD;
GO


SELECT * FROM TestTypes;
SELECT * FROM Tests;
SELECT * FROM TestAppointments;
SELECT tav.* FROM TestAppointments_View tav --WHERE FullName like 'Kh%' AND TestTypeTitle LIKE 'Vision%'
INNER JOIN TestTypes tt ON tav.TestTypeTitle = tt.TestTypeTitle ORDER BY tt.TestTypeID;
--WHERE tt.TestTypeID = 2;


SELECT * FROM LicenseClasses;
SELECT * FROM Licenses;
GO

-- Add person.										   -- Done
-- Add an local driving license application to person. -- Done
-- Sechdule vision test.
---- Take the test.
-- Sechdule written test.
---- Take the test.
-- Sechdule street test.
---- Take the test.
GO



SELECT * FROM DVLD.dbo.Drivers;
SELECT * FROM DVLD.dbo.Drivers_View;
GO