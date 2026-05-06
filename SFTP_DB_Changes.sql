-- 1. Add RecordCount to SftpResponseHistory
IF NOT EXISTS (
    SELECT * FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'SftpResponseHistory' AND COLUMN_NAME = 'RecordCount'
)
BEGIN
    ALTER TABLE SftpResponseHistory ADD RecordCount INT NULL;
END
GO

-- 2. Create SftpProcessingLogs
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SftpProcessingLogs]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[SftpProcessingLogs](
        [Id] [int] IDENTITY(1,1) NOT NULL,
        [FileName] [nvarchar](255) NULL,
        [FileType] [nvarchar](50) NULL,
        [RowNumber] [int] NULL,
        [ErrorType] [nvarchar](50) NULL,
        [ErrorMessage] [nvarchar](max) NULL,
        [CreatedOn] [datetime] NOT NULL DEFAULT GETDATE(),
        CONSTRAINT [PK_SftpProcessingLogs] PRIMARY KEY CLUSTERED ([Id] ASC)
    )
END
GO

-- 3. Create Staging Tables
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Stg_ValidationResponse]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Stg_ValidationResponse](
        [APPLICATION_REFERENCE_NO] [nvarchar](100) NULL,
        [DISTRICT] [nvarchar](100) NULL,
        [BENE_IFSC] [nvarchar](100) NULL,
        [NAME_OF_APPLICANT] [nvarchar](200) NULL,
        [ACCOUNTNO] [nvarchar](20) NULL,
        [CATEGORY] [nvarchar](20) NULL,
        [CBS_NAME] [nvarchar](100) NULL,
        [BRANCH_CODE] [nvarchar](100) NULL,
        [ACCOUNT_STATUS] [nvarchar](100) NULL,
        [AADHAAR_STATUS] [nvarchar](100) NULL,
        [ACCT_SCHEME_TYPE] [nvarchar](50) NULL
    )
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Stg_DisbursementResponse]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Stg_DisbursementResponse](
        [ApplicationRefNo] [nvarchar](100) NULL,
        [TransactionRefNo] [nvarchar](100) NULL,
        [Department] [nvarchar](100) NULL,
        [DepartmentAccountNo] [nvarchar](50) NULL,
        [Amount] [nvarchar](50) NULL,
        [DateText] [nvarchar](50) NULL,
        [DepartmentBankName] [nvarchar](100) NULL,
        [DepartmentBankIFSC] [nvarchar](50) NULL,
        [Name] [nvarchar](200) NULL,
        [IFSC] [nvarchar](50) NULL,
        [AccountNo] [nvarchar](50) NULL,
        [Scheme] [nvarchar](50) NULL,
        [Status] [nvarchar](50) NULL,
        [TransactionReference] [nvarchar](100) NULL,
        [TransactionDate] [nvarchar](50) NULL,
        [Remarks] [nvarchar](max) NULL
    )
END
GO

-- 4. Stored Procedures
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'USP_ProcessValidationResponseFile')
    DROP PROCEDURE USP_ProcessValidationResponseFile
GO

