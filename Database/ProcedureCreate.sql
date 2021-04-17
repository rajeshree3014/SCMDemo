CREATE PROC spGetNotesList
(
	@intUserID INT
)
AS
BEGIN
	
	SELECT 
		intNoteID
		,intUserID
		,strNote
		,dteCreatedDate
		,dteUpdatedDate
	FROM 
		tblNotes
	WHERE 
		intUserID = @intUserID
END

GO

CREATE PROC spGetSpecificUserByUserName
(
	@strUserName NVARCHAR(20),
	@strPassword NVARCHAR(20)
)
AS
BEGIN
	SELECT 
		intUserID
		,strUserName
		,strPassword
	FROM 
		tblUsers
	WHERE 
		strUserName = @strUserName AND strPassword = @strPassword
END

GO

CREATE PROC spSaveNotes
(
	 @intNoteID INT OUT
	,@intUserID INT
	,@strNote NVARCHAR(500)
)
AS
BEGIN
	DECLARE @intRowCount INT,
			@intErrorSave INT

	IF(@intNoteID IS NULL OR @intNoteID = 0)
	BEGIN
		INSERT INTO tblNotes
		(
			intNoteID
			,intUserID
			,strNote
			,dteCreatedDate
		)
		VALUES
		(
			 @intNoteID
			,@intUserID
			,@strNote
			,GETDATE()
		)

		SELECT	@intNoteID = @@IDENTITY,
				@intRowCount = @@ROWCOUNT,
				@intErrorSave = @@ERROR

		IF(@intErrorSave <> 0)
		BEGIN
			RAISERROR('spSaveNotes : Error number %d on insert',1 ,1, @intErrorSave)
			RETURN @intErrorSave
		END

		IF(@intRowCount <> 0)
		BEGIN
			RAISERROR('spSaveNotes : No records inserted into tblNotes',1 ,1)
			RETURN -1
		END
	END
	ELSE
	BEGIN
		UPDATE tblNotes 
		SET strNote = @strNote,
			dteUpdatedDate = GETDATE()
		WHERE intNoteID = @intNoteID

		SELECT	@intRowCount = @@ROWCOUNT,
				@intErrorSave = @@ERROR

		IF(@intErrorSave <> 0)
		BEGIN
			RAISERROR('spSaveNotes : Error number %d on update',1 ,1, @intErrorSave)
			RETURN @intErrorSave
		END

		IF(@intRowCount <> 0)
		BEGIN
			RAISERROR('spSaveNotes : No records updated into tblNotes',1 ,1)
			RETURN -1
		END
	END
END

GO

CREATE PROC spDeleteNotes
(
	@intNoteID INT
)
AS
BEGIN
	DELETE FROM tblNotes WHERE intNoteID = @intNoteID
END