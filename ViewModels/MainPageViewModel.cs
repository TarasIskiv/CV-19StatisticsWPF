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
        private List<string> _countries;

        public List<string> Countries
        {
            get => _countries;
            set => Set(ref _countries, value);
        }

        private string _selectedCountry;

        public string SelectedCountry 
        {
            get => _selectedCountry;
            set => Set(ref _selectedCountry, value);
        }


        public MainPageViewModel()
        {
            Statistics = new Models.ContriesAndStatistics();
            Countries = Statistics.contriesNames;
        }
    }
}
