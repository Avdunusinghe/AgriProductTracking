using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriProductTracker.Data.Migrations
{
    public partial class AgriProductTracker00002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Province",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "UpdatedOn" },
                values: new object[] { new DateTime(2022, 5, 18, 6, 19, 3, 11, DateTimeKind.Utc).AddTicks(7669), new DateTime(2022, 5, 18, 6, 19, 3, 11, DateTimeKind.Utc).AddTicks(7672) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "UpdatedOn" },
                values: new object[] { new DateTime(2022, 5, 18, 6, 19, 3, 11, DateTimeKind.Utc).AddTicks(7673), new DateTime(2022, 5, 18, 6, 19, 3, 11, DateTimeKind.Utc).AddTicks(7673) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "UpdatedOn" },
                values: new object[] { new DateTime(2022, 5, 18, 6, 19, 3, 11, DateTimeKind.Utc).AddTicks(7674), new DateTime(2022, 5, 18, 6, 19, 3, 11, DateTimeKind.Utc).AddTicks(7674) });

            migrationBuilder.UpdateData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 },
                columns: new[] { "CreatedOn", "UpdatedOn" },
                values: new object[] { new DateTime(2022, 5, 18, 6, 19, 3, 14, DateTimeKind.Utc).AddTicks(8817), new DateTime(2022, 5, 18, 6, 19, 3, 14, DateTimeKind.Utc).AddTicks(8822) });

            migrationBuilder.UpdateData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 2 },
                columns: new[] { "CreatedOn", "UpdatedOn" },
                values: new object[] { new DateTime(2022, 5, 18, 6, 19, 3, 14, DateTimeKind.Utc).AddTicks(8824), new DateTime(2022, 5, 18, 6, 19, 3, 14, DateTimeKind.Utc).AddTicks(8824) });

            migrationBuilder.UpdateData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 3, 3 },
                columns: new[] { "CreatedOn", "UpdatedOn" },
                values: new object[] { new DateTime(2022, 5, 18, 6, 19, 3, 14, DateTimeKind.Utc).AddTicks(8825), new DateTime(2022, 5, 18, 6, 19, 3, 14, DateTimeKind.Utc).AddTicks(8825) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Province",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ShippingAddress",
                table: "Order");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "UpdatedOn" },
                values: new object[] { new DateTime(2022, 5, 11, 12, 28, 55, 781, DateTimeKind.Utc).AddTicks(6990), new DateTime(2022, 5, 11, 12, 28, 55, 781, DateTimeKind.Utc).AddTicks(6991) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "UpdatedOn" },
                values: new object[] { new DateTime(2022, 5, 11, 12, 28, 55, 781, DateTimeKind.Utc).AddTicks(6993), new DateTime(2022, 5, 11, 12, 28, 55, 781, DateTimeKind.Utc).AddTicks(6994) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "UpdatedOn" },
                values: new object[] { new DateTime(2022, 5, 11, 12, 28, 55, 781, DateTimeKind.Utc).AddTicks(6995), new DateTime(2022, 5, 11, 12, 28, 55, 781, DateTimeKind.Utc).AddTicks(6995) });

            migrationBuilder.UpdateData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 },
                columns: new[] { "CreatedOn", "UpdatedOn" },
                values: new object[] { new DateTime(2022, 5, 11, 12, 28, 55, 788, DateTimeKind.Utc).AddTicks(4476), new DateTime(2022, 5, 11, 12, 28, 55, 788, DateTimeKind.Utc).AddTicks(4478) });

            migrationBuilder.UpdateData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 2 },
                columns: new[] { "CreatedOn", "UpdatedOn" },
                values: new object[] { new DateTime(2022, 5, 11, 12, 28, 55, 788, DateTimeKind.Utc).AddTicks(4482), new DateTime(2022, 5, 11, 12, 28, 55, 788, DateTimeKind.Utc).AddTicks(4483) });

            migrationBuilder.UpdateData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 3, 3 },
                columns: new[] { "CreatedOn", "UpdatedOn" },
                values: new object[] { new DateTime(2022, 5, 11, 12, 28, 55, 788, DateTimeKind.Utc).AddTicks(4530), new DateTime(2022, 5, 11, 12, 28, 55, 788, DateTimeKind.Utc).AddTicks(4531) });
        }
    }
}
