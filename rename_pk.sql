select * into ValveSections_010319 from ValveSections


sp_rename @objname = N'[ValveSectionFeatures].[PK_dbo.ValveSectionFeatures]', @newname = N'PK_ValveSectionFeatures'

sp_rename @objname = N'[ValveSections].[PK_dbo.ValveSections]', @newname = N'PK_ValveSections'
 