using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDatesOnEventModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Events",
                newName: "Start");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Events",
                newName: "End");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Start",
                table: "Events",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "End",
                table: "Events",
                newName: "EndDate");
        }
    }
}
