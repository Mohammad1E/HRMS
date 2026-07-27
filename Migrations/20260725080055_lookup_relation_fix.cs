using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.Migrations
{
    /// <inheritdoc />
    public partial class lookup_relation_fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PositionName",
                table: "Employees");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PositionName",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
