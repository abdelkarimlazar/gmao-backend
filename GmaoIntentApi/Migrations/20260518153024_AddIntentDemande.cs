using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace GmaoIntentApi.Migrations
{
    /// <inheritdoc />
    public partial class AddIntentDemande : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IntentDemandes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IntentReclamationId = table.Column<string>(type: "longtext", nullable: false),
                    DateCreationIntent = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateReception = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ClientNom = table.Column<string>(type: "longtext", nullable: false),
                    ClientEmail = table.Column<string>(type: "longtext", nullable: false),
                    ClientTelephone = table.Column<string>(type: "longtext", nullable: false),
                    ClientSociete = table.Column<string>(type: "longtext", nullable: false),
                    EquipementRefIntent = table.Column<string>(type: "longtext", nullable: false),
                    EquipementDesignation = table.Column<string>(type: "longtext", nullable: false),
                    DescriptionPanne = table.Column<string>(type: "longtext", nullable: false),
                    NiveauUrgence = table.Column<string>(type: "longtext", nullable: false),
                    Statut = table.Column<string>(type: "longtext", nullable: false),
                    MotifRefus = table.Column<string>(type: "longtext", nullable: false),
                    InterventionGMAOId = table.Column<int>(type: "int", nullable: true),
                    TraiteePar = table.Column<string>(type: "longtext", nullable: false),
                    DateTraitement = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntentDemandes", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IntentDemandes");
        }
    }
}
