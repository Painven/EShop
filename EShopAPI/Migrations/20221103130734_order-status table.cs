using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EShopAPI.Migrations
{
    public partial class orderstatustable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_OrderStatuses_status_id",
                table: "order");

            migrationBuilder.DropTable(
                name: "OrderStatuses");

            migrationBuilder.DropIndex(
                name: "IX_order_status_id",
                table: "order");

            migrationBuilder.DropColumn(
                name: "status_id",
                table: "order");

            migrationBuilder.AddColumn<bool>(
                name: "is_completed",
                table: "order",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
