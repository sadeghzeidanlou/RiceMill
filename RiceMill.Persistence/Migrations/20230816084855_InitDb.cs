using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RiceMill.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Concerns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RiceMillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Concerns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Deliveries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnbrokenRice = table.Column<short>(type: "smallint", nullable: false),
                    BrokenRice = table.Column<short>(type: "smallint", nullable: false),
                    ChickenRice = table.Column<short>(type: "smallint", nullable: false),
                    Flour = table.Column<short>(type: "smallint", nullable: false),
                    DeliveryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DelivererPersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiverPersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CarrierPersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RiceMillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliveries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryRiceThreshing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeliveryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RiceThreshingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryRiceThreshing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryRiceThreshing_Deliveries_DeliveryId",
                        column: x => x.DeliveryId,
                        principalTable: "Deliveries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DryerHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Operation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DryerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RiceThreshingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RiceMillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DryerHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DryerHistoryInputLoad",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DryerHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InputLoadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DryerHistoryInputLoad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DryerHistoryInputLoad_DryerHistories_DryerHistoryId",
                        column: x => x.DryerHistoryId,
                        principalTable: "DryerHistories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Dryers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RiceMillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dryers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Incomes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IncomeTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UnbrokenRice = table.Column<float>(type: "real", nullable: false),
                    BrokenRice = table.Column<float>(type: "real", nullable: false),
                    Flour = table.Column<float>(type: "real", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RiceMillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incomes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InputLoads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumberOfBags = table.Column<short>(type: "smallint", nullable: false),
                    NumberOfBagsInDryer = table.Column<short>(type: "smallint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ReceiveTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VillageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DelivererPersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiverPersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CarrierPersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerPersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RiceMillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InputLoads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UnbrokenRice = table.Column<float>(type: "real", nullable: false),
                    BrokenRice = table.Column<float>(type: "real", nullable: false),
                    Flour = table.Column<float>(type: "real", nullable: false),
                    Money = table.Column<int>(type: "int", nullable: false),
                    PaidPersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConcernId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InputLoadId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RiceMillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Concerns_ConcernId",
                        column: x => x.ConcernId,
                        principalTable: "Concerns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_InputLoads_InputLoadId",
                        column: x => x.InputLoadId,
                        principalTable: "InputLoads",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Family = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Gender = table.Column<byte>(type: "tinyint", nullable: false),
                    MobileNumber = table.Column<string>(type: "nchar(11)", fixedLength: true, maxLength: 11, nullable: false),
                    HomeNumber = table.Column<string>(type: "nchar(11)", fixedLength: true, maxLength: 11, nullable: true),
                    NoticesType = table.Column<byte>(type: "tinyint", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RiceMillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RiceMills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Wage = table.Column<byte>(type: "tinyint", nullable: false),
                    Phone = table.Column<string>(type: "nchar(11)", fixedLength: true, maxLength: 11, nullable: true),
                    PostalCode = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    OwnerPersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiceMills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RiceMills_People_OwnerPersonId",
                        column: x => x.OwnerPersonId,
                        principalTable: "People",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Role = table.Column<byte>(type: "tinyint", nullable: false),
                    UserPersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RiceMillId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_People_UserPersonId",
                        column: x => x.UserPersonId,
                        principalTable: "People",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_RiceMills_RiceMillId",
                        column: x => x.RiceMillId,
                        principalTable: "RiceMills",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RiceThreshings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UnbrokenRice = table.Column<float>(type: "real", nullable: false),
                    BrokenRice = table.Column<float>(type: "real", nullable: false),
                    ChickenRice = table.Column<float>(type: "real", nullable: false),
                    Flour = table.Column<float>(type: "real", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsDelivered = table.Column<bool>(type: "bit", nullable: false),
                    IncomeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RiceMillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiceThreshings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RiceThreshings_Incomes_IncomeId",
                        column: x => x.IncomeId,
                        principalTable: "Incomes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RiceThreshings_RiceMills_RiceMillId",
                        column: x => x.RiceMillId,
                        principalTable: "RiceMills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RiceThreshings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserActivities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ip = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    UserActivityType = table.Column<byte>(type: "tinyint", nullable: false),
                    EntityType = table.Column<byte>(type: "tinyint", nullable: false),
                    ApplicationId = table.Column<byte>(type: "tinyint", nullable: false),
                    BeforeEdit = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: true),
                    AfterEdit = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: true),
                    RiceMillId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserActivities_RiceMills_RiceMillId",
                        column: x => x.RiceMillId,
                        principalTable: "RiceMills",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserActivities_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Plate = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    VehicleType = table.Column<byte>(type: "tinyint", nullable: false),
                    OwnerPersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RiceMillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_People_OwnerPersonId",
                        column: x => x.OwnerPersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vehicles_RiceMills_RiceMillId",
                        column: x => x.RiceMillId,
                        principalTable: "RiceMills",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Vehicles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Villages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RiceMillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Villages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Villages_RiceMills_RiceMillId",
                        column: x => x.RiceMillId,
                        principalTable: "RiceMills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Villages_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Concerns_RiceMillId",
                table: "Concerns",
                column: "RiceMillId");

            migrationBuilder.CreateIndex(
                name: "IX_Concerns_UserId",
                table: "Concerns",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_CarrierPersonId",
                table: "Deliveries",
                column: "CarrierPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_DelivererPersonId",
                table: "Deliveries",
                column: "DelivererPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_ReceiverPersonId",
                table: "Deliveries",
                column: "ReceiverPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_RiceMillId",
                table: "Deliveries",
                column: "RiceMillId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_UserId",
                table: "Deliveries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_VehicleId",
                table: "Deliveries",
                column: "VehicleId");

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
                name: "IX_DryerHistories_DryerId",
                table: "DryerHistories",
                column: "DryerId");

            migrationBuilder.CreateIndex(
                name: "IX_DryerHistories_RiceMillId",
                table: "DryerHistories",
                column: "RiceMillId");

            migrationBuilder.CreateIndex(
                name: "IX_DryerHistories_RiceThreshingId",
                table: "DryerHistories",
                column: "RiceThreshingId");

            migrationBuilder.CreateIndex(
                name: "IX_DryerHistories_UserId",
                table: "DryerHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DryerHistoryInputLoad_DryerHistoryId_InputLoadId",
                table: "DryerHistoryInputLoad",
                columns: new[] { "DryerHistoryId", "InputLoadId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DryerHistoryInputLoad_InputLoadId",
                table: "DryerHistoryInputLoad",
                column: "InputLoadId");

            migrationBuilder.CreateIndex(
                name: "IX_Dryers_RiceMillId",
                table: "Dryers",
                column: "RiceMillId");

            migrationBuilder.CreateIndex(
                name: "IX_Dryers_UserId",
                table: "Dryers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_RiceMillId",
                table: "Incomes",
                column: "RiceMillId");

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_UserId",
                table: "Incomes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InputLoads_CarrierPersonId",
                table: "InputLoads",
                column: "CarrierPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_InputLoads_DelivererPersonId",
                table: "InputLoads",
                column: "DelivererPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_InputLoads_OwnerPersonId",
                table: "InputLoads",
                column: "OwnerPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_InputLoads_ReceiverPersonId",
                table: "InputLoads",
                column: "ReceiverPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_InputLoads_RiceMillId",
                table: "InputLoads",
                column: "RiceMillId");

            migrationBuilder.CreateIndex(
                name: "IX_InputLoads_UserId",
                table: "InputLoads",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InputLoads_VehicleId",
                table: "InputLoads",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_InputLoads_VillageId",
                table: "InputLoads",
                column: "VillageId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ConcernId",
                table: "Payments",
                column: "ConcernId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_InputLoadId",
                table: "Payments",
                column: "InputLoadId",
                unique: true,
                filter: "[InputLoadId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaidPersonId",
                table: "Payments",
                column: "PaidPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_RiceMillId",
                table: "Payments",
                column: "RiceMillId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UserId",
                table: "Payments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_People_MobileNumber_RiceMillId",
                table: "People",
                columns: new[] { "MobileNumber", "RiceMillId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_RiceMillId",
                table: "People",
                column: "RiceMillId");

            migrationBuilder.CreateIndex(
                name: "IX_People_UserId",
                table: "People",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RiceMills_OwnerPersonId",
                table: "RiceMills",
                column: "OwnerPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_RiceThreshings_IncomeId",
                table: "RiceThreshings",
                column: "IncomeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RiceThreshings_RiceMillId",
                table: "RiceThreshings",
                column: "RiceMillId");

            migrationBuilder.CreateIndex(
                name: "IX_RiceThreshings_UserId",
                table: "RiceThreshings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserActivities_RiceMillId",
                table: "UserActivities",
                column: "RiceMillId");

            migrationBuilder.CreateIndex(
                name: "IX_UserActivities_UserId",
                table: "UserActivities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RiceMillId",
                table: "Users",
                column: "RiceMillId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserPersonId",
                table: "Users",
                column: "UserPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_OwnerPersonId",
                table: "Vehicles",
                column: "OwnerPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_RiceMillId",
                table: "Vehicles",
                column: "RiceMillId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_UserId",
                table: "Vehicles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Villages_RiceMillId",
                table: "Villages",
                column: "RiceMillId");

            migrationBuilder.CreateIndex(
                name: "IX_Villages_UserId",
                table: "Villages",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Concerns_RiceMills_RiceMillId",
                table: "Concerns",
                column: "RiceMillId",
                principalTable: "RiceMills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Concerns_Users_UserId",
                table: "Concerns",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_People_CarrierPersonId",
                table: "Deliveries",
                column: "CarrierPersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_People_DelivererPersonId",
                table: "Deliveries",
                column: "DelivererPersonId",
                principalTable: "People",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_People_ReceiverPersonId",
                table: "Deliveries",
                column: "ReceiverPersonId",
                principalTable: "People",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_RiceMills_RiceMillId",
                table: "Deliveries",
                column: "RiceMillId",
                principalTable: "RiceMills",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Users_UserId",
                table: "Deliveries",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Vehicles_VehicleId",
                table: "Deliveries",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryRiceThreshing_RiceThreshings_RiceThreshingId",
                table: "DeliveryRiceThreshing",
                column: "RiceThreshingId",
                principalTable: "RiceThreshings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DryerHistories_Dryers_DryerId",
                table: "DryerHistories",
                column: "DryerId",
                principalTable: "Dryers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DryerHistories_RiceMills_RiceMillId",
                table: "DryerHistories",
                column: "RiceMillId",
                principalTable: "RiceMills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DryerHistories_RiceThreshings_RiceThreshingId",
                table: "DryerHistories",
                column: "RiceThreshingId",
                principalTable: "RiceThreshings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DryerHistories_Users_UserId",
                table: "DryerHistories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DryerHistoryInputLoad_InputLoads_InputLoadId",
                table: "DryerHistoryInputLoad",
                column: "InputLoadId",
                principalTable: "InputLoads",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dryers_RiceMills_RiceMillId",
                table: "Dryers",
                column: "RiceMillId",
                principalTable: "RiceMills",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dryers_Users_UserId",
                table: "Dryers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Incomes_RiceMills_RiceMillId",
                table: "Incomes",
                column: "RiceMillId",
                principalTable: "RiceMills",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Incomes_Users_UserId",
                table: "Incomes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InputLoads_People_CarrierPersonId",
                table: "InputLoads",
                column: "CarrierPersonId",
                principalTable: "People",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InputLoads_People_DelivererPersonId",
                table: "InputLoads",
                column: "DelivererPersonId",
                principalTable: "People",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InputLoads_People_OwnerPersonId",
                table: "InputLoads",
                column: "OwnerPersonId",
                principalTable: "People",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InputLoads_People_ReceiverPersonId",
                table: "InputLoads",
                column: "ReceiverPersonId",
                principalTable: "People",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InputLoads_RiceMills_RiceMillId",
                table: "InputLoads",
                column: "RiceMillId",
                principalTable: "RiceMills",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InputLoads_Users_UserId",
                table: "InputLoads",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InputLoads_Vehicles_VehicleId",
                table: "InputLoads",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InputLoads_Villages_VillageId",
                table: "InputLoads",
                column: "VillageId",
                principalTable: "Villages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_People_PaidPersonId",
                table: "Payments",
                column: "PaidPersonId",
                principalTable: "People",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_RiceMills_RiceMillId",
                table: "Payments",
                column: "RiceMillId",
                principalTable: "RiceMills",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Users_UserId",
                table: "Payments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_People_RiceMills_RiceMillId",
                table: "People",
                column: "RiceMillId",
                principalTable: "RiceMills",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_People_Users_UserId",
                table: "People",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_RiceMills_RiceMillId",
                table: "People");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_RiceMills_RiceMillId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_People_Users_UserId",
                table: "People");

            migrationBuilder.DropTable(
                name: "DeliveryRiceThreshing");

            migrationBuilder.DropTable(
                name: "DryerHistoryInputLoad");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "UserActivities");

            migrationBuilder.DropTable(
                name: "Deliveries");

            migrationBuilder.DropTable(
                name: "DryerHistories");

            migrationBuilder.DropTable(
                name: "Concerns");

            migrationBuilder.DropTable(
                name: "InputLoads");

            migrationBuilder.DropTable(
                name: "Dryers");

            migrationBuilder.DropTable(
                name: "RiceThreshings");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Villages");

            migrationBuilder.DropTable(
                name: "Incomes");

            migrationBuilder.DropTable(
                name: "RiceMills");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
