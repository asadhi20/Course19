USE DVLD;
GO


------////////////////////////////////////////////////////
------ Test Appointments Stored Procedures
GO
----/////////////////////////////// Create __ C
---- Create Test Appointment
GO
--////// Created
--CREATE PROCEDURE SP_AddNewTestAppointment
--	@TestTypeID INT,
--	@LocalDrivingLicenseApplicationID INT,
--	@AppointmentDate SMALLDATETIME,
--	@PaidFees SMALLMONEY,
--	@CreatedByUserID INT,
--	@IsLocked BIT,
--	@TestAppointmentID INT OUTPUT
--AS 
--BEGIN
--    SET NOCOUNT ON;
--
--    BEGIN TRY
--        BEGIN TRANSACTION;
--
--        INSERT INTO TestAppointments (TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked)
--        VALUES (@TestTypeID, @LocalDrivingLicenseApplicationID, @AppointmentDate, @PaidFees, @CreatedByUserID, @IsLocked);
--
--        SET @TestAppointmentID = SCOPE_IDENTITY();
--
--        IF @TestAppointmentID IS NULL THROW 50104, 'Could not retrieve the newly inserted TestAppointmentID.', 1;
--
--        COMMIT;
--    END TRY
--    BEGIN CATCH
--        ROLLBACK;
--
--        SET @TestAppointmentID = NULL;
--
--        THROW;
--    END CATCH
--END;
GO


----/////////////////////////////// Read __ R
---- Read Test Appointment
GO
--////// Created
--CREATE PROCEDURE SP_GetSingleTestAppointmentWithLDLAppInfo_By_TestAppointmentID_TestTypeID
--	@TestAppointmentID INT,
--	@TestTypeID INT
--AS
--BEGIN
--	SET NOCOUNT ON;
--
--	SELECT tapp.TestTypeID, tapp.LocalDrivingLicenseApplicationID, app.ApplicationID, app.ApplicantPersonID, p.NationalNo, p.FirstName,
--		p.SecondName, P.ThirdName, P.LastName, p.DateOfBirth, p.Gender, p.Address, p.Phone, p.Email, p.NationalityCountryID, p.ImagePath,
--		app.ApplicationDate, app.ApplicationTypeID, app.ApplicationStatus, app.LastStatusDate, ApplicationPaidFees = app.PaidFees,
--		ApplicationCreatedByUserID = app.CreatedByUserID, ldla.LicenseClassID,
--		tapp.AppointmentDate, tapp.PaidFees, tapp.CreatedByUserID, tapp.IsLocked
--	FROM TestAppointments tapp
--	INNER JOIN LocalDrivingLicenseApplications ldla ON tapp.LocalDrivingLicenseApplicationID = ldla.LocalDrivingLicenseApplicationID
--	INNER JOIN Applications app ON ldla.ApplicationID = app.ApplicationID
--	INNER JOIN People p ON app.ApplicantPersonID = p.PersonID
--	WHERE tapp.TestAppointmentID = @TestAppointmentID AND tapp.TestTypeID = @TestTypeID;
--END;
GO

--////// Created
--CREATE PROCEDURE SP_GetTestAppointmentWithLDLAppInfo_By_ID
--	@TestAppointmentID INT
--AS 
--BEGIN
--	SET NOCOUNT ON;
--
--	SELECT tapp.TestTypeID, tapp.LocalDrivingLicenseApplicationID, app.ApplicationID, app.ApplicantPersonID, p.NationalNo, p.FirstName,
--		p.SecondName, P.ThirdName, P.LastName, p.DateOfBirth, p.Gender, p.Address, p.Phone, p.Email, p.NationalityCountryID, p.ImagePath,
--		app.ApplicationDate, app.ApplicationTypeID, app.ApplicationStatus, app.LastStatusDate, ApplicationPaidFees = app.PaidFees,
--		ApplicationCreatedByUserID = app.CreatedByUserID, ldla.LicenseClassID,
--		tapp.AppointmentDate, tapp.PaidFees, tapp.CreatedByUserID, tapp.IsLocked
--	FROM TestAppointments tapp
--	INNER JOIN LocalDrivingLicenseApplications ldla ON tapp.LocalDrivingLicenseApplicationID = ldla.LocalDrivingLicenseApplicationID
--	INNER JOIN Applications app ON ldla.ApplicationID = app.ApplicationID
--	INNER JOIN People p ON app.ApplicantPersonID = p.PersonID
--	WHERE tapp.TestAppointmentID = @TestAppointmentID;
--END;
GO

