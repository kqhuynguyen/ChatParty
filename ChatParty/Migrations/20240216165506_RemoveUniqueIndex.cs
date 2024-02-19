using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatParty.Migrations
{
    public partial class RemoveUniqueIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Message_FromId_ToId",
                table: "Message");

            migrationBuilder.CreateIndex(
                name: "IX_Message_FromId",
                table: "Message",
                column: "FromId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Message_FromId",
                table: "Message");

            migrationBuilder.CreateIndex(
                name: "IX_Message_FromId_ToId",
                table: "Message",
                columns: new[] { "FromId", "ToId" },
                unique: true);
        }
    }
}
