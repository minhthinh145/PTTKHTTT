using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLDangKyHocPhan.Migrations
{
    /// <inheritdoc />
    public partial class addDateOfBirth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "TAIKHOAN",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "TAIKHOAN");
        }
    }
}
