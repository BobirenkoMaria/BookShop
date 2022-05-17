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

        public List<Books> Books { get; set; }

        private Books selectedBook;
        public event EventHandler<Books> SelectedBookChanged;

        public Books SelectedBook
        {
            get => selectedBook;
            set
            {
                selectedBook = value;
                Signal();
                SelectedBookChanged?.Invoke(this, value);
            }
        }

        public StatisticVM(Books selectedBook)
        {
            SqlModel sqlModel = SqlModel.GetInstance();
            Books = sqlModel.SelectBooksDB();
            SelectedBook = Books.FirstOrDefault(s => s.ID == selectedBook?.ID);

            DataList();
            Points();
        }
        public StatisticVM()
        {
            SqlModel sqlModel = SqlModel.GetInstance();
            Books = sqlModel.SelectBooksDB();

        }

        public void DataList()
        {
            dateImportBooksModelList = SqlModel.GetInstance().SelectStatisticDB("ImportBooks", selectedBook);
            dateExpensesModelList = SqlModel.GetInstance().SelectStatisticDB("Expenses", selectedBook);
            dateBooksSoldModelList = SqlModel.GetInstance().SelectStatisticDB("BooksSold", selectedBook);
            datePriceModelList = SqlModel.GetInstance().SelectStatisticDB("Price", selectedBook);
        }

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
                    Values = new ChartValues<DateModel>(dateImportBooksModelList),
                    Title = "На складе",
                    Fill = Brushes.Transparent
                },
                new LineSeries
                {
                    Values = new ChartValues<DateModel>(dateExpensesModelList),
                    Title = "Себестоимость",
                    Fill = Brushes.Transparent
                },
                new LineSeries
                {
                    Values = new ChartValues<DateModel>(dateBooksSoldModelList),
                    Title = "Продано",
                    Fill = Brushes.Transparent
                },
                new LineSeries
                {
                    Values = new ChartValues<DateModel>(datePriceModelList),
                    Title = "Цена",
                    Fill = Brushes.Transparent
                }
            };

            //Formatter = value => new System.DateTime((long)(value * TimeSpan.FromHours(1).Ticks)).ToString("t");  // по часам
            Formatter = value => new DateTime((long)(value * TimeSpan.FromDays(1).Ticks * 30.44)).ToString("M"); // по месяцам
        }
    }
}
