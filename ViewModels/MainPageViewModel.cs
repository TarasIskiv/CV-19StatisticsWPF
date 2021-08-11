using CV_19StatisticsWPF.BaseClasses;
using CV_19StatisticsWPF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

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

        private IEnumerable<DataPoint> _dataPointsForNewCases;

        private IEnumerable<DataPoint> _dataPointsForNewDeaths;


        public IEnumerable<DataPoint> DataPointsForNewCases
        {
            get => _dataPointsForNewCases;
            set => Set(ref _dataPointsForNewCases, value);
        }
        public IEnumerable<DataPoint> DataPointsForNewDeaths 
        {
            get => _dataPointsForNewDeaths;
            set => Set(ref _dataPointsForNewDeaths, value);
        }


        private int _maximumValueForPlot;
        public int MaximumValueForPlot 
        {
            get => _maximumValueForPlot;
            set => Set(ref _maximumValueForPlot, value);
        }

        public StatisticsParameters SelectedCountry 
        {
            get => _selectedCountry;
            set
            {
                if(!Set(ref _selectedCountry, value)) return;
                _dataPointsForNewDeaths = generatePoints(SelectedCountry.NewDeaths);
                OnPropertyChanged(nameof(DataPointsForNewDeaths));
                _dataPointsForNewCases = generatePoints(SelectedCountry.NewCases);
                OnPropertyChanged(nameof(DataPointsForNewCases));
                _maximumValueForPlot = generateMaximumIndexForYValueInPlot();
                OnPropertyChanged(nameof(MaximumValueForPlot));
            }
        }

        private int generateMaximumIndexForYValueInPlot() 
        {

            if (DataPointsForNewCases.ToArray().Length >= 2)
            {
                return (int)DataPointsForNewCases.ToList()[10].YValue + 100;
            }
            else
            {
                if (DataPointsForNewDeaths.ToArray().Length >= 2)
                {
                    return (int)DataPointsForNewDeaths.ToList()[10].YValue + 100;
                }
                else
                {
                    return 1000;
                }
            }
        }


        private List<DataPoint> generatePoints(string selectedValue)
        {
            var emptyList = new List<DataPoint>();
            emptyList.Add(new DataPoint() { XValue = 0, YValue = 0 });
            if (string.IsNullOrWhiteSpace(selectedValue)) return emptyList;
            selectedValue = selectedValue.Substring(1);
            if (selectedValue.Contains(","))
            {
                var tempArr = selectedValue.ToCharArray();
                int elementToRemoveIndex = -1;
                string newString = string.Empty;
                for(var i = 0; i <tempArr.Length; ++i)
                {
                    if (tempArr[i] == ',')
                    {
                    elementToRemoveIndex = i;
                        break;
                    }
                }
                for (var i = 0; i < tempArr.Length; ++i)
                { 
                    if(i == elementToRemoveIndex)
                    {
                        continue;
                    }

                    newString += tempArr[i];
                }

                selectedValue = newString;
            }
            Int32.TryParse(selectedValue, out int generateTo);
            if (generateTo == 0) return emptyList; 
            var currentDataPoints = new List<Models.DataPoint>();
            double step = generateTo / 10;
            double y = 0;
            for(int x = 0; x <= 10; ++x)
            {
                currentDataPoints.Add(new DataPoint() { XValue = x, YValue = y });
                y += step;
            }

            return currentDataPoints;
        }

        public MainPageViewModel()
        {
            Statistics = new Models.ContriesAndStatistics();
            Parameters = Statistics.parameters;
            //DataPoints = null;
        }
    }
}
