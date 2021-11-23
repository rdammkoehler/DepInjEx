using System;
using System.Numerics;

namespace DepInjEx
{
    public class Program
    {
        // Classic Approach
        /// <summary>
        /// Given a BOMId as an argument, calculate the total cost of the BOM.
        /// </summary>
        /// <param name="args">Expects 1 numeric value, the BOMId</param>
        public static void Main(string[] args)
        {
            var bomId = BigInteger.Parse(args[0]);
            var totalCost = new ClassicBOMCalculator().TotalCost(bomId);
            Console.WriteLine($"The total cost of BOM {bomId} is ${totalCost}");
        }

        // Dependency Injection Approach
        /// <summary>
        /// Given a BOMId as an argument, calculate the total cost of the BOM.
        /// </summary>
        /// <param name="args">Expects 1 numeric value, the BOMId</param>
        public static void Main_(string[] args)
        {
            var bomId = BigInteger.Parse(args[0]);
            // Here we are manually injecting the BOMFetcher instance
            var totalCost = new BOMCalculator(new DBBOMFetcher()).TotalCost(bomId);
            Console.WriteLine($"The total cost of BOM {bomId} is ${totalCost}");
        }
    }
}