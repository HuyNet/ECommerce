using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "updateDate",
                table: "Products",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<long>(
                name: "FileSize",
                table: "ProductImages",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("30658b70-9f61-40b8-a6a8-c0e6ef85b2d7"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "cd0e44ad-4291-47cc-bca2-159f7cd92351", "AQAAAAIAAYagAAAAEAYj1DLsyvJw3a4eUA2z1CnEIAHgXDjDReggrBHUZOu4oC4Rrxfkj9gXxkan0LNYmQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "updateDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FileSize",
                table: "ProductImages",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("30658b70-9f61-40b8-a6a8-c0e6ef85b2d7"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "415653e4-ed26-4f44-92e9-a00b5e5a9314", "AQAAAAIAAYagAAAAEEGu4VEg4mJ7TZlKjJpDd+sDMxr/YYl3fIMsnE+UDgn1Wc3gpRptD1jasLndkrWoiA==" });
        }
    }
}
