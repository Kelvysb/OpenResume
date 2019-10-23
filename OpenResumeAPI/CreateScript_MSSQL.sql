--CREATE DATABASE OpenResume
--CREATE LOGIN [OpenResume] WITH PASSWORD = '@open_resume@'
--USE OpenResume
--CREATE USER OpenResume FOR LOGIN OpenResume
--EXEC sp_addrolemember 'db_datareader', 'OpenResume'
--EXEC sp_addrolemember 'db_datawriter', 'OpenResume'

if not exists (select * from sysobjects where name = 'users' and type = 'U')
begin
	create table users (
		id int identity not null,
		name varchar(100) not null,
		description varchar(500) not null,
		itemOrder int not null,
		login varchar(100) not null,
		email varchar(100) not null,
		lastName varchar(100) not null,
		passwordHash varchar(500) not null,
		emailConfirmed bit not null,
		resetPassword bit not null,
		createdDate datetime not null,
		updatedDate datetime not null,
		lastActivity datetime not null,
		primary key (id)
	)
	create unique index Iusers1 on users(login)
	create unique index Iusers2 on users(email)
end
go

if not exists (select * from sysobjects where name = 'resumes' and type = 'U')
begin
	create table resumes (
		id int identity not null,
		name varchar(100) not null,
		description varchar(500) not null,
		userId int not null,
		itemOrder int not null,
		link varchar(100) not null,
		language varchar(100) not null,
		template varchar(100) not null,		
		accessLevel int not null,		
		createdDate datetime not null,
		updatedDate datetime not null,
		primary key (id, userId)
	)
	create index Iresumes1 on resumes(name)
	create index Iresumes2 on resumes(language)
end
go

if not exists (select * from sysobjects where name = 'blocks' and type = 'U')
begin
	create table blocks (
		id int identity not null,
		name varchar(100) not null,
		description varchar(500) not null,
		userId int not null,
		resumeId int not null,	
		blockType varchar(100) not null,
		title varchar(200) not null,
		content varchar(max) not null,		
		primary key (id, userId, resumeId)
	)
	create index Iblocks1 on blocks(blockType)
end
go

if not exists (select * from sysobjects where name = 'fields' and type = 'U')
begin
	create table fields (
		id int identity not null,
		name varchar(100) not null,
		description varchar(500) not null,
		userId int not null,
		resumeId int not null,	
		blockId int not null,
		fieldType varchar(100) not null,
		content varchar(max) not null,		
		level int not null,
		initialDate datetime not null,
		finalDate datetime not null,
		primary key (id, userId, resumeId, blockId)
	)
	create index Ifields1 on fields(fieldType)
end
go