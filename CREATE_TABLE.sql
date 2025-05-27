/****** Object:  Table [dbo].[DbLanguage]    Script Date: 10/18/2022 2:04:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DbLanguage](
	[LanguageCode] [nvarchar](20) NOT NULL,
	[Description] [nvarchar](300) NULL,
	[Active] [bit] NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedProgram] [nvarchar](100) NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedProgram] [nvarchar](100) NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_DbLanguage] PRIMARY KEY CLUSTERED 
(
	[LanguageCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DbLanguageLang]    Script Date: 10/18/2022 2:04:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DbLanguageLang](
	[LanguageCode] [nvarchar](20) NOT NULL,
	[LanguageCodeForName] [nvarchar](20) NOT NULL,
	[LanguageName] [nvarchar](200) NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedProgram] [nvarchar](100) NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedProgram] [nvarchar](100) NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_DbLanguageLang] PRIMARY KEY CLUSTERED 
(
	[LanguageCode] ASC,
	[LanguageCodeForName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DbLanguageLang]  WITH CHECK ADD  CONSTRAINT [FK_DbLanguageLang_DbLanguage] FOREIGN KEY([LanguageCodeForName])
REFERENCES [dbo].[DbLanguage] ([LanguageCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DbLanguageLang] CHECK CONSTRAINT [FK_DbLanguageLang_DbLanguage]
GO

/****** Object:  Table [dbo].[SuActivityLog]    Script Date: 10/18/2022 8:15:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuActivityLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ActivityTypeCode] [nvarchar](20) NOT NULL,
	[LogMessage] [nvarchar](max) NULL,
	[LoggedBy] [nvarchar](100) NULL,
	[LoggedDate] [datetime] NULL,
	[Active] [bit] NOT NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedProgram] [nvarchar](100) NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedProgram] [nvarchar](100) NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_SuActivityLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SuActivityType]    Script Date: 10/18/2022 8:15:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuActivityType](
	[ActivityTypeCode] [nvarchar](20) NOT NULL,
	[ActivityTypeName] [nvarchar](200) NULL,
	[ActivityGroupCode] [nvarchar](100) NOT NULL,
	[LogTemplate] [nvarchar](max) NULL,
	[Active] [bit] NOT NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedProgram] [nvarchar](100) NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedProgram] [nvarchar](100) NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_SuActivityType] PRIMARY KEY CLUSTERED 
(
	[ActivityTypeCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SuContent]    Script Date: 10/18/2022 8:15:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuContent](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NULL,
	[Path] [nvarchar](500) NULL,
	[Reference] [bit] NULL,
	[ValidatePath] [bit] NOT NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedProgram] [nvarchar](100) NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedProgram] [nvarchar](100) NULL,
	[RowVersion] [timestamp] NOT NULL,
	[Container] [nvarchar](500) NULL,
 CONSTRAINT [PK_SuContent] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SuMenu]    Script Date: 10/18/2022 8:15:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuMenu](
	[MenuCode] [nvarchar](20) NOT NULL,
	[ProgramCode] [nvarchar](50) NULL,
	[MainMenu] [nvarchar](20) NULL,
	[SystemCode] [nvarchar](5) NULL,
	[Icon] [nvarchar](50) NULL,
	[Active] [bit] NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedProgram] [nvarchar](100) NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedProgram] [nvarchar](100) NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_SuMenu] PRIMARY KEY CLUSTERED 
(
	[MenuCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SuMenuLabel]    Script Date: 10/18/2022 8:15:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuMenuLabel](
	[MenuCode] [nvarchar](20) NOT NULL,
	[LanguageCode] [nvarchar](20) NOT NULL,
	[MenuName] [nvarchar](200) NULL,
	[SystemCode] [varchar](5) NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedProgram] [nvarchar](100) NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedProgram] [nvarchar](100) NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_SuMenuLang] PRIMARY KEY CLUSTERED 
(
	[MenuCode] ASC,
	[LanguageCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SuMessage]    Script Date: 10/18/2022 8:15:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuMessage](
	[MessageCode] [nvarchar](20) NOT NULL,
	[LanguageCode] [nvarchar](20) NOT NULL,
	[MessageDesc] [nvarchar](200) NULL,
	[Remark] [nvarchar](200) NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedProgram] [nvarchar](100) NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedProgram] [nvarchar](100) NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_SuMessage] PRIMARY KEY CLUSTERED 
(
	[MessageCode] ASC,
	[LanguageCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SuParameter]    Script Date: 10/18/2022 8:15:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuParameter](
	[ParameterGroupCode] [nvarchar](100) NOT NULL,
	[ParameterCode] [nvarchar](100) NOT NULL,
	[ParameterValue] [nvarchar](200) NULL,
	[Remark] [nvarchar](200) NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedProgram] [nvarchar](100) NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedProgram] [nvarchar](100) NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_SuParameter] PRIMARY KEY CLUSTERED 
(
	[ParameterGroupCode] ASC,
	[ParameterCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SuPasswordPolicy]    Script Date: 10/18/2022 8:15:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuPasswordPolicy](
	[PasswordPolicyCode] [nvarchar](10) NOT NULL,
	[PasswordPolicyName] [nvarchar](100) NULL,
	[PasswordPolicyDescription] [nvarchar](200) NULL,
	[PasswordMinimumLength] [numeric](18, 0) NULL,
	[PasswordMaximumLength] [numeric](18, 0) NULL,
	[FailTime] [numeric](18, 0) NULL,
	[PasswordAge] [numeric](18, 0) NULL,
	[MaxDupPassword] [int] NULL,
	[UsingUpperCase] [bit] NULL,
	[UsingLowerCase] [bit] NULL,
	[UsingNumericChar] [bit] NULL,
	[UsingSpecialChar] [bit] NULL,
	[Active] [bit] NULL,
	[CreatedBy] [nvarchar](30) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [nvarchar](30) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedProgram] [nvarchar](50) NOT NULL,
	[CreatedProgram] [nvarchar](50) NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_SuPasswordPolicy] PRIMARY KEY CLUSTERED 
(
	[PasswordPolicyCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SuProfile]    Script Date: 10/18/2022 8:15:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuProfile](
	[ProfileCode] [nvarchar](20) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[Active] [bit] NOT NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedProgram] [nvarchar](100) NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedProgram] [nvarchar](100) NULL,
	[ProfileType] [nvarchar](100) NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_SuProfile] PRIMARY KEY CLUSTERED 
(
	[ProfileCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SuProfileLang]    Script Date: 10/18/2022 8:15:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuProfileLang](
	[ProfileCode] [nvarchar](20) NOT NULL,
	[LanguageCode] [nvarchar](20) NOT NULL,
	[ProfileName] [nvarchar](200) NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedProgram] [nvarchar](100) NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedProgram] [nvarchar](100) NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_SuProfileLang] PRIMARY KEY CLUSTERED 
(
	[ProfileCode] ASC,
	[LanguageCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_SuProfileLang_Unique] UNIQUE NONCLUSTERED 
(
	[ProfileCode] ASC,
	[LanguageCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SuProfileMenu]    Script Date: 10/18/2022 8:15:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuProfileMenu](
	[ProfileCode] [nvarchar](20) NOT NULL,
	[MenuCode] [nvarchar](20) NOT NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedProgram] [nvarchar](100) NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedProgram] [nvarchar](100) NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_SuProfileMenu] PRIMARY KEY CLUSTERED 
(
	[ProfileCode] ASC,
	[MenuCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SuProgram]    Script Date: 10/18/2022 8:15:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuProgram](
	[ProgramCode] [nvarchar](50) NOT NULL,
	[ProgramName] [nvarchar](200) NULL,
	[ProgramPath] [nvarchar](200) NULL,
	[SystemCode] [nvarchar](5) NULL,
	[ModuleCode] [varchar](5) NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedProgram] [nvarchar](100) NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedProgram] [nvarchar](100) NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_SuProgram] PRIMARY KEY CLUSTERED 
(
	[ProgramCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SuProgramLabel]    Script Date: 10/18/2022 8:15:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuProgramLabel](
	[ProgramCode] [nvarchar](50) NOT NULL,
	[FieldName] [nvarchar](200) NOT NULL,
	[LanguageCode] [nvarchar](20) NOT NULL,
	[LabelName] [nvarchar](1000) NULL,
	[SystemCode] [nvarchar](5) NULL,
	[ModuleCode] [nvarchar](5) NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedProgram] [nvarchar](100) NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedProgram] [nvarchar](100) NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [SuProgramLabel_PK] PRIMARY KEY CLUSTERED 
(
	[ProgramCode] ASC,
	[FieldName] ASC,
	[LanguageCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SuUser]    Script Date: 10/18/2022 8:15:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuUser](
	[UserId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[PasswordPolicyCode] [nvarchar](10) NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[DefaultLang] [nvarchar](20) NULL,
	[LockoutEnabled] [bit] NULL,
	[ForceChangePassword] [bit] NULL,
	[StartEffectiveDate] [datetime] NULL,
	[EndEffectiveDate] [datetime] NULL,
	[LastChangePassword] [datetime] NULL,
	[AccessFailedCount] [int] NULL,
	[ConcurrencyStamp] [nvarchar](50) NULL,
	[SecurityStamp] [nvarchar](50) NULL,
	[Email] [nvarchar](200) NULL,
	[PhoneNumber] [nvarchar](20) NULL,
	[Firstname] [nvarchar](200) NULL,
	[Lastname] [nvarchar](200) NULL,
	[NormalizedUserName] [nvarchar](50) NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[Active] [bit] NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedProgram] [nvarchar](100) NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedProgram] [nvarchar](100) NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_SuUser] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SuUserProfile]    Script Date: 10/18/2022 8:15:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuUserProfile](
	[UserId] [bigint] NOT NULL,
	[ProfileCode] [nvarchar](20) NOT NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedProgram] [nvarchar](100) NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedProgram] [nvarchar](100) NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_SuUserProfile] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[ProfileCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SuActivityLog] ADD  CONSTRAINT [DF_SuActivityLog_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[SuActivityType] ADD  CONSTRAINT [DF_SuActivityType_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[SuContent] ADD  CONSTRAINT [DF_SuContent_ValidatePath]  DEFAULT ((0)) FOR [ValidatePath]
GO
ALTER TABLE [dbo].[SuUser] ADD  DEFAULT ((1)) FOR [LockoutEnabled]
GO
ALTER TABLE [dbo].[SuUser] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[SuActivityLog]  WITH CHECK ADD  CONSTRAINT [FK_SuActivityLog_SuActivityType] FOREIGN KEY([ActivityTypeCode])
REFERENCES [dbo].[SuActivityType] ([ActivityTypeCode])
GO
ALTER TABLE [dbo].[SuActivityLog] CHECK CONSTRAINT [FK_SuActivityLog_SuActivityType]
GO
ALTER TABLE [dbo].[SuMenu]  WITH CHECK ADD  CONSTRAINT [FK_SuMenu_SuMenu_MainMenu] FOREIGN KEY([MainMenu])
REFERENCES [dbo].[SuMenu] ([MenuCode])
GO
ALTER TABLE [dbo].[SuMenu] CHECK CONSTRAINT [FK_SuMenu_SuMenu_MainMenu]
GO
ALTER TABLE [dbo].[SuMenu]  WITH CHECK ADD  CONSTRAINT [FK_SuMenu_SuProgram] FOREIGN KEY([ProgramCode])
REFERENCES [dbo].[SuProgram] ([ProgramCode])
GO
ALTER TABLE [dbo].[SuMenu] CHECK CONSTRAINT [FK_SuMenu_SuProgram]
GO
ALTER TABLE [dbo].[SuMenuLabel]  WITH CHECK ADD  CONSTRAINT [FK_SuMenuLang_DbLanguang] FOREIGN KEY([LanguageCode])
REFERENCES [dbo].[DbLanguage] ([LanguageCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SuMenuLabel] CHECK CONSTRAINT [FK_SuMenuLang_DbLanguang]
GO
ALTER TABLE [dbo].[SuMenuLabel]  WITH CHECK ADD  CONSTRAINT [FK_SuMenuLang_SuMenu] FOREIGN KEY([MenuCode])
REFERENCES [dbo].[SuMenu] ([MenuCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SuMenuLabel] CHECK CONSTRAINT [FK_SuMenuLang_SuMenu]
GO
ALTER TABLE [dbo].[SuProfileLang]  WITH CHECK ADD  CONSTRAINT [FK_SuProfileLang_DbLanguage] FOREIGN KEY([LanguageCode])
REFERENCES [dbo].[DbLanguage] ([LanguageCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SuProfileLang] CHECK CONSTRAINT [FK_SuProfileLang_DbLanguage]
GO
ALTER TABLE [dbo].[SuProfileLang]  WITH CHECK ADD  CONSTRAINT [FK_SuProfileLang_SuProfile] FOREIGN KEY([ProfileCode])
REFERENCES [dbo].[SuProfile] ([ProfileCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SuProfileLang] CHECK CONSTRAINT [FK_SuProfileLang_SuProfile]
GO
ALTER TABLE [dbo].[SuProfileMenu]  WITH CHECK ADD  CONSTRAINT [FK_SuProfileMenu_SuMenu] FOREIGN KEY([MenuCode])
REFERENCES [dbo].[SuMenu] ([MenuCode])
GO
ALTER TABLE [dbo].[SuProfileMenu] CHECK CONSTRAINT [FK_SuProfileMenu_SuMenu]
GO
ALTER TABLE [dbo].[SuProfileMenu]  WITH CHECK ADD  CONSTRAINT [FK_SuProfileMenu_SuProfile] FOREIGN KEY([ProfileCode])
REFERENCES [dbo].[SuProfile] ([ProfileCode])
GO
ALTER TABLE [dbo].[SuProfileMenu] CHECK CONSTRAINT [FK_SuProfileMenu_SuProfile]
GO
ALTER TABLE [dbo].[SuProgramLabel]  WITH CHECK ADD  CONSTRAINT [SuProgramLabel_FK] FOREIGN KEY([ProgramCode])
REFERENCES [dbo].[SuProgram] ([ProgramCode])
GO
ALTER TABLE [dbo].[SuProgramLabel] CHECK CONSTRAINT [SuProgramLabel_FK]
GO
ALTER TABLE [dbo].[SuUserProfile]  WITH CHECK ADD  CONSTRAINT [FK_SuUserProfile_SuProfile] FOREIGN KEY([ProfileCode])
REFERENCES [dbo].[SuProfile] ([ProfileCode])
GO
ALTER TABLE [dbo].[SuUserProfile] CHECK CONSTRAINT [FK_SuUserProfile_SuProfile]
GO
ALTER TABLE [dbo].[SuUserProfile]  WITH CHECK ADD  CONSTRAINT [FK_SuUserProfile_SuUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[SuUser] ([UserId])
GO
ALTER TABLE [dbo].[SuUserProfile] CHECK CONSTRAINT [FK_SuUserProfile_SuUser]
GO
