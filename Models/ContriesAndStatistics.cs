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
        private string _urlXPath = "//div[@class='col-md-8']//div[@id='nav-tabContent']//div[@class='main_table_countries_div']//table[@id='main_table_countries_today']/tbody";


        private HtmlAgilityPack.HtmlDocument htmlDocumentFromURL;

        readonly int TOTAL_RECOVERED_INDEX;

        internal List<string> contriesNames;

        internal List<BaseClasses.StatisticsParameters> parameters;

        private List<string> continents = new List<string>()
        {
            "All",
            "Europe",
            "North America",
            "Asia",
            "South America",
            "Africa",
            "Oceania",
            "Australia/Oceania"
        };



        public ContriesAndStatistics() 
        {
            htmlDocumentFromURL = getHtmlDocument();
            TOTAL_RECOVERED_INDEX = 6;

            contriesNames = new List<string>();
            parameters = new List<BaseClasses.StatisticsParameters>();
            setValuesToVariable(_urlXPath);
        }


        public HtmlAgilityPack.HtmlDocument getHtmlDocument()
        {
            HtmlAgilityPack.HtmlWeb htmlAgility = new HtmlAgilityPack.HtmlWeb();
            HtmlAgilityPack.HtmlDocument document = htmlAgility.Load(_urlAddress);
            return document;
        }


        public List<string> parsingHtmlDocument(string urlXPath)
        {
            var l = new List<string>();
            foreach (var item in htmlDocumentFromURL.DocumentNode.SelectNodes(urlXPath))
            {
                l.Add(item.InnerText);
            }

            
            l = cuttingList(l, "USA");
            
            return l;
        }

        public List<string> cuttingList(List<string> workingList, string keyWord)
        {
            for (int i = 0; i < workingList.Count; ++i)
            {
                string line = workingList[i].Trim(new char[] { '\n', ' ' });
                workingList[i] = line;
            }

            var newWorkingList = new List<string>();
            foreach (var item in workingList)
            {
                string line = item;
                var temporaryList = line.Split('\n').ToList();
                foreach (var element in temporaryList)
                {
                    newWorkingList.Add(element.Trim().ToString());
                }
            }
            int indexToSkip = -1;
            for (int i = 0; i < newWorkingList.Count; ++i)
            {
                if (newWorkingList[i].Equals(keyWord))
                {
                    indexToSkip = i;
                    break;
                }
            }
            if (keyWord.Equals("USA"))
                return newWorkingList.Skip(indexToSkip).ToList();

            return newWorkingList.Skip(++indexToSkip).ToList();

        }


        public void setValuesToVariable(string urlXPath)
        {
            averageElementsInContriesStatList(parsingHtmlDocument(urlXPath));
        }


        public void averageElementsInContriesStatList(List<string> workingList)
        {
            
            int indexToSkip = 0;
            foreach (var it_ in workingList)
            {
                if (it_.Contains("Total:"))
                {
                    break;
                }
                indexToSkip++;
            }
            workingList = workingList.Take(indexToSkip).ToList();

            for (int i = 0; i < workingList.Count; ++i)
            {
                for (int j = 0; j < continents.Count; ++j)
                {
                    if (workingList[i].Equals(continents[j]))
                    {
                        workingList[i] = " ";
                    }
                }
            }
            for (int i = 0; i < workingList.Count; i += 23)
            {
                //Console.WriteLine("COUNTRY : " + workingList[i]);
                contriesNames.Add(workingList[i]);
                
                var temporaryListOfParameters = new List<string>();
                if (i + TOTAL_RECOVERED_INDEX < workingList.Count)
                {
                    for (int j = i; j < i + TOTAL_RECOVERED_INDEX; j++)
                    {
                        temporaryListOfParameters.Add(workingList[j]);
                    }
                    parameters.Add(new BaseClasses.StatisticsParameters(temporaryListOfParameters));
                }
                //temporaryListOfParameters.Clear();
            }
            
        }
    }

}