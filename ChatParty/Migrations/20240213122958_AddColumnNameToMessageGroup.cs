using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatParty.Migrations
{
    public partial class AddColumnNameToMessageGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "MessageGroup",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "MessageGroup");
        }
    }
}
