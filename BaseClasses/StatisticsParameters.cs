using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_19StatisticsWPF.BaseClasses
{
    internal class StatisticsParameters
    {
        public StatisticsParameters() { }
        public string CountryName { get; set; }
        public string TotalCases { get; set; }
        public string NewCases { get; set; }
        public string TotalDeaths { get; set; }
        public string NewDeaths { get; set; }
        public string TotalRecovered { get; set; }

        public StatisticsParameters(List<string> parameters)
        {
            CountryName = parameters[0];
            TotalCases = parameters[1];
            NewCases = parameters[2];
            TotalDeaths = parameters[3];
            NewDeaths = parameters[4];
            TotalRecovered = parameters[5];
        }

        

    }
}
