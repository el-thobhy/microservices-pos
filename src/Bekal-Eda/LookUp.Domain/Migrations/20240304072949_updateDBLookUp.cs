using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LookUp.Domain.Migrations
{
    /// <inheritdoc />
    public partial class updateDBLookUp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Attributes",
                keyColumn: "Id",
                keyValue: new Guid("d13aca09-a4b5-4093-8c77-506d14264df5"));

            migrationBuilder.InsertData(
                table: "Attributes",
                columns: new[] { "Id", "ModifiedBy", "ModifiedDate", "Status", "Type", "Unit" },
                values: new object[] { new Guid("56350731-08ca-48b4-a14f-c874ce1490ce"), null, null, 0, 0, "Tes1" });

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_Unit",
                table: "Attributes",
                column: "Unit",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Attributes_Unit",
                table: "Attributes");

            migrationBuilder.DeleteData(
                table: "Attributes",
                keyColumn: "Id",
                keyValue: new Guid("56350731-08ca-48b4-a14f-c874ce1490ce"));

            migrationBuilder.InsertData(
                table: "Attributes",
                columns: new[] { "Id", "ModifiedBy", "ModifiedDate", "Status", "Type", "Unit" },
                values: new object[] { new Guid("d13aca09-a4b5-4093-8c77-506d14264df5"), null, null, 0, 0, "Tes1" });
        }
    }
}
