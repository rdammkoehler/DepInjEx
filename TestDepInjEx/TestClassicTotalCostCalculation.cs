using DepInjEx;
using Xunit;

namespace TestDepInjEx
{
    public class TestClassicTotalCostCalculation
    {
        /// <summary>
        /// This doesn't actually run but represents the original integration test
        /// for TotalCost.
        /// </summary>
        [Fact]
        public void TestTotalCost_ReturnsSumOfLineItemsInBOM_OneItem()
        {
            var expected = 10.0m;
            // insert a single line item into the database with a cost of 10.0m
            var bomId = 1; // insert into BOM (createDate) values (default) returning bomId;
            // insert into LineItem (BOMId, desc, itemCost, itemCount, cost) values ($bomId, "2x4",2.50,4, $expected)

            var bomCalculator = new ClassicBOMCalculator();

            var result = bomCalculator.TotalCost(bomId);

            Assert.Equal(expected, result);

            // now clean up
            // delete from LineItem where BOMId == bomId
            // delete from BOM where BOMId == bomId
        }

        // ...more tests with more sql statements and complications...
    }
}