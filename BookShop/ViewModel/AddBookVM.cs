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
    public class AddBookVM : BaseVM
    {
        public Books EditBooks { get; set; }
        public CommandVM SaveBook { get; set; }
        private CurrentPageControl currentPageControl;
        private Dispatcher dispatcher;

        internal MainPageVM mainVM { get; set; }

        public AddBookVM(CurrentPageControl currentPageControl)
        {
            this.currentPageControl = currentPageControl;
            EditBooks = new Books();
            InitCommand();
        }

        public AddBookVM(CurrentPageControl currentPageControl, Dispatcher dispatcher) : this(currentPageControl)
        {
            this.dispatcher = dispatcher;
        }

        private void InitCommand()
        {
            SaveBook = new CommandVM(() => {
                var model = SqlModel.GetInstance();
                if (EditBooks.ID == 0)
                    model.Insert(EditBooks);
                else
                    model.Update(EditBooks);
               Task.Delay(1000).ContinueWith(s =>
               dispatcher.Invoke(()=>
               mainVM.ListViewBooks.UpdateListView())
               );
                SqlModel.GetInstance().InsertBookToSaleDB();
            });
            
        }
    }
}
