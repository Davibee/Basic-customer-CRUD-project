using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace V3.Migrations
{
    /// <inheritdoc />
    public partial class nametomembershiptypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE MembershipTypes SET name = 'Pay As You Go' WHERE Id = 1;");
            migrationBuilder.Sql("UPDATE MembershipTypes SET name = 'Monthly' WHERE Id = 2;");
            migrationBuilder.Sql("UPDATE MembershipTypes SET name = 'Annual' WHERE Id = 3;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM MembershipTypes WHERE Id IN(1, 2, 3);");
        }
    }
}
