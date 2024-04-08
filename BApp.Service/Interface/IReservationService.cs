using BApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BApp.Service.Interface
{
    public interface IReservationService
    {
        IQueryable<Reservation> GetReservations();
        IQueryable<Reservation> GetReservationById(Guid? id);
        void CreateNewReservation (Reservation p);
        void UpdateReservation (Reservation p);
        void DeleteReservation (Guid id);
    }
}
