using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventAndMediaHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedLocationnameinevent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LocationName",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationName",
                table: "Events");
        }
    }
}
