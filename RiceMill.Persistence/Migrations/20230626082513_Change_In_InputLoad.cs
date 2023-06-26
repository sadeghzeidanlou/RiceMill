using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RiceMill.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Change_In_InputLoad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInDryer",
                table: "InputLoads");

            migrationBuilder.AddColumn<short>(
                name: "NumberOfBagsInDryer",
                table: "InputLoads",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfBagsInDryer",
                table: "InputLoads");

            migrationBuilder.AddColumn<bool>(
                name: "IsInDryer",
                table: "InputLoads",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
