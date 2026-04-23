-- ============================================================
-- SQL Script: Add "Receive SFTP Response" to SecModule 
-- and grant permissions via SecRoleModule
-- ============================================================

DECLARE @ParentId INT;
DECLARE @DisplayOrder INT;

-- Step 1: Find the Parent ID based on the existing "Upload Pension File" module
SELECT TOP 1 @ParentId = ParentId, @DisplayOrder = DisplayOrder + 5
FROM SecModule 
WHERE ControllerName = 'PensionProcess' AND ActionName = 'UploadPensionFile' AND IsActive = 1;

-- If not found, try to find a parent named 'Pension Process'
IF (@ParentId IS NULL)
BEGIN
    SELECT TOP 1 @ParentId = Id FROM SecModule WHERE ModuleName LIKE '%Pension Process%' AND ParentId = 0 AND IsActive = 1;
    SET @DisplayOrder = 100; -- Default display order
END

-- Step 2: Check if "Receive SFTP Response" already exists
IF NOT EXISTS (SELECT 1 FROM SecModule WHERE ControllerName = 'PensionProcess' AND ActionName = 'ReceiveSftpResponse' AND IsActive = 1)
BEGIN
    -- Step 3: Insert SecModule entry for "Receive SFTP Response"
    INSERT INTO SecModule (ModuleName, ModuleDesc, ParentId, Url, ActionName, ControllerName, ModuleClass, DisplayOrder, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn, IsActive)
    VALUES (
        'Receive SFTP Response',       -- ModuleName
        'Fetch and process response files from SFTP',  -- ModuleDesc
        ISNULL(@ParentId, 0),          -- ParentId
        '/PensionProcess/ReceiveSftpResponse', -- Url
        'ReceiveSftpResponse',         -- ActionName
        'PensionProcess',              -- ControllerName
        'fa fa-download fa-lg',        -- ModuleClass (icon)
        ISNULL(@DisplayOrder, 100),    -- DisplayOrder
        1,                             -- CreatedBy (admin user)
        GETDATE(),                     -- CreatedOn
        1,                             -- ModifiedBy
        GETDATE(),                     -- ModifiedOn
        1                              -- IsActive
    );

    PRINT 'SecModule entry for "Receive SFTP Response" created successfully.';
    
    -- Step 4: Get the newly inserted module Id
    DECLARE @NewModuleId INT;
    SET @NewModuleId = SCOPE_IDENTITY();
    
    -- Step 5: Grant permissions to Admin role (and others if needed)
    INSERT INTO SecRoleModule (RoleID, ModuleID, ViewPermission, AddPermssion, EditPermission, DeletePermission, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn, IsActive)
    SELECT 
        r.Id,                -- RoleID
        @NewModuleId,        -- ModuleID
        1,                   -- ViewPermission
        1,                   -- AddPermssion
        1,                   -- EditPermission
        1,                   -- DeletePermission
        1,                   -- CreatedBy
        GETDATE(),           -- CreatedOn
        1,                   -- ModifiedBy
        GETDATE(),           -- ModifiedOn
        1                    -- IsActive
    FROM AppRole r
    WHERE r.Name IN ('Admin', 'SuperAdmin'); 

    PRINT 'Permissions granted to Admin roles.';
END
ELSE
BEGIN
    PRINT 'Receive SFTP Response module already exists in SecModule.';
END
