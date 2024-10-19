using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelJurnal.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNewDestinations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Check if ImagePath column already exists
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT * FROM SYS.COLUMNS 
                WHERE NAME = 'ImagePath' AND OBJECT_ID = OBJECT_ID('Destinations'))
                BEGIN
                    ALTER TABLE Destinations 
                    ADD ImagePath nvarchar(100) NULL;
                END;
            ");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Destinations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Destinations");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Destinations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
