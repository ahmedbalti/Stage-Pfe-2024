using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User.Gestion.Data.Migrations
{
    public partial class sevenMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d4fb8cd1-e833-473e-ad38-0f2e0139e649");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab7503c-f4a6-4e9e-a187-f413f18eb377");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { "d4fb8cd1-e833-473e-ad38-0f2e0139e649", "2", "Client", "Client" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fab7503c-f4a6-4e9e-a187-f413f18eb377", "1", "User", "User" });
        }
    }
}
