using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelJurnal.Data.Migrations
{
    /// <inheritdoc />
    public partial class destination : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DestinationId",
                table: "Trip",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Destination",
                columns: table => new
                {
                    DestinationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destination", x => x.DestinationId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trip_DestinationId",
                table: "Trip",
                column: "DestinationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_Destination_DestinationId",
                table: "Trip",
                column: "DestinationId",
                principalTable: "Destination",
                principalColumn: "DestinationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trip_Destination_DestinationId",
                table: "Trip");

            migrationBuilder.DropTable(
                name: "Destination");

            migrationBuilder.DropIndex(
                name: "IX_Trip_DestinationId",
                table: "Trip");

            migrationBuilder.DropColumn(
                name: "DestinationId",
                table: "Trip");
        }
    }
}
