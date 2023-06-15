using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeadHunterVer1._0.Migrations
{
    /// <inheritdoc />
    public partial class InitialModelResponseApplications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResponseApplications",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ResumeId = table.Column<int>(type: "integer", nullable: false),
                    VacancyId = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    DispatchTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsAcceptResponse = table.Column<bool>(type: "boolean", nullable: false),
                    IsRejectedAcceptResponse = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponseApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResponseApplications_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResponseApplications_Resumes_ResumeId",
                        column: x => x.ResumeId,
                        principalTable: "Resumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResponseApplications_Vacancies_VacancyId",
                        column: x => x.VacancyId,
                        principalTable: "Vacancies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResponseApplications_ResumeId",
                table: "ResponseApplications",
                column: "ResumeId");

            migrationBuilder.CreateIndex(
                name: "IX_ResponseApplications_UserId",
                table: "ResponseApplications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResponseApplications_VacancyId",
                table: "ResponseApplications",
                column: "VacancyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResponseApplications");
        }
    }
}
