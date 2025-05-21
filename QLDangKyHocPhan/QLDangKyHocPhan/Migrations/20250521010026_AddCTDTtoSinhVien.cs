using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLDangKyHocPhan.Migrations
{
    /// <inheritdoc />
    public partial class AddCTDTtoSinhVien : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaCT",
                table: "Sinhviens",
                type: "nvarchar(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Sinhviens_MaCT",
                table: "Sinhviens",
                column: "MaCT");

            migrationBuilder.AddForeignKey(
                name: "FK_Sinhviens_CTDAOTAO_MaCT",
                table: "Sinhviens",
                column: "MaCT",
                principalTable: "CTDAOTAO",
                principalColumn: "MaCT",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sinhviens_CTDAOTAO_MaCT",
                table: "Sinhviens");

            migrationBuilder.DropIndex(
                name: "IX_Sinhviens_MaCT",
                table: "Sinhviens");

            migrationBuilder.DropColumn(
                name: "MaCT",
                table: "Sinhviens");
        }
    }
}
