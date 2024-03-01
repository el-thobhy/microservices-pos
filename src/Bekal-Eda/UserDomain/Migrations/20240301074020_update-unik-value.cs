using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User.Domain.Migrations
{
    /// <inheritdoc />
    public partial class updateunikvalue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("927913c6-bde4-432b-9af8-859e5c9a71b9"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Modified", "Password", "Status", "Type", "UserName" },
                values: new object[] { new Guid("3ec89007-33aa-4276-accf-a14da9657ed5"), "auriwanyasper007@gmail.com", "Super", "User", new DateTime(2024, 3, 1, 14, 40, 19, 906, DateTimeKind.Local).AddTicks(7241), "5ce41ada64f1e8ffb0acfaafa622b141438f3a5777785e7f0b830fb73e40d3d6", 0, 1, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserName",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3ec89007-33aa-4276-accf-a14da9657ed5"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Modified", "Password", "Status", "Type", "UserName" },
                values: new object[] { new Guid("927913c6-bde4-432b-9af8-859e5c9a71b9"), "auriwanyasper007@gmail.com", "Super", "User", new DateTime(2024, 2, 29, 13, 36, 26, 992, DateTimeKind.Local).AddTicks(5442), "5ce41ada64f1e8ffb0acfaafa622b141438f3a5777785e7f0b830fb73e40d3d6", 0, 1, "admin" });
        }
    }
}
