using System;
using System.Collections.Generic;
using System.Text;

namespace IMDBDataScraper
{
    public class IMDBResult
    {
        public string Title { get; set; }
        public string Id { get; set; }
        public string Actors { get; set; }
        public string Type { get; set; }
        public string ImageID { get; set; }
        public string SmallImage { get; set; }
        public string MediumImage { get; set; }
        public string LargeImage { get; set; }
        public int Year { get; set; }
    }

    public enum IMDBTypes
    {
        Newest = 0,
        ComingSoon = 1,
        NewDVDReleases = 2,
        TopRatedTV = 3,
        TopRatedMovies = 4,
        MostPopularMovies = 5,
        MostPopularTV = 6,
    }

    public enum IMDBCategories
    {
        Action = 0,
        Adventure = 1,
        Animation = 2,
        Biography = 3,
        Comedy = 4,
        Crime = 5,
        Documentary = 6,
        Drama = 7,
        Family = 8,
        Fantasy = 9,
        FilmNoir = 10,
        History = 11,
        Horror = 12,
        Music = 13,
        Musical = 14,
        Mystery = 15,
        Romance = 16,
        SciFi = 17, 
        Short = 18,
        Sport = 19,
        Superhero = 20,
        Thriller = 21,
        War = 22,
        Western = 23
    }

    public enum HomePageType
    {
        MoviesInTheaters = 0,
        MoviesComingSoon = 1,
    }
}
