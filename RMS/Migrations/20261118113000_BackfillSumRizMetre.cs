using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <summary>
    /// Initializes and repairs the persisted aggregate for every existing FB.
    /// </summary>
    public partial class BackfillSumRizMetre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF COL_LENGTH(N'dbo.tblFB', N'SumRizMetre') IS NULL
BEGIN
    ALTER TABLE dbo.tblFB
        ADD SumRizMetre decimal(18,4) NULL;
END;

UPDATE fb
SET fb.SumRizMetre = COALESCE(
    totals.SumMeghdarJoz,
    CONVERT(decimal(18,4), 0)
)
FROM dbo.tblFB AS fb
OUTER APPLY
(
    SELECT SUM(rm.MeghdarJoz) AS SumMeghdarJoz
    FROM dbo.tblRizMetreUsers AS rm
    WHERE rm.FBId = fb.ID
      AND ISNULL(rm.IsDeleted, 0) = 0
) AS totals;
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // This is a data-repair migration. Reverting it would restore stale or
            // uninitialized aggregates, so the persisted values are intentionally kept.
        }
    }
}
