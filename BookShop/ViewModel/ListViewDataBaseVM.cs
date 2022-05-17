using BookShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShop.DTO;
using BookShop.Model;
using BookShop.Tools;
using System.Collections.ObjectModel;

namespace BookShop.ViewModel
{
    class ListViewDataBaseVM : BaseVM
    {
        private ObservableCollection<Books> booksList ;
        public ObservableCollection<Books> BooksList
        {
            get => booksList;
            set
            {
                booksList = value;
                Signal();
            }
        }

        public ListViewDataBaseVM()
        {
            UpdateListView();
        }

        public void UpdateListView()
        {
            BooksList = new ObservableCollection<Books>( SqlModel.GetInstance().SelectBooksDB());
        }

    }
}
