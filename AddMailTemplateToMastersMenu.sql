BEGIN TRANSACTION;

BEGIN TRY
    DECLARE @ParentId INT;
    DECLARE @NewModuleId INT;

    -- 1. Find the ParentId for the "Masters" Menu
    SELECT @ParentId = Id FROM [dbo].[SecModule] WHERE ModuleName = 'Masters' AND ParentId = 0 AND IsActive = 1;

    IF @ParentId IS NULL
    BEGIN
        PRINT 'Error: "Masters" menu not found. Please ensure the parent menu exists.';
        ROLLBACK TRANSACTION;
        RETURN;
    END

    -- 2. Insert the Mail Template record into SecModule
    -- Checking if it already exists to prevent duplicate entries
    IF NOT EXISTS (SELECT 1 FROM [dbo].[SecModule] WHERE ModuleName = 'Mail Template' AND ParentId = @ParentId)
    BEGIN
        -- Find the maximum DisplayOrder under the Masters menu so we can append it at the end
        DECLARE @MaxDisplayOrder INT;
        SELECT @MaxDisplayOrder = ISNULL(MAX(DisplayOrder), 0) FROM [dbo].[SecModule] WHERE ParentId = @ParentId;

        INSERT INTO [dbo].[SecModule] (
            ModuleName,
            ModuleDesc,
            ParentId,
            Url,
            ActionName,
            ControllerName,
            ModuleClass,
            DisplayOrder,
            CreatedBy,
            CreatedOn,
            IsActive,
			ModifiedBy
        )
        VALUES (
            'Mail Template',           -- ModuleName
            'Manage Email Templates',  -- ModuleDesc
            @ParentId,                 -- ParentId (Masters)
            '/MailTemplate/Index',     -- Url
            'Index',                   -- ActionName
            'MailTemplate',            -- ControllerName
            'fa fa-envelope fa-lg',    -- ModuleClass (using the envelope icon we used in _Layout)
            @MaxDisplayOrder + 1,      -- DisplayOrder
            1,                         -- CreatedBy (System/Admin ID)
            GETDATE(),                 -- CreatedOn
            1,                          -- IsActive
			1
        );

        SET @NewModuleId = SCOPE_IDENTITY();
        PRINT 'Successfully added "Mail Template" to SecModule with ID: ' + CAST(@NewModuleId AS VARCHAR);

        -- 3. Map the new module to valid Roles in SecRoleModule
        -- Assuming RoleID 1 is typically "Admin" or the highest level role. 
        -- This logic inserts permissions for all active roles that currently have access to the "Masters" parent menu.
        
        INSERT INTO [dbo].[SecRoleModule] (
            RoleID,
            ModuleID,
            AddPermssion,
            EditPermission,
            DeletePermission,
            ViewPermission,
            CreatedBy,
            CreatedOn,
            IsActive,
			ModifiedBy
        )
        SELECT 
            RoleID,
            @NewModuleId,
            1,          -- AddPermssion
            1,          -- EditPermission
            1,          -- DeletePermission
            1,          -- ViewPermission
            1,          -- CreatedBy
            GETDATE(),  -- CreatedOn
            1,           -- IsActive
			1
        FROM [dbo].[SecRoleModule]
        WHERE ModuleID = @ParentId AND ViewPermission = 1 AND IsActive = 1;

        PRINT 'Successfully mapped "Mail Template" to Roles that have access to the "Masters" menu.';
    END
    ELSE
    BEGIN
        PRINT 'Information: "Mail Template" module already exists under the "Masters" menu. Skipped insertion.';
    END

    COMMIT TRANSACTION;
    PRINT 'Transaction committed successfully.';
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    PRINT 'ERROR: Transaction rolled back.'
    PRINT 'Error Message: ' + ERROR_MESSAGE();
    PRINT 'Error Line: ' + CAST(ERROR_LINE() AS VARCHAR);
END CATCH
GO
