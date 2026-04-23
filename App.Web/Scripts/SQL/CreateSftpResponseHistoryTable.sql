-- ============================================================
-- SQL Script: Create SftpResponseHistory Table
-- ============================================================

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SftpResponseHistory]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[SftpResponseHistory](
        [Id] [int] IDENTITY(1,1) NOT NULL,
        [FileName] [nvarchar](max) NULL,
        [FileType] [nvarchar](50) NULL,
        [ProcessDate] [datetime] NOT NULL,
        [Status] [nvarchar](50) NULL,
        [Remarks] [nvarchar](max) NULL,
        
        -- BaseEntity fields
        [CreatedBy] [int] NOT NULL,
        [CreatedOn] [smalldatetime] NULL,
        [ModifiedBy] [int] NOT NULL,
        [ModifiedOn] [smalldatetime] NULL,
        [IsActive] [bit] NOT NULL,

        CONSTRAINT [PK_SftpResponseHistory] PRIMARY KEY CLUSTERED 
        (
            [Id] ASC
        ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

    -- Set default value for IsActive
    ALTER TABLE [dbo].[SftpResponseHistory] ADD CONSTRAINT [DF_SftpResponseHistory_IsActive] DEFAULT ((1)) FOR [IsActive]
    
    PRINT 'SftpResponseHistory table created successfully.';
END
ELSE
BEGIN
    PRINT 'SftpResponseHistory table already exists.';
END
