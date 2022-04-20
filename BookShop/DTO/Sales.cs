using BookShop.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.DTO
{
    [Table("`booksdeliveries")]
    public class Sales : BaseDTO
    {
        [Column("ImportBooks")]
        public int ImportBooks;

        [Column("Expenses")]
        public int Expenses;

        [Column("BooksSold")]
        public int BooksSold;

        [Column("Price")]
        public int Price;
    }
}
