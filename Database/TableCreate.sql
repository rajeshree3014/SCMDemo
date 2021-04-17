CREATE TABLE tblUsers
(
	intUserID INT IDENTITY(1,1),
	strUserName NVARCHAR(20),
	strPassword NVARCHAR(20)
	CONSTRAINT PK_tblUsers PRIMARY KEY (intUserID),
	CONSTRAINT UK_tblUsers UNIQUE (strUserName)
)

GO

CREATE TABLE tblNotes
(
	intNoteID INT IDENTITY(1,1),
	intUserID INT,
	strNote NVARCHAR(500),
	dteCreatedDate DATETIME,
	dteUpdatedDate DATETIME NULL
	CONSTRAINT PK_tblNotes PRIMARY KEY (intNoteID),
	CONSTRAINT FK_tblNotes_tblUsers FOREIGN KEY (intUserID) REFERENCES tblUsers (intUserID),
)

GO

INSERT INTO tblUsers VALUES ('Rajeshree.Gajjar', 'password01')

GO

INSERT INTO tblUsers VALUES ('Abhi.Shah', 'password02')

