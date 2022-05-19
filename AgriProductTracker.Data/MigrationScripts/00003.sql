BEGIN TRANSACTION;
GO

DROP INDEX [IX_OrderItem_ProductId] ON [OrderItem];
GO

EXEC sp_rename N'[Order].[DeleveryServiceId]', N'DeliveryServiceId', N'COLUMN';
GO

EXEC sp_rename N'[DeliveryService].[DiliveryDetails]', N'DeliveryDetails', N'COLUMN';
GO

UPDATE [User] SET [CreatedOn] = '2022-05-19T03:25:57.5261726Z', [UpdatedOn] = '2022-05-19T03:25:57.5261728Z'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [User] SET [CreatedOn] = '2022-05-19T03:25:57.5261729Z', [UpdatedOn] = '2022-05-19T03:25:57.5261729Z'
WHERE [Id] = 2;
SELECT @@ROWCOUNT;

GO

UPDATE [User] SET [CreatedOn] = '2022-05-19T03:25:57.5261730Z', [UpdatedOn] = '2022-05-19T03:25:57.5261730Z'
WHERE [Id] = 3;
SELECT @@ROWCOUNT;

GO

UPDATE [UserRole] SET [CreatedOn] = '2022-05-19T03:25:57.5303873Z', [UpdatedOn] = '2022-05-19T03:25:57.5303875Z'
WHERE [RoleId] = 1 AND [UserId] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [UserRole] SET [CreatedOn] = '2022-05-19T03:25:57.5303877Z', [UpdatedOn] = '2022-05-19T03:25:57.5303877Z'
WHERE [RoleId] = 2 AND [UserId] = 2;
SELECT @@ROWCOUNT;

GO

UPDATE [UserRole] SET [CreatedOn] = '2022-05-19T03:25:57.5303878Z', [UpdatedOn] = '2022-05-19T03:25:57.5303887Z'
WHERE [RoleId] = 3 AND [UserId] = 3;
SELECT @@ROWCOUNT;

GO

CREATE INDEX [IX_OrderItem_ProductId] ON [OrderItem] ([ProductId]);
GO

CREATE INDEX [IX_Order_DeliveryServiceId] ON [Order] ([DeliveryServiceId]);
GO

ALTER TABLE [Order] ADD CONSTRAINT [FK_Order_DeliveryService_DeliveryServiceId] FOREIGN KEY ([DeliveryServiceId]) REFERENCES [DeliveryService] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220519032558_AgriProductTracker00003', N'6.0.5');
GO

COMMIT;
GO

