using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeadHunterVer1._0.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePropBoolResponseApplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsAcceptOrRejectedResponse",
                table: "ResponseApplications",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsAcceptOrRejectedResponse",
                table: "ResponseApplications",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);
        }
    }
}
