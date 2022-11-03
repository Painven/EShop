using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShopAPI.Migrations
{
    public partial class orderstatustable4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_order_status_status_id",
                table: "order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_order_status",
                table: "order_status");

            migrationBuilder.RenameTable(
                name: "order_status",
                newName: "OrderStatuses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderStatuses",
                table: "OrderStatuses",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_order_OrderStatuses_status_id",
                table: "order",
                column: "status_id",
                principalTable: "OrderStatuses",
                principalColumn: "id");
        }
    }
}
