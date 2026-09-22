using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialApp.Migrations
{
    /// <inheritdoc />
    public partial class Added_Post_isPrivateFlag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsProvate",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsProvate",
                table: "Posts");
        }
    }
}
