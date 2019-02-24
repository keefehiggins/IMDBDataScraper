using IMDBDataScraper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBTestConcole
{
    class Program
    {
        static void Main(string[] args)
        {
            var categoryResults = new Categories().GetMostPopularMoviesByGenre(IMDBCategories.Action);

            foreach(var catResult in categoryResults)
            {
                Console.WriteLine($"{catResult.Actors} {catResult.Id}");
                Console.WriteLine($"{catResult.ImageID} {catResult.LargeImage}");
                Console.WriteLine($"{catResult.MediumImage} {catResult.SmallImage}");
                Console.WriteLine($"{catResult.Title} {catResult.Type} {catResult.Year}");
            }

            var mostPopulalResults = new SpecialItems().GetSpecialItems(IMDBTypes.MostPopularMovies);

            foreach (var popResult in mostPopulalResults)
            {
                Console.WriteLine($"{popResult.Actors} {popResult.Id}");
                Console.WriteLine($"{popResult.ImageID} {popResult.LargeImage}");
                Console.WriteLine($"{popResult.MediumImage} {popResult.SmallImage}");
                Console.WriteLine($"{popResult.Title} {popResult.Type} {popResult.Year}");
            }

            Console.ReadKey();
        }
    }
}
