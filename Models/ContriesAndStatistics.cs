using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_19StatisticsWPF.Models
{
    internal class ContriesAndStatistics
    {
        private string _urlAddress = "https://www.worldometers.info/coronavirus/";

        private HtmlAgilityPack.HtmlDocument htmlDocumentFromURL;
        private HtmlAgilityPack.HtmlDocument getHtmlDocument()
        {
            HtmlAgilityPack.HtmlWeb htmlAgility = new HtmlAgilityPack.HtmlWeb();
            HtmlAgilityPack.HtmlDocument document = htmlAgility.Load(_urlAddress);
            return document;
            //List<string> rates = new List<string>();

            //List<string> ratesValues = new List<string>();
            //foreach (var item in document.DocumentNode.SelectNodes("//div[@class='data_container']/table[@class='table table-data -important']/tbody/tr/td/span/span"))
            //{
            //    ratesValues.Add(item.InnerText);
            //}
            //var t = new List<string>();
            //for (int i = 0; i < 24; ++i)
            //{
            //    t.Add(ratesValues[i]);
            //}

            //div[@class='col-md-8']//div[@id='nav-tabContent']//div[@class='main_table_countries_div']//table[@id='main_table_countries_today']
            ////div[@class='col-md-8']//div[@id='nav-tabContent']//div[@class='main_table_countries_div']//table[@id='main_table_countries_today']/thead
            ////div[@class='col-md-8']//div[@id='nav-tabContent']//div[@class='main_table_countries_div']//table[@id='main_table_countries_today']/tbody/tr[@class='odd']
        }

        public void parsingHtmlDocument() 
        {
            var l = new List<string>();
            foreach (var item in htmlDocumentFromURL.DocumentNode.SelectNodes("//div[@class='data_container']/table[@class='table table-data -important']/tbody/tr/td/span/span"))
            {
                l.Add(item.InnerText);
            }
        }

        public ContriesAndStatistics() { }

    }

}