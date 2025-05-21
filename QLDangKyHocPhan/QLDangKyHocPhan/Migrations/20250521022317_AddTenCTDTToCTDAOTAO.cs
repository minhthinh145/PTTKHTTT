using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLDangKyHocPhan.Migrations
{
    /// <inheritdoc />
    public partial class AddTenCTDTToCTDAOTAO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TenCTDT",
                table: "CTDAOTAO",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenCTDT",
                table: "CTDAOTAO");
        }
    }
}
