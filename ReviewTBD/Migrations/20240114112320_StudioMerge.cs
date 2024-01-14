using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReviewTBDAPI.Migrations
{
    /// <inheritdoc />
    public partial class StudioMerge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animes_AnimeStudios_AnimeStudioId",
                table: "Animes");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_GameStudios_GameCreatorId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_MovieStudios_MovieStudioId",
                table: "Movies");

            migrationBuilder.DropTable(
                name: "AnimeStudios");

            migrationBuilder.DropTable(
                name: "GameStudios");

            migrationBuilder.DropTable(
                name: "MovieStudios");

            migrationBuilder.CreateTable(
                name: "Studios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FoundedDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studios", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Animes_Studios_AnimeStudioId",
                table: "Animes",
                column: "AnimeStudioId",
                principalTable: "Studios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Studios_GameCreatorId",
                table: "Games",
                column: "GameCreatorId",
                principalTable: "Studios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Studios_MovieStudioId",
                table: "Movies",
                column: "MovieStudioId",
                principalTable: "Studios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animes_Studios_AnimeStudioId",
                table: "Animes");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Studios_GameCreatorId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Studios_MovieStudioId",
                table: "Movies");

            migrationBuilder.DropTable(
                name: "Studios");

            migrationBuilder.CreateTable(
                name: "AnimeStudios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FoundedDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeStudios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameStudios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FoundedDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStudios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovieStudios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FoundedDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieStudios", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Animes_AnimeStudios_AnimeStudioId",
                table: "Animes",
                column: "AnimeStudioId",
                principalTable: "AnimeStudios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_GameStudios_GameCreatorId",
                table: "Games",
                column: "GameCreatorId",
                principalTable: "GameStudios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_MovieStudios_MovieStudioId",
                table: "Movies",
                column: "MovieStudioId",
                principalTable: "MovieStudios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
