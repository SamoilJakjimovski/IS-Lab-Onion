using BApp.Domain;
using BApp.Repository.Interface;
using BApp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BApp.Service.Implementation
{
    public class ApartmentService : IApartmentService
    {

        private readonly IRepository<Apartment> _repository;

        public ApartmentService(IRepository<Apartment> repository)
        {
            _repository = repository;
        }

        public IEnumerable<Apartment> GetApartments()
        {
           return _repository.GetAll();
        }
    }
}
