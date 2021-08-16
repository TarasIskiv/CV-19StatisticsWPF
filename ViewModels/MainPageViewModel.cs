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
        private const int ADDTIONAL_VALUE_TO_YSCALE_IN_GRAPHIC = 1;
        private const int DEFAULT_VALUE_TO_YSCALE_IN_GRAPHIC = 1000;

        private Models.ContriesAndStatistics Statistics;
        private List<StatisticsParameters> parameters;

        private StatisticsParameters _selectedCountry;
        private IEnumerable<DataPoint> _dataPointsForNewCases;
        private IEnumerable<DataPoint> _dataPointsForNewDeaths;

        private int _maximumValueForPlot;
        private int LastIndexInListOfGeneratedPointsForGraphic;
        public List<StatisticsParameters> Parameters
        {
            get => parameters;
            set => Set(ref parameters, value);
        }
        public StatisticsParameters SelectedCountry
        {
            get => _selectedCountry;
            set
            {
                if (!Set(ref _selectedCountry, value)) return;
                _dataPointsForNewDeaths = generatePoints(SelectedCountry.NewDeaths);
                OnPropertyChanged(nameof(DataPointsForNewDeaths));
                _dataPointsForNewCases = generatePoints(SelectedCountry.NewCases);
                OnPropertyChanged(nameof(DataPointsForNewCases));
                _maximumValueForPlot = generateMaximumIndexForYValueInPlot();
                OnPropertyChanged(nameof(MaximumValueForPlot));
                refreshLastIndexInListOfGeneratedPointsForGraphic();
            }
        }
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

        public int MaximumValueForPlot
        {
            get => _maximumValueForPlot;
            set => Set(ref _maximumValueForPlot, value);
        }
     
        
        private List<DataPoint> generatePoints(string selectedValue)
        {
            var currentDataPoints = new List<Models.DataPoint>();
            int generateTo;
            int step;

            if (string.IsNullOrWhiteSpace(selectedValue))
            {
                return new List<DataPoint>() {new DataPoint() { XValue = 0, YValue = 0 }};
            }
            
            selectedValue = selectedValue.Substring(1);

            if (selectedValue.Contains(","))
            {
                selectedValue = parsingSelectedValue(selectedValue);
            }
            
            if (!Int32.TryParse(selectedValue, out generateTo))
            {
                return new List<DataPoint>() { new DataPoint() { XValue = 0, YValue = 0 } }; 
            }

            step = generateTo / 10;

            currentDataPoints = getPointsForGraphic(step, generateTo);

            LastIndexInListOfGeneratedPointsForGraphic = currentDataPoints.Count - 1;

            return currentDataPoints;
        }

        private List<DataPoint> getPointsForGraphic(int step, int generateTo)
        {
            var currentDataPoints = new List<Models.DataPoint>();
            int x = 0;
            int y = 0;
            for (; x < 10; ++x)
            {
                currentDataPoints.Add(new DataPoint() { XValue = x, YValue = y });
                y += step;
            }
            currentDataPoints.Add(new DataPoint() { XValue = x, YValue = generateTo });

            return currentDataPoints;
        }
        private string parsingSelectedValue(string stringToParse)
        {
            var tempArr = stringToParse.ToCharArray();
            int elementToRemoveIndex = -1;
            string newString = string.Empty;
            for (var i = 0; i < tempArr.Length; ++i)
            {
                if (tempArr[i] == ',')
                {
                    elementToRemoveIndex = i;
                    break;
                }
            }
            for (var i = 0; i < tempArr.Length; ++i)
            {
                if (i == elementToRemoveIndex)
                {
                    continue;
                }

                newString += tempArr[i];
            }

            return newString;
        }

        private int generateMaximumIndexForYValueInPlot()
        {
            int index = DataPointsForNewCases.ToList()[LastIndexInListOfGeneratedPointsForGraphic].YValue;
            return index == 0 ? DEFAULT_VALUE_TO_YSCALE_IN_GRAPHIC + ADDTIONAL_VALUE_TO_YSCALE_IN_GRAPHIC : index + ADDTIONAL_VALUE_TO_YSCALE_IN_GRAPHIC;
        }

        private void refreshLastIndexInListOfGeneratedPointsForGraphic()
        {
            LastIndexInListOfGeneratedPointsForGraphic = 0;
        }

        public MainPageViewModel()
        {
            Statistics = new Models.ContriesAndStatistics();
            Parameters = Statistics.parameters;
        }
    }
}
