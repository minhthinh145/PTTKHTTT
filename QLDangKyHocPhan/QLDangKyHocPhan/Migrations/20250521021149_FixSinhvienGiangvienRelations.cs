using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLDangKyHocPhan.Migrations
{
    public partial class FixSinhvienGiangvienRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Giangviens_TAIKHOAN_TaiKhoanDangKy",
                table: "Giangviens");

            migrationBuilder.DropIndex(
                name: "IX_Sinhviens_TaiKhoanId",
                table: "Sinhviens");

            migrationBuilder.DropIndex(
                name: "IX_Giangviens_TaiKhoanDangKy",
                table: "Giangviens");

            migrationBuilder.RenameColumn(
                name: "TaiKhoanDangKy",
                table: "Giangviens",
                newName: "TaiKhoanId");

            // Bỏ đoạn thay đổi cột MaSinhVien để tránh lỗi:
            // migrationBuilder.AlterColumn<string>(
            //     name: "MaSinhVien",
            //     table: "Sinhviens",
            //     type: "nvarchar(20)",
            //     maxLength: 20,
            //     nullable: false,
            //     oldClrType: typeof(string),
            //     oldType: "nvarchar(10)",
            //     oldMaxLength: 10);

            // migrationBuilder.AlterColumn<string>(
            //     name: "MaSinhVien",
            //     table: "DangKys",
            //     type: "nvarchar(20)",
            //     nullable: false,
            //     oldClrType: typeof(string),
            //     oldType: "nvarchar(10)");

            migrationBuilder.CreateIndex(
                name: "IX_Sinhviens_TaiKhoanId",
                table: "Sinhviens",
                column: "TaiKhoanId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Giangviens_TaiKhoanId",
                table: "Giangviens",
                column: "TaiKhoanId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Giangviens_TAIKHOAN_TaiKhoanId",
                table: "Giangviens",
                column: "TaiKhoanId",
                principalTable: "TAIKHOAN",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Giangviens_TAIKHOAN_TaiKhoanId",
                table: "Giangviens");

            migrationBuilder.DropIndex(
                name: "IX_Sinhviens_TaiKhoanId",
                table: "Sinhviens");

            migrationBuilder.DropIndex(
                name: "IX_Giangviens_TaiKhoanId",
                table: "Giangviens");

            migrationBuilder.RenameColumn(
                name: "TaiKhoanId",
                table: "Giangviens",
                newName: "TaiKhoanDangKy");

            // Bỏ đoạn thay đổi cột MaSinhVien để tránh lỗi:
            // migrationBuilder.AlterColumn<string>(
            //     name: "MaSinhVien",
            //     table: "Sinhviens",
            //     type: "nvarchar(10)",
            //     maxLength: 10,
            //     nullable: false,
            //     oldClrType: typeof(string),
            //     oldType: "nvarchar(20)",
            //     oldMaxLength: 20);

            // migrationBuilder.AlterColumn<string>(
            //     name: "MaSinhVien",
            //     table: "DangKys",
            //     type: "nvarchar(10)",
            //     nullable: false,
            //     oldClrType: typeof(string),
            //     oldType: "nvarchar(20)");

            migrationBuilder.CreateIndex(
                name: "IX_Sinhviens_TaiKhoanId",
                table: "Sinhviens",
                column: "TaiKhoanId");

            migrationBuilder.CreateIndex(
                name: "IX_Giangviens_TaiKhoanDangKy",
                table: "Giangviens",
                column: "TaiKhoanDangKy");

            migrationBuilder.AddForeignKey(
                name: "FK_Giangviens_TAIKHOAN_TaiKhoanDangKy",
                table: "Giangviens",
                column: "TaiKhoanDangKy",
                principalTable: "TAIKHOAN",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
