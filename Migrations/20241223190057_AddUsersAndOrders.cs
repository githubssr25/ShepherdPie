using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShepherdPie.Migrations
{
    /// <inheritdoc />
    public partial class AddUsersAndOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OrderStatus",
                table: "Orders",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1", 0, "a95319a3-6bff-4087-9958-4707bb936982", "johndoe@example.com", false, "John", "Doe", false, null, "JOHNDOE@EXAMPLE.COM", "JOHNDOE", "AQAAAAIAAYagAAAAEGwWZZErNlBxXJ1NNiYZW/J6DjR/7VXDm21IvJYB0aA==", null, false, "394dc79d-e869-4bcb-95a4-c57bec05ffd2", false, "johndoe" },
                    { "2", 0, "4d6444eb-85fe-469e-9113-638d3fad3c1d", "janesmith@example.com", false, "Jane", "Smith", false, null, "JANESMITH@EXAMPLE.COM", "JANESMITH", "AQAAAAIAAYagAAAAEGwWZZErNlBxXJ1NNiYZW/J6DjR/7VXDm21IvJYB0aA==", null, false, "3fe30ea1-47bb-4f04-a7f1-78a0d3f585fa", false, "janesmith" },
                    { "3", 0, "72149967-f0d8-4079-a853-80ea49bfa37a", "alicebrown@example.com", false, "Alice", "Brown", false, null, "ALICEBROWN@EXAMPLE.COM", "ALICEBROWN", "AQAAAAIAAYagAAAAEGwWZZErNlBxXJ1NNiYZW/J6DjR/7VXDm21IvJYB0aA==", null, false, "9dc1342d-1b93-4f72-8328-2f280c10df6c", false, "alicebrown" },
                    { "4", 0, "bd0c5065-cc07-4c6f-957b-b348c1e245da", "bobjohnson@example.com", false, "Bob", "Johnson", false, null, "BOBJOHNSON@EXAMPLE.COM", "BOBJOHNSON", "AQAAAAIAAYagAAAAEGwWZZErNlBxXJ1NNiYZW/J6DjR/7VXDm21IvJYB0aA==", null, false, "09db7bbc-38f6-4621-8732-d4cfca440ea8", false, "bobjohnson" }
                });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "UserId",
                value: "2");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2,
                column: "UserId",
                value: "4");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 3,
                column: "UserId",
                value: "3");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 4,
                column: "UserId",
                value: "1");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 5,
                column: "UserId",
                value: "2");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 6,
                column: "UserId",
                value: "4");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 7,
                column: "UserId",
                value: "2");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserId",
                table: "Orders");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "OrderStatus",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
