BEGIN TRANSACTION;
GO

ALTER TABLE [OrderItem] DROP CONSTRAINT [FK_OrderItem_Product_ProductId];
GO

CREATE TABLE [ProductImage] (
    [Id] bigint NOT NULL IDENTITY,
    [ProductId] bigint NOT NULL,
    [Attachment] nvarchar(max) NULL,
    [AttachmentName] nvarchar(max) NULL,
    CONSTRAINT [PK_ProductImage] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProductImage_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([Id]) ON DELETE CASCADE
);
GO

UPDATE [User] SET [CreatedOn] = '2022-05-08T13:56:24.8687447Z', [Password] = N'VfVhdwEeZLLo99lbFKIc0JHwxfeEVFPFWEG0poBHx/A=', [UpdatedOn] = '2022-05-08T13:56:24.8687450Z'
WHERE [Id] = CAST(1 AS bigint);
SELECT @@ROWCOUNT;

GO

UPDATE [User] SET [CreatedOn] = '2022-05-08T13:56:24.9247185Z', [Password] = N'nUNFtPYIpUHTU0IJrsH4fv4aGo2W0EwoeQlH1gGbcaU=', [UpdatedOn] = '2022-05-08T13:56:24.9247188Z'
WHERE [Id] = CAST(2 AS bigint);
SELECT @@ROWCOUNT;

GO

CREATE INDEX [IX_ProductImage_ProductId] ON [ProductImage] ([ProductId]);
GO

ALTER TABLE [OrderItem] ADD CONSTRAINT [FK_OrderItem_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220508135625_AgriProductTracker00002', N'6.0.4');
GO

COMMIT;
GO

