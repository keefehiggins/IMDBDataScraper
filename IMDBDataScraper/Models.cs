using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

    public class IMDBMoviePageModel
    {
        public string context { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public string name { get; set; }
        public string image { get; set; }

        [JsonConverter(typeof(SingleOrArrayConverter<string>))]
        public List<string> genre { get; set; }
        public string contentRating { get; set; }
        public Actor[] actor { get; set; }
        public Creator[] creator { get; set; }
        public string description { get; set; }
        public string datePublished { get; set; }
        public string keywords { get; set; }
        public Aggregaterating aggregateRating { get; set; }
        public Review review { get; set; }
        public Trailer trailer { get; set; }

        private class SingleOrArrayConverter<T> : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return (objectType == typeof(List<T>));
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                JToken token = JToken.Load(reader);
                if (token.Type == JTokenType.Array)
                {
                    return token.ToObject<List<T>>();
                }
                return new List<T> { token.ToObject<T>() };
            }

            public override bool CanWrite
            {
                get { return false; }
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }
    }

    public class Aggregaterating
    {
        public string type { get; set; }
        public int ratingCount { get; set; }
        public string bestRating { get; set; }
        public string worstRating { get; set; }
        public string ratingValue { get; set; }
    }

    public class Review
    {
        public string type { get; set; }
        public Itemreviewed itemReviewed { get; set; }
        public Author author { get; set; }
        public string dateCreated { get; set; }
        public string inLanguage { get; set; }
        public string name { get; set; }
        public string reviewBody { get; set; }
        public Reviewrating reviewRating { get; set; }
    }

    public class Itemreviewed
    {
        public string type { get; set; }
        public string url { get; set; }
    }

    public class Author
    {
        public string type { get; set; }
        public string name { get; set; }
    }

    public class Reviewrating
    {
        public string type { get; set; }
        public string worstRating { get; set; }
        public string bestRating { get; set; }
        public string ratingValue { get; set; }
    }

    public class Trailer
    {
        public string type { get; set; }
        public string name { get; set; }
        public string embedUrl { get; set; }
        public Thumbnail thumbnail { get; set; }
        public string thumbnailUrl { get; set; }
        public string description { get; set; }
        public DateTime uploadDate { get; set; }
    }

    public class Thumbnail
    {
        public string type { get; set; }
        public string contentUrl { get; set; }
    }

    public class Actor
    {
        public string type { get; set; }
        public string url { get; set; }
        public string name { get; set; }
    }

    public class Creator
    {
        public string type { get; set; }
        public string url { get; set; }
        public string name { get; set; }
    }

}
