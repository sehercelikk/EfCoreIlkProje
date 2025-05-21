using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfCore.Migrations
{
    /// <inheritdoc />
    public partial class Mig_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kurslar_Egitmenler_EgitmenId",
                table: "Kurslar");

            migrationBuilder.AlterColumn<int>(
                name: "EgitmenId",
                table: "Kurslar",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Kurslar_Egitmenler_EgitmenId",
                table: "Kurslar",
                column: "EgitmenId",
                principalTable: "Egitmenler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kurslar_Egitmenler_EgitmenId",
                table: "Kurslar");

            migrationBuilder.AlterColumn<int>(
                name: "EgitmenId",
                table: "Kurslar",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Kurslar_Egitmenler_EgitmenId",
                table: "Kurslar",
                column: "EgitmenId",
                principalTable: "Egitmenler",
                principalColumn: "Id");
        }
    }
}
