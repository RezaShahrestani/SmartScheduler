using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartScheduler.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("318739db-4ef0-4da5-b56c-074c851f0091"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "Username" },
                values: new object[] { new Guid("e40efb60-9548-4224-bca9-71e23cd23569"), "$2a$11$zZvy4aNrWiq1.gwVoeO44O6wgAMtAJ.AbCVg6NdICcg0WjNKU1hd6", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e40efb60-9548-4224-bca9-71e23cd23569"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "Username" },
                values: new object[] { new Guid("318739db-4ef0-4da5-b56c-074c851f0091"), "$2a$11$MiMO4ZlNfQDUBT6IBfUaD.jXbhFlwMobI1n1Mo.9lQaYsDSIODzfG", "admin" });
        }
    }
}
