using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMDBDataScraper
{
    public class Movie
    {
        public IMDBMoviePageModel GetMainMoviePage(string IMDBId)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load($"https://www.imdb.com/title/{IMDBId}/?ref_=fn_al_tt_1");
            var script = doc.DocumentNode.Descendants()
                             .Where(n => n.Attributes.Any(x => x.Value == "application/ld+json")).FirstOrDefault(); 
            try
            {
                var settings = new JsonSerializerSettings { Error = (se, ev) => { ev.ErrorContext.Handled = true; } };
                return JsonConvert.DeserializeObject<IMDBMoviePageModel>(script.InnerText, settings);
            }
            catch (Exception ex)
            {
                //Crashes.TrackError(ex);
                return null;
            }
        }

    }
}
