using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShop.Tools;

namespace BookShop.DTO
{
    [Table("books")]
    public class Books : BaseDTO
    {
        [Column("Title")]
        public string Title { get; set; }


        [Column("Author")]
        public string Author { get; set; }


        [Column("ReleaseDate")]
        public DateTime ReleaseDate { get; set; }


        [Column("Description")]
        public string Description { get; set; }


        [Column("Genre")]
        public string Genre { get; set; }
    }
}