CREATE PROCEDURE [dbo].[USP_ProcessValidationResponseFile]
    @filePathWithName NVARCHAR(500),
    @UserId INT,
    @fileName NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @userName NVARCHAR(100);
    SELECT @userName = UserName FROM AppUser WHERE id = @UserId;

    -- Clear staging table
    TRUNCATE TABLE [dbo].[Stg_ValidationResponse];

    -- Bulk Insert
    DECLARE @query NVARCHAR(MAX);
    SET @query = N'BULK INSERT [dbo].[Stg_ValidationResponse]
    FROM '''+@filePathWithName+'''
    WITH (
       FIELDTERMINATOR = '','',  
       ROWTERMINATOR = ''\n'',   
       FIRSTROW = 2           
    );';
    
    BEGIN TRY
        EXEC (@query);
    END TRY
    BEGIN CATCH
        INSERT INTO SftpProcessingLogs (FileName, FileType, ErrorType, ErrorMessage)
        VALUES (@fileName, 'Validation', 'BCP Error', ERROR_MESSAGE());
        RETURN;
    END CATCH

    -- Validate Data
    -- E.g., Identify rows with missing mandatory fields and log them
    INSERT INTO SftpProcessingLogs (FileName, FileType, ErrorType, ErrorMessage)
    SELECT @fileName, 'Validation', 'Data Error', 'Missing Application Reference No'
    FROM [dbo].[Stg_ValidationResponse]
    WHERE ISNULL(APPLICATION_REFERENCE_NO, '') = '';

    -- Process logic (converted from existing UpdateEmpMasterEmpBankDetails)
    IF OBJECT_ID('tempdb..#temp6') IS NOT NULL DROP TABLE #temp6;
    
    CREATE TABLE #temp6 (
        empl_code NVARCHAR(6),
        [CBS_NAME_OF_APPLICANT] NVARCHAR(100),
        APPLICATION_REFERENCE_NO NVARCHAR(100),
        DISTRICT NVARCHAR(100),
        BENE_IFSC NVARCHAR(100),
        NAME_OF_APPLICANT NVARCHAR(200),
        ACCOUNTNO NVARCHAR(20),
        CBS_NAME NVARCHAR(100),
        BRANCH_CODE NVARCHAR(100),
        ACCOUNT_STATUS NVARCHAR(100),
        AADHAAR_STATUS NVARCHAR(100),
        ACCT_SCHEME_TYPE NVARCHAR(50),
        APPLICANT_BANK_IFSC_CODE NVARCHAR(50)
    );

    INSERT INTO #temp6
    SELECT ME.Empl_Code, 
    CONCAT(CASE WHEN ME.first_name IS NOT NULL AND ME.first_name <> '' THEN ME.first_name + ' ' ELSE '' END,CASE WHEN ME.middle_name IS NOT NULL AND ME.middle_name <> '' THEN ME.middle_name + ' ' ELSE '' END, CASE WHEN ME.last_name IS NOT NULL AND ME.last_name <> '' THEN ME.last_name + ' ' ELSE '' END ) AS [CBS_NAME_OF_APPLICANT],
    MBD.APPLICATION_REFERENCE_NO,
    MBD.DISTRICT,
    MBD.BENE_IFSC,
    MBD.NAME_OF_APPLICANT,
    MBD.ACCOUNTNO,
    MBD.CBS_NAME,
    MBD.BRANCH_CODE,
    MBD.ACCOUNT_STATUS,
    MBD.AADHAAR_STATUS,
    MBD.ACCT_SCHEME_TYPE,
    MEBD.APPLICANT_BANK_IFSC_CODE
    FROM [dbo].[Stg_ValidationResponse] MBD
    JOIN MasterEmployee ME ON ME.ApplicationReferenceNo = MBD.APPLICATION_REFERENCE_NO
    JOIN MasterEmpBankDetails MEBD ON MEBD.empl_code = ME.Empl_Code;

    -- Update MasterEmpBankDetails
    MERGE INTO MasterEmpBankDetails AS target
    USING #temp6 AS source
    ON target.empl_code = source.empl_code
    WHEN MATCHED THEN
        UPDATE SET
            target.CBS_NAME = REPLACE(REPLACE(source.CBS_NAME, char(10),''),char(13),''),
            target.BRANCH_CODE = REPLACE(REPLACE(source.BRANCH_CODE, char(10),''),char(13),''),
            target.ACCOUNT_STATUS = CASE WHEN REPLACE(REPLACE(source.ACCOUNT_STATUS, char(10),''),char(13),'') = 'ACTIVE' AND REPLACE(REPLACE(source.AADHAAR_STATUS, char(10),''),char(13),'') = 'AADHAAR SEEDED' AND REPLACE(REPLACE(source.ACCT_SCHEME_TYPE, char(10),''),char(13),'') = 'SAVINGS ACCOUNT' AND REPLACE(REPLACE(source.CBS_NAME_OF_APPLICANT, char(10),''),char(13),'') = REPLACE(REPLACE(source.CBS_NAME, char(10),''),char(13),'') AND REPLACE(REPLACE(source.APPLICANT_BANK_IFSC_CODE, char(10),''),char(13),'') = REPLACE(REPLACE(source.BRANCH_CODE, char(10),''),char(13),'') THEN 'ACTIVE' ELSE NULL END,
            target.AADHAAR_STATUS = REPLACE(REPLACE(source.AADHAAR_STATUS, char(10),''),char(13),''),
            target.ACCT_SCHEME_TYPE = REPLACE(REPLACE(source.ACCT_SCHEME_TYPE, char(10),''),char(13),''),
            target.IsUpload = 1,
            target.UploadedBy = @UserId,
            target.UploadedOn = GETDATE(),
            target.Remarks = CONCAT(target.Remarks, (CASE WHEN target.Remarks = NULL OR target.Remarks = '' THEN '' ELSE '' END), CONCAT((CASE WHEN REPLACE(REPLACE(source.ACCOUNT_STATUS, char(10),''),char(13),'') <> 'ACTIVE' THEN NCHAR(8226) + ' [' + FORMAT(GETDATE(), 'dd/MM/yyyy HH:mm') + '] [' + @userName + '] : Account status : ' + source.ACCOUNT_STATUS + '' ELSE '' END), (CASE WHEN REPLACE(REPLACE(source.AADHAAR_STATUS, char(10),''),char(13),'') <> 'AADHAAR SEEDED' THEN NCHAR(8226) + ' [' + FORMAT(GETDATE(), 'dd/MM/yyyy HH:mm') + '] [' + @userName + '] : Aadhaar Status : ' + source.AADHAAR_STATUS + '' ELSE '' END), (CASE WHEN REPLACE(REPLACE(source.ACCT_SCHEME_TYPE, char(10),''),char(13),'') <> 'SAVINGS ACCOUNT' THEN NCHAR(8226) + ' [' + FORMAT(GETDATE(), 'dd/MM/yyyy HH:mm') + '] [' + @userName + '] : Account Scheme Type : ' + source.ACCT_SCHEME_TYPE + '' ELSE '' END), (CASE WHEN REPLACE(REPLACE(source.CBS_NAME_OF_APPLICANT, char(10),''),char(13),'') <> REPLACE(REPLACE(source.CBS_NAME, char(10),''),char(13),'') THEN NCHAR(8226) + ' [' + FORMAT(GETDATE(), 'dd/MM/yyyy HH:mm') + '] [' + @userName + '] : Applicant Name Mismatch ( ' + source.CBS_NAME_OF_APPLICANT + ', ' + source.CBS_NAME + ') </br>' ELSE '' END), (CASE WHEN REPLACE(REPLACE(source.APPLICANT_BANK_IFSC_CODE, char(10),''),char(13),'') <> REPLACE(REPLACE(source.BRANCH_CODE, char(10),''),char(13),'') THEN NCHAR(8226) + ' [' + FORMAT(GETDATE(), 'dd/MM/yyyy HH:mm') + '] [' + @userName + '] : IFSC Code Mismacth ( ' + source.APPLICANT_BANK_IFSC_CODE + ', ' + source.BRANCH_CODE + ')' ELSE '' END)));

    -- Update MasterEmployee
    MERGE INTO MasterEmployee AS target
    USING #temp6 AS source
    ON target.empl_code = source.empl_code
    WHEN MATCHED THEN
        UPDATE SET
            target.hold_pymnt = CASE WHEN REPLACE(REPLACE(source.ACCOUNT_STATUS, char(10),''),char(13),'') = 'ACTIVE' AND REPLACE(REPLACE(source.AADHAAR_STATUS, char(10),''),char(13),'') = 'AADHAAR SEEDED' AND REPLACE(REPLACE(source.ACCT_SCHEME_TYPE, char(10),''),char(13),'') = 'SAVINGS ACCOUNT' AND REPLACE(REPLACE(source.CBS_NAME_OF_APPLICANT, char(10),''),char(13),'') = REPLACE(REPLACE(source.CBS_NAME, char(10),''),char(13),'') AND REPLACE(REPLACE(source.APPLICANT_BANK_IFSC_CODE, char(10),''),char(13),'') = REPLACE(REPLACE(source.BRANCH_CODE, char(10),''),char(13),'') THEN 'N' ELSE 'Y' END,
            target.ReasonForChange = CONCAT(target.ReasonForChange, (CASE WHEN target.ReasonForChange = NULL OR target.ReasonForChange = '' THEN '' ELSE '' END), CONCAT((CASE WHEN REPLACE(REPLACE(source.ACCOUNT_STATUS, char(10),''),char(13),'') <> 'ACTIVE' THEN NCHAR(8226) + ' [' + FORMAT(GETDATE(), 'dd/MM/yyyy HH:mm') + '] [' + @userName + '] : Account status : ' + source.ACCOUNT_STATUS + '' ELSE '' END), (CASE WHEN REPLACE(REPLACE(source.AADHAAR_STATUS, char(10),''),char(13),'') <> 'AADHAAR SEEDED' THEN NCHAR(8226) + ' [' + FORMAT(GETDATE(), 'dd/MM/yyyy HH:mm')  + '] [' + @userName + '] : Aadhaar Status : ' + source.AADHAAR_STATUS + '' ELSE '' END), (CASE WHEN REPLACE(REPLACE(source.ACCT_SCHEME_TYPE, char(10),''),char(13),'') <> 'SAVINGS ACCOUNT' THEN NCHAR(8226) + ' [' + FORMAT(GETDATE(), 'dd/MM/yyyy HH:mm') + ' Account Scheme Type : ' + source.ACCT_SCHEME_TYPE + '' ELSE '' END), (CASE WHEN REPLACE(REPLACE(source.CBS_NAME_OF_APPLICANT, char(10),''),char(13),'') <> REPLACE(REPLACE(source.CBS_NAME, char(10),''),char(13),'') THEN NCHAR(8226) + ' [' + FORMAT(GETDATE(), 'dd/MM/yyyy HH:mm') + '] [' + @userName + '] : Applicant Name Mismatch ( ' + source.CBS_NAME_OF_APPLICANT + ', ' + source.CBS_NAME + ')' ELSE '' END), (CASE WHEN REPLACE(REPLACE(source.APPLICANT_BANK_IFSC_CODE, char(10),''),char(13),'') <> REPLACE(REPLACE(source.BRANCH_CODE, char(10),''),char(13),'') THEN NCHAR(8226) + ' [' + FORMAT(GETDATE(), 'dd/MM/yyyy HH:mm') + '] [' + @userName + '] : IFSC Code Mismacth ( ' + source.APPLICANT_BANK_IFSC_CODE + ', ' + source.BRANCH_CODE + ')' ELSE '' END)));

    SELECT COUNT(1) AS RecordCount FROM [dbo].[Stg_ValidationResponse];
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'USP_ProcessDisbursementResponseFile')
    DROP PROCEDURE USP_ProcessDisbursementResponseFile
