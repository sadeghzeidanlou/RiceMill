using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RiceMill.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Change_RiceThreshing_Delivery_DryerHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DryerHistories_RiceThreshings_RiceThreshingId",
                table: "DryerHistories");

            migrationBuilder.DropTable(
                name: "DeliveryRiceThreshing");

            migrationBuilder.DropTable(
                name: "DryerHistoryInputLoad");

            migrationBuilder.DropIndex(
                name: "IX_DryerHistories_RiceThreshingId",
                table: "DryerHistories");

            migrationBuilder.DropColumn(
                name: "RiceThreshingId",
                table: "DryerHistories");

            migrationBuilder.AddColumn<Guid>(
                name: "InputLoadId",
                table: "RiceThreshings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "InputLoadId",
                table: "DryerHistories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RiceThreshingId",
                table: "Deliveries",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_RiceThreshings_InputLoadId",
                table: "RiceThreshings",
                column: "InputLoadId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DryerHistories_InputLoadId",
                table: "DryerHistories",
                column: "InputLoadId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_RiceThreshingId",
                table: "Deliveries",
                column: "RiceThreshingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_RiceThreshings_RiceThreshingId",
                table: "Deliveries",
                column: "RiceThreshingId",
                principalTable: "RiceThreshings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DryerHistories_InputLoads_InputLoadId",
                table: "DryerHistories",
                column: "InputLoadId",
                principalTable: "InputLoads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RiceThreshings_InputLoads_InputLoadId",
                table: "RiceThreshings",
                column: "InputLoadId",
                principalTable: "InputLoads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_RiceThreshings_RiceThreshingId",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_DryerHistories_InputLoads_InputLoadId",
                table: "DryerHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_RiceThreshings_InputLoads_InputLoadId",
                table: "RiceThreshings");

            migrationBuilder.DropIndex(
                name: "IX_RiceThreshings_InputLoadId",
                table: "RiceThreshings");

            migrationBuilder.DropIndex(
                name: "IX_DryerHistories_InputLoadId",
                table: "DryerHistories");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_RiceThreshingId",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "InputLoadId",
                table: "RiceThreshings");

            migrationBuilder.DropColumn(
                name: "InputLoadId",
                table: "DryerHistories");

            migrationBuilder.DropColumn(
                name: "RiceThreshingId",
                table: "Deliveries");

            migrationBuilder.AddColumn<Guid>(
                name: "RiceThreshingId",
                table: "DryerHistories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DeliveryRiceThreshing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeliveryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RiceThreshingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryRiceThreshing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryRiceThreshing_Deliveries_DeliveryId",
                        column: x => x.DeliveryId,
                        principalTable: "Deliveries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeliveryRiceThreshing_RiceThreshings_RiceThreshingId",
                        column: x => x.RiceThreshingId,
                        principalTable: "RiceThreshings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DryerHistoryInputLoad",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DryerHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InputLoadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DryerHistoryInputLoad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DryerHistoryInputLoad_DryerHistories_DryerHistoryId",
                        column: x => x.DryerHistoryId,
                        principalTable: "DryerHistories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DryerHistoryInputLoad_InputLoads_InputLoadId",
                        column: x => x.InputLoadId,
                        principalTable: "InputLoads",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DryerHistories_RiceThreshingId",
                table: "DryerHistories",
                column: "RiceThreshingId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryRiceThreshing_DeliveryId_RiceThreshingId",
                table: "DeliveryRiceThreshing",
                columns: new[] { "DeliveryId", "RiceThreshingId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryRiceThreshing_RiceThreshingId",
                table: "DeliveryRiceThreshing",
                column: "RiceThreshingId");

            migrationBuilder.CreateIndex(
                name: "IX_DryerHistoryInputLoad_DryerHistoryId_InputLoadId",
                table: "DryerHistoryInputLoad",
                columns: new[] { "DryerHistoryId", "InputLoadId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DryerHistoryInputLoad_InputLoadId",
                table: "DryerHistoryInputLoad",
                column: "InputLoadId");

            migrationBuilder.AddForeignKey(
                name: "FK_DryerHistories_RiceThreshings_RiceThreshingId",
                table: "DryerHistories",
                column: "RiceThreshingId",
                principalTable: "RiceThreshings",
                principalColumn: "Id");
        }
    }
}
