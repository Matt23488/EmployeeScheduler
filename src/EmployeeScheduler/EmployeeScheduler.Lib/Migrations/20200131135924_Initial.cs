using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeScheduler.Lib.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeID);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleWeeks",
                columns: table => new
                {
                    ScheduleWeekID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleWeeks", x => x.ScheduleWeekID);
                });

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    TokenID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Role = table.Column<int>(nullable: false),
                    TokenValue = table.Column<string>(nullable: true),
                    IpAddress = table.Column<string>(nullable: true),
                    Expires = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.TokenID);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleDays",
                columns: table => new
                {
                    ScheduleDayID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsClosed = table.Column<bool>(nullable: false),
                    ScheduleWeekID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleDays", x => x.ScheduleDayID);
                    table.ForeignKey(
                        name: "FK_ScheduleDays_ScheduleWeeks_ScheduleWeekID",
                        column: x => x.ScheduleWeekID,
                        principalTable: "ScheduleWeeks",
                        principalColumn: "ScheduleWeekID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSchedules",
                columns: table => new
                {
                    EmployeeScheduleID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsOff = table.Column<bool>(nullable: false),
                    From = table.Column<DateTime>(nullable: false),
                    To = table.Column<DateTime>(nullable: false),
                    LunchType = table.Column<int>(nullable: false),
                    EmployeeID = table.Column<int>(nullable: false),
                    ScheduleDayID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSchedules", x => x.EmployeeScheduleID);
                    table.ForeignKey(
                        name: "FK_EmployeeSchedules_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeSchedules_ScheduleDays_ScheduleDayID",
                        column: x => x.ScheduleDayID,
                        principalTable: "ScheduleDays",
                        principalColumn: "ScheduleDayID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSchedules_EmployeeID",
                table: "EmployeeSchedules",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSchedules_ScheduleDayID",
                table: "EmployeeSchedules",
                column: "ScheduleDayID");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleDays_ScheduleWeekID",
                table: "ScheduleDays",
                column: "ScheduleWeekID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeSchedules");

            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "ScheduleDays");

            migrationBuilder.DropTable(
                name: "ScheduleWeeks");
        }
    }
}
