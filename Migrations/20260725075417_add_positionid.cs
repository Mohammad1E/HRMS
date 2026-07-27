using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.Migrations
{
    /// <inheritdoc />
    public partial class add_positionid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PositionId",
                table: "Employees",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PositionName",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PositionId",
                table: "Employees",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Lookups_PositionId",
                table: "Employees",
                column: "PositionId",
                principalTable: "Lookups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Lookups_PositionId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_PositionId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PositionName",
                table: "Employees");
        }
    }
}
