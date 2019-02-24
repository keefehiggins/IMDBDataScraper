using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;

namespace IMDBDataScraper
{
    public class AutoComplete
    {
        public List<IMDBResult> GetAutoCompletResultsByTerm(string Term)
        {
            List<IMDBResult> returnList = new List<IMDBResult>();
            Term = Term.ToLower().Replace(" ", "_");

            string suggestURL = $"https://v2.sg.media-imdb.com/suggests/{Term.FirstOrDefault()}/{Term}.json";
            try
            {
                using (HttpClient wc = new HttpClient(new HttpClientHandler
                {
                    UseProxy = false
                }))
                {
                    var autoCompleteResponse = wc.GetStringAsync(new Uri(suggestURL)).Result;
                    autoCompleteResponse = autoCompleteResponse.Replace($"imdb${Term}(", "").Replace("]})", "]}");

                    return ProcessIMDBResults(JsonConvert.DeserializeObject<IMDBAutoCompleteResult>(autoCompleteResponse));
                }
            }
            catch (Exception ex)
            {
                return returnList;
            }
        }

        private static List<IMDBResult> ProcessIMDBResults(IMDBAutoCompleteResult IMDBAutoList)
        {
            List<IMDBResult> returnList = new List<IMDBResult>();
            var regex = new Regex(".jpg", RegexOptions.IgnoreCase);

            foreach (var movie in IMDBAutoList.d)
            {
                var smallImage = regex.Replace(movie.i.FirstOrDefault().ToString(), "UX182_CR0,0,182,268_AL_.jpg");
                returnList.Add(new IMDBResult
                {
                    Actors = movie.s,
                    Id = movie.id,
                    SmallImage = smallImage,
                    LargeImage = movie.i.FirstOrDefault().ToString(),
                    MediumImage = regex.Replace(movie.i.FirstOrDefault().ToString(), "SY1000_CR0,0,640,1000_AL_.jpg"),
                    Title = movie.l,
                    Type = movie.q,
                    Year = movie.y
                });
            }

            return returnList;
        }


        private class IMDBAutoCompleteResult
        {
            public int v { get; set; }
            public string q { get; set; }
            public D[] d { get; set; }
        }

        private class D
        {
            public string l { get; set; }
            public string id { get; set; }
            public string s { get; set; }
            public int y { get; set; }
            public string yr { get; set; }
            public string q { get; set; }
            public int vt { get; set; }
            public object[] i { get; set; }
        }
    }


}
