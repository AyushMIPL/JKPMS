-- ============================================================
-- SQL Script: Add "Transaction Monitoring" to SecModule 
-- and grant permissions via SecRoleModule
-- ============================================================
-- IMPORTANT: Run this on BOTH databases (Jammu & Kashmir regions)
-- if the application uses separate databases per region.
-- ============================================================

-- Step 1: Find the Reports parent module ID
-- Run this first to get the Reports ParentId:
SELECT Id, ModuleName, ParentId, ControllerName, ActionName, DisplayOrder 
FROM SecModule 
WHERE ModuleName LIKE '%Report%' AND ParentId = 0 AND IsActive = 1;

-- NOTE: Replace @ReportsParentId below with the actual Id from the query above.
-- For example, if the Reports module Id is 9, set @ReportsParentId = 9

DECLARE @ReportsParentId INT;
SET @ReportsParentId = 0; -- << REPLACE THIS with the actual Reports parent Id from the query above

-- Step 2: Check if "Transaction Monitoring" already exists
IF NOT EXISTS (SELECT 1 FROM SecModule WHERE ControllerName = 'Transaction' AND ActionName = 'Index' AND IsActive = 1)
BEGIN
    -- Step 3: Insert SecModule entry for "Transaction Monitoring"
    INSERT INTO SecModule (ModuleName, ModuleDesc, ParentId, Url, ActionName, ControllerName, ModuleClass, DisplayOrder, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn, IsActive)
    VALUES (
        'Transaction Monitoring',       -- ModuleName
        'View and analyze transaction data from uploaded files',  -- ModuleDesc
        @ReportsParentId,               -- ParentId (Reports menu)
        '/Transaction/Index',           -- Url
        'Index',                        -- ActionName
        'Transaction',                  -- ControllerName
        'fa fa-exchange fa-lg',         -- ModuleClass (icon)
        160,                            -- DisplayOrder (adjust as needed to position within Reports)
        1,                              -- CreatedBy (admin user)
        GETDATE(),                      -- CreatedOn
        1,                              -- ModifiedBy
        GETDATE(),                      -- ModifiedOn
        1                               -- IsActive
    );

    PRINT 'SecModule entry created successfully.';
    
    -- Step 4: Get the newly inserted module Id
    DECLARE @NewModuleId INT;
    SET @NewModuleId = SCOPE_IDENTITY();
    
    PRINT 'New Module ID: ' + CAST(@NewModuleId AS VARCHAR(10));

    -- Step 5: Grant permissions to all existing roles
    -- This inserts a SecRoleModule entry for each role, granting View permission
    -- Adjust ViewPermission/AddPermssion/EditPermission/DeletePermission as needed per role
    INSERT INTO SecRoleModule (RoleID, ModuleID, ViewPermission, AddPermssion, EditPermission, DeletePermission, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn, IsActive)
    SELECT 
        r.Id,                -- RoleID
        @NewModuleId,        -- ModuleID
        1,                   -- ViewPermission (granted)
        0,                   -- AddPermssion (not needed for view-only screen)
        0,                   -- EditPermission
        0,                   -- DeletePermission
        1,                   -- CreatedBy
        GETDATE(),           -- CreatedOn
        1,                   -- ModifiedBy
        GETDATE(),           -- ModifiedOn
        1                    -- IsActive
    FROM AppRole r
    WHERE r.Name IN ('Admin');  -- << Add more roles as needed, e.g. 'Admin', 'Director Finance (DFSW)', etc.

    PRINT 'SecRoleModule permissions granted successfully.';
END
ELSE
BEGIN
    PRINT 'Transaction Monitoring module already exists in SecModule.';
END

-- ============================================================
-- VERIFICATION: Run these after the insert to confirm
-- ============================================================
-- SELECT * FROM SecModule WHERE ControllerName = 'Transaction';
-- SELECT * FROM SecRoleModule WHERE ModuleID = (SELECT Id FROM SecModule WHERE ControllerName = 'Transaction' AND ActionName = 'Index');
