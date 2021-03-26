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
    public class Member : IEntity
    {
        [Key]
        [Description("ignore")]
        public int Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }
    }
}
