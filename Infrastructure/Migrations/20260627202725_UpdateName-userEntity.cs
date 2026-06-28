using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNameuserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "نام اواتاری",
                schema: "user",
                table: "Users",
                newName: "AvatarName");

            migrationBuilder.RenameColumn(
                name: "MobileNumber",
                schema: "user",
                table: "Users",
                newName: "PhoneNumber");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                schema: "user",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AvatarName",
                schema: "user",
                table: "Users",
                newName: "نام اواتاری");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                schema: "user",
                table: "Users",
                newName: "MobileNumber");

            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                schema: "user",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
