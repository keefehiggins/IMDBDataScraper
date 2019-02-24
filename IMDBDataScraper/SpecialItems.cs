using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace IMDBDataScraper
{
    public class SpecialItems
    {
        public List<IMDBResult> GetSpecialItems(IMDBTypes SpecialType, HomePageType homePage) => GetResultSet(SpecialType, homePage);
        public List<IMDBResult> GetSpecialItems(IMDBTypes SpecialType) => GetResultSet(SpecialType);
        
        private static List<IMDBResult> GetResultSet(IMDBTypes SpecialType, HomePageType homePage = HomePageType.MoviesInTheaters)
        {
            switch (SpecialType)
            {
                case IMDBTypes.ComingSoon: return GetMoviesComingSoon();
                case IMDBTypes.MostPopularMovies: return GetMostPopularMovies();
                case IMDBTypes.MostPopularTV: return GetMostPopularTVShows();
                case IMDBTypes.NewDVDReleases: return GetNewDVDReleases();
                case IMDBTypes.Newest: return GetNewestMovies(homePage);
                case IMDBTypes.TopRatedMovies: return GetTopRatedMovies();
                case IMDBTypes.TopRatedTV: return GetTopRatedTVShows();
                default: throw new NotImplementedException($"{SpecialType} has not been implemented");
            }
        }

        private static List<IMDBResult> GetMostPopularTVShows()
        {
            List<IMDBResult> imdbSimpleModel = new List<IMDBResult>();
            try
            {
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load($"https://www.imdb.com/chart/tvmeter?ref_=nv_tvv_mptv");

                var allMovieNodes = doc.DocumentNode.SelectSingleNode("//tbody[contains(@class, 'lister-list')]").SelectNodes(".//tr");

                foreach (var node in allMovieNodes)
                {
                    var model = new IMDBResult();
                    var smallImage = node.SelectSingleNode(".//td[contains(@class, 'posterColumn')]").SelectSingleNode(".//a").SelectSingleNode(".//img").Attributes["src"].Value;
                    SetImages(model, smallImage);
                    model.Title = node.SelectSingleNode(".//td[contains(@class, 'titleColumn')]").SelectSingleNode(".//a").InnerText;
                    model.Id = $"tt{Regex.Match(node.SelectSingleNode(".//td[contains(@class, 'posterColumn')]").SelectSingleNode(".//a").Attributes["href"].Value.Split('?').FirstOrDefault(), @"\d+").Value}";
                    model.Year = int.Parse(Regex.Match(node.SelectSingleNode(".//span[contains(@class, 'secondaryInfo')]").InnerText, @"\b\d{4}\b").Value);
                    imdbSimpleModel.Add(model);
                }

                return imdbSimpleModel;
            }
            catch (Exception ex)
            {
                //Crashes.TrackError(ex);
               return  imdbSimpleModel;
            }
        }

        private static void SetImages(IMDBResult model, string smallImage)
        {
            var imagelinkParts = smallImage.Split(',');
            var correctLink = $"{imagelinkParts[0]}.{imagelinkParts[1]}.{imagelinkParts[2]}";
            model.SmallImage = $"{correctLink}._V1_UX140_CR0,0,140,209_AL_.jpg";
            model.MediumImage = $"{correctLink}._SY1000_CR0,0,640,1000_AL_.jpg";
            model.LargeImage = $"{correctLink}.jpg";
        }

        private static List<IMDBResult> GetMostPopularMovies()
        {
            List<IMDBResult> imdbSimpleModel = new List<IMDBResult>();
            try
            {
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load($"https://www.imdb.com/chart/moviemeter?ref_=nv_mv_mpm");

                var allMovieNodes = doc.DocumentNode.SelectSingleNode("//tbody[contains(@class, 'lister-list')]").SelectNodes(".//tr");

                foreach (var node in allMovieNodes)
                {
                    var model = new IMDBResult();
                    var smallImage = node.SelectSingleNode(".//td[contains(@class, 'posterColumn')]").SelectSingleNode(".//a").SelectSingleNode(".//img").Attributes["src"].Value;
                    SetImages(model, smallImage);
                    model.Title = node.SelectSingleNode(".//td[contains(@class, 'titleColumn')]").SelectSingleNode(".//a").InnerText;
                    model.Id = $"tt{Regex.Match(node.SelectSingleNode(".//td[contains(@class, 'posterColumn')]").SelectSingleNode(".//a").Attributes["href"].Value.Split('?').FirstOrDefault(), @"\d+").Value}";
                    model.Year = int.Parse(Regex.Match(node.SelectSingleNode(".//span[contains(@class, 'secondaryInfo')]").InnerText, @"\b\d{4}\b").Value);
                    imdbSimpleModel.Add(model);
                }

                return imdbSimpleModel;
            }
            catch (Exception ex)
            {
                //Crashes.TrackError(ex);
                return imdbSimpleModel;
            }
        }

        private static List<IMDBResult> GetTopRatedMovies()
        {
            List<IMDBResult> imdbSimpleModel = new List<IMDBResult>();
            try
            {
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load($"https://www.imdb.com/chart/top?ref_=nv_mv_250");

                var allMovieNodes = doc.DocumentNode.SelectSingleNode("//tbody[contains(@class, 'lister-list')]").SelectNodes(".//tr");


                foreach (var node in allMovieNodes)
                {
                    var model = new IMDBResult();
                    var smallImage = node.SelectSingleNode(".//td[contains(@class, 'posterColumn')]").SelectSingleNode(".//a").SelectSingleNode(".//img").Attributes["src"].Value;
                    SetImages(model, smallImage);
                    model.Title = node.SelectSingleNode(".//td[contains(@class, 'titleColumn')]").SelectSingleNode(".//a").InnerText;
                    model.Id = $"tt{Regex.Match(node.SelectSingleNode(".//td[contains(@class, 'posterColumn')]").SelectSingleNode(".//a").Attributes["href"].Value.Split('?').FirstOrDefault(), @"\d+").Value}";
                    model.Year = int.Parse(Regex.Match(node.SelectSingleNode(".//span[contains(@class, 'secondaryInfo')]").InnerText, @"\b\d{4}\b").Value);
                    imdbSimpleModel.Add(model);
                }

                return imdbSimpleModel;
            }
            catch (Exception ex)
            {
                //Crashes.TrackError(ex);
                return imdbSimpleModel;
            }
        }

        private static List<IMDBResult> GetTopRatedTVShows()
        {
            List<IMDBResult> imdbSimpleModel = new List<IMDBResult>();
            try
            {
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load($"https://www.imdb.com/chart/toptv/?ref_=nv_tvv_250");

                var allMovieNodes = doc.DocumentNode.SelectSingleNode("//tbody[contains(@class, 'lister-list')]").SelectNodes(".//tr");
                
                foreach (var node in allMovieNodes)
                {
                    var model = new IMDBResult();
                    var smallImage = node.SelectSingleNode(".//td[contains(@class, 'posterColumn')]").SelectSingleNode(".//a").SelectSingleNode(".//img").Attributes["src"].Value;
                    SetImages(model, smallImage);
                    model.Title = node.SelectSingleNode(".//td[contains(@class, 'titleColumn')]").SelectSingleNode(".//a").InnerText;
                    model.Id = $"tt{Regex.Match(node.SelectSingleNode(".//td[contains(@class, 'posterColumn')]").SelectSingleNode(".//a").Attributes["href"].Value.Split('?').FirstOrDefault(), @"\d+").Value}";
                    model.Year = int.Parse(Regex.Match(node.SelectSingleNode(".//span[contains(@class, 'secondaryInfo')]").InnerText, @"\b\d{4}\b").Value);
                    imdbSimpleModel.Add(model);
                }

                return imdbSimpleModel;
            }
            catch (Exception ex)
            {
                //Crashes.TrackError(ex);
                return imdbSimpleModel;
            }
        }

        private static List<IMDBResult> GetNewDVDReleases()
        {
            List<IMDBResult> imdbSimpleModel = new List<IMDBResult>();
            try
            {
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load($"https://www.imdb.com/list/ls016522954/?ref_=nv_tvv_dvd");

                var allMovieNodes = doc.DocumentNode.SelectNodes("//*[contains(@class,'lister-item mode-detail')]");

                foreach (var node in allMovieNodes)
                {
                    var model = new IMDBResult();
                    var smallImage = node.SelectNodes(".//img").Where(x => x.Attributes.Contains("loadlate")).FirstOrDefault().Attributes["loadlate"].Value;
                    SetImages(model, smallImage);
                    var titleNode = node.SelectNodes(".//*[contains(@class,'lister-item-header')]").FirstOrDefault();
                    model.Title = titleNode.SelectSingleNode(".//a").InnerText;
                    model.Id = $"tt{Regex.Match(node.SelectSingleNode(".//div[contains(@class, 'image')]").SelectSingleNode(".//a").Attributes["href"].Value, @"\d+").Value}";
                    model.Year = int.Parse(Regex.Match(titleNode.SelectNodes(".//span[contains(@class, 'lister-item-year')]").LastOrDefault().InnerText, @"\b\d{4}\b").Value);
                    imdbSimpleModel.Add(model);
                }

                return imdbSimpleModel;
            }
            catch (Exception ex)
            {
                //Crashes.TrackError(ex);
                return imdbSimpleModel;
            }
        }

        internal static List<IMDBResult> GetMoviesComingSoon()
        {
            List<IMDBResult> imdbSimpleModel = new List<IMDBResult>();
            try
            {
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load($"https://www.imdb.com/movies-coming-soon/");

                var allMovieNodes = doc.DocumentNode.SelectNodes("//*[contains(@class,'list_item')]");

                foreach (var node in allMovieNodes)
                {
                    var model = new IMDBResult();
                    var smallImage = node.SelectNodes(".//img").FirstOrDefault().Attributes["src"].Value;
                    SetImages(model, smallImage);
                    var title = node.SelectNodes(".//*[contains(@class,'overview-top')]").FirstOrDefault().SelectSingleNode(".//a").Attributes["title"].Value;
                    model.Title = title;
                    model.Title = Regex.Replace(model.Title, @"\((\d{4})\)$", "").TrimEnd();
                    model.Id = $"tt{Regex.Match(node.SelectSingleNode(".//div[contains(@class, 'image')]").SelectSingleNode(".//a").Attributes["href"].Value, @"\d+").Value}";
                    model.Year = int.Parse(Regex.Match(title, @"\b\d{4}\b").Value);
                    imdbSimpleModel.Add(model);
                }

                return imdbSimpleModel;
            }
            catch (Exception ex)
            {
                //Crashes.TrackError(ex);
                return imdbSimpleModel;
            }
        }

        private static List<IMDBResult> GetNewestMovies(HomePageType movieType = HomePageType.MoviesInTheaters)
        {
            List<IMDBResult> imdbSimpleModel = new List<IMDBResult>();
            try
            {
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load($"https://www.imdb.com/movies-in-theaters/");

                var allMovieNodes = doc.DocumentNode.SelectNodes("//*[contains(@class,'list_item')]");

                foreach (var node in allMovieNodes)
                {
                    var model = new IMDBResult();
                    var smallImage = node.SelectNodes(".//img").FirstOrDefault().Attributes["src"].Value;
                    SetImages(model, smallImage);
                    var title = node.SelectNodes(".//*[contains(@class,'overview-top')]").FirstOrDefault().SelectSingleNode(".//a").Attributes["title"].Value;
                    model.Title = title;
                    model.Title = Regex.Replace(model.Title, @"\((\d{4})\)$", "").TrimEnd();
                    model.Id = $"tt{Regex.Match(node.SelectSingleNode(".//div[contains(@class, 'image')]").SelectSingleNode(".//a").Attributes["href"].Value, @"\d+").Value}";
                    model.Year = int.Parse(Regex.Match(title, @"\b\d{4}\b").Value);
                    imdbSimpleModel.Add(model);
                }

                return imdbSimpleModel;
            }
            catch (Exception ex)
            {
                //Crashes.TrackError(ex);
                return imdbSimpleModel;
            }
        }

    }
}
