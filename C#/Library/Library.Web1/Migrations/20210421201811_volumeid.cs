using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Migrations
{
    public partial class volumeid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Volumes_volumeId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "volumeId",
                table: "Reservations",
                newName: "VolumeId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_volumeId",
                table: "Reservations",
                newName: "IX_Reservations_VolumeId");

            migrationBuilder.AlterColumn<int>(
                name: "VolumeId",
                table: "Reservations",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Volumes_VolumeId",
                table: "Reservations",
                column: "VolumeId",
                principalTable: "Volumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Volumes_VolumeId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "VolumeId",
                table: "Reservations",
                newName: "volumeId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_VolumeId",
                table: "Reservations",
                newName: "IX_Reservations_volumeId");

            migrationBuilder.AlterColumn<int>(
                name: "volumeId",
                table: "Reservations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Volumes_volumeId",
                table: "Reservations",
                column: "volumeId",
                principalTable: "Volumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
