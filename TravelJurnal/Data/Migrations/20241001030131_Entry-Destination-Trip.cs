using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelJurnal.Data.Migrations
{
    /// <inheritdoc />
    public partial class EntryDestinationTrip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trip_Destination_DestinationId",
                table: "Trip");

            migrationBuilder.DropIndex(
                name: "IX_Trip_DestinationId",
                table: "Trip");

            migrationBuilder.DropColumn(
                name: "DestinationId",
                table: "Trip");

            migrationBuilder.CreateTable(
                name: "DestinationTrip",
                columns: table => new
                {
                    DestinationsDestinationId = table.Column<int>(type: "int", nullable: false),
                    TripsTripId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DestinationTrip", x => new { x.DestinationsDestinationId, x.TripsTripId });
                    table.ForeignKey(
                        name: "FK_DestinationTrip_Destination_DestinationsDestinationId",
                        column: x => x.DestinationsDestinationId,
                        principalTable: "Destination",
                        principalColumn: "DestinationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DestinationTrip_Trip_TripsTripId",
                        column: x => x.TripsTripId,
                        principalTable: "Trip",
                        principalColumn: "TripId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DestinationTrip_TripsTripId",
                table: "DestinationTrip",
                column: "TripsTripId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DestinationTrip");

            migrationBuilder.AddColumn<int>(
                name: "DestinationId",
                table: "Trip",
                type: "int",
                nullable: true);

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
    }
}
