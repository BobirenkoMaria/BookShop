﻿using BookShop.DTO;
using BookShop.Model;
using BookShop.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.ViewModel
{
    class StatisticVM : BaseVM
    {


        private List<int> pageIndexes;
        private int selectedIndex;
        private int viewRows;

        private List<DateTime> statisticDate;
        public List<DateTime> StatisticDate
        {
            get => statisticDate;
            set
            {
                statisticDate = value;
                Signal();
            }
        }

        public CommandVM ViewBack { get; set; }
        public CommandVM ViewForward { get; set; }
        public List<int> PageIndexes
        {
            get => pageIndexes;
            set
            {
                pageIndexes = value;
                Signal();
            }
        }

        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                selectedIndex = value;
                StatisticDate = SqlModel.GetInstance().SelectRowToList_DateTime("SaleDate");
                Signal();
            }
        }
        //public int[] RowsCountVariants { get; set; }
        public int ViewRows
        {
            get => viewRows;
            set
            {
                viewRows = value;
                InitPages();
                Signal();
            }
        }

        public StatisticVM()
        {
            //RowsCountVariants = new int[] { 10, 50, 100 };
            ViewRows = 2;

            ViewBack = new CommandVM(() =>
            {
                if (SelectedIndex > 1)
                    SelectedIndex--;
            });

            ViewForward = new CommandVM(() =>
            {
                if (SelectedIndex < PageIndexes.Last())
                    SelectedIndex++;
            });
        }

        private void InitPages()
        {
            var sqlModel = SqlModel.GetInstance();
            int pageCount = (sqlModel.GetNumRows(typeof(DateTime)));
            PageIndexes = new List<int>(Enumerable.Range(1, pageCount));
            SelectedIndex = 1;
        }
    }
}