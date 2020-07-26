using Microsoft.EntityFrameworkCore.Migrations;

namespace T2RMSWS.Migrations
{
    public partial class allIDsFixedFinally : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_People_ManagerId1",
                table: "People");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_People_CustomerId1",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_User_People_ManagerId1",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_People_MemberId1",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_People_StaffId1",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_ManagerId1",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_MemberId1",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_StaffId1",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_CustomerId1",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_People_ManagerId1",
                table: "People");

            migrationBuilder.DropColumn(
                name: "ManagerId1",
                table: "User");

            migrationBuilder.DropColumn(
                name: "MemberId1",
                table: "User");

            migrationBuilder.DropColumn(
                name: "StaffId1",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ManagerId1",
                table: "People");

            migrationBuilder.AlterColumn<string>(
                name: "StaffId",
                table: "User",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "MemberId",
                table: "User",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ManagerId",
                table: "User",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "Reservations",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Staff_ManagerId",
                table: "People",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_ManagerId",
                table: "User",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_User_MemberId",
                table: "User",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_User_StaffId",
                table: "User",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CustomerId",
                table: "Reservations",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_People_Staff_ManagerId",
                table: "People",
                column: "Staff_ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_People_People_Staff_ManagerId",
                table: "People",
                column: "Staff_ManagerId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_People_CustomerId",
                table: "Reservations",
                column: "CustomerId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_People_ManagerId",
                table: "User",
                column: "ManagerId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_People_MemberId",
                table: "User",
                column: "MemberId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_People_StaffId",
                table: "User",
                column: "StaffId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_People_Staff_ManagerId",
                table: "People");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_People_CustomerId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_User_People_ManagerId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_People_MemberId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_People_StaffId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_ManagerId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_MemberId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_StaffId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_CustomerId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_People_Staff_ManagerId",
                table: "People");

            migrationBuilder.AlterColumn<int>(
                name: "StaffId",
                table: "User",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                table: "User",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ManagerId",
                table: "User",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagerId1",
                table: "User",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MemberId1",
                table: "User",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StaffId1",
                table: "User",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Reservations",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerId1",
                table: "Reservations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Staff_ManagerId",
                table: "People",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagerId1",
                table: "People",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_ManagerId1",
                table: "User",
                column: "ManagerId1");

            migrationBuilder.CreateIndex(
                name: "IX_User_MemberId1",
                table: "User",
                column: "MemberId1");

            migrationBuilder.CreateIndex(
                name: "IX_User_StaffId1",
                table: "User",
                column: "StaffId1");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CustomerId1",
                table: "Reservations",
                column: "CustomerId1");

            migrationBuilder.CreateIndex(
                name: "IX_People_ManagerId1",
                table: "People",
                column: "ManagerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_People_People_ManagerId1",
                table: "People",
                column: "ManagerId1",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_People_CustomerId1",
                table: "Reservations",
                column: "CustomerId1",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_People_ManagerId1",
                table: "User",
                column: "ManagerId1",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_People_MemberId1",
                table: "User",
                column: "MemberId1",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_People_StaffId1",
                table: "User",
                column: "StaffId1",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
