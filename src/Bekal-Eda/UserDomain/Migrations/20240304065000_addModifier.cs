using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User.Domain.Migrations
{
    /// <inheritdoc />
    public partial class addModifier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3ec89007-33aa-4276-accf-a14da9657ed5"));

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "ModifiedBy", "ModifiedDate", "Password", "Status", "Type", "UserName" },
                values: new object[] { new Guid("8d40ded7-6887-4a47-b327-0dbf1c2ef44d"), "auriwanyasper007@gmail.com", "Super", "User", null, new DateTime(2024, 3, 4, 13, 50, 0, 679, DateTimeKind.Local).AddTicks(1781), "5ce41ada64f1e8ffb0acfaafa622b141438f3a5777785e7f0b830fb73e40d3d6", 0, 1, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8d40ded7-6887-4a47-b327-0dbf1c2ef44d"));

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Users");

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Modified", "Password", "Status", "Type", "UserName" },
                values: new object[] { new Guid("3ec89007-33aa-4276-accf-a14da9657ed5"), "auriwanyasper007@gmail.com", "Super", "User", new DateTime(2024, 3, 1, 14, 40, 19, 906, DateTimeKind.Local).AddTicks(7241), "5ce41ada64f1e8ffb0acfaafa622b141438f3a5777785e7f0b830fb73e40d3d6", 0, 1, "admin" });
        }
    }
}
