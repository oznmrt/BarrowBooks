using BarrowBooks.Business.Abstract;
using BarrowBooks.DataAccess.Abstract;
using BarrowBooks.Entites.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BarrowBooks.Business.Concrete
{
    public class MemberManager : IMemberService
    {
        private IMemberDAL _memberDAL;

        public MemberManager(IMemberDAL memberDAL)
        {
            _memberDAL = memberDAL;
        }

        public IEnumerable<Member> GetAll()
        {
            return _memberDAL.GetAll();
        }

        public void Insert(Member t)
        {
            _memberDAL.Insert(t);
        }
    }
}
