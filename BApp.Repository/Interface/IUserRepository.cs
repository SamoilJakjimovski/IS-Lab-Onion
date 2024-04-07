using BApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BApp.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<BookingApplicationUser> GetAll();
        BookingApplicationUser Get(string id);
        void Insert(BookingApplicationUser user);
        void Update(BookingApplicationUser user);
        void Delete(BookingApplicationUser user);

    }
}
