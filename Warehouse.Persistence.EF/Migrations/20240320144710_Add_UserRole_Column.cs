using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warehouse.Persistence.EF.Migrations
{
    /// <inheritdoc />
    public partial class Add_UserRole_Column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-2321-2321-111111111111"),
                column: "Name",
                value: 1);

            migrationBuilder.UpdateData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: new Guid("22222222-1111-1234-4321-222222222222"),
                column: "Name",
                value: 2);

            migrationBuilder.UpdateData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3322-1122-4444-333333333333"),
                column: "Name",
                value: 3);

            migrationBuilder.UpdateData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: new Guid("44444444-5555-5555-6666-666666666666"),
                column: "Name",
                value: 4);

            migrationBuilder.UpdateData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-8888-888888888888"),
                column: "Name",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-2321-2321-111111111456"),
                column: "Role",
                value: "admin");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Users",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.UpdateData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-2321-2321-111111111111"),
                column: "Name",
                value: 0);

            migrationBuilder.UpdateData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: new Guid("22222222-1111-1234-4321-222222222222"),
                column: "Name",
                value: 1);

            migrationBuilder.UpdateData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3322-1122-4444-333333333333"),
                column: "Name",
                value: 2);

            migrationBuilder.UpdateData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: new Guid("44444444-5555-5555-6666-666666666666"),
                column: "Name",
                value: 3);

            migrationBuilder.UpdateData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-8888-888888888888"),
                column: "Name",
                value: 4);
        }
    }
}