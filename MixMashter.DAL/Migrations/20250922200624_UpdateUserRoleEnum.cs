using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MixMashter.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserRoleEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Explicit",
                table: "Songs",
                newName: "IsExplicit");

            migrationBuilder.RenameColumn(
                name: "Explicit",
                table: "Mashups",
                newName: "IsExplicit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsExplicit",
                table: "Songs",
                newName: "Explicit");

            migrationBuilder.RenameColumn(
                name: "IsExplicit",
                table: "Mashups",
                newName: "Explicit");
        }
    }
}
