using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WeddingPlanner.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeddingPlans",
                columns: table => new
                {
                    PlanId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    WedderOne = table.Column<string>(nullable: false),
                    WedderTwo = table.Column<string>(nullable: false),
                    WeddingDate = table.Column<DateTime>(nullable: false),
                    WeddingAddress = table.Column<string>(nullable: false),
                    CreatorId = table.Column<int>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeddingPlans", x => x.PlanId);
                });

            migrationBuilder.CreateTable(
                name: "WeddingGuests",
                columns: table => new
                {
                    GuestId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    PlanId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeddingGuests", x => x.GuestId);
                    table.ForeignKey(
                        name: "FK_WeddingGuests_WeddingPlans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "WeddingPlans",
                        principalColumn: "PlanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WeddingGuests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeddingGuests_PlanId",
                table: "WeddingGuests",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_WeddingGuests_UserId",
                table: "WeddingGuests",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeddingGuests");

            migrationBuilder.DropTable(
                name: "WeddingPlans");
        }
    }
}
