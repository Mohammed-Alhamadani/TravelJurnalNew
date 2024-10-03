using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelJurnal.Data.Migrations
{
    /// <inheritdoc />
    public partial class TravellerProfileTrip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TravellerProfileTravellerId",
                table: "Trip",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trip_TravellerProfileTravellerId",
                table: "Trip",
                column: "TravellerProfileTravellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_TravellerProfile_TravellerProfileTravellerId",
                table: "Trip",
                column: "TravellerProfileTravellerId",
                principalTable: "TravellerProfile",
                principalColumn: "TravellerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trip_TravellerProfile_TravellerProfileTravellerId",
                table: "Trip");

            migrationBuilder.DropIndex(
                name: "IX_Trip_TravellerProfileTravellerId",
                table: "Trip");

            migrationBuilder.DropColumn(
                name: "TravellerProfileTravellerId",
                table: "Trip");
        }
    }
}
