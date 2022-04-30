using BookShop.ViewModel;

namespace BookShop
{
    internal class MainPageVM
    {
        public SettingsVM Settings { get; set; }
        public ListViewDataBaseVM ListView { get; set; }
        public AddBookVM AddNewBook { get; set; }
        public ListViewSalesVM ListViewSales { get; set; }
        public OperationsVM AddNewOperation { get; set; }
        public AddOperationVM AddNewOperations { get; set; }
        public StatisticVM Statistic { get; set; }
    }
}