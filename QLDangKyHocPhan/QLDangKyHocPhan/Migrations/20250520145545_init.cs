using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLDangKyHocPhan.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KHOA",
                columns: table => new
                {
                    MaKhoa = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TenKhoa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__KHOA__65390405BDF59709", x => x.MaKhoa);
                });

            migrationBuilder.CreateTable(
                name: "TAIKHOAN",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenDangNhap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoaiTaiKhoan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAIKHOAN", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TAIKHOAN_VAI_TRO",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAIKHOAN_VAI_TRO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CTDAOTAO",
                columns: table => new
                {
                    MaCT = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MaKhoa = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    NamHoc = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTDAOTAO", x => x.MaCT);
                    table.ForeignKey(
                        name: "FK_CTDAOTAO_KHOA_MaKhoa",
                        column: x => x.MaKhoa,
                        principalTable: "KHOA",
                        principalColumn: "MaKhoa",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HOCPHAN",
                columns: table => new
                {
                    MaHocPhan = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TenHocPhan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SoTC = table.Column<int>(type: "int", nullable: true),
                    DKTienQuyet = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MaKhoa = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    HocKy = table.Column<int>(type: "int", nullable: true),
                    LoaiHocPhan = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__HOCPHAN__9A13F25E14D4648A", x => x.MaHocPhan);
                    table.ForeignKey(
                        name: "FK_HOCPHAN_KHOA_MaKhoa",
                        column: x => x.MaKhoa,
                        principalTable: "KHOA",
                        principalColumn: "MaKhoa",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Giangviens",
                columns: table => new
                {
                    MaGiangVien = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LopHoc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TaiKhoanDangKy = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Giangviens", x => x.MaGiangVien);
                    table.ForeignKey(
                        name: "FK_Giangviens_TAIKHOAN_TaiKhoanDangKy",
                        column: x => x.TaiKhoanDangKy,
                        principalTable: "TAIKHOAN",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Revoked = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_TAIKHOAN_UserId",
                        column: x => x.UserId,
                        principalTable: "TAIKHOAN",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sinhviens",
                columns: table => new
                {
                    MaSinhVien = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GioiTinh = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MaKhoa = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TaiKhoanId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sinhviens", x => x.MaSinhVien);
                    table.ForeignKey(
                        name: "FK_Sinhviens_KHOA_MaKhoa",
                        column: x => x.MaKhoa,
                        principalTable: "KHOA",
                        principalColumn: "MaKhoa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sinhviens_TAIKHOAN_TaiKhoanId",
                        column: x => x.TaiKhoanId,
                        principalTable: "TAIKHOAN",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TAIKHOAN_CLAIM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAIKHOAN_CLAIM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TAIKHOAN_CLAIM_TAIKHOAN_UserId",
                        column: x => x.UserId,
                        principalTable: "TAIKHOAN",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TAIKHOAN_LOGIN",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAIKHOAN_LOGIN", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_TAIKHOAN_LOGIN_TAIKHOAN_UserId",
                        column: x => x.UserId,
                        principalTable: "TAIKHOAN",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TAIKHOAN_TOKEN",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAIKHOAN_TOKEN", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_TAIKHOAN_TOKEN_TAIKHOAN_UserId",
                        column: x => x.UserId,
                        principalTable: "TAIKHOAN",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TAIKHOAN_VAI_TRO_CLAIM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAIKHOAN_VAI_TRO_CLAIM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TAIKHOAN_VAI_TRO_CLAIM_TAIKHOAN_VAI_TRO_RoleId",
                        column: x => x.RoleId,
                        principalTable: "TAIKHOAN_VAI_TRO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TAIKHOAN_VAI_TRO_MAP",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAIKHOAN_VAI_TRO_MAP", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_TAIKHOAN_VAI_TRO_MAP_TAIKHOAN_UserId",
                        column: x => x.UserId,
                        principalTable: "TAIKHOAN",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TAIKHOAN_VAI_TRO_MAP_TAIKHOAN_VAI_TRO_RoleId",
                        column: x => x.RoleId,
                        principalTable: "TAIKHOAN_VAI_TRO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CHITIET_CTDT",
                columns: table => new
                {
                    MaCT_CTDT = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MaCT = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MaHocPhan = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CHITIET_CTDT", x => x.MaCT_CTDT);
                    table.ForeignKey(
                        name: "FK_CHITIET_CTDT_CTDAOTAO_MaCT",
                        column: x => x.MaCT,
                        principalTable: "CTDAOTAO",
                        principalColumn: "MaCT",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CHITIET_CTDT_HOCPHAN_MaHocPhan",
                        column: x => x.MaHocPhan,
                        principalTable: "HOCPHAN",
                        principalColumn: "MaHocPhan",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LOPHOCPHAN",
                columns: table => new
                {
                    MaLopHocPhan = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PhongHoc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NgayHoc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SoLuong = table.Column<int>(type: "int", nullable: true),
                    MaHocPhan = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    MaGiangVien = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    MaHocPhanNavigationMaHocPhan = table.Column<string>(type: "nvarchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LOPHOCPH__82581CD9FD3C38B2", x => x.MaLopHocPhan);
                    table.ForeignKey(
                        name: "FK_LOPHOCPHAN_HOCPHAN_MaHocPhan",
                        column: x => x.MaHocPhan,
                        principalTable: "HOCPHAN",
                        principalColumn: "MaHocPhan");
                    table.ForeignKey(
                        name: "FK_LOPHOCPHAN_HOCPHAN_MaHocPhanNavigationMaHocPhan",
                        column: x => x.MaHocPhanNavigationMaHocPhan,
                        principalTable: "HOCPHAN",
                        principalColumn: "MaHocPhan");
                    table.ForeignKey(
                        name: "FK__LOPHOCPHA__MaGia__5BE2A6F2",
                        column: x => x.MaGiangVien,
                        principalTable: "Giangviens",
                        principalColumn: "MaGiangVien");
                });

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
                name: "IX_CHITIET_CTDT_MaCT",
                table: "CHITIET_CTDT",
                column: "MaCT");

            migrationBuilder.CreateIndex(
                name: "IX_CHITIET_CTDT_MaHocPhan",
                table: "CHITIET_CTDT",
                column: "MaHocPhan");

            migrationBuilder.CreateIndex(
                name: "IX_CTDAOTAO_MaKhoa",
                table: "CTDAOTAO",
                column: "MaKhoa");

            migrationBuilder.CreateIndex(
                name: "IX_DangKys_MaLopHP",
                table: "DangKys",
                column: "MaLopHP");

            migrationBuilder.CreateIndex(
                name: "IX_DangKys_MaSinhVien",
                table: "DangKys",
                column: "MaSinhVien");

            migrationBuilder.CreateIndex(
                name: "IX_Giangviens_TaiKhoanDangKy",
                table: "Giangviens",
                column: "TaiKhoanDangKy");

            migrationBuilder.CreateIndex(
                name: "IX_HOCPHAN_MaKhoa",
                table: "HOCPHAN",
                column: "MaKhoa");

            migrationBuilder.CreateIndex(
                name: "IX_LOPHOCPHAN_MaGiangVien",
                table: "LOPHOCPHAN",
                column: "MaGiangVien");

            migrationBuilder.CreateIndex(
                name: "IX_LOPHOCPHAN_MaHocPhan",
                table: "LOPHOCPHAN",
                column: "MaHocPhan");

            migrationBuilder.CreateIndex(
                name: "IX_LOPHOCPHAN_MaHocPhanNavigationMaHocPhan",
                table: "LOPHOCPHAN",
                column: "MaHocPhanNavigationMaHocPhan");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_Token",
                table: "RefreshTokens",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Sinhviens_MaKhoa",
                table: "Sinhviens",
                column: "MaKhoa");

            migrationBuilder.CreateIndex(
                name: "IX_Sinhviens_TaiKhoanId",
                table: "Sinhviens",
                column: "TaiKhoanId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "TAIKHOAN",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "TAIKHOAN",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TAIKHOAN_CLAIM_UserId",
                table: "TAIKHOAN_CLAIM",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TAIKHOAN_LOGIN_UserId",
                table: "TAIKHOAN_LOGIN",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "TAIKHOAN_VAI_TRO",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TAIKHOAN_VAI_TRO_CLAIM_RoleId",
                table: "TAIKHOAN_VAI_TRO_CLAIM",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TAIKHOAN_VAI_TRO_MAP_RoleId",
                table: "TAIKHOAN_VAI_TRO_MAP",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CHITIET_CTDT");

            migrationBuilder.DropTable(
                name: "DangKys");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "TAIKHOAN_CLAIM");

            migrationBuilder.DropTable(
                name: "TAIKHOAN_LOGIN");

            migrationBuilder.DropTable(
                name: "TAIKHOAN_TOKEN");

            migrationBuilder.DropTable(
                name: "TAIKHOAN_VAI_TRO_CLAIM");

            migrationBuilder.DropTable(
                name: "TAIKHOAN_VAI_TRO_MAP");

            migrationBuilder.DropTable(
                name: "CTDAOTAO");

            migrationBuilder.DropTable(
                name: "LOPHOCPHAN");

            migrationBuilder.DropTable(
                name: "Sinhviens");

            migrationBuilder.DropTable(
                name: "TAIKHOAN_VAI_TRO");

            migrationBuilder.DropTable(
                name: "HOCPHAN");

            migrationBuilder.DropTable(
                name: "Giangviens");

            migrationBuilder.DropTable(
                name: "KHOA");

            migrationBuilder.DropTable(
                name: "TAIKHOAN");
        }
    }
}
