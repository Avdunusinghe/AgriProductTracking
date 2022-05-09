using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriProductTracker.Data.Migrations
{
    public partial class AgriProductTracker00002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Product_ProductId",
                table: "OrderItem");

            migrationBuilder.CreateTable(
                name: "ProductImage",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Attachment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttachmentName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImage_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedOn", "Password", "UpdatedOn" },
                values: new object[] { new DateTime(2022, 5, 8, 13, 56, 24, 868, DateTimeKind.Utc).AddTicks(7447), "VfVhdwEeZLLo99lbFKIc0JHwxfeEVFPFWEG0poBHx/A=", new DateTime(2022, 5, 8, 13, 56, 24, 868, DateTimeKind.Utc).AddTicks(7450) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedOn", "Password", "UpdatedOn" },
                values: new object[] { new DateTime(2022, 5, 8, 13, 56, 24, 924, DateTimeKind.Utc).AddTicks(7185), "nUNFtPYIpUHTU0IJrsH4fv4aGo2W0EwoeQlH1gGbcaU=", new DateTime(2022, 5, 8, 13, 56, 24, 924, DateTimeKind.Utc).AddTicks(7188) });

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ProductId",
                table: "ProductImage",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Product_ProductId",
                table: "OrderItem",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Product_ProductId",
                table: "OrderItem");

            migrationBuilder.DropTable(
                name: "ProductImage");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedOn", "Password", "UpdatedOn" },
                values: new object[] { new DateTime(2022, 5, 7, 11, 56, 5, 6, DateTimeKind.Utc).AddTicks(4912), "4daXfVcF7xh6hUzHFDTZtVN/33euJ2fdMtQZbFvTqG8=", new DateTime(2022, 5, 7, 11, 56, 5, 6, DateTimeKind.Utc).AddTicks(4915) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedOn", "Password", "UpdatedOn" },
                values: new object[] { new DateTime(2022, 5, 7, 11, 56, 5, 72, DateTimeKind.Utc).AddTicks(5147), "9oTQThg/VdWdzT3lniHuFGymyls+HgLF3I05FzSbwuM=", new DateTime(2022, 5, 7, 11, 56, 5, 72, DateTimeKind.Utc).AddTicks(5151) });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Product_ProductId",
                table: "OrderItem",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
