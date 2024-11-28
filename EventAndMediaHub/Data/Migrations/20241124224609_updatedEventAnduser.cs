using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventAndMediaHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedEventAnduser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventId1",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventId1",
                table: "Events",
                column: "EventId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Events_EventId1",
                table: "Events",
                column: "EventId1",
                principalTable: "Events",
                principalColumn: "EventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Events_EventId1",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_EventId1",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventId1",
                table: "Events");
        }
    }
}
