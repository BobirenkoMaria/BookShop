using BookShop.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.DTO
{
    [Table("sales")]
    public class Sales : BaseDTO
    {
        [Column("id")]
        public string Title { get; set; }

        [Column("ImportBooks")]
        public int ImportBooks { get; set; }

        [Column("Expenses")]
        public int Expenses { get; set; }

        [Column("BooksSold")]
        public int BooksSold { get; set; }

        [Column("Price")]
        public int Price { get; set; }
    }
}
