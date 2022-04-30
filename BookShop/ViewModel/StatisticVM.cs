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
    class StatisticVM 
    {
        var dayConfig = Mappers.Xy<DateModel>()
                .X(dayModel => (double)dayModel.Date.Ticks / TimeSpan.FromHours(1).Ticks)
                .Y(dayModel => dayModel.Value);


        public SeriesCollection Series { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        private void Points()
        {
            Series = new SeriesCollection(dayConfig)
            {
                new LineSeries
                {
                    Values = new ChartValues<DateModel>
                    {
                        new DateModel
                        {
                            Date = System.DateTime.Now,
                            Value = 5
                        },
                        new DateModel
                        {
                            Date = System.DateTime.Now.AddHours(2),
                            Value = 9
                        }
                    },
                    Title = "На складе",
                    Fill = Brushes.Transparent
                },
                new LineSeries
                {
                    Values = new ChartValues<DateModel>
                    {
                        new DateModel
                        {
                            Date = System.DateTime.Now,
                            Value = 5
                        },
                        new DateModel
                        {
                            Date = System.DateTime.Now.AddHours(2),
                            Value = 9
                        },
                    },
                    Title = "Себестоимость",
                    Fill = Brushes.Transparent
                },
                new LineSeries
                {
                    Values = new ChartValues<DateModel>
                    {
                        new DateModel
                        {
                            Date = System.DateTime.Now,
                            Value = 5
                        },
                        new DateModel
                        {
                            Date = System.DateTime.Now.AddHours(2),
                            Value = 9
                        },
                    },
                    Title = "Продано",
                    Fill = Brushes.Transparent
                },
                new LineSeries
                {
                    Values = new ChartValues<DateModel>
                    {
                        new DateModel
                        {
                            Date = System.DateTime.Now,
                            Value = 5
                        },
                        new DateModel
                        {
                            Date = System.DateTime.Now.AddHours(2),
                            Value = 9
                        },
                    },
                    Title = "Цена",
                    Fill = Brushes.Transparent
                }
            };

            Formatter = value => new System.DateTime((long)(value * TimeSpan.FromHours(1).Ticks)).ToString("t");
        }

        public StatisticVM()
        {
            Points();
        }
    }
}
