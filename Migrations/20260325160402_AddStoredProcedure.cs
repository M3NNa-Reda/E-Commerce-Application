using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce_Application.Migrations
{
    /// <inheritdoc />
    public partial class AddStoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@" CREATE PROCEDURE shop.GetPendingOrders
                        AS
                        BEGIN
                            SELECT *
                            FROM shop.Orders 
                            WHERE Status = 'Pending'
                        END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(@"DROP PROCEDURE shop.GetPendingOrders");
        }
    }
}
