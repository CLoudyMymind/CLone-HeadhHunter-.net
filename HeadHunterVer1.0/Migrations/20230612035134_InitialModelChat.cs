using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeadHunterVer1._0.Migrations
{
    /// <inheritdoc />
    public partial class InitialModelChat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    DateSendMessage = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserEmployeeId = table.Column<string>(type: "text", nullable: false),
                    UserEmployerId = table.Column<string>(type: "text", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    VacancyId = table.Column<string>(type: "text", nullable: false),
                    ResumeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chats_AspNetUsers_UserEmployeeId",
                        column: x => x.UserEmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Chats_AspNetUsers_UserEmployerId",
                        column: x => x.UserEmployerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Chats_Resumes_ResumeId",
                        column: x => x.ResumeId,
                        principalTable: "Resumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Chats_Vacancies_VacancyId",
                        column: x => x.VacancyId,
                        principalTable: "Vacancies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chats_ResumeId",
                table: "Chats",
                column: "ResumeId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_UserEmployeeId",
                table: "Chats",
                column: "UserEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_UserEmployerId",
                table: "Chats",
                column: "UserEmployerId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_VacancyId",
                table: "Chats",
                column: "VacancyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chats");
        }
    }
}
