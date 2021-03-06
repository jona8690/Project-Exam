CREATE TRIGGER  trgInsertUser ON Users
FOR INSERT AS
	declare @command	int;
	declare @table		NVarChar(max);
	declare	@PK			NVarChar(max);

	set @command =  0;
	set @table = 'Users';
	select @PK = i.Username from inserted i;

	INSERT INTO Change(Command, TableName, PrimaryKey) VALUES
	(@command, @table, @PK);
	
	PRINT 'Trigger: Insert User'
GO

CREATE TRIGGER trgUpdateUser ON Users
FOR UPDATE AS
	declare @command	int;
	declare @table		NVarChar(max);
	declare	@PK			NVarChar(max);

	set @command =  1;
	set @table = 'Users';
	select @PK = i.Username from inserted i;
	
	INSERT INTO Change(Command, TableName, PrimaryKey) VALUES
	(@command, @table, @PK);
	
	PRINT 'Trigger: Update User'
GO

CREATE TRIGGER trgDeleteUser on Users
FOR DELETE AS
	declare @command	int;
	declare @table		NVarChar(max);
	declare	@PK			NVarChar(max);

	set @command =  2;
	set @table = 'Users';
	select @PK = i.Username from deleted i;
	
	INSERT INTO Change(Command, TableName, PrimaryKey) VALUES
	(@command, @table, @PK);
	
	PRINT 'Trigger: Delete User'
GO
