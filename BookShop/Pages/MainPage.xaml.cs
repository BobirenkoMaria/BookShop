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
            //NavigationService.Navigate(new AddBook());
            if (!OpenedAddBookWin)
            {
                AddBookWin.Visibility = Visibility.Visible;
                DataBaseBorder.Visibility = Visibility.Hidden;

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
    }
}
