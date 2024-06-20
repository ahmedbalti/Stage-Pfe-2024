using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User.Gestion.Data.Migrations
{
    public partial class SixMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f28955c-f5a9-4e4f-92d5-d4a0c34b8f06");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f755fd5e-063a-43d5-a0fa-6e934f0c5803");

            migrationBuilder.CreateTable(
                name: "Devis",
                columns: table => new
                {
                    IdDevis = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeAssurance = table.Column<int>(type: "int", nullable: false),
                    Montant = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroImmatriculation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreDeChevaux = table.Column<int>(type: "int", nullable: true),
                    AgeVoiture = table.Column<int>(type: "int", nullable: true),
                    Carburant = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InformationsHabitation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surface = table.Column<int>(type: "int", nullable: true),
                    NombreDePieces = table.Column<int>(type: "int", nullable: true),
                    NumeroSecuriteSociale = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Sexe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fumeur = table.Column<bool>(type: "bit", nullable: true),
                    Beneficiaire = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duree = table.Column<int>(type: "int", nullable: true),
                    Capital = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devis", x => x.IdDevis);
                    table.ForeignKey(
                        name: "FK_Devis_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d4fb8cd1-e833-473e-ad38-0f2e0139e649", "2", "Client", "Client" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fab7503c-f4a6-4e9e-a187-f413f18eb377", "1", "User", "User" });

            migrationBuilder.CreateIndex(
                name: "IX_Devis_OwnerId",
                table: "Devis",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Devis");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d4fb8cd1-e833-473e-ad38-0f2e0139e649");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab7503c-f4a6-4e9e-a187-f413f18eb377");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7f28955c-f5a9-4e4f-92d5-d4a0c34b8f06", "1", "User", "User" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f755fd5e-063a-43d5-a0fa-6e934f0c5803", "2", "Client", "Client" });
        }
    }
}
