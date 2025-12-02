using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace F_DAL.Migrations
{
    /// <inheritdoc />
    public partial class localizaion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "catgries");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "catgries");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "catgries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "CatgryTranslation",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CatgryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatgryTranslation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CatgryTranslation_catgries_CatgryId",
                        column: x => x.CatgryId,
                        principalTable: "catgries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatgryTranslation_CatgryId",
                table: "CatgryTranslation",
                column: "CatgryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatgryTranslation");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "catgries");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "catgries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "catgries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
