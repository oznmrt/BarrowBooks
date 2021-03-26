using BarrowBooks.DataAccess.Abstract;
using BarrowBooks.Entites.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrowBooks.DataAccess.Concrete.Dapper
{
    public class MemberDAL : EntityRepositoryBase<Member>, IMemberDAL
    {
        public MemberDAL() : base("Member") { }
    }
}
