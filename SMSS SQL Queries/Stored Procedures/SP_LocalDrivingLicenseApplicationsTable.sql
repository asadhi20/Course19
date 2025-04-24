USE DVLD;
GO

------////////////////////////////////////////////////////
------ Local Driving License Application Stored Procedures
GO
----/////////////////////////////// Create __ C
---- Add New Local Driving License Application
GO
--////// Created
CREATE PROCEDURE SP_AddNewLocalDLApplication
	@ApplicantPersonID INT,
	@ApplicationDate   DATETIME,
	@ApplicationTypeID INT,
	@ApplicationStatus TINYINT,
	@LastStatusDate	   DATETIME,
	@PaidFees		   SMALLMONEY,
	@CreatedByUserID   INT,
	@LicenseClassID	   INT,
	@NewApplicationID  INT OUTPUT,
	@NewLocalDrivingLicenseApplicationID INT OUTPUT
AS BEGIN
	-- First insert application into applications table.
	INSERT INTO Applications (ApplicantPersonID, ApplicationDate, ApplicationTypeID,
							  ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID)
	VALUES (@ApplicantPersonID, @ApplicationDate, @ApplicationTypeID,
			@ApplicationStatus, @LastStatusDate, @PaidFees, @CreatedByUserID);

	SET @NewApplicationID = SCOPE_IDENTITY();

	-- After insert the new application then mark this application as local driveing license application.
	--	  by insert it into Local Driving License Applications.
	INSERT INTO LocalDrivingLicenseApplications (ApplicationID, LicenseClassID)
	VALUES (@NewApplicationID, @LicenseClassID);

	SET @NewLocalDrivingLicenseApplicationID = SCOPE_IDENTITY();
END;
GO


----/////////////////////////////// Read __ R
---- Get LDLApplication Info
GO
--////// Created
CREATE PROCEDURE SP_GetAllLcoalDLApplications_View
AS 
BEGIN
	SET NOCOUNT ON;

	SELECT [L.D.L.AppID] = LocalDrivingLicenseApplicationID, [Driving Class] = ClassName, [National No.] = NationalNo, 
		   [Full Name] = FullName, [Application Date] = ApplicationDate, [Passed Tests] = PassedTestCount, Status
	FROM LocalDrivingLicenseApplications_View;
END;
GO

--////// Created
CREATE PROCEDURE SP_GetAllLocalDLApplicationInfo
AS 
BEGIN
	SET NOCOUNT ON;

	SELECT ldla.LocalDrivingLicenseApplicationID, ldla.ApplicationID, app.ApplicantPersonID, p.NationalNo, p.FirstName, p.SecondName, 
		p.ThirdName, p.LastName, p.DateOfBirth, p.Gender, p.Address, p.Phone, p.Email, p.NationalityCountryID, p.ImagePath, 
		app.ApplicationDate, app.ApplicationTypeID, app.ApplicationStatus, app.LastStatusDate, app.PaidFees, app.CreatedByUserID, ldla.LicenseClassID
    FROM LocalDrivingLicenseApplications ldla
	INNER JOIN Applications app ON ldla.ApplicationID = app.ApplicationID
	INNER JOIN People p ON p.PersonID = app.ApplicantPersonID;
END;
GO

--////// Created
CREATE PROCEDURE SP_GetSingleLcoalDLApplications_View_By_ID
	@LocalDrivingLicenseApplicationID INT
AS BEGIN
	SET NOCOUNT ON;

	SELECT TOP 1 DrivingClass = ClassName, NationalNo, FullName, ApplicationDate, PassedTests = PassedTestCount, Status
	FROM LocalDrivingLicenseApplications_View
	WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;
END;
GO


--////// Created
CREATE PROCEDURE SP_GetLocalDLApplication_By_ID
	@LocalDrivingLicenseApplicationID INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT TOP 1 app.ApplicationID, app.ApplicantPersonID, p.NationalNo, p.FirstName, p.SecondName, p.ThirdName, p.LastName, p.DateOfBirth, p.Gender,
		p.Address, p.Phone, p.Email, p.NationalityCountryID, p.ImagePath, app.ApplicationDate, app.ApplicationTypeID, app.ApplicationStatus,
		app.LastStatusDate, ApplicationPaidFees = app.PaidFees, ApplicationCreatedByUserID = app.CreatedByUserID, ldlc.LicenseClassID
	FROM LocalDrivingLicenseApplications ldlc
	INNER JOIN Applications app ON ldlc.ApplicationID = app.ApplicationID
	INNER JOIN People p ON app.ApplicantPersonID = p.PersonID
	WHERE ldlc.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;
END;
GO


--////// Created
CREATE PROCEDURE SP_GetSingleApplicationIDWhenStatusNewOrCompleted_By_ApplicantPersonID_LicClassID
    @ApplicantPersonID INT,
    @LicenseClassID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1 ISNULL(app.ApplicationID, -1) AS ApplicationID
    FROM LocalDrivingLicenseApplications ldla
    INNER JOIN Applications app ON ldla.ApplicationID = app.ApplicationID
    WHERE app.ApplicantPersonID = @ApplicantPersonID
      AND app.ApplicationStatus IN (1, 3)
      AND ldla.LicenseClassID = @LicenseClassID;
END;
GO


