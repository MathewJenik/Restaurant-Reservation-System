using Microsoft.EntityFrameworkCore.Migrations;

namespace T2RMSWS.Migrations
{
    public partial class FixedTableId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Areas_AreaId",
                table: "Tables");

            migrationBuilder.DropForeignKey(
                name: "FK_TableSittings_Tables_TableNo",
                table: "TableSittings");

            migrationBuilder.DropIndex(
                name: "IX_TableSittings_TableNo",
                table: "TableSittings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tables",
                table: "Tables");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Areas",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "TableNo",
                table: "TableSittings");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "Areas");

            migrationBuilder.AddColumn<int>(
                name: "TableId",
                table: "TableSittings",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AreaId",
                table: "Tables",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TableNo",
                table: "Tables",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Tables",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Areas",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tables",
                table: "Tables",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Areas",
                table: "Areas",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TableSittings_TableId",
                table: "TableSittings",
                column: "TableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Areas_AreaId",
                table: "Tables",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TableSittings_Tables_TableId",
                table: "TableSittings",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Areas_AreaId",
                table: "Tables");

            migrationBuilder.DropForeignKey(
                name: "FK_TableSittings_Tables_TableId",
                table: "TableSittings");

            migrationBuilder.DropIndex(
                name: "IX_TableSittings_TableId",
                table: "TableSittings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tables",
                table: "Tables");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Areas",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "TableId",
                table: "TableSittings");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Areas");

            migrationBuilder.AddColumn<string>(
                name: "TableNo",
                table: "TableSittings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TableNo",
                table: "Tables",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AreaId",
                table: "Tables",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "Areas",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tables",
                table: "Tables",
                column: "TableNo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Areas",
                table: "Areas",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_TableSittings_TableNo",
                table: "TableSittings",
                column: "TableNo");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Areas_AreaId",
                table: "Tables",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "AreaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TableSittings_Tables_TableNo",
                table: "TableSittings",
                column: "TableNo",
                principalTable: "Tables",
                principalColumn: "TableNo",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
