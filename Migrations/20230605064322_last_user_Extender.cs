using Microsoft.EntityFrameworkCore.Migrations;

namespace File_Sharing.Migrations
{
    public partial class last_user_Extender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortName",
                table: "AspNetUsers");
        }
    }
}
