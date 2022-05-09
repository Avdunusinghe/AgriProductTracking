using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriProductTracker.Data.Migrations
{
    public partial class AgriProductTracker00003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AttachmentName",
                table: "ProductImage",
                newName: "AttachementName");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedOn", "Password", "UpdatedOn" },
                values: new object[] { new DateTime(2022, 5, 8, 14, 4, 31, 321, DateTimeKind.Utc).AddTicks(8065), "r+rNgUse87Xp2SO7fOpqOqjws8xSGrPzr2nBT6QKW7U=", new DateTime(2022, 5, 8, 14, 4, 31, 321, DateTimeKind.Utc).AddTicks(8068) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedOn", "Password", "UpdatedOn" },
                values: new object[] { new DateTime(2022, 5, 8, 14, 4, 31, 374, DateTimeKind.Utc).AddTicks(2589), "DgBvmVmvGdzZv+1HA7atLH67moRXwmUs9QWlqzni+ow=", new DateTime(2022, 5, 8, 14, 4, 31, 374, DateTimeKind.Utc).AddTicks(2593) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AttachementName",
                table: "ProductImage",
                newName: "AttachmentName");

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
        }
    }
}
