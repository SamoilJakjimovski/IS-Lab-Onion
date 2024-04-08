using BApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BApp.Service.Interface
{
    public interface IApartmentService
    {
        IEnumerable<Apartment> GetApartments();
       
    }
}
