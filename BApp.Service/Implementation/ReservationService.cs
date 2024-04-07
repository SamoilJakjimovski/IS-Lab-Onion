using BApp.Domain;
using BApp.Repository.Interface;
using BApp.Service.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BApp.Service.Implementation
{
    internal class ReservationService : IReservationService
    {
        private readonly IRepository<Reservation> _reservationRepository;
  

        public ReservationService(IRepository<Reservation> reservationRepository)
        {
            _reservationRepository = reservationRepository;
          
        }

        public void CreateNewReservation(Reservation p)
        {
            _reservationRepository.Insert(p);
        }

        public void DeleteReservation(Guid id)
        {
            var reservation = _reservationRepository.Get(id);
            _reservationRepository.Delete(reservation);

        }

        public Reservation GetReservationById(Guid? id)
        {
            return _reservationRepository.Get(id);
        }

        public List<Reservation> GetReservations()
        {
            return _reservationRepository.GetAll().ToList();
        }

        public void UpdateReservation(Reservation p)
        {
            _reservationRepository.Update(p);
        }
    }
}
