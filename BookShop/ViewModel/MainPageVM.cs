using BookShop.ViewModel;

namespace BookShop
{
    internal class MainPageVM
    {
        public SettingsVM Settings { get; set; }
        public ListViewDataBaseVM ListView { get; set; }
        public AddBookVM AddNewBook { get; set; }
        public ListViewSalesVM ListViewSales { get; set; }
    }
}