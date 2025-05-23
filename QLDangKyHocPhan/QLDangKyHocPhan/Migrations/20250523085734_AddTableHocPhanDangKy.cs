using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLDangKyHocPhan.Migrations
{
    public partial class AddTableHocPhanDangKy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HocPhanDangKys",
                columns: table => new
                {
                    MaHP_DangKy = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaSinhVien = table.Column<string>(type: "varchar(20)", nullable: false),
                    MaLopHocPhan = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocPhanDangKys", x => x.MaHP_DangKy);
                    table.ForeignKey(
                        name: "FK_HocPhanDangKys_LOPHOCPHAN_MaLopHocPhan",
                        column: x => x.MaLopHocPhan,
                        principalTable: "LOPHOCPHAN",
                        principalColumn: "MaLopHocPhan",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HocPhanDangKys_Sinhviens_MaSinhVien",
                        column: x => x.MaSinhVien,
                        principalTable: "Sinhviens",
                        principalColumn: "MaSinhVien",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HocPhanDangKys_MaLopHocPhan",
                table: "HocPhanDangKys",
                column: "MaLopHocPhan");

            migrationBuilder.CreateIndex(
                name: "IX_HocPhanDangKys_MaSinhVien",
                table: "HocPhanDangKys",
                column: "MaSinhVien");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HocPhanDangKys");
        }
    }
}
