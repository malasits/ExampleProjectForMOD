CREATE DATABASE DbForMOD
USE DbForMOD

--DATA DEFINITION
--Tables

CREATE TABLE People(
	[ID] INT NOT NULL IDENTITY(1,1),
	[FirstName] NVARCHAR(150) NOT NULL,
	[LastName] NVARCHAR(150) NOT NULL,
	[BornDate] DATE NOT NULL,

	PRIMARY KEY ([ID])
)

CREATE TABLE PeopleID(
	[ID] INT NOT NULL IDENTITY(1,1),
	[PeopleID] INT NOT NULL,
	[PeopleCode] UNIQUEIDENTIFIER NOT NULL,
	[RegDate] DATETIMEOFFSET NOT NULL,

	PRIMARY KEY ([ID]),
	CONSTRAINT FK_PeopleID_PeopleID FOREIGN KEY([PeopleID])
		REFERENCES People ([ID])
)

--DEFAULTS
CREATE DEFAULT DEF_UiqueIdentifier AS NEWID()
EXECUTE sp_bindefault 'DEF_UiqueIdentifier', 'PeopleID.[PeopleCode]' --Automatically set new id for person

CREATE DEFAULT DEF_RegDate AS GETDATE()
EXECUTE sp_bindefault 'DEF_RegDate', 'PeopleID.[RegDate]'--Automatically set current date in to registration date

--DATA QUERY
--VIEWS

CREATE VIEW V_GetAllDataFromDatabase AS
SELECT People.ID, People.LastName, People.FirstName, 
	People.BornDate, PeopleID.PeopleCode, PeopleID.RegDate 
FROM People LEFT JOIN PeopleID
ON People.ID = PeopleID.PeopleID


--TSQL
--STORED PROCEDURE

CREATE PROCEDURE P_InsertData --Insert new person in the database
	@FirstName NVARCHAR(150),
	@LastName NVARCHAR(150),
	@BornDate DATE
AS
BEGIN
	BEGIN TRAN
		INSERT INTO People(FirstName, LastName, BornDate) 
		VALUES (@FirstName, @LastName, @BornDate)
			BEGIN TRAN
			IF NOT EXISTS (SELECT PeopleID.ID FROM PeopleID JOIN People ON PeopleID.PeopleID = People.ID WHERE PeopleID.PeopleID = SCOPE_IDENTITY())
			INSERT INTO PeopleID(PeopleID) VALUES((SELECT SCOPE_IDENTITY()))
			COMMIT TRAN
	COMMIT TRAN
END


CREATE PROCEDURE P_UpdateData--Update data in the People
	@ID INT,
	@FirstName NVARCHAR(150),
	@LastName NVARCHAR(150),
	@BornDate DATE
AS
BEGIN
	BEGIN TRAN
		UPDATE People SET FirstName = @FirstName, LastName = @LastName, BornDate = @BornDate
		WHERE People.ID = @ID
	COMMIT TRAN
END


CREATE PROCEDURE P_DeleteData -- Delete the record from database
	@ID INT 
AS
BEGIN
	BEGIN TRAN
		DELETE FROM PeopleID WHERE PeopleID.PeopleID = @ID
		DELETE FROM People WHERE People.ID = @ID
	COMMIT TRAN
END


CREATE PROCEDURE P_GetPersonByID --Get only one person 
	@ID INT
AS
BEGIN
	SELECT * FROM People WHERE ID = @ID
END

CREATE PROCEDURE P_MakeBackup AS --Make a backup file 
BEGIN
	DECLARE @fileName varchar(100);
	DECLARE @dbName varchar(100);
	DECLARE @fileDate varchar(20);

	SET @fileName = 'D:\Backup\';
	SET @dbName = 'DbForMOD';
	SET @fileDate = CONVERT (varchar(20), GETDATE(), 112);

	SET @fileName = @fileName + @dbName + '-' + @fileDate + '.bak';

	BACKUP DATABASE @dbName TO DISK  = @fileName;
END


EXEC P_InsertData 'Attila', 'Malasits', '1991.10.21'
EXEC P_InsertData 'Zita', 'Para', '1996.08.06'
EXEC P_InsertData 'Simon', 'Pop', '1986.10.28'
EXEC P_InsertData 'Pál', 'Bekre', '1990.04.12'
EXEC P_InsertData 'Pista', 'Trap', '2006.11.18'
EXEC P_InsertData 'Lenke', 'Fejet', '1991.10.17'
EXEC P_InsertData 'Zsolt', 'Mor', '2012.02.01'

EXEC P_MakeBackup