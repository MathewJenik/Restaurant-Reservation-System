using Microsoft.EntityFrameworkCore.Migrations;

namespace T2RMSWS.Migrations
{
    public partial class updateListstoAccessIDsInActualTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_TableReserations_TableReservationReservedId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Sittings_TableSittings_TableSittingId",
                table: "Sittings");

            migrationBuilder.DropForeignKey(
                name: "FK_Tables_TableSittings_TableSittingId",
                table: "Tables");

            migrationBuilder.DropForeignKey(
                name: "FK_TableSittings_TableReserations_TableReservationReservedId",
                table: "TableSittings");

            migrationBuilder.DropIndex(
                name: "IX_TableSittings_TableReservationReservedId",
                table: "TableSittings");

            migrationBuilder.DropIndex(
                name: "IX_Tables_TableSittingId",
                table: "Tables");

            migrationBuilder.DropIndex(
                name: "IX_Sittings_TableSittingId",
                table: "Sittings");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_TableReservationReservedId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "TableReservationReservedId",
                table: "TableSittings");

            migrationBuilder.DropColumn(
                name: "TableSittingId",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "TableSittingId",
                table: "Sittings");

            migrationBuilder.DropColumn(
                name: "TableReservationReservedId",
                table: "Reservations");

            migrationBuilder.AddColumn<int>(
                name: "SittingId",
                table: "TableSittings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TableNo",
                table: "TableSittings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReservationId",
                table: "TableReserations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TableSittingId",
                table: "TableReserations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TableSittings_SittingId",
                table: "TableSittings",
                column: "SittingId");

            migrationBuilder.CreateIndex(
                name: "IX_TableSittings_TableNo",
                table: "TableSittings",
                column: "TableNo");

            migrationBuilder.CreateIndex(
                name: "IX_TableReserations_ReservationId",
                table: "TableReserations",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_TableReserations_TableSittingId",
                table: "TableReserations",
                column: "TableSittingId");

            migrationBuilder.AddForeignKey(
                name: "FK_TableReserations_Reservations_ReservationId",
                table: "TableReserations",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "ReservationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TableReserations_TableSittings_TableSittingId",
                table: "TableReserations",
                column: "TableSittingId",
                principalTable: "TableSittings",
                principalColumn: "TableSittingId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TableSittings_Sittings_SittingId",
                table: "TableSittings",
                column: "SittingId",
                principalTable: "Sittings",
                principalColumn: "SittingId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TableSittings_Tables_TableNo",
                table: "TableSittings",
                column: "TableNo",
                principalTable: "Tables",
                principalColumn: "TableNo",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TableReserations_Reservations_ReservationId",
                table: "TableReserations");

            migrationBuilder.DropForeignKey(
                name: "FK_TableReserations_TableSittings_TableSittingId",
                table: "TableReserations");

            migrationBuilder.DropForeignKey(
                name: "FK_TableSittings_Sittings_SittingId",
                table: "TableSittings");

            migrationBuilder.DropForeignKey(
                name: "FK_TableSittings_Tables_TableNo",
                table: "TableSittings");

            migrationBuilder.DropIndex(
                name: "IX_TableSittings_SittingId",
                table: "TableSittings");

            migrationBuilder.DropIndex(
                name: "IX_TableSittings_TableNo",
                table: "TableSittings");

            migrationBuilder.DropIndex(
                name: "IX_TableReserations_ReservationId",
                table: "TableReserations");

            migrationBuilder.DropIndex(
                name: "IX_TableReserations_TableSittingId",
                table: "TableReserations");

            migrationBuilder.DropColumn(
                name: "SittingId",
                table: "TableSittings");

            migrationBuilder.DropColumn(
                name: "TableNo",
                table: "TableSittings");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "TableReserations");

            migrationBuilder.DropColumn(
                name: "TableSittingId",
                table: "TableReserations");

            migrationBuilder.AddColumn<int>(
                name: "TableReservationReservedId",
                table: "TableSittings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TableSittingId",
                table: "Tables",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TableSittingId",
                table: "Sittings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TableReservationReservedId",
                table: "Reservations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TableSittings_TableReservationReservedId",
                table: "TableSittings",
                column: "TableReservationReservedId");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_TableSittingId",
                table: "Tables",
                column: "TableSittingId");

            migrationBuilder.CreateIndex(
                name: "IX_Sittings_TableSittingId",
                table: "Sittings",
                column: "TableSittingId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_TableReservationReservedId",
                table: "Reservations",
                column: "TableReservationReservedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_TableReserations_TableReservationReservedId",
                table: "Reservations",
                column: "TableReservationReservedId",
                principalTable: "TableReserations",
                principalColumn: "ReservedId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sittings_TableSittings_TableSittingId",
                table: "Sittings",
                column: "TableSittingId",
                principalTable: "TableSittings",
                principalColumn: "TableSittingId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_TableSittings_TableSittingId",
                table: "Tables",
                column: "TableSittingId",
                principalTable: "TableSittings",
                principalColumn: "TableSittingId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TableSittings_TableReserations_TableReservationReservedId",
                table: "TableSittings",
                column: "TableReservationReservedId",
                principalTable: "TableReserations",
                principalColumn: "ReservedId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
