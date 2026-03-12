-- ============================================================
-- FIX: Move "Transaction Monitoring" under the Reports menu
-- ============================================================

-- Step 1: Find the Reports parent module ID
SELECT Id, ModuleName, ParentId, DisplayOrder 
FROM SecModule 
WHERE ModuleName LIKE '%Report%' AND ParentId = 0 AND IsActive = 1;

-- Step 2: Update Transaction Monitoring to be a child of Reports
-- Replace <<REPORTS_ID>> with the Id from Step 1 above
UPDATE SecModule
SET ParentId = <<REPORTS_ID>>
WHERE ControllerName = 'Transaction' AND ActionName = 'Index' AND IsActive = 1;

-- Verify
SELECT Id, ModuleName, ParentId, DisplayOrder 
FROM SecModule 
WHERE ControllerName = 'Transaction';
