using BookShop.ViewModel;

namespace BookShop
{
    internal class MainPageVM: BaseVM
    {
        public SettingsVM Settings { get; set; }
        public ListViewDataBaseVM ListViewBooks { get; set; }
        public AddBookVM AddNewBook { get; set; }
        public ListViewSalesVM ListViewSales { get; set; }
        public OperationsVM ListViewOperation { get; set; }
        public AddOperationVM AddNewOperations { get; set; }
        public StatisticVM Statistic { get; set; }
    }
}