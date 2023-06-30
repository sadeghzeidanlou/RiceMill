using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RiceMill.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Set_AllowNull_Some_Column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_People_UserPersonId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserPersonId",
                table: "Users");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserPersonId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerPersonId",
                table: "RiceMills",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserPersonId",
                table: "Users",
                column: "UserPersonId",
                unique: true,
                filter: "[UserPersonId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_People_UserPersonId",
                table: "Users",
                column: "UserPersonId",
                principalTable: "People",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_People_UserPersonId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserPersonId",
                table: "Users");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserPersonId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerPersonId",
                table: "RiceMills",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserPersonId",
                table: "Users",
                column: "UserPersonId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_People_UserPersonId",
                table: "Users",
                column: "UserPersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
