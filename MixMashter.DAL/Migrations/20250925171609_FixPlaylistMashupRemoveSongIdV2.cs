using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MixMashter.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixPlaylistMashupRemoveSongIdV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Supprimer FK et PK existants liés à SongId
            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistMashups_Songs_SongId",
                table: "PlaylistMashups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlaylistMashups",
                table: "PlaylistMashups");

            migrationBuilder.DropIndex(
                name: "IX_PlaylistMashups_SongId",
                table: "PlaylistMashups");

            migrationBuilder.DropColumn(
                name: "SongId",
                table: "PlaylistMashups");

            // Recréer la PK avec PlaylistId + MashupId
            migrationBuilder.AddPrimaryKey(
                name: "PK_PlaylistMashups",
                table: "PlaylistMashups",
                columns: new[] { "PlaylistId", "MashupId" });

            // Ajouter uniquement la FK vers Mashups
            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistMashups_Mashups_MashupId",
                table: "PlaylistMashups",
                column: "MashupId",
                principalTable: "Mashups",
                principalColumn: "MashupId",
                onDelete: ReferentialAction.Restrict); // éviter cascade multiple
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Supprimer la FK Mashups
            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistMashups_Mashups_MashupId",
                table: "PlaylistMashups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlaylistMashups",
                table: "PlaylistMashups");

            // Réintroduire SongId
            migrationBuilder.AddColumn<int>(
                name: "SongId",
                table: "PlaylistMashups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // PK avec PlaylistId + SongId
            migrationBuilder.AddPrimaryKey(
                name: "PK_PlaylistMashups",
                table: "PlaylistMashups",
                columns: new[] { "PlaylistId", "SongId" });

            // Recréer l’index SongId
            migrationBuilder.CreateIndex(
                name: "IX_PlaylistMashups_SongId",
                table: "PlaylistMashups",
                column: "SongId");

            // FK SongId → Songs
            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistMashups_Songs_SongId",
                table: "PlaylistMashups",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "SongId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
