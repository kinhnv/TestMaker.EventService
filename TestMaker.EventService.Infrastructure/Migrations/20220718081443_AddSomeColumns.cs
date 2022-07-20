using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestMaker.EventService.Infrastructure.Migrations
{
    public partial class AddSomeColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MarkingType",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "CandidateAnswers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarkingType",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "CandidateAnswers");
        }
    }
}
