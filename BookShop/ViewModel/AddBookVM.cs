using BookShop.DTO;
using BookShop.Model;
using BookShop.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.ViewModel
{
    public class AddBookVM : BaseVM
    {
        public Books EditBooks { get; set; }
        public CommandVM SaveBook { get; set; }
        private CurrentPageControl currentPageControl;


        public AddBookVM(CurrentPageControl currentPageControl)
        {
            this.currentPageControl = currentPageControl;
            EditBooks = new Books();
            InitCommand();
        }
        public AddBookVM(Books editBook, CurrentPageControl currentPageControl)
        {
            this.currentPageControl = currentPageControl;
            EditBooks = editBook;
            InitCommand();
        }

        private void InitCommand()
        {
            SaveBook = new CommandVM(() => {
                var model = SqlModel.GetInstance();
                if (EditBooks.ID == 0)
                    model.Insert(EditBooks);
                else
                    model.Update(EditBooks);
            });
        }
    }
}
