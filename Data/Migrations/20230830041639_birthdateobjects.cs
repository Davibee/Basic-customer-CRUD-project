using Microsoft.EntityFrameworkCore.Migrations;
using V3.Models;

#nullable disable

namespace V3.Migrations
{
    /// <inheritdoc />
    public partial class birthdateobjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Customers(Id,name, IsSubcribedTo, MembershipTypeId, Birthdate) VALUES(3,'John Doe', true, 1, '1990-05-15')");
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Customers WHERE Id IN(3)");
        }
    }
}
