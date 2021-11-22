using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Numerics;

namespace DepInjEx
{
    /// <summary>
    /// Represents a single line item on a BOM
    /// </summary>
    class BOMLineItem
    {
        private decimal _cost;

        public BOMLineItem(decimal cost)
        {
            _cost = cost;
        }

        public decimal Cost
        {
            get => _cost;
        }
    }

    /// <summary>
    /// Fetches BOM Data from the database using the BOM Id
    /// </summary>
    class BOMFetcher
    {
        public List<BOMLineItem> LineItems(BigInteger bomId)
        {
            string constr;
            SqlConnection conn;

            constr = @"Data Source=MyDatabase;Initial Catalog=DepInjEx;User ID=sa;Password=password";

            conn = new SqlConnection(constr);

            conn.Open();
            SqlCommand cmd;
            SqlDataReader dataReader;
            string sql, output = "";
            sql = $"select Cost from LineItem where BOMId = {bomId}";
            cmd = new SqlCommand(sql, conn);
            dataReader = cmd.ExecuteReader();
            List<BOMLineItem> results = new List<BOMLineItem>();
            while (dataReader.Read())
            {
                results.Add(new BOMLineItem(dataReader.GetDecimal(0)));
            }

            conn.Close();
            return results;
        }
    }

    /// <summary>
    /// Calculates things about a BOM from the database
    /// </summary>
    public class ClassicBOMCalculator
    {
        /// <summary>
        /// Get the TotalCost of the BOM Line Items from the database
        /// </summary>
        /// <param name="bomId">The Unique ID of the BOM in the database</param>
        /// <returns>The Sum of LineItem Costs</returns>
        public decimal TotalCost(BigInteger bomId)
        {
            var bomFetcher = new BOMFetcher();
            return bomFetcher.LineItems(bomId).Sum(lineItem => lineItem.Cost);
        }
    }
}