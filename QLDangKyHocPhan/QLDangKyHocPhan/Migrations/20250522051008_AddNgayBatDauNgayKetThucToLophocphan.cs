using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLDangKyHocPhan.Migrations
{
    /// <inheritdoc />
    public partial class AddNgayBatDauNgayKetThucToLophocphan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NgayHoc",
                table: "LOPHOCPHAN");

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayBatDau",
                table: "LOPHOCPHAN",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKetThuc",
                table: "LOPHOCPHAN",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NgayBatDau",
                table: "LOPHOCPHAN");

            migrationBuilder.DropColumn(
                name: "NgayKetThuc",
                table: "LOPHOCPHAN");

            migrationBuilder.AddColumn<string>(
                name: "NgayHoc",
                table: "LOPHOCPHAN",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
