using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShoppingCartMvcUI.Migrations
{
    /// <inheritdoc />
    public partial class TopNSellingBooksStoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sql = @"
            EXEC ('CREATE PROCEDURE [dbo].[Usp_GetTopNSellingBooksByDate]
            @startDate datetime,
            @endDate datetime
            AS
            BEGIN
                SET NOCOUNT ON;

                WITH UnitSold AS
                (
                    SELECT od.BookId, SUM(od.Quantity) AS TotalUnitSold 
                    FROM [Order] o 
                    JOIN OrderDetail od ON o.Id = od.OrderId
                    WHERE o.IsPaid = 1 AND o.IsDeleted = 0 AND o.CreateDate BETWEEN @startDate AND @endDate
                    GROUP BY od.BookId
                )
                SELECT TOP 5 b.BookName, b.AuthorName, b.[Image], us.TotalUnitSold 
                FROM UnitSold us
                JOIN [Book] b ON us.BookId = b.Id
                ORDER BY us.TotalUnitSold DESC;
            END');
          ";
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
