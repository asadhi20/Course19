USE DVLD;
GO


------////////////////////////////////////////////////////
------ Application Stored Procedures
GO

----/////////////////////////////// Create __ C
---- Add New Application
GO
--////// Created
--CREATE PROCEDURE SP_AddNewApplication
--    @ApplicantPersonID INT,
--    @ApplicationDate DATETIME,
--    @ApplicationTypeID INT,
--    @ApplicationStatus TINYINT,
--    @LastStatusDate DATETIME,
--    @PaidFees SMALLMONEY,
--    @CreatedByUserID INT,
--	@NewApplicationID INT OUTPUT
--AS
--BEGIN
--    SET NOCOUNT ON;
--
--    INSERT INTO Applications (ApplicantPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID)
--    VALUES (@ApplicantPersonID, @ApplicationDate, @ApplicationTypeID, @ApplicationStatus, @LastStatusDate, @PaidFees, @CreatedByUserID);
--
--    SELECT @NewApplicationID = SCOPE_IDENTITY();
--END;
GO



----/////////////////////////////// Read __ R
---- Get Application Info
GO
--////// Created
--CREATE PROCEDURE SP_GetSingleApplicationDetails_By_ID
--    @ApplicationID INT
--AS
--BEGIN
--    SET NOCOUNT ON;
--
--    SELECT TOP 1 app.ApplicantPersonID, p.NationalNo, p.FirstName, p.SecondName, p.ThirdName, p.LastName, p.DateOfBirth,
--		p.Gender, p.Address, p.Phone, p.Email, p.NationalityCountryID, p.ImagePath, app.ApplicationDate,
--		app.ApplicationTypeID, app.ApplicationStatus, app.LastStatusDate, app.PaidFees, app.CreatedByUserID
--    FROM Applications app
--	INNER JOIN People p ON app.ApplicantPersonID = p.PersonID
--    WHERE ApplicationID = @ApplicationID;
--END;
GO


--////// Created
--CREATE PROCEDURE SP_SingleGetApplicationDetails_By_ApplicantPersonID_CreatedByUserID
--    @ApplicantPersonID INT,
--    @CreatedByUserID INT
--AS
--BEGIN
--    SET NOCOUNT ON;
--
--    SELECT TOP 1 app.ApplicationID, app.ApplicationDate, app.ApplicationTypeID, app.ApplicationStatus, app.LastStatusDate,
--		app.PaidFees, p.NationalNo, p.FirstName, p.SecondName, p.ThirdName, p.LastName, p.DateOfBirth, p.Gender, p.Address,
--        p.Phone, p.Email, p.NationalityCountryID, p.ImagePath
--    FROM Applications app
--    INNER JOIN People p ON app.ApplicantPersonID = p.PersonID
--    WHERE app.ApplicantPersonID = @ApplicantPersonID AND app.CreatedByUserID = @CreatedByUserID;
--END;
GO


--////// Created
--CREATE PROCEDURE SP_GetAllApplicationsDetails
--AS
--BEGIN
--    SET NOCOUNT ON;
--
--    SELECT app.ApplicationID, app.ApplicantPersonID, p.NationalNo, p.FirstName, p.SecondName, p.ThirdName, p.LastName,
--		p.DateOfBirth, p.Gender, p.Address, p.Phone, p.Email, p.NationalityCountryID, p.ImagePath, app.ApplicationDate,
--		app.ApplicationTypeID, app.ApplicationStatus, app.LastStatusDate, app.PaidFees, app.CreatedByUserID
--    FROM Applications app
--    INNER JOIN People p ON app.ApplicantPersonID = p.PersonID;
--END;
GO

--////// Created
--CREATE PROCEDURE SP_GetAllApplications
--AS
--BEGIN
--    SET NOCOUNT ON;
--    SELECT * FROM Applications;
--END;
GO


----/////////////////////////////// Update __ U
---- Update Application
GO
--////// Created
--CREATE PROCEDURE SP_UpdateApplication_By_ID
--    @ApplicationID INT,
--    @ApplicantPersonID INT,
--    @ApplicationDate DATETIME,
--    @ApplicationTypeID INT,
--    @ApplicationStatus TINYINT,
--    @LastStatusDate DATETIME,
--    @PaidFees SMALLMONEY,
--    @CreatedByUserID INT
--AS
--BEGIN
--    SET NOCOUNT ON;
--
--    UPDATE Applications
--    SET ApplicantPersonID = @ApplicantPersonID,
--        ApplicationDate   = @ApplicationDate,
--        ApplicationTypeID = @ApplicationTypeID,
--        ApplicationStatus = @ApplicationStatus,
--        LastStatusDate    = @LastStatusDate,
--        PaidFees          = @PaidFees,
--        CreatedByUserID   = @CreatedByUserID
--    WHERE ApplicationID = @ApplicationID;
--END;
GO


----/////////////////////////////// Delete __ D
---- Delete Application
GO
--////// Created
--CREATE PROCEDURE SP_DeleteApplication_By_ID
--    @ApplicationID INT
--AS
--BEGIN
--    SET NOCOUNT ON;
--
--    DELETE FROM Applications
--    WHERE ApplicationID = @ApplicationID;
--END;
GO
