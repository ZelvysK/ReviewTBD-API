using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReviewTBDAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddFirstTimeLoginFlag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FirstTimeLogin",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstTimeLogin",
                table: "AspNetUsers");
        }
    }
}
