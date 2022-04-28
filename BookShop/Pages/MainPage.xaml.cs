using BookShop.Tools;
using BookShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;

namespace BookShop
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        CurrentPageControl currentPageControl;

        bool OpenedWin;

        bool OpenedAddBookWin;
        bool OpenedAddOperationWin;

        public MainPage(CurrentPageControl currentPageControl)
        {
            InitializeComponent();

            this.currentPageControl = currentPageControl;

            DataContext = new MainPageVM { Settings = new SettingsVM(passwordBox, currentPageControl), AddNewBook = new AddBookVM(currentPageControl), 
                ListView = new ListViewDataBaseVM(), ListViewSales = new ListViewSalesVM() };

            OpenedWin = false;
            DataBaseBorder.Visibility = Visibility.Hidden;

            StatWin.Visibility = Visibility.Hidden;

            OpenedAddBookWin = false;
            AddBookWin.Visibility = Visibility.Hidden;

            OpenedAddOperationWin = false;
            AddOperationWin.Visibility = Visibility.Hidden;
        }

        private void ConnectDataBase(object sender, RoutedEventArgs e)
        {
            if (!OpenedWin)
            {
                DataBaseBorder.Visibility= Visibility.Visible;
                OpenedWin = true;
            }
            else
            {
                DataBaseBorder.Visibility = Visibility.Hidden;
                OpenedWin = false;
            }
        }

        private void AddNewBook(object sender, RoutedEventArgs e)
        {
            if (!OpenedAddBookWin)
            {
                AddBookWin.Visibility = Visibility.Visible;
                DataBaseBorder.Visibility = Visibility.Hidden;
                AddOperationWin.Visibility = Visibility.Hidden;

                OpenedAddOperationWin = false;
                OpenedAddBookWin = true;
            }
            else
            {
                AddBookWin.Visibility = Visibility.Hidden;
                OpenedAddBookWin = false;
            }
        }

        private void MainWin(object sender, RoutedEventArgs e)
        {
            StatWin.Visibility= Visibility.Hidden;
        }

        private void StatisticWin(object sender, RoutedEventArgs e)
        {
            StatWin.Visibility = Visibility.Visible;
        }

        private void AddBook(object sender, RoutedEventArgs e)
        {
            AddBookWin.Visibility = Visibility.Hidden;
            NavigationService.Navigate(new MainPage(currentPageControl));
        }

        private void AddNewOperation(object sender, RoutedEventArgs e)
        {
            if (!OpenedAddOperationWin)
            {
                AddOperationWin.Visibility = Visibility.Visible;
                DataBaseBorder.Visibility = Visibility.Hidden;
                AddBookWin.Visibility = Visibility.Hidden;

                OpenedAddBookWin = false;
                OpenedAddOperationWin = true;
            }
            else
            {
                AddOperationWin.Visibility = Visibility.Hidden;
                OpenedAddOperationWin = false;
            }
        }
    }
}
