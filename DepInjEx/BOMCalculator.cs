using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace DepInjEx
{
    /// <summary>
    /// Represents a single line item on a BOM
    /// </summary>
    public interface IBOMLineItem
    {
        decimal Cost { get; }
    }

    /// <summary>
    /// Fetches BOM Data from the database using the BOM Id
    /// </summary>
    public interface IBOMFetcher
    {
        List<IBOMLineItem> LineItems(BigInteger bomId);
    }

    /// <summary>
    /// An implementation of IBOMFetcher that connects to the database to fetch LineItems from a BOM
    ///
    /// This is a stub for demo purposes, actual code has been omitted.
    /// </summary>
    public class DBBOMFetcher : IBOMFetcher
    {
        public List<IBOMLineItem> LineItems(BigInteger bomId)
        {
            throw new System.NotImplementedException();
        }
    }

    /// <summary>
    /// Calculates things about a BOM from the database
    /// </summary>
    public class BOMCalculator
    {
        private IBOMFetcher _bomFetcher;

        /// <summary>
        /// Create a BOM Calculator that can be used to calculate the Cost of BOM
        /// </summary>
        /// <param name="bomFetcher">An Object to fetch data from the database</param>
        public BOMCalculator(IBOMFetcher bomFetcher)
        {
            _bomFetcher = bomFetcher;
        }

        /// <summary>
        /// Get the TotalCost of the BOM Line Items from the database
        /// </summary>
        /// <param name="bomId">The Unique ID of the BOM in the database</param>
        /// <returns>The Sum of LineItem Costs</returns>
        public decimal TotalCost(BigInteger bomId)
        {
            return _bomFetcher.LineItems(bomId).Sum(lineItem => lineItem.Cost);
        }
    }
}