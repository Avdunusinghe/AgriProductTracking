BEGIN TRANSACTION;
GO

EXEC sp_rename N'[ProductImage].[AttachmentName]', N'AttachementName', N'COLUMN';
GO

UPDATE [User] SET [CreatedOn] = '2022-05-08T14:04:31.3218065Z', [Password] = N'r+rNgUse87Xp2SO7fOpqOqjws8xSGrPzr2nBT6QKW7U=', [UpdatedOn] = '2022-05-08T14:04:31.3218068Z'
WHERE [Id] = CAST(1 AS bigint);
SELECT @@ROWCOUNT;

GO

UPDATE [User] SET [CreatedOn] = '2022-05-08T14:04:31.3742589Z', [Password] = N'DgBvmVmvGdzZv+1HA7atLH67moRXwmUs9QWlqzni+ow=', [UpdatedOn] = '2022-05-08T14:04:31.3742593Z'
WHERE [Id] = CAST(2 AS bigint);
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220508140431_AgriProductTracker00003', N'6.0.4');
GO

COMMIT;
GO

