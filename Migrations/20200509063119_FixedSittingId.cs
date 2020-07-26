using Microsoft.EntityFrameworkCore.Migrations;

namespace T2RMSWS.Migrations
{
    public partial class FixedSittingId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sittings_SittingStatuses_SittingStatusId",
                table: "Sittings");

            migrationBuilder.DropForeignKey(
                name: "FK_Sittings_SittingTypes_SittingTypeId",
                table: "Sittings");

            migrationBuilder.DropForeignKey(
                name: "FK_TableSittings_Sittings_SittingId",
                table: "TableSittings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SittingTypes",
                table: "SittingTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SittingStatuses",
                table: "SittingStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sittings",
                table: "Sittings");

            migrationBuilder.DropColumn(
                name: "SittingTypeId",
                table: "SittingTypes");

            migrationBuilder.DropColumn(
                name: "SittingStatusId",
                table: "SittingStatuses");

            migrationBuilder.DropColumn(
                name: "SittingId",
                table: "Sittings");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "SittingTypes",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "SittingStatuses",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "SittingTypeId",
                table: "Sittings",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SittingStatusId",
                table: "Sittings",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Sittings",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SittingTypes",
                table: "SittingTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SittingStatuses",
                table: "SittingStatuses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sittings",
                table: "Sittings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sittings_SittingStatuses_SittingStatusId",
                table: "Sittings",
                column: "SittingStatusId",
                principalTable: "SittingStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sittings_SittingTypes_SittingTypeId",
                table: "Sittings",
                column: "SittingTypeId",
                principalTable: "SittingTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TableSittings_Sittings_SittingId",
                table: "TableSittings",
                column: "SittingId",
                principalTable: "Sittings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sittings_SittingStatuses_SittingStatusId",
                table: "Sittings");

            migrationBuilder.DropForeignKey(
                name: "FK_Sittings_SittingTypes_SittingTypeId",
                table: "Sittings");

            migrationBuilder.DropForeignKey(
                name: "FK_TableSittings_Sittings_SittingId",
                table: "TableSittings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SittingTypes",
                table: "SittingTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SittingStatuses",
                table: "SittingStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sittings",
                table: "Sittings");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SittingTypes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SittingStatuses");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Sittings");

            migrationBuilder.AddColumn<int>(
                name: "SittingTypeId",
                table: "SittingTypes",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "SittingStatusId",
                table: "SittingStatuses",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "SittingTypeId",
                table: "Sittings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "SittingStatusId",
                table: "Sittings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "SittingId",
                table: "Sittings",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SittingTypes",
                table: "SittingTypes",
                column: "SittingTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SittingStatuses",
                table: "SittingStatuses",
                column: "SittingStatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sittings",
                table: "Sittings",
                column: "SittingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sittings_SittingStatuses_SittingStatusId",
                table: "Sittings",
                column: "SittingStatusId",
                principalTable: "SittingStatuses",
                principalColumn: "SittingStatusId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sittings_SittingTypes_SittingTypeId",
                table: "Sittings",
                column: "SittingTypeId",
                principalTable: "SittingTypes",
                principalColumn: "SittingTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TableSittings_Sittings_SittingId",
                table: "TableSittings",
                column: "SittingId",
                principalTable: "Sittings",
                principalColumn: "SittingId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
