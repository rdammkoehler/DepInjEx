using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Numerics;

namespace DepInjEx
{
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

    class BOMFetcher
    {
        public List<BOMLineItem> LineItems(BigInteger bomId)
        {
            string constr;
            SqlConnection conn;

            constr = @"Data Source=DESKTOP-GP8F496;Initial Catalog=Demodb;User ID=sa;Password=24518300";

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

    public class ClassicBOMCalculator
    {
        public decimal TotalCost(BigInteger bomId)
        {
            var bomFetcher = new BOMFetcher();
            return bomFetcher.LineItems(bomId).Sum(lineItem => lineItem.Cost);
        }
    }
}