using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLDangKyHocPhan.Migrations
{
    /// <inheritdoc />
    public partial class AddDangKyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DangKys",
                columns: table => new
                {
                    MaDangKy = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaSinhVien = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    MaLopHP = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    NgayThucHien = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoaiDangKy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DangKys", x => x.MaDangKy);
                    table.ForeignKey(
                        name: "FK_DangKys_LOPHOCPHAN_MaLopHP",
                        column: x => x.MaLopHP,
                        principalTable: "LOPHOCPHAN",
                        principalColumn: "MaLopHocPhan",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DangKys_Sinhviens_MaSinhVien",
                        column: x => x.MaSinhVien,
                        principalTable: "Sinhviens",
                        principalColumn: "MaSinhVien",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DangKys_MaLopHP",
                table: "DangKys",
                column: "MaLopHP");

            migrationBuilder.CreateIndex(
                name: "IX_DangKys_MaSinhVien",
                table: "DangKys",
                column: "MaSinhVien");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DangKys");
        }
    }
}
