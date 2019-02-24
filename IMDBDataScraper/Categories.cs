using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace IMDBDataScraper
{
    public class Categories
    {
        public  List<IMDBResult> GetMostPopularMoviesByGenre(IMDBCategories Category)
        {
            List<IMDBResult> imdbSimpleModel = new List<IMDBResult>();
            string genre = GetCategoryName(Category);

            try
            {
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load($"https://www.imdb.com/search/title?genres={genre}&explore=title_type,genres");
                var allMovieNodes = doc.DocumentNode.SelectNodes("//div[contains(@class, 'lister-item mode-advanced')]");

                foreach (var node in allMovieNodes)
                {
                    var model = new IMDBResult();
                    var smallImage = node.SelectSingleNode(".//div[contains(@class, 'lister-item-image float-left')]").SelectSingleNode(".//img").Attributes["loadlate"].Value;
                    var imagelinkParts = smallImage.Split('.');
                    var correctLink = $"{imagelinkParts[0]}.{imagelinkParts[1]}.{imagelinkParts[2]}";
                    model.SmallImage = $"{correctLink}._V1_UX140_CR0,0,140,209_AL_.jpg";
                    model.MediumImage = $"{correctLink}._SY1000_CR0,0,640,1000_AL_.jpg";
                    model.LargeImage = $"{correctLink}.jpg";
                    model.Title = node.SelectSingleNode(".//h3[contains(@class, 'lister-item-header')]").SelectSingleNode(".//a").InnerText;
                    model.Id = $"tt{Regex.Match(node.SelectSingleNode(".//h3[contains(@class, 'lister-item-header')]").SelectSingleNode(".//a").Attributes["href"].Value.Split('?').FirstOrDefault(), @"\d+").Value}";
                    model.Year = int.Parse(Regex.Match(node.SelectSingleNode(".//span[contains(@class, 'lister-item-year')]").InnerText, @"\b\d{4}\b").Value);
                    //actors
                    //type
                    imdbSimpleModel.Add(model);

                }

                return imdbSimpleModel;
            }
            catch (Exception ex)
            {
                //TODO: Enable logging
                return imdbSimpleModel;
            }
        }

        private static string GetCategoryName(IMDBCategories Category)
        {
            switch (Category)
            {
                case IMDBCategories.FilmNoir : return "film-noir";
                case IMDBCategories.SciFi: return "sci-fi";
                default: return Category.ToString().ToLower();
            }
        }
    }
}
