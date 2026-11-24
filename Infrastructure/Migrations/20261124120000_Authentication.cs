using System;
using Infrastructure.Persistent.EF;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
namespace Infrastructure.Migrations;

[DbContext(typeof(ShahriarBookStoreContext))]
[Migration("20261124120000_Authentication")]
public partial class Authentication : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(name: "PasswordHash", schema: "user", table: "Users", type: "nvarchar(max)", nullable: true, oldClrType: typeof(string), oldType: "nvarchar(max)");
        migrationBuilder.AlterColumn<string>(name: "PasswordSalt", schema: "user", table: "Users", type: "nvarchar(max)", nullable: true, oldClrType: typeof(string), oldType: "nvarchar(max)");
        migrationBuilder.CreateIndex(name: "IX_Users_PhoneNumber", schema: "user", table: "Users", column: "PhoneNumber", unique: true);
        migrationBuilder.CreateTable(name: "OtpChallenges", schema: "user", columns: table => new
        {
            PhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
            CodeHash = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
            ExpiresAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
            LastSentAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
            Attempts = table.Column<int>(type: "int", nullable: false),
            Consumed = table.Column<bool>(type: "bit", nullable: false)
        }, constraints: table => table.PrimaryKey("PK_OtpChallenges", x => x.PhoneNumber));
        migrationBuilder.CreateTable(name: "Tokens", schema: "user", columns: table => new
        {
            UserId = table.Column<long>(type: "bigint", nullable: false),
            Id = table.Column<long>(type: "bigint", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
            CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            HashJwtToken = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
            HashRefreshToken = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
            TokenExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            RefreshTokenExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            Device = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
        }, constraints: table =>
        {
            table.PrimaryKey("PK_Tokens", x => new { x.UserId, x.Id });
            table.ForeignKey(name: "FK_Tokens_Users_UserId", column: x => x.UserId, principalSchema: "user", principalTable: "Users", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
        });
    }
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "OtpChallenges", schema: "user");
        migrationBuilder.DropTable(name: "Tokens", schema: "user");
        migrationBuilder.DropIndex(name: "IX_Users_PhoneNumber", schema: "user", table: "Users");
        // Passwordless accounts must be migrated or removed before rollback.
        migrationBuilder.AlterColumn<string>(name: "PasswordHash", schema: "user", table: "Users", type: "nvarchar(max)", nullable: false, oldClrType: typeof(string), oldType: "nvarchar(max)", oldNullable: true);
        migrationBuilder.AlterColumn<string>(name: "PasswordSalt", schema: "user", table: "Users", type: "nvarchar(max)", nullable: false, oldClrType: typeof(string), oldType: "nvarchar(max)", oldNullable: true);
    }
}
