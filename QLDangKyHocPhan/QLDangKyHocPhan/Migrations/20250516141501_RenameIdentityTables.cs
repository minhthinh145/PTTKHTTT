using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLDangKyHocPhan.Migrations
{
    /// <inheritdoc />
    public partial class RenameIdentityTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Giangviens_AspNetUsers_TaiKhoanDangKy",
                table: "Giangviens");

            migrationBuilder.DropForeignKey(
                name: "FK_Sinhviens_AspNetUsers_TaiKhoanId",
                table: "Sinhviens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "TAIKHOAN_TOKEN");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "TAIKHOAN");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "TAIKHOAN_VAI_TRO_MAP");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "TAIKHOAN_LOGIN");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "TAIKHOAN_CLAIM");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "TAIKHOAN_VAI_TRO");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "TAIKHOAN_VAI_TRO_CLAIM");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "TAIKHOAN_VAI_TRO_MAP",
                newName: "IX_TAIKHOAN_VAI_TRO_MAP_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "TAIKHOAN_LOGIN",
                newName: "IX_TAIKHOAN_LOGIN_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "TAIKHOAN_CLAIM",
                newName: "IX_TAIKHOAN_CLAIM_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "TAIKHOAN_VAI_TRO_CLAIM",
                newName: "IX_TAIKHOAN_VAI_TRO_CLAIM_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TAIKHOAN_TOKEN",
                table: "TAIKHOAN_TOKEN",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TAIKHOAN",
                table: "TAIKHOAN",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TAIKHOAN_VAI_TRO_MAP",
                table: "TAIKHOAN_VAI_TRO_MAP",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TAIKHOAN_LOGIN",
                table: "TAIKHOAN_LOGIN",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TAIKHOAN_CLAIM",
                table: "TAIKHOAN_CLAIM",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TAIKHOAN_VAI_TRO",
                table: "TAIKHOAN_VAI_TRO",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TAIKHOAN_VAI_TRO_CLAIM",
                table: "TAIKHOAN_VAI_TRO_CLAIM",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Giangviens_TAIKHOAN_TaiKhoanDangKy",
                table: "Giangviens",
                column: "TaiKhoanDangKy",
                principalTable: "TAIKHOAN",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sinhviens_TAIKHOAN_TaiKhoanId",
                table: "Sinhviens",
                column: "TaiKhoanId",
                principalTable: "TAIKHOAN",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TAIKHOAN_CLAIM_TAIKHOAN_UserId",
                table: "TAIKHOAN_CLAIM",
                column: "UserId",
                principalTable: "TAIKHOAN",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TAIKHOAN_LOGIN_TAIKHOAN_UserId",
                table: "TAIKHOAN_LOGIN",
                column: "UserId",
                principalTable: "TAIKHOAN",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TAIKHOAN_TOKEN_TAIKHOAN_UserId",
                table: "TAIKHOAN_TOKEN",
                column: "UserId",
                principalTable: "TAIKHOAN",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TAIKHOAN_VAI_TRO_CLAIM_TAIKHOAN_VAI_TRO_RoleId",
                table: "TAIKHOAN_VAI_TRO_CLAIM",
                column: "RoleId",
                principalTable: "TAIKHOAN_VAI_TRO",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TAIKHOAN_VAI_TRO_MAP_TAIKHOAN_UserId",
                table: "TAIKHOAN_VAI_TRO_MAP",
                column: "UserId",
                principalTable: "TAIKHOAN",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TAIKHOAN_VAI_TRO_MAP_TAIKHOAN_VAI_TRO_RoleId",
                table: "TAIKHOAN_VAI_TRO_MAP",
                column: "RoleId",
                principalTable: "TAIKHOAN_VAI_TRO",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Giangviens_TAIKHOAN_TaiKhoanDangKy",
                table: "Giangviens");

            migrationBuilder.DropForeignKey(
                name: "FK_Sinhviens_TAIKHOAN_TaiKhoanId",
                table: "Sinhviens");

            migrationBuilder.DropForeignKey(
                name: "FK_TAIKHOAN_CLAIM_TAIKHOAN_UserId",
                table: "TAIKHOAN_CLAIM");

            migrationBuilder.DropForeignKey(
                name: "FK_TAIKHOAN_LOGIN_TAIKHOAN_UserId",
                table: "TAIKHOAN_LOGIN");

            migrationBuilder.DropForeignKey(
                name: "FK_TAIKHOAN_TOKEN_TAIKHOAN_UserId",
                table: "TAIKHOAN_TOKEN");

            migrationBuilder.DropForeignKey(
                name: "FK_TAIKHOAN_VAI_TRO_CLAIM_TAIKHOAN_VAI_TRO_RoleId",
                table: "TAIKHOAN_VAI_TRO_CLAIM");

            migrationBuilder.DropForeignKey(
                name: "FK_TAIKHOAN_VAI_TRO_MAP_TAIKHOAN_UserId",
                table: "TAIKHOAN_VAI_TRO_MAP");

            migrationBuilder.DropForeignKey(
                name: "FK_TAIKHOAN_VAI_TRO_MAP_TAIKHOAN_VAI_TRO_RoleId",
                table: "TAIKHOAN_VAI_TRO_MAP");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TAIKHOAN_VAI_TRO_MAP",
                table: "TAIKHOAN_VAI_TRO_MAP");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TAIKHOAN_VAI_TRO_CLAIM",
                table: "TAIKHOAN_VAI_TRO_CLAIM");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TAIKHOAN_VAI_TRO",
                table: "TAIKHOAN_VAI_TRO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TAIKHOAN_TOKEN",
                table: "TAIKHOAN_TOKEN");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TAIKHOAN_LOGIN",
                table: "TAIKHOAN_LOGIN");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TAIKHOAN_CLAIM",
                table: "TAIKHOAN_CLAIM");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TAIKHOAN",
                table: "TAIKHOAN");

            migrationBuilder.RenameTable(
                name: "TAIKHOAN_VAI_TRO_MAP",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "TAIKHOAN_VAI_TRO_CLAIM",
                newName: "AspNetRoleClaims");

            migrationBuilder.RenameTable(
                name: "TAIKHOAN_VAI_TRO",
                newName: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "TAIKHOAN_TOKEN",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "TAIKHOAN_LOGIN",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "TAIKHOAN_CLAIM",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "TAIKHOAN",
                newName: "AspNetUsers");

            migrationBuilder.RenameIndex(
                name: "IX_TAIKHOAN_VAI_TRO_MAP_RoleId",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_TAIKHOAN_VAI_TRO_CLAIM_RoleId",
                table: "AspNetRoleClaims",
                newName: "IX_AspNetRoleClaims_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_TAIKHOAN_LOGIN_UserId",
                table: "AspNetUserLogins",
                newName: "IX_AspNetUserLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TAIKHOAN_CLAIM_UserId",
                table: "AspNetUserClaims",
                newName: "IX_AspNetUserClaims_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Giangviens_AspNetUsers_TaiKhoanDangKy",
                table: "Giangviens",
                column: "TaiKhoanDangKy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sinhviens_AspNetUsers_TaiKhoanId",
                table: "Sinhviens",
                column: "TaiKhoanId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