GO

CREATE PROCEDURE [dbo].[USP_ProcessDisbursementResponseFile]
    @filePathWithName NVARCHAR(500),
    @UserId INT,
    @fileName NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    -- Clear staging table
    TRUNCATE TABLE [dbo].[Stg_DisbursementResponse];

    -- Bulk Insert
    DECLARE @query NVARCHAR(MAX);
    SET @query = N'BULK INSERT [dbo].[Stg_DisbursementResponse]
    FROM '''+@filePathWithName+'''
    WITH (
       FIELDTERMINATOR = '','',  
       ROWTERMINATOR = ''\n'',   
       FIRSTROW = 2           
    );';
    
    BEGIN TRY
        EXEC (@query);
    END TRY
    BEGIN CATCH
        INSERT INTO SftpProcessingLogs (FileName, FileType, ErrorType, ErrorMessage)
        VALUES (@fileName, 'Disbursement', 'BCP Error', ERROR_MESSAGE());
        RETURN;
    END CATCH

    -- Validate Data
    INSERT INTO SftpProcessingLogs (FileName, FileType, ErrorType, ErrorMessage)
    SELECT @fileName, 'Disbursement', 'Data Error', 'Invalid Amount'
    FROM [dbo].[Stg_DisbursementResponse]
    WHERE TRY_CONVERT(FLOAT, [Amount]) IS NULL AND [Amount] IS NOT NULL;

    -- Update MasterEmployeeIncomes
    UPDATE I SET I.inc_rate = TRY_CONVERT(FLOAT, T.Amount)
    FROM MasterEmployeeIncomes I 
    INNER JOIN [dbo].[Stg_DisbursementResponse] T ON I.ApplicationReferenceNo = T.ApplicationRefNo
    WHERE TRY_CONVERT(FLOAT, T.Amount) IS NOT NULL;

    -- Update MasterEmpBankDetails
    UPDATE I SET 
        I.APPLICANT_ACCOUNT_NO = T.AccountNo,
        I.ACCT_SCHEME_TYPE = T.Scheme, 
        I.ACCOUNT_STATUS='ACTIVE',
        I.AADHAAR_STATUS='AADHAAR SEEDED',
        I.NAME_OF_APPLICANT = T.Name,
        I.amount = TRY_CONVERT(FLOAT, T.Amount)
    FROM MasterEmpBankDetails I 
    INNER JOIN [dbo].[Stg_DisbursementResponse] T ON I.APPLICATION_REFERENCE_NO = T.ApplicationRefNo
    WHERE TRY_CONVERT(FLOAT, T.Amount) IS NOT NULL;

    -- Update MasterEmployee hold_pymnt
    UPDATE ME SET ME.hold_pymnt = 'N'
    FROM MasterEmployee ME
    INNER JOIN MasterEmpBankDetails MEBD on ME.Empl_Code = MEBD.Empl_Code
    INNER JOIN [dbo].[Stg_DisbursementResponse] T ON MEBD.APPLICATION_REFERENCE_NO = T.ApplicationRefNo;

    -- Insert into history tables txnHeader and txnDetail
    DECLARE @HeaderId INT;
    DECLARE @TxnDate DATETIME;

    SELECT TOP 1 @TxnDate = dbo.fn_ConvertCustomDateToDatetime([DateText])
    FROM [dbo].[Stg_DisbursementResponse]
    WHERE [DateText] IS NOT NULL 
      AND dbo.fn_ConvertCustomDateToDatetime([DateText]) IS NOT NULL;

    INSERT INTO dbo.txnHeader (SourceTable, TxnDate)
    VALUES (@fileName, @TxnDate);

    SET @HeaderId = SCOPE_IDENTITY();

    INSERT INTO dbo.txnDetail 
    (
        HeaderId, [Application Reference No#], [Application Reference No#1], [Department],
        [Department Account No#], [Amount], [DateText], [Department Bank Name],
        [Department Bank IFSC], [Name], [IFSC], [Account No#], [Scheme],[Status],[TransactionReference],[TransactionDate],[Remarks]
    )
    SELECT 
        @HeaderId, ApplicationRefNo, TransactionRefNo, [Department],
        DepartmentAccountNo, TRY_CONVERT(FLOAT, [Amount]), [DateText],
        DepartmentBankName, DepartmentBankIFSC, [Name], [IFSC], AccountNo, [Scheme], Status, TransactionReference, TransactionDate, Remarks
    FROM [dbo].[Stg_DisbursementResponse]
    WHERE TRY_CONVERT(FLOAT, [Amount]) IS NOT NULL;

    SELECT COUNT(1) AS RecordCount FROM [dbo].[Stg_DisbursementResponse];
END
GO
