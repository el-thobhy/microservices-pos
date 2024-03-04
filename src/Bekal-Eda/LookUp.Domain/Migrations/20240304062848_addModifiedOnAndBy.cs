using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LookUp.Domain.Migrations
{
    /// <inheritdoc />
    public partial class addModifiedOnAndBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Attributes",
                keyColumn: "Id",
                keyValue: new Guid("b627869e-abaf-4481-ac92-12ee929855a8"));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Attributes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Attributes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Attributes",
                columns: new[] { "Id", "ModifiedBy", "ModifiedOn", "Status", "Type", "Unit" },
                values: new object[] { new Guid("661d80dc-106f-45a2-b94b-350effe7ff4e"), null, new DateTime(2024, 3, 4, 13, 28, 48, 466, DateTimeKind.Local).AddTicks(5249), 0, 0, "Tes1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Attributes",
                keyColumn: "Id",
                keyValue: new Guid("661d80dc-106f-45a2-b94b-350effe7ff4e"));

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Attributes");

            migrationBuilder.InsertData(
                table: "Attributes",
                columns: new[] { "Id", "Status", "Type", "Unit" },
                values: new object[] { new Guid("b627869e-abaf-4481-ac92-12ee929855a8"), 0, 0, "Tes1" });
        }
    }
}
