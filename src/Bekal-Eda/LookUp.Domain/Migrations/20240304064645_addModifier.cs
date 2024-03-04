using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LookUp.Domain.Migrations
{
    /// <inheritdoc />
    public partial class addModifier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Attributes",
                keyColumn: "Id",
                keyValue: new Guid("661d80dc-106f-45a2-b94b-350effe7ff4e"));

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "Attributes",
                newName: "ModifiedDate");

            migrationBuilder.AlterColumn<Guid>(
                name: "ModifiedBy",
                table: "Attributes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Attributes",
                columns: new[] { "Id", "ModifiedBy", "ModifiedDate", "Status", "Type", "Unit" },
                values: new object[] { new Guid("d13aca09-a4b5-4093-8c77-506d14264df5"), null, null, 0, 0, "Tes1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Attributes",
                keyColumn: "Id",
                keyValue: new Guid("d13aca09-a4b5-4093-8c77-506d14264df5"));

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "Attributes",
                newName: "ModifiedOn");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Attributes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Attributes",
                columns: new[] { "Id", "ModifiedBy", "ModifiedOn", "Status", "Type", "Unit" },
                values: new object[] { new Guid("661d80dc-106f-45a2-b94b-350effe7ff4e"), null, new DateTime(2024, 3, 4, 13, 28, 48, 466, DateTimeKind.Local).AddTicks(5249), 0, 0, "Tes1" });
        }
    }
}
