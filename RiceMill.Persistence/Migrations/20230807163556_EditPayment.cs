using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RiceMill.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EditPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_InputLoads_InputLoadId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_InputLoadId",
                table: "Payments");

            migrationBuilder.AlterColumn<Guid>(
                name: "InputLoadId",
                table: "Payments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_InputLoadId",
                table: "Payments",
                column: "InputLoadId",
                unique: true,
                filter: "[InputLoadId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_InputLoads_InputLoadId",
                table: "Payments",
                column: "InputLoadId",
                principalTable: "InputLoads",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_InputLoads_InputLoadId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_InputLoadId",
                table: "Payments");

            migrationBuilder.AlterColumn<Guid>(
                name: "InputLoadId",
                table: "Payments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_InputLoadId",
                table: "Payments",
                column: "InputLoadId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_InputLoads_InputLoadId",
                table: "Payments",
                column: "InputLoadId",
                principalTable: "InputLoads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
