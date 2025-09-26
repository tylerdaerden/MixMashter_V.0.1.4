using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MixMashter.DAL.Migrations
{
    public partial class RemoveSongIdFinal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Supprimer la FK SongId si encore présente
            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistMashups_Songs_SongId",
                table: "PlaylistMashups");

            // Supprimer l’index SongId si encore présent
            migrationBuilder.DropIndex(
                name: "IX_PlaylistMashups_SongId",
                table: "PlaylistMashups");

            // Supprimer la colonne SongId si encore présente
            migrationBuilder.DropColumn(
                name: "SongId",
                table: "PlaylistMashups");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Réintroduire SongId
            migrationBuilder.AddColumn<int>(
                name: "SongId",
                table: "PlaylistMashups",
                type: "int",
                nullable: true);

            // Recréer index
            migrationBuilder.CreateIndex(
                name: "IX_PlaylistMashups_SongId",
                table: "PlaylistMashups",
                column: "SongId");

            // Recréer la FK
            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistMashups_Songs_SongId",
                table: "PlaylistMashups",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "SongId");
        }
    }
}
