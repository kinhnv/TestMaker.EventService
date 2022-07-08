using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestMaker.EventService.Infrastructure.Migrations
{
    public partial class AddSomeColumnsInEventTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Events",
                newName: "ScopeType");

            migrationBuilder.AddColumn<int>(
                name: "QuestionContentType",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CandidateAnswers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionContentType",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CandidateAnswers");

            migrationBuilder.RenameColumn(
                name: "ScopeType",
                table: "Events",
                newName: "Type");
        }
    }
}
