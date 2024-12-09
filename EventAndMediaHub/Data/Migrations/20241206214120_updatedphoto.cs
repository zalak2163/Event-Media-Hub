using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventAndMediaHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedphoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PicExtension",
                table: "Photos");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Photos");

            migrationBuilder.AddColumn<string>(
                name: "PicExtension",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
