using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User.Domain.Migrations
{
    /// <inheritdoc />
    public partial class adduser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Modified", "Password", "Status", "Type", "UserName" },
                values: new object[] { new Guid("927913c6-bde4-432b-9af8-859e5c9a71b9"), "auriwanyasper007@gmail.com", "Super", "User", new DateTime(2024, 2, 29, 13, 36, 26, 992, DateTimeKind.Local).AddTicks(5442), "5ce41ada64f1e8ffb0acfaafa622b141438f3a5777785e7f0b830fb73e40d3d6", 0, 1, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("927913c6-bde4-432b-9af8-859e5c9a71b9"));
        }
    }
}
