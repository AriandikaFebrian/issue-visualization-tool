using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugNest.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasUploadedSource",
                table: "Projects",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SourceUploadPath",
                table: "Projects",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasUploadedSource",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "SourceUploadPath",
                table: "Projects");
        }
    }
}