----/////////////////////////////// Read __ R
---- Is Exists LDLApplicationID
GO
--////// Created
CREATE PROCEDURE SP_IsExistsLocalDLApplication_By_LDLAppID_AppID_ApplicantPersonID_LicClassID
	@LocalDrivingLicenseApplicationID INT,
	@ApplicationID INT,
	@ApplicantPersonID INT,
	@LicenseClassID INT
AS BEGIN
	SET NOCOUNT ON;

	SELECT 1 FROM LocalDrivingLicenseApplications ldla
	INNER JOIN Applications app ON ldla.ApplicationID = app.ApplicationID
	WHERE ldla.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
	  AND ldla.ApplicationID	= @ApplicationID
	  AND app.ApplicantPersonID = @ApplicantPersonID
	  AND ldla.LicenseClassID	= @LicenseClassID;
END;
GO


--////// Created
CREATE PROCEDURE SP_IsExistsLocalDLApplication_By_LDLAppID_AppID_LicClassID
	@LocalDrivingLicenseApplicationID INT,
	@ApplicationID INT,
	@LicenseClassID INT
AS 
BEGIN
	SET NOCOUNT ON;

	SELECT 1 FROM LocalDrivingLicenseApplications ldla
	WHERE ldla.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
	  AND ldla.ApplicationID = @ApplicationID
	  AND ldla.LicenseClassID = @LicenseClassID;
END;
GO


--////// Created
CREATE PROCEDURE SP_IsExistsLocalDLApplication_By_ApplicantPersonID_AppStatus_LicClassID
	@ApplicantPersonID INT,
	@ApplicationStatus TINYINT,
	@LicenseClassID INT
AS 
BEGIN
	SET NOCOUNT ON;

	SELECT 1 FROM LocalDrivingLicenseApplications ldla
	INNER JOIN Applications app ON ldla.ApplicationID = app.ApplicationID
	WHERE app.ApplicantPersonID = @ApplicantPersonID
	  AND app.ApplicationStatus = @ApplicationStatus
	  AND ldla.LicenseClassID = @LicenseClassID;
END;
GO


--////// Created
CREATE PROCEDURE SP_IsHasNewOrCompletedLocalDLApplication_By_ApplicantPersonID_LicClassID
	@ApplicantPersonID INT,
	@LicenseClassID INT
AS 
BEGIN
	SET NOCOUNT ON;

	SELECT 1 FROM LocalDrivingLicenseApplications ldla
	INNER JOIN Applications app ON ldla.ApplicationID = app.ApplicationID
	WHERE app.ApplicantPersonID = @ApplicantPersonID
	  AND app.ApplicationStatus IN (1, 3)
	  AND ldla.LicenseClassID = @LicenseClassID;
END;
GO



----/////////////////////////////// Update __ U
---- Update Local Driving License Application
GO
--////// Created
CREATE PROCEDURE SP_UpdateLocalDLApplication_By_LDLAppID_AppID
	@LocalDrivingLicenseApplicationID INT,
	@ApplicationID INT,
	@ApplicantPersonID INT,
	@ApplicationDate DATETIME,
	@ApplicationTypeID INT,
	@ApplicationStatus TINYINT,
	@LastStatusDate DATETIME,
	@PaidFees SMALLMONEY,
	@CreatedByUserID INT,
	@LicenseClassID INT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE Applications 
	SET	ApplicantPersonID = @ApplicantPersonID,
		ApplicationDate = @ApplicationDate,
		ApplicationTypeID = @ApplicationTypeID,
		ApplicationStatus = @ApplicationStatus,
		LastStatusDate = @LastStatusDate,
		PaidFees = @PaidFees,
		CreatedByUserID = @CreatedByUserID
	WHERE ApplicationID	= @ApplicationID;

	UPDATE LocalDrivingLicenseApplications
	SET LicenseClassID = @LicenseClassID
	WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;
END;
GO


--////// Created
CREATE PROCEDURE SP_UpdateLocalDLApplication_ApplicationStatusOnly_By_LocalDLAppID
	@LocalDrivingLicenseApplicationID INT,
	@NewApplicationStatus TINYINT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE app
	SET app.ApplicationStatus = @NewApplicationStatus
	FROM Applications as app
	INNER JOIN LocalDrivingLicenseApplications ldlc ON app.ApplicationID = ldlc.ApplicationID
	WHERE ldlc.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;
END;
GO



----/////////////////////////////// Delete __ D
---- Delete Local Driving License Application
GO
--////// Created
CREATE PROCEDURE SP_DeleteLocalDLApplication_By_LDLAppID
	@LocalDrivingLicenseApplicationID INT
AS 
BEGIN
	DECLARE @ApplicationID INT;

	SELECT TOP 1 @ApplicationID = ApplicationID 
	FROM LocalDrivingLicenseApplications 
	WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;

	BEGIN TRY
		IF @ApplicationID IS NULL THROW 50100, 'No matching Local Driving License Application found.', 1;
	END TRY
	BEGIN CATCH RETURN; END CATCH

	BEGIN TRY
		BEGIN TRANSACTION;

		-- First delete from LocalDrivingLicenseApplications table
		DELETE FROM LocalDrivingLicenseApplications 
		WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;

		IF (@@ROWCOUNT = 0) THROW 50102, 'No matching Local Driving License Application found to delete.', 1;

		-- Then delete from Applications table
		DELETE FROM Applications WHERE ApplicationID = @ApplicationID;

		IF (@@ROWCOUNT = 0) THROW 50103, 'No matching Application found to delete.', 1;

		COMMIT;
	END TRY
	BEGIN CATCH ROLLBACK; END CATCH
END;
GO
