using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RiceMill.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Rearrange_NoticeType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoticesType",
                table: "InputLoads");

            migrationBuilder.AddColumn<byte>(
                name: "NoticesType",
                table: "People",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateIndex(
                name: "IX_People_MobileNumber_RiceMillId",
                table: "People",
                columns: new[] { "MobileNumber", "RiceMillId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_People_MobileNumber_RiceMillId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "NoticesType",
                table: "People");

            migrationBuilder.AddColumn<byte>(
                name: "NoticesType",
                table: "InputLoads",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
