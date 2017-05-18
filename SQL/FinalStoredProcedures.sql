CREATE PROCEDURE SP_GetAllUsers AS
BEGIN
	SELECT Username, Email, PermissionLevel
	FROM Users
END

GO
CREATE PROCEDURE SP_GetAllRooms AS
BEGIN
	SELECT Building, FloorNr, Nr, MaxPeople, MinPermissionLevel
	FROM Rooms
END

GO
CREATE PROCEDURE SP_GetAllReservations AS
BEGIN
	SELECT ID, PeopleNr, DateTo, DateFrom, Building, FloorNr, Nr, Username
	FROM Reservations
END

-- Insert 
GO
CREATE PROCEDURE SP_InsertUser (
	@Username NVarChar(100),
	@Email NVarChar(MAX),
	@PermissionLevel Int
) AS
BEGIN
INSERT INTO Users (Username, Email, PermissionLevel) VALUES
	(@Username, @Email, @PermissionLevel)
	UPDATE Change set Identifier = 1 WHERE PrimaryKey = @Username
END

GO
CREATE PROCEDURE SP_InsertRoom (
	@Building Char,
	@FloorNr NVarChar(max),
	@Nr NVarChar(max),
	@MaxPeople Int,
	@MinPermissionLevel Int
) AS
BEGIN
INSERT INTO Rooms (Building, FloorNr, Nr, MaxPeople, MinPermissionLevel) VALUES
	(@Building, @FloorNr, @Nr, @MaxPeople, @MinPermissionLevel)
	UPDATE Change set Identifier = 1 WHERE PrimaryKey = @Building +';' + @FloorNr + ';' + @Nr
END

GO
CREATE PROCEDURE SP_InsertReservation (
	@PeopleNr Int,
	@DateTo DateTime2,
	@DateFrom DateTime2,
	@Building Char,
	@FloorNr Int,
	@Nr Int,
	@Username NVarChar(100)
) AS
BEGIN
	INSERT INTO Reservations (PeopleNr, DateTo, DateFrom, Building, FloorNr, Nr, Username) VALUES
	(@PeopleNr, @DateTo, @DateFrom, @Building, @FloorNr, @Nr, @Username)
	UPDATE Change set Identifier = 1 WHERE PrimaryKey = CAST(SCOPE_IDENTITY() AS NVarChar(100))
END


-- Delete Specific
GO
ALTER PROCEDURE SP_DeleteReservation (
	@Username NVarChar(100),
	@DateFrom DateTime2,
	@DateTo DateTime 
) AS
BEGIN
	DELETE FROM Reservations
	WHERE Username = @Username AND DateFrom = @DateFrom AND DateTo = @DateTo
	UPDATE Change set Identifier = 1 WHERE PrimaryKey = (SELECT i.ID FROM deleted i)
END

GO
CREATE PROCEDURE SP_DeleteRoom (
	@Building Char,
	@FloorNr NVarChar(max),
	@Nr NVarChar(max)) AS
BEGIN
	DELETE FROM Rooms
	WHERE Building = @Building AND FloorNr = @FloorNr AND Nr = @Nr
	UPDATE Change set Identifier = 1 WHERE PrimaryKey = @Building + ';' + @FloorNr + ';' + @Nr
END

GO
CREATE PROCEDURE SP_DeleteUser (@Username NVarChar(100)) AS
BEGIN
	DELETE FROM Users
	WHERE Username = @Username
	UPDATE Change set Identifier = 1 WHERE PrimaryKey = @Username
END

GO
ALTER PROCEDURE SP_DeleteAllUser AS
BEGIN
	DELETE FROM Users
END

GO
ALTER PROCEDURE SP_DeleteAllRooms AS
BEGIN
	DELETE FROM Rooms
END

GO
ALTER PROCEDURE SP_DeleteAllReservation AS
BEGIN
	DELETE FROM Reservations
END

