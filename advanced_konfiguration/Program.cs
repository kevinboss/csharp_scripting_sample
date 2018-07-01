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

                var fileInfo = new FileInfo(FilePath);
                if (fileInfo.Exists)
                {
                    var scriptFileManifest = ScriptFileManifestFactory.Create(fileInfo.FullName);
                    var result = ScriptExecutor.Execute<int, PizzaPriceInfo>(scriptFileManifest, pizzaPriceInfo);

                    Console.WriteLine($"Total cost for {pizzaPriceInfo.PizzasOrdered} pizzas is {result} CHF.");
                }
            }
        }
    }

    public class PizzaPriceInfo
    {
        public int PizzasOrdered;
        public int PricePerPizza;
    }
}