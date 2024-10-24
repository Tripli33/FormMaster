using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormMaster.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ulpdate_template_entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Templates",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "Templates");
        }
    }
}
