using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductApi.Migrations
{
    /// <inheritdoc />
    public partial class UserPropertyChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_authenticateUser",
                table: "authenticateUser");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "authenticateUser",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "authenticateUser",
                newName: "Username");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "authenticateUser",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "EmailID",
                table: "authenticateUser",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_authenticateUser",
                table: "authenticateUser",
                column: "EmailID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_authenticateUser",
                table: "authenticateUser");

            migrationBuilder.DropColumn(
                name: "EmailID",
                table: "authenticateUser");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "authenticateUser",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "authenticateUser",
                newName: "password");

            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "authenticateUser",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_authenticateUser",
                table: "authenticateUser",
                column: "username");
        }
    }
}
