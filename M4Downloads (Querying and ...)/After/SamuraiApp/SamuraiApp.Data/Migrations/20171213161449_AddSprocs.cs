using Microsoft.EntityFrameworkCore.Migrations;

namespace SamuraiApp.Data.Migrations
{
    public partial class AddSprocs : Migration {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                        @"CREATE PROCEDURE FilterSamuraiByNamePart
              @namepart varchar(50) 
              AS
              select * from samurais where name like '%'+@namepart+'%'");
            migrationBuilder.Sql(
              @"create procedure FindLongestName
              @procResult varchar(50) OUTPUT
              AS
              BEGIN
              SET NOCOUNT ON;
              select top 1 @procResult= name from samurais order by len(name) desc
              END"
            );
        }
        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.Sql("DROP PROCEDURE FindLongestName");
            migrationBuilder.Sql("drop procedure FilterSamuraiByNamePart");

        }
    }
}
