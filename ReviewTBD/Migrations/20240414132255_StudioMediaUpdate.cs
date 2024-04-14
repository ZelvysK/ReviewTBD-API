using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReviewTBDAPI.Migrations
{
    /// <inheritdoc />
    public partial class StudioMediaUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Founder",
                table: "Studios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Headquarters",
                table: "Studios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Genre",
                table: "Media",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PublishedBy",
                table: "Media",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Founder",
                table: "Studios");

            migrationBuilder.DropColumn(
                name: "Headquarters",
                table: "Studios");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "PublishedBy",
                table: "Media");
        }
    }
}
