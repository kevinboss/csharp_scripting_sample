using shared;
using System;
using System.IO;

namespace advanced_konfiguration
{
    class Program
    {
        private const string FilePath = "./PizzaPriceCalculator/Solution.csx";

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("How many Pizzas do you need? Amount: ");
                var pizzas = Convert.ToInt32(Console.ReadLine());

                var pizzaPriceInfo = new PizzaPriceInfo
                {
                    PizzasOrdered = pizzas,
                    PricePerPizza = 5
                };
                var result = new ScriptExecutor().Execute<int, PizzaPriceInfo>(FilePath, pizzaPriceInfo);

                Console.WriteLine($"Total cost for {pizzaPriceInfo.PizzasOrdered} pizzas is {result} CHF.");
            }

            // ReSharper disable once FunctionNeverReturns
        }
    }

    public class PizzaPriceInfo
    {
        public int PizzasOrdered;
        public int PricePerPizza;
    }
}