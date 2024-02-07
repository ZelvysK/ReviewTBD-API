using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReviewTBDAPI.Migrations;

/// <inheritdoc />
public partial class ChangedDateVariableNaming : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "CreatedDate",
            table: "Studios",
            newName: "DateCreated");

        migrationBuilder.RenameColumn(
            name: "CreatedDate",
            table: "Movies",
            newName: "DateCreated");

        migrationBuilder.RenameColumn(
            name: "CreatedDate",
            table: "Games",
            newName: "DateCreated");

        migrationBuilder.RenameColumn(
            name: "CreatedDate",
            table: "Animes",
            newName: "DateCreated");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "DateCreated",
            table: "Studios",
            newName: "CreatedDate");

        migrationBuilder.RenameColumn(
            name: "DateCreated",
            table: "Movies",
            newName: "CreatedDate");

        migrationBuilder.RenameColumn(
            name: "DateCreated",
            table: "Games",
            newName: "CreatedDate");

        migrationBuilder.RenameColumn(
            name: "DateCreated",
            table: "Animes",
            newName: "CreatedDate");
    }
}
