using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelJurnal.Data.Migrations
{
    /// <inheritdoc />
    public partial class DestinationTrip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entry_Destination_DestinationId",
                table: "Entry");

            migrationBuilder.DropForeignKey(
                name: "FK_Entry_Trip_TripId",
                table: "Entry");

            migrationBuilder.DropForeignKey(
                name: "FK_Trip_TravellerProfile_TravelerId",
                table: "Trip");

            migrationBuilder.DropTable(
                name: "DestinationTrip");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trip",
                table: "Trip");

            migrationBuilder.DropIndex(
                name: "IX_Trip_TravelerId",
                table: "Trip");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Entry",
                table: "Entry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Destination",
                table: "Destination");

            migrationBuilder.RenameTable(
                name: "Trip",
                newName: "Trips");

            migrationBuilder.RenameTable(
                name: "Entry",
                newName: "Entries");

            migrationBuilder.RenameTable(
                name: "Destination",
                newName: "Destinations");

            migrationBuilder.RenameIndex(
                name: "IX_Entry_TripId",
                table: "Entries",
                newName: "IX_Entries_TripId");

            migrationBuilder.RenameIndex(
                name: "IX_Entry_DestinationId",
                table: "Entries",
                newName: "IX_Entries_DestinationId");

            migrationBuilder.AddColumn<int>(
                name: "DestinationId",
                table: "Trips",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TravellerProfileTravellerId",
                table: "Trips",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trips",
                table: "Trips",
                column: "TripId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Entries",
                table: "Entries",
                column: "EntryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Destinations",
                table: "Destinations",
                column: "DestinationId");

            migrationBuilder.CreateTable(
                name: "DestinationTrips",
                columns: table => new
                {
                    DestinationTripId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TripId = table.Column<int>(type: "int", nullable: false),
                    DestinationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DestinationTrips", x => x.DestinationTripId);
                    table.ForeignKey(
                        name: "FK_DestinationTrips_Destinations_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Destinations",
                        principalColumn: "DestinationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DestinationTrips_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "TripId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trips_DestinationId",
                table: "Trips",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_TravellerProfileTravellerId",
                table: "Trips",
                column: "TravellerProfileTravellerId");

            migrationBuilder.CreateIndex(
                name: "IX_DestinationTrips_DestinationId",
                table: "DestinationTrips",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_DestinationTrips_TripId",
                table: "DestinationTrips",
                column: "TripId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Destinations_DestinationId",
                table: "Entries",
                column: "DestinationId",
                principalTable: "Destinations",
                principalColumn: "DestinationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Trips_TripId",
                table: "Entries",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "TripId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Destinations_DestinationId",
                table: "Trips",
                column: "DestinationId",
                principalTable: "Destinations",
                principalColumn: "DestinationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_TravellerProfile_TravellerProfileTravellerId",
                table: "Trips",
                column: "TravellerProfileTravellerId",
                principalTable: "TravellerProfile",
                principalColumn: "TravellerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Destinations_DestinationId",
                table: "Entries");

            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Trips_TripId",
                table: "Entries");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Destinations_DestinationId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_TravellerProfile_TravellerProfileTravellerId",
                table: "Trips");

            migrationBuilder.DropTable(
                name: "DestinationTrips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trips",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_DestinationId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_TravellerProfileTravellerId",
                table: "Trips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Entries",
                table: "Entries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Destinations",
                table: "Destinations");

            migrationBuilder.DropColumn(
                name: "DestinationId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "TravellerProfileTravellerId",
                table: "Trips");

            migrationBuilder.RenameTable(
                name: "Trips",
                newName: "Trip");

            migrationBuilder.RenameTable(
                name: "Entries",
                newName: "Entry");

            migrationBuilder.RenameTable(
                name: "Destinations",
                newName: "Destination");

            migrationBuilder.RenameIndex(
                name: "IX_Entries_TripId",
                table: "Entry",
                newName: "IX_Entry_TripId");

            migrationBuilder.RenameIndex(
                name: "IX_Entries_DestinationId",
                table: "Entry",
                newName: "IX_Entry_DestinationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trip",
                table: "Trip",
                column: "TripId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Entry",
                table: "Entry",
                column: "EntryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Destination",
                table: "Destination",
                column: "DestinationId");

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
                name: "IX_Trip_TravelerId",
                table: "Trip",
                column: "TravelerId");

            migrationBuilder.CreateIndex(
                name: "IX_DestinationTrip_TripsTripId",
                table: "DestinationTrip",
                column: "TripsTripId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entry_Destination_DestinationId",
                table: "Entry",
                column: "DestinationId",
                principalTable: "Destination",
                principalColumn: "DestinationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Entry_Trip_TripId",
                table: "Entry",
                column: "TripId",
                principalTable: "Trip",
                principalColumn: "TripId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_TravellerProfile_TravelerId",
                table: "Trip",
                column: "TravelerId",
                principalTable: "TravellerProfile",
                principalColumn: "TravellerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
