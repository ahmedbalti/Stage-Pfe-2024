using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User.Gestion.Data.Migrations
{
    public partial class eightMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "070b7f28-72b8-490d-bf15-a62dee48efbe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1c8e58ee-e353-45f2-a2b0-54d1b7c7aeed");

            migrationBuilder.RenameColumn(
                name: "DevisType",
                table: "Devis",
                newName: "Discriminator");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "89ac71be-6e46-4f00-a18f-d8a40e3f5968", "1", "User", "User" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a9a27994-49ff-48a7-a7e0-7f9b2b6570eb", "2", "Client", "Client" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89ac71be-6e46-4f00-a18f-d8a40e3f5968");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a9a27994-49ff-48a7-a7e0-7f9b2b6570eb");

            migrationBuilder.RenameColumn(
                name: "Discriminator",
                table: "Devis",
                newName: "DevisType");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "070b7f28-72b8-490d-bf15-a62dee48efbe", "2", "Client", "Client" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1c8e58ee-e353-45f2-a2b0-54d1b7c7aeed", "1", "User", "User" });
        }
    }
}
