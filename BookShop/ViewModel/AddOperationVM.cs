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
    public class AddOperationVM : BaseVM
    {
        public List<Books> Books { get; set; }

        public Operations EditOperations { get; set; }
        public CommandVM SaveOperation { get; set; }
        private CurrentPageControl currentPageControl;


        public AddOperationVM(CurrentPageControl currentPageControl)
        {
            this.currentPageControl = currentPageControl;
            EditOperations = new Operations();
            InitCommand();
        }
        public AddOperationVM(Operations editOperation, CurrentPageControl currentPageControl)
        {
            this.currentPageControl = currentPageControl;
            EditOperations = editOperation;
            InitCommand();
        }

        private void InitCommand()
        {
            SaveOperation = new CommandVM(() => {
                var model = SqlModel.GetInstance();
                if (EditOperations.ID == 0)
                    model.Insert(EditOperations);
                else
                    model.Update(EditOperations);
            });
        }
    }
}
