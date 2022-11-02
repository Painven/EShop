using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShopAPI.Migrations
{
    public partial class ordertable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_line_order_Orderid",
                table: "order_line");

            migrationBuilder.DropForeignKey(
                name: "FK_product_category_Categoryid",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_product_Categoryid",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_order_line_Orderid",
                table: "order_line");

            migrationBuilder.DropColumn(
                name: "Categoryid",
                table: "product");

            migrationBuilder.DropColumn(
                name: "Orderid",
                table: "order_line");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "product",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "order",
                newName: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_category_id",
                table: "product",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_line_order_id",
                table: "order_line",
                column: "order_id");

            migrationBuilder.AddForeignKey(
                name: "FK_order_line_order_order_id",
                table: "order_line",
                column: "order_id",
                principalTable: "order",
                principalColumn: "order_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_category_category_id",
                table: "product",
                column: "category_id",
                principalTable: "category",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_line_order_order_id",
                table: "order_line");

            migrationBuilder.DropForeignKey(
                name: "FK_product_category_category_id",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_product_category_id",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_order_line_order_id",
                table: "order_line");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "product",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "order_id",
                table: "order",
                newName: "id");

            migrationBuilder.AddColumn<int>(
                name: "Categoryid",
                table: "product",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Orderid",
                table: "order_line",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_product_Categoryid",
                table: "product",
                column: "Categoryid");

            migrationBuilder.CreateIndex(
                name: "IX_order_line_Orderid",
                table: "order_line",
                column: "Orderid");

            migrationBuilder.AddForeignKey(
                name: "FK_order_line_order_Orderid",
                table: "order_line",
                column: "Orderid",
                principalTable: "order",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_category_Categoryid",
                table: "product",
                column: "Categoryid",
                principalTable: "category",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
