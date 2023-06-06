using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeadHunterVer1._0.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdatedAtResume : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Resumes",
                type: "timestamp without time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Resumes");
        }
    }
}
