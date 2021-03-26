using BarrowBooks.Entites.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrowBooks.Entites.Concrete
{
    public class BookTransaction : IEntity
    {
        [Description("ignore")]
        public int Id { get; set; }

        public int BookISBN { get; set; }

        public int MemberId { get; set; }

        public DateTime ReturnDate { get; set; }

        public int Status { get; set; }
    }
}
