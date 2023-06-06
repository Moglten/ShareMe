using Microsoft.EntityFrameworkCore.Migrations;

namespace File_Sharing.Migrations
{
    public partial class sizebelong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Size",
                table: "Uploads",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Size",
                table: "Uploads",
                type: "real",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
