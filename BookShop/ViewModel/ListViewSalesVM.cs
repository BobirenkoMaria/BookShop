using BookShop.DTO;
using BookShop.Model;
using BookShop.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.ViewModel
{
    class ListViewSalesVM : BaseVM
    {

        private ObservableCollection<Sales> salesList;
        public ObservableCollection<Sales> SalesList
        {
            get => salesList;
            set
            {
                salesList = value;
                Signal();
            }
        }

        public ListViewSalesVM()
        {
            UpdateListView();
            //UpdateDB();
        }

        public void UpdateListView()
        {
            SalesList = new ObservableCollection<Sales>(SqlModel.GetInstance().SelectSalesDB());
        }



        public void UpdateDB()
        {
            ObservableCollection<Sales> NewSalesList = new ObservableCollection<Sales>();
            int count = SqlModel.GetInstance().GetCountRows("sales");

            for (int i = 1; i <= count; i++) 
            {
                Sales newSale = new Sales();

                int Expences, Price, ImportBooks, BooksSold;

                Expences = SqlModel.GetInstance().SelectIntByLastDate("Expences", i);
                Price = SqlModel.GetInstance().SelectIntByLastDate("Price", i);

                ImportBooks = SqlModel.GetInstance().SelectSalesDB("ImportBooks", i);
                BooksSold = SqlModel.GetInstance().SelectSalesDB("BooksSold", i);

                newSale.Expenses = Expences;
                newSale.Price = Price;
                newSale.ImportBooks = ImportBooks;
                newSale.BooksSold = BooksSold;

                NewSalesList.Add(newSale);
            }

            SalesList = NewSalesList;
        }

    }
}