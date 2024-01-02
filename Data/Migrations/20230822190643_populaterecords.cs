using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace V3.Migrations
{
    /// <inheritdoc />
    public partial class populaterecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          
            migrationBuilder.Sql(@"INSERT INTO MembershipTypes (Id, SignUpFee, DurationInMonths, DurationRates)
            VALUES (1, 0, 1, 0);");

            migrationBuilder.Sql(@"INSERT INTO MembershipTypes (Id, SignUpFee, DurationInMonths, DurationRates)
            VALUES (2, 10, 1, 10);");

            migrationBuilder.Sql(@"INSERT INTO MembershipTypes (Id, SignUpFee, DurationInMonths, DurationRates)
            VALUES (3, 25, 12, 10);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM MembershipTypes WHERE Id IN(1, 2, 3);");
        }
    }
}
