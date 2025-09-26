using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MixMashter.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ResyncPlaylistMashupFinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SongId",
                table: "PlaylistMashups",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistMashups_SongId",
                table: "PlaylistMashups",
                column: "SongId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistMashups_Songs_SongId",
                table: "PlaylistMashups",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "SongId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistMashups_Songs_SongId",
                table: "PlaylistMashups");

            migrationBuilder.DropIndex(
                name: "IX_PlaylistMashups_SongId",
                table: "PlaylistMashups");

            migrationBuilder.DropColumn(
                name: "SongId",
                table: "PlaylistMashups");
        }
    }
}
