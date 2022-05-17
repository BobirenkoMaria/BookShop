using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShop.DTO;
using BookShop.Model;
using BookShop.Tools;

namespace BookShop.ViewModel
{
    class OperationsVM : BaseVM
    {
        private ObservableCollection<OperationsStr> operationsList;
        public ObservableCollection<OperationsStr> OperationsList
        {
            get => operationsList;
            set
            {
                operationsList = value;
                Signal();
            }
        }

        public OperationsVM()
        {
            UpdateListView();
        }

        public void UpdateListView()
        {
            OperationsList = new ObservableCollection<OperationsStr>(SqlModel.GetInstance().SelectOperationsStrDB());
        }
    }
}
