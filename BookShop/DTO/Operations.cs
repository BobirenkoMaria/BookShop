using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShop.Tools;

namespace BookShop.DTO
{
    [Table("operations")]
    public class Operations : BaseDTO
    {
        [Column("Book_id")]
        public int Book_id { get; set; }

        [Column("ImportBooks")]
        public int ImportBooks { get; set; }

        [Column("Expenses")]
        public int Expenses { get; set; }

        [Column("BooksSold")]
        public int BooksSold { get; set; }

        [Column("Price")]
        public int Price { get; set; }

        [Column("OperationDate")]
        public DateTime OperationDate { get; set; }
    }
}
