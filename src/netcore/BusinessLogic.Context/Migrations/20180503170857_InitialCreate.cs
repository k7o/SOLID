using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Contexts.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adressen",
                columns: table => new
                {
                    Postcode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adressen", x => x.Postcode);
                });

            migrationBuilder.CreateTable(
                name: "Bsns",
                columns: table => new
                {
                    Bsnnummer = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bsns", x => x.Bsnnummer);
                });

            migrationBuilder.CreateTable(
                name: "BsnUzovis",
                columns: table => new
                {
                    Bsnnummer = table.Column<int>(nullable: false),
                    Uzovi = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BsnUzovis", x => new { x.Bsnnummer, x.Uzovi });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adressen");

            migrationBuilder.DropTable(
                name: "Bsns");

            migrationBuilder.DropTable(
                name: "BsnUzovis");
        }
    }
}
