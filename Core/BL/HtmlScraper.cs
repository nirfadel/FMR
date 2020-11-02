using Core.BL.Interfaces;
using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using System.Net;
using System.IO;
using System.Text;
using System.Xml;

namespace Core.BL
{
    class HtmlScraper : IHtmlScraper
    {
        private const string TOP_100_CELEB_URL = "https://www.imdb.com/list/ls052283250/";
        private const string CELEB_MAIN_URL = "https://www.imdb.com/name/{0}";

        /// <summary>
        /// load html page by url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private HtmlDocument LoadHtml(string url)
        {
            HtmlDocument _htmlPage;
            url = url.TrimEnd('/');
            string html = string.Empty;
            _htmlPage = new HtmlDocument();
            try
            {
                using (WebClient client = new WebClient())
                {
                    html = client.DownloadString(url);
                }
                _htmlPage.LoadHtml(html);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return _htmlPage;
        }

        //test without HtmlAgilityPack
        private string LoadHtml2(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                string data = readStream.ReadToEnd();

                response.Close();
                readStream.Close();
                return data;
            }
            return null;
        }

        /// <summary>
        /// get all celebs id's from top 100 celebs page 
        /// </summary>
        /// <returns></returns>
        private List<string> ScrapeAllCelebsIds()
        {
            List<string> celebritieStringList = null;
            try
            {
                HtmlDocument _mainHtmlPage = LoadHtml(TOP_100_CELEB_URL);
                celebritieStringList = new List<string>();

                HtmlNode node = _mainHtmlPage.DocumentNode.SelectSingleNode("//*[@class='lister-list']");

                foreach (HtmlNode _celebNode in node.ChildNodes)
                {
                    if (!(_celebNode is HtmlTextNode))
                    {
                        string id = string.Empty;
                        string imgHref = _celebNode.ChildNodes[1].ChildNodes[1].Attributes["href"].Value;
                        imgHref = imgHref.Remove(0, 6);
                        id = imgHref.Remove(imgHref.IndexOf("/?"));
                        if (string.IsNullOrEmpty(id) == false && id.StartsWith("nm") && !celebritieStringList.Contains(id))
                            celebritieStringList.Add(id);
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return celebritieStringList;
        }

        /// <summary>
        /// get all celebrities
        /// </summary>
        /// <returns></returns>
        public List<Celebrity> GetAllCelebrities()
        {
            List<Celebrity> listOfCelebs = null;
            try
            {
                List<string> celebsIds = ScrapeAllCelebsIds();
                if (celebsIds.Count > 0)
                {
                    listOfCelebs = new List<Celebrity>();
                    foreach (string id in celebsIds)
                    {

                        string url = String.Format(CELEB_MAIN_URL, id);
                        HtmlDocument _mainHtmlPage = LoadHtml(url);
                        string celebName = _mainHtmlPage.DocumentNode.SelectSingleNode("//*[@class='name-overview-widget__section']/h1/span").InnerText;
                        string role = _mainHtmlPage.DocumentNode.SelectSingleNode("//*[@class='name-overview-widget__section']/div/a/span").InnerText;
                        PersonGender gender = role.Contains("Actress") ? PersonGender.FEMALE : PersonGender.MALE; //if acctress must be female, else male - but if not actress I can't decide, it takes most of the cases
                        DateTime birthDate = DateTime.Parse(_mainHtmlPage.DocumentNode.SelectSingleNode("//*[@id='name-born-info']/time").Attributes["datetime"].Value);
                        string Photo = _mainHtmlPage.DocumentNode.SelectSingleNode("//*[@id='name-poster']").Attributes["src"].Value;

                        Celebrity celeb = new Celebrity
                        {
                            Id = id,
                            Name = celebName,
                            DateOfBirth = birthDate,
                            Gender = gender,
                            Role = role,
                            Photo = Photo
                        };
                        listOfCelebs.Add(celeb);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return listOfCelebs;
        }

        /// <summary>
        /// get all celebs roles
        /// </summary>
        /// <param name="htmlNode"></param>
        /// <returns></returns>
        private List<string> GetAllCelebRoles(HtmlNode htmlNode)
        {
            List<string> roles = new List<string>();
            try
            {
                foreach (var node in htmlNode.ChildNodes)
                {
                    if (node.Name == "a")
                        roles.Add(node.InnerText);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return roles;
        }
    }
}
