using BookShop.DTO;
using BookShop.Model;
using BookShop.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace BookShop.ViewModel
{
    public class AddOperationVM : BaseVM
    {
        public Operations EditOperations { get; set; }
        public CommandVM SaveOperation { get; set; }
        private CurrentPageControl currentPageControl;
        private Dispatcher dispatcher;

        internal MainPageVM mainVM { get; set; }

        public Books OperationsBook
        {
            get => operationsBook;
            set
            {
                operationsBook = value;
                Signal();
            }
        }
        private Books operationsBook;
        public List<Books> Books { get; set; }


        public AddOperationVM(CurrentPageControl currentPageControl)
        {
            this.currentPageControl = currentPageControl;
            EditOperations = new Operations();

            SqlModel sqlModel = SqlModel.GetInstance();
            Books = sqlModel.SelectBooksDB();
            InitCommand();
        }
        public AddOperationVM(CurrentPageControl currentPageControl, Dispatcher dispatcher) : this(currentPageControl)
        {
            this.dispatcher = dispatcher;
        }

        private void InitCommand()
        {
            SaveOperation = new CommandVM(() => {
                var model = SqlModel.GetInstance();
                if (EditOperations.ID == 0)
                    model.Insert(EditOperations);
                else
                    model.Update(EditOperations);
                Task.Delay(1000).ContinueWith(s =>
                dispatcher.Invoke(() =>
                mainVM.ListViewSales.UpdateListView())
                );
                Task.Delay(1000).ContinueWith(s =>
                dispatcher.Invoke(() =>
                mainVM.ListViewOperation.UpdateListView()));
            });
        }
    }
}
