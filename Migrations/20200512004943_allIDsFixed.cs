using Microsoft.EntityFrameworkCore.Migrations;

namespace T2RMSWS.Migrations
{
    public partial class allIDsFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Areas_Restaurants_RestaurantId",
                table: "Areas");

            migrationBuilder.DropForeignKey(
                name: "FK_People_Restaurants_RestaurantId",
                table: "People");

            migrationBuilder.DropForeignKey(
                name: "FK_People_People_Staff_ManagerId",
                table: "People");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_People_CustomerId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ReservationStatuses_ReservationStatusId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ReservationTypes_ReservationTypeId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_User_UserId",
                table: "Reservations");

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
                name: "FK_TableSittings_Tables_TableId",
                table: "TableSittings");

            migrationBuilder.DropForeignKey(
                name: "FK_User_People_ManagerId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_People_MemberId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_People_StaffId",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_TableSittings",
                table: "TableSittings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TableReserations",
                table: "TableReserations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Restaurants",
                table: "Restaurants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReservationTypes",
                table: "ReservationTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReservationStatuses",
                table: "ReservationStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_CustomerId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_People_Staff_ManagerId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Desc",
                table: "User");

            migrationBuilder.DropColumn(
                name: "TableSittingId",
                table: "TableSittings");

            migrationBuilder.DropColumn(
                name: "ReservedId",
                table: "TableReserations");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "ReservationTypeId",
                table: "ReservationTypes");

            migrationBuilder.DropColumn(
                name: "ReservationStatusId",
                table: "ReservationStatuses");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "Reservations");

            migrationBuilder.AlterColumn<int>(
                name: "StaffId",
                table: "User",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                table: "User",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ManagerId",
                table: "User",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "User",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagerId1",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MemberId1",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StaffId1",
                table: "User",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TableId",
                table: "TableSittings",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SittingId",
                table: "TableSittings",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TableSittings",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "TableSittingId",
                table: "TableReserations",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReservationId",
                table: "TableReserations",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TableReserations",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Restaurants",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ReservationTypes",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ReservationStatuses",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Reservations",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReservationTypeId",
                table: "Reservations",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReservationStatusId",
                table: "Reservations",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Reservations",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Reservations",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "CustomerId1",
                table: "Reservations",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Staff_ManagerId",
                table: "People",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "People",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagerId1",
                table: "People",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RestaurantId",
                table: "Areas",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TableSittings",
                table: "TableSittings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TableReserations",
                table: "TableReserations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Restaurants",
                table: "Restaurants",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReservationTypes",
                table: "ReservationTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReservationStatuses",
                table: "ReservationStatuses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations",
                column: "Id");

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
                name: "FK_Areas_Restaurants_RestaurantId",
                table: "Areas",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_People_Restaurants_RestaurantId",
                table: "People",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Reservations_ReservationStatuses_ReservationStatusId",
                table: "Reservations",
                column: "ReservationStatusId",
                principalTable: "ReservationStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_ReservationTypes_ReservationTypeId",
                table: "Reservations",
                column: "ReservationTypeId",
                principalTable: "ReservationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_TableSittings_Sittings_SittingId",
                table: "TableSittings",
                column: "SittingId",
                principalTable: "Sittings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TableSittings_Tables_TableId",
                table: "TableSittings",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Areas_Restaurants_RestaurantId",
                table: "Areas");

            migrationBuilder.DropForeignKey(
                name: "FK_People_Restaurants_RestaurantId",
                table: "People");

            migrationBuilder.DropForeignKey(
                name: "FK_People_People_ManagerId1",
                table: "People");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_People_CustomerId1",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ReservationStatuses_ReservationStatusId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ReservationTypes_ReservationTypeId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_User_UserId",
                table: "Reservations");

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
                name: "FK_TableSittings_Tables_TableId",
                table: "TableSittings");

            migrationBuilder.DropForeignKey(
                name: "FK_User_People_ManagerId1",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_People_MemberId1",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_People_StaffId1",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_TableSittings",
                table: "TableSittings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TableReserations",
                table: "TableReserations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Restaurants",
                table: "Restaurants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReservationTypes",
                table: "ReservationTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReservationStatuses",
                table: "ReservationStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_CustomerId1",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_People_ManagerId1",
                table: "People");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "User");

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
                name: "Id",
                table: "TableSittings");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TableReserations");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ReservationTypes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ReservationStatuses");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ManagerId1",
                table: "People");

            migrationBuilder.AlterColumn<string>(
                name: "StaffId",
                table: "User",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "MemberId",
                table: "User",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "ManagerId",
                table: "User",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Desc",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TableId",
                table: "TableSittings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "SittingId",
                table: "TableSittings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "TableSittingId",
                table: "TableSittings",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "TableSittingId",
                table: "TableReserations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ReservationId",
                table: "TableReserations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ReservedId",
                table: "TableReserations",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "Restaurants",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ReservationTypeId",
                table: "ReservationTypes",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ReservationStatusId",
                table: "ReservationStatuses",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Reservations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ReservationTypeId",
                table: "Reservations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ReservationStatusId",
                table: "Reservations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "Reservations",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ReservationId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Staff_ManagerId",
                table: "People",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "People",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RestaurantId",
                table: "Areas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TableSittings",
                table: "TableSittings",
                column: "TableSittingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TableReserations",
                table: "TableReserations",
                column: "ReservedId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Restaurants",
                table: "Restaurants",
                column: "RestaurantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReservationTypes",
                table: "ReservationTypes",
                column: "ReservationTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReservationStatuses",
                table: "ReservationStatuses",
                column: "ReservationStatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations",
                column: "ReservationId");

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
                name: "FK_Areas_Restaurants_RestaurantId",
                table: "Areas",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "RestaurantId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_People_Restaurants_RestaurantId",
                table: "People",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "RestaurantId",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Reservations_ReservationStatuses_ReservationStatusId",
                table: "Reservations",
                column: "ReservationStatusId",
                principalTable: "ReservationStatuses",
                principalColumn: "ReservationStatusId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_ReservationTypes_ReservationTypeId",
                table: "Reservations",
                column: "ReservationTypeId",
                principalTable: "ReservationTypes",
                principalColumn: "ReservationTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_User_UserId",
                table: "Reservations",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

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
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TableSittings_Tables_TableId",
                table: "TableSittings",
                column: "TableId",
                principalTable: "Tables",
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
    }
}
