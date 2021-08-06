using CV_19StatisticsWPF.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_19StatisticsWPF.ViewModels
{
    internal class MainPageViewModel : BaseViewModel.ViewModel
    {
        private Models.ContriesAndStatistics Statistics;
        private List<StatisticsParameters> parameters;

        public List<StatisticsParameters> Parameters
        {
            get => parameters;
            set => Set(ref parameters, value);
        }

        

        private StatisticsParameters _selectedCountry;

        public StatisticsParameters SelectedCountry 
        {
            get => _selectedCountry;
            set => Set(ref _selectedCountry, value);
        }

        

        public MainPageViewModel()
        {
            Statistics = new Models.ContriesAndStatistics();
            Parameters = Statistics.parameters;
        }
    }
}
