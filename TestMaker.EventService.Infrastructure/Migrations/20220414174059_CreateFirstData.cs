using Microsoft.EntityFrameworkCore.Migrations;

namespace TestMaker.EventService.Infrastructure.Migrations
{
    public partial class CreateFirstData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT [dbo].[Candidates] ([CandidateId], [Code], [Status], [EventId]) VALUES (N'1fcdf5e6-ee54-4ba2-10b4-08d9fcbe77bf', N'WYGADKMW', 1, N'c78b7a38-d471-45e9-80a9-08d9fcbe76a4')");
            migrationBuilder.Sql("INSERT [dbo].[Events] ([EventId], [Name], [Code], [TestId]) VALUES (N'c78b7a38-d471-45e9-80a9-08d9fcbe76a4', N'Sự kiện kiểm tra (test)', N'MM28WLGD', N'181007d1-4a73-4109-c3ac-08d9fcbbc438')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
