using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User.Gestion.Data.Migrations
{
    public partial class fifthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "03f9c33b-a238-4608-9607-da577b641fcf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e7c5004-84e2-4498-9d91-aacb5731416e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7f28955c-f5a9-4e4f-92d5-d4a0c34b8f06", "1", "User", "User" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f755fd5e-063a-43d5-a0fa-6e934f0c5803", "2", "Client", "Client" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f28955c-f5a9-4e4f-92d5-d4a0c34b8f06");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f755fd5e-063a-43d5-a0fa-6e934f0c5803");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "03f9c33b-a238-4608-9607-da577b641fcf", "2", "Client", "Client" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5e7c5004-84e2-4498-9d91-aacb5731416e", "1", "User", "User" });
        }
    }
}