--////// Created
--CREATE PROCEDURE SP_GetAllTestAppointments_By_LocalDLAppID_TestTypeID
--	@TestTypeID INT,
--	@LocalDrivingLicenseApplicationID INT
--AS 
--BEGIN
--	SET NOCOUNT ON;
--
--	SELECT [Appointment ID] = TestAppointmentID, [Appointment Date] = AppointmentDate, [Paid Fees] = PaidFees, [Is Locked] = IsLocked
--	FROM TestAppointments
--	WHERE TestTypeID = @TestTypeID AND LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;
--END;
GO

--////// Created
--CREATE PROCEDURE SP_GetAllTestAppointments_View
--AS
--BEGIN
--	SET NOCOUNT ON;
--
--	SELECT TestAppointmentID, ClassName, LocalDrivingLicenseApplicationID, TestTypeTitle, AppointmentDate, PaidFees, FullName, IsLocked
--	FROM TestAppointments_View;
--END;
GO

--////// Created
--ALTER PROCEDURE SP_GetTestAppointment_By_TestTypeID
--	@TestTypeID INT
--AS BEGIN
--	SET NOCOUNT ON;
--
--	SELECT TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked
--	FROM TestAppointments
--	WHERE TestTypeID = @TestTypeID;
--END;
GO


----/////////////////////////////// Update __ U
---- Update Test Apponitment
GO
--////// Created
--CREATE PROCEDURE SP_UpdateTestAppointment_By_ID
--	@TestAppointmentID INT,
--	@TestTypeID		 INT,
--	@LocalDrivingLicenseApplicationID INT,
--	@AppointmentDate SMALLDATETIME,
--	@PaidFees SMALLMONEY,
--	@CreatedByUserID INT,
--	@IsLocked		 BIT
--AS 
--BEGIN
--	SET NOCOUNT ON;
--
--	UPDATE TestAppointments 
--	SET	  TestTypeID = @TestTypeID,
--		  LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID,
--		  AppointmentDate = @AppointmentDate,
--		  PaidFees = @PaidFees,
--		  CreatedByUserID = @CreatedByUserID,
--		  IsLocked = @IsLocked
--	WHERE TestAppointmentID = @TestAppointmentID;
--END;
GO

--////// Created
--CREATE PROCEDURE SP_UpdateTestAppointment_AppointmetDateOnly_By_ID
--	@TestAppointmentID INT,
--	@AppointmentDate SMALLDATETIME
--AS BEGIN
--	SET NOCOUNT ON;
--
--	UPDATE TestAppointments 
--	SET	AppointmentDate = @AppointmentDate
--	WHERE TestAppointmentID = @TestAppointmentID;
--END;
GO

--////// Created
--CREATE PROCEDURE SP_UpdateTestAppointment_IsLockedOnly_By_ID
--	@TestAppointmentID INT,
--	@IsLocked BIT
--AS 
--BEGIN
--	SET NOCOUNT ON;
--
--	UPDATE TestAppointments 
--	SET	IsLocked = @IsLocked
--	WHERE TestAppointmentID = @TestAppointmentID;
--END;
GO


----/////////////////////////////// Delete __ D
---- Delete Test Appointment
GO
--////// Created
--ALTER PROCEDURE SP_DeleteTestAppointment_By_ID
--	@TestAppointmentID INT
--AS 
--BEGIN
--	SET NOCOUNT ON;
--	DELETE TestAppointments WHERE TestAppointmentID = @TestAppointmentID;
--END;
GO
