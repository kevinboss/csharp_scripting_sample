using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace extension
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            var albums = new List<Album>
            {
                new Album { Quantity = 10, Artist = "Betontod", Title = "Revolution" },
                new Album { Quantity = 50, Artist = "The Dangerous Summer", Title = "The Dangerous Summer" },
                new Album { Quantity = 200, Artist = "Depeche Mode", Title = "Spirit" },
            };
            var discountFilter = File.ReadAllText("DiscountFilter.txt");
            var options = ScriptOptions.Default.AddReferences(typeof(Album).Assembly);
            
            var discountFilterExpression = 
                await CSharpScript.EvaluateAsync<Func<Album, bool>>(discountFilter, options);
            
            var discountedAlbums = albums.Where(discountFilterExpression);
    
            foreach(var album in discountedAlbums) {
                Console.WriteLine($"{album.Quantity} - {album.Artist} - {album.Title}");
            }
        }
    }
}
