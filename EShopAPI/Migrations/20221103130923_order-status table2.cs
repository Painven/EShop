using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShopAPI.Migrations
{
    public partial class orderstatustable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_OrderStatuses_status_id",
                table: "order");

            migrationBuilder.DropForeignKey(
                name: "FK_product_category_category_id",
                table: "product");

            migrationBuilder.AlterColumn<int>(
                name: "category_id",
                table: "product",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "status_id",
                table: "order",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_order_OrderStatuses_status_id",
                table: "order",
                column: "status_id",
                principalTable: "OrderStatuses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_category_category_id",
                table: "product",
                column: "category_id",
                principalTable: "category",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
