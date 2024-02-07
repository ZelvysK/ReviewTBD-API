using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReviewTBDAPI.Migrations;

/// <inheritdoc />
public partial class RenamedDateColumn : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "FoundedDate",
            table: "Studios",
            newName: "CreatedDate");

        migrationBuilder.RenameColumn(
            name: "ReleaseDate",
            table: "Games",
            newName: "CreatedDate");

        migrationBuilder.RenameColumn(
            name: "ReleaseDate",
            table: "Animes",
            newName: "CreatedDate");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "CreatedDate",
            table: "Studios",
            newName: "FoundedDate");

        migrationBuilder.RenameColumn(
            name: "CreatedDate",
            table: "Games",
            newName: "ReleaseDate");

        migrationBuilder.RenameColumn(
            name: "CreatedDate",
            table: "Animes",
            newName: "ReleaseDate");
    }
}
