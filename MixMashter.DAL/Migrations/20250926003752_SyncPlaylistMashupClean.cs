using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MixMashter.DAL.Migrations
{
    public partial class SyncPlaylistMashupClean : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Rien à supprimer car SongId est déjà parti
            // Cette migration ne fait qu’assurer la synchro avec le snapshot
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Si on rollback, on réintroduit SongId
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
    }
}
