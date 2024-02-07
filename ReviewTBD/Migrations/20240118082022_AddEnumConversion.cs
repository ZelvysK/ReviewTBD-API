using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReviewTBDAPI.Migrations;

/// <inheritdoc />
public partial class AddEnumConversion : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "CoverUrl",
            table: "Games",
            newName: "CoverImageUrl");

        migrationBuilder.RenameColumn(
            name: "CoverUrl",
            table: "Animes",
            newName: "CoverImageUrl");

        migrationBuilder.AlterColumn<string>(
            name: "Type",
            table: "Studios",
            type: "nvarchar(max)",
            nullable: false,
            oldClrType: typeof(int),
            oldType: "int");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "CoverImageUrl",
            table: "Games",
            newName: "CoverUrl");

        migrationBuilder.RenameColumn(
            name: "CoverImageUrl",
            table: "Animes",
            newName: "CoverUrl");

        migrationBuilder.AlterColumn<int>(
            name: "Type",
            table: "Studios",
            type: "int",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");
    }
}
