using BookShop.DTO;
using BookShop.Model;
using BookShop.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using System.Windows.Media;
using LiveCharts.Configurations;

namespace BookShop.ViewModel
{
    class StatisticVM : BaseVM
    {
        private List<DateModel> dateImportBooksModelList;
        private List<DateModel> dateExpensesModelList;
        private List<DateModel> dateBooksSoldModelList;
        private List<DateModel> datePriceModelList;
        //public List<DateModel> DateModelList
        //{
        //    get => dateModelList;
        //    set
        //    {
        //        dateModelList = value;
        //        Signal();
        //    }
        //}

        public StatisticVM()
        {
            DateList();

            Points();
        }

        public void DateList()
        {
            dateImportBooksModelList = SqlModel.GetInstance().SelectStatisticDB("ImportBooks");
            dateExpensesModelList = SqlModel.GetInstance().SelectStatisticDB("Expenses");
            dateBooksSoldModelList = SqlModel.GetInstance().SelectStatisticDB("BooksSold");
            datePriceModelList = SqlModel.GetInstance().SelectStatisticDB("Price");
        }

        /// <summary>
        /// ///////////////////////////////////////
        /// </summary>

        public SeriesCollection Series { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        private void Points()
        {
            var dayConfig = Mappers.Xy<DateModel>()
            .X(dateModel => dateModel.Date.Ticks / (TimeSpan.FromDays(1).Ticks * 30.44))
            .Y(dateModel => dateModel.Value);

            Series = new SeriesCollection(dayConfig)
            {
                new LineSeries
                {
                    Values = new ChartValues<DateModel>
                    {
                        dateImportBooksModelList[0],
                        dateImportBooksModelList[1],
                        dateImportBooksModelList[2],
                    },
                    Title = "На складе",
                    Fill = Brushes.Transparent
                },
                new LineSeries
                {
                    Values = new ChartValues<Tools.DateModel>
                    {
                        dateExpensesModelList[0],
                        dateExpensesModelList[1],
                        dateExpensesModelList[2],
                    },
                    Title = "Себестоимость",
                    Fill = Brushes.Transparent
                },
                new LineSeries
                {
                    Values = new ChartValues<Tools.DateModel>
                    {
                        dateBooksSoldModelList[0],
                        dateBooksSoldModelList[1],
                        dateBooksSoldModelList[2],
                    },
                    Title = "Продано",
                    Fill = Brushes.Transparent
                },
                new LineSeries
                {
                    Values = new ChartValues<Tools.DateModel>
                    {
                        datePriceModelList[0],
                        datePriceModelList[1],
                        datePriceModelList[2],
                    },
                    Title = "Цена",
                    Fill = Brushes.Transparent
                }
            };

            //Formatter = value => new System.DateTime((long)(value * TimeSpan.FromHours(1).Ticks)).ToString("t");  // по часам
            Formatter = value => new DateTime((long)(value * TimeSpan.FromDays(1).Ticks * 30.44)).ToString("M"); // по месяцам
        }
    }
}
