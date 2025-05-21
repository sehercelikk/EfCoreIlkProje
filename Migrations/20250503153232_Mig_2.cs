using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfCore.Migrations
{
    /// <inheritdoc />
    public partial class Mig_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EgitmenId",
                table: "Kurslar",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Egitmenler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AdSoyad = table.Column<string>(type: "TEXT", nullable: true),
                    EPosta = table.Column<string>(type: "TEXT", nullable: false),
                    Telefon = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Egitmenler", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kurslar_EgitmenId",
                table: "Kurslar",
                column: "EgitmenId");

            migrationBuilder.CreateIndex(
                name: "IX_KursKayitlari_KursId",
                table: "KursKayitlari",
                column: "KursId");

            migrationBuilder.CreateIndex(
                name: "IX_KursKayitlari_OgrenciId",
                table: "KursKayitlari",
                column: "OgrenciId");

            migrationBuilder.AddForeignKey(
                name: "FK_KursKayitlari_Kurslar_KursId",
                table: "KursKayitlari",
                column: "KursId",
                principalTable: "Kurslar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KursKayitlari_Ogrenciler_OgrenciId",
                table: "KursKayitlari",
                column: "OgrenciId",
                principalTable: "Ogrenciler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Kurslar_Egitmenler_EgitmenId",
                table: "Kurslar",
                column: "EgitmenId",
                principalTable: "Egitmenler",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KursKayitlari_Kurslar_KursId",
                table: "KursKayitlari");

            migrationBuilder.DropForeignKey(
                name: "FK_KursKayitlari_Ogrenciler_OgrenciId",
                table: "KursKayitlari");

            migrationBuilder.DropForeignKey(
                name: "FK_Kurslar_Egitmenler_EgitmenId",
                table: "Kurslar");

            migrationBuilder.DropTable(
                name: "Egitmenler");

            migrationBuilder.DropIndex(
                name: "IX_Kurslar_EgitmenId",
                table: "Kurslar");

            migrationBuilder.DropIndex(
                name: "IX_KursKayitlari_KursId",
                table: "KursKayitlari");

            migrationBuilder.DropIndex(
                name: "IX_KursKayitlari_OgrenciId",
                table: "KursKayitlari");

            migrationBuilder.DropColumn(
                name: "EgitmenId",
                table: "Kurslar");
        }
    }
}
