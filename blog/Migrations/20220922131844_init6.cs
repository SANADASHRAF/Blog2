using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blog.Migrations
{
    public partial class init6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_news_customer_customerID",
                table: "news");

            migrationBuilder.AlterColumn<int>(
                name: "customerID",
                table: "news",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_news_customer_customerID",
                table: "news",
                column: "customerID",
                principalTable: "customer",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_news_customer_customerID",
                table: "news");

            migrationBuilder.AlterColumn<int>(
                name: "customerID",
                table: "news",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_news_customer_customerID",
                table: "news",
                column: "customerID",
                principalTable: "customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
