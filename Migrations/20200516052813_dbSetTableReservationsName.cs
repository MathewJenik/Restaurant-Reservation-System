using Microsoft.EntityFrameworkCore.Migrations;

namespace T2RMSWS.Migrations
{
    public partial class dbSetTableReservationsName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.DropForeignKey(
                name: "FK_TableReserations_Reservations_ReservationId",
                table: "TableReserations");

            migrationBuilder.DropForeignKey(
                name: "FK_TableReserations_TableSittings_TableSittingId",
                table: "TableReserations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TableReserations",
                table: "TableReserations");

            migrationBuilder.RenameTable(
                name: "TableReserations",
                newName: "TableReservations");

            migrationBuilder.RenameIndex(
                name: "IX_TableReserations_TableSittingId",
                table: "TableReservations",
                newName: "IX_TableReservations_TableSittingId");

            migrationBuilder.RenameIndex(
                name: "IX_TableReserations_ReservationId",
                table: "TableReservations",
                newName: "IX_TableReservations_ReservationId");

            

            migrationBuilder.AlterColumn<int>(
                name: "TableSittingId",
                table: "TableReservations",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TableReservations",
                table: "TableReservations",
                column: "Id");

            
            migrationBuilder.AddForeignKey(
                name: "FK_TableReservations_Reservations_ReservationId",
                table: "TableReservations",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TableReservations_TableSittings_TableSittingId",
                table: "TableReservations",
                column: "TableSittingId",
                principalTable: "TableSittings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_User_UserId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_TableReservations_Reservations_ReservationId",
                table: "TableReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_TableReservations_TableSittings_TableSittingId",
                table: "TableReservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TableReservations",
                table: "TableReservations");

            migrationBuilder.RenameTable(
                name: "TableReservations",
                newName: "TableReserations");

            migrationBuilder.RenameIndex(
                name: "IX_TableReservations_TableSittingId",
                table: "TableReserations",
                newName: "IX_TableReserations_TableSittingId");

            migrationBuilder.RenameIndex(
                name: "IX_TableReservations_ReservationId",
                table: "TableReserations",
                newName: "IX_TableReserations_ReservationId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Reservations",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TableSittingId",
                table: "TableReserations",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TableReserations",
                table: "TableReserations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_User_UserId",
                table: "Reservations",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TableReserations_Reservations_ReservationId",
                table: "TableReserations",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TableReserations_TableSittings_TableSittingId",
                table: "TableReserations",
                column: "TableSittingId",
                principalTable: "TableSittings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
