using System.Collections.Generic;
using System.Numerics;
using DepInjEx;
using Xunit;

namespace TestDepInjEx
{
    public class TestTotalCostCalculation
    {
        private class FakeBOMLineItem : IBOMLineItem
        {
            public decimal Cost { get; }

            public FakeBOMLineItem(decimal fakeCost)
            {
                Cost = fakeCost;
            }
        }

        private class FakeBOMFetcher : IBOMFetcher
        {
            private List<IBOMLineItem> fakeData = new List<IBOMLineItem>();

            public FakeBOMFetcher(int recordCount, decimal expectedSum)
            {
                if (expectedSum > 0m)
                {
                    var intermediateValue = expectedSum / recordCount;
                    for (var idx = 0; idx < recordCount; idx++)
                    {
                        fakeData.Add(new FakeBOMLineItem(intermediateValue));
                    }
                }
            }

            public List<IBOMLineItem> LineItems(BigInteger bomId)
            {
                return fakeData;
            }
        }

        [Fact]
        public void TestTotalCost_ReturnsSumOfLineItemsInBOM_OneItem()
        {
            var expectedSum = 10.00m;
            // 1 - Create Test Double
            var fakeBomFetcher = new FakeBOMFetcher(1, expectedSum);
            // 2 - Inject Test Double (Constructor Injection)
            var bomCalculator = new BOMCalculator(fakeBomFetcher);
            var bomId = new BigInteger(1);

            // 3 - Execute Test
            var result = bomCalculator.TotalCost(bomId);

            // 4 - Verify Result
            Assert.Equal(expectedSum, result);
        }

        [Fact]
        public void TestTotalCost_ReturnsSumOfLineItemsInBOM_TenItems()
        {
            var expectedSum = 10.00m;
            // 1 - Create Test Double
            var fakeBomFetcher = new FakeBOMFetcher(10, expectedSum);
            // 2 - Inject Test Double (Constructor Injection)
            var bomCalculator = new BOMCalculator(fakeBomFetcher);
            var bomId = new BigInteger(1);

            // 3 - Execute Test
            var result = bomCalculator.TotalCost(bomId);

            // 4 - Verify Result
            Assert.Equal(expectedSum, result);
        }

        [Fact]
        public void TestTotalCost_ReturnsSumOfLineItemsInBOM_ZeroItems()
        {
            var expectedSum = 0m;
            // 1 - Create Test Double
            var fakeBomFetcher = new FakeBOMFetcher(0, expectedSum);
            // 2 - Inject Test Double (Constructor Injection)
            var bomCalculator = new BOMCalculator(fakeBomFetcher);
            var bomId = new BigInteger(1);

            // 3 - Execute Test
            var result = bomCalculator.TotalCost(bomId);

            // 4 - Verify Result
            Assert.Equal(expectedSum, result);
        }
    }
}