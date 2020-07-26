using Microsoft.EntityFrameworkCore.Migrations;

namespace T2RMSWS.Migrations
{
    public partial class AddRelationshipToArea_Restaurant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "Areas",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Areas_RestaurantId",
                table: "Areas",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Areas_Restaurants_RestaurantId",
                table: "Areas",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "RestaurantId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Areas_Restaurants_RestaurantId",
                table: "Areas");

            migrationBuilder.DropIndex(
                name: "IX_Areas_RestaurantId",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "Areas");
        }
    }
}
