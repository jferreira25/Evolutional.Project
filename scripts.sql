USE [projeto-evolucional]
GO

/****** Object:  Table [dbo].[students_lessons]    Script Date: 02/04/2021 00:02:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'students' AND ss.name = N'dbo')
BEGIN
	CREATE TABLE [dbo].[students](
		[Id] [bigint] IDENTITY(1,1) NOT NULL,
		[name] [varchar](50) NOT NULL,
	 CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'lessons' AND ss.name = N'dbo')
BEGIN
	CREATE TABLE [dbo].[lessons](
		[Id] [bigint] IDENTITY(1,1) NOT NULL,
		[name] [varchar](50) NOT NULL,
	 CONSTRAINT [PK_LESSONS] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'students_lessons' AND ss.name = N'dbo')
BEGIN
	CREATE TABLE [dbo].[students_lessons](
		[Id] [bigint] IDENTITY(1,1) NOT NULL,
		[StudentsId] [bigint] NOT NULL,
		[LessonsId] [bigint] NOT NULL,
		[SchoolGrades] [decimal](18, 2) NULL,
	 CONSTRAINT [PK_Students_Lessons] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
	

	ALTER TABLE [dbo].[students_lessons]  WITH CHECK ADD FOREIGN KEY([LessonsId])
	REFERENCES [dbo].[lessons] ([Id])
	

	ALTER TABLE [dbo].[students_lessons]  WITH CHECK ADD FOREIGN KEY([StudentsId])
	REFERENCES [dbo].[students] ([Id])
	
END


IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'users' AND ss.name = N'dbo')
BEGIN
	CREATE TABLE [dbo].[users](
		[Id] [bigint] IDENTITY(1,1) NOT NULL,
		[name] [varchar](50) NOT NULL,
		[password] [varchar](20) NOT NULL,
	 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]

INSERT INTO USERS
	VALUES('candidato-evolucional','123456')	
END

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Insert_Students_And_Lessons'))
BEGIN
	DROP PROCEDURE [dbo].[Insert_Students_And_Lessons];
	PRINT 'DELETOU [Insert_Students_And_Lessons]'
END
GO

 CREATE PROCEDURE Insert_Students_And_Lessons(
 @StudentName VARCHAR(50),
 @LessonId BIGINT,
 @SchoolGrades decimal(18,2)
 )
 AS
 BEGIN
 DECLARE @STUDENTID BIGINT;
 IF NOT EXISTS (SELECT NAME FROM students WHERE NAME = @StudentName )
	BEGIN
	 INSERT INTO students
	 VALUES( @StudentName);
	 SELECT @STUDENTID = SCOPE_IDENTITY();
	END
ELSE
BEGIN
	SET @STUDENTID = (SELECT ID FROM students WHERE NAME = @StudentName)
	INSERT INTO students_lessons
	VALUES(@STUDENTID,@LessonId,@SchoolGrades)
	
	SELECT @STUDENTID
END
END

