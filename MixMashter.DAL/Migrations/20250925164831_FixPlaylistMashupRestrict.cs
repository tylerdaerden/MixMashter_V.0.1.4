using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MixMashter.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixPlaylistMashupRestrict : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistMashups_Songs_SongId",
                table: "PlaylistMashups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlaylistMashups",
                table: "PlaylistMashups");

            migrationBuilder.AlterColumn<int>(
                name: "SongId",
                table: "PlaylistMashups",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MashupId",
                table: "PlaylistMashups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlaylistMashups",
                table: "PlaylistMashups",
                columns: new[] { "PlaylistId", "MashupId" });

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistMashups_MashupId",
                table: "PlaylistMashups",
                column: "MashupId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistMashups_Mashups_MashupId",
                table: "PlaylistMashups",
                column: "MashupId",
                principalTable: "Mashups",
                principalColumn: "MashupId",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_PlaylistMashups_Mashups_MashupId",
                table: "PlaylistMashups");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistMashups_Songs_SongId",
                table: "PlaylistMashups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlaylistMashups",
                table: "PlaylistMashups");

            migrationBuilder.DropIndex(
                name: "IX_PlaylistMashups_MashupId",
                table: "PlaylistMashups");

            migrationBuilder.DropColumn(
                name: "MashupId",
                table: "PlaylistMashups");

            migrationBuilder.AlterColumn<int>(
                name: "SongId",
                table: "PlaylistMashups",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlaylistMashups",
                table: "PlaylistMashups",
                columns: new[] { "PlaylistId", "SongId" });

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
