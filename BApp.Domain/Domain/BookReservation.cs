using BApp.Domain.Domain;
using System.ComponentModel.DataAnnotations;

namespace BApp.Domain
{
    public class BookReservation : BaseEntity
    {
       
        public Guid ReservationId { get; set; }
        public Guid BookingListId { get; set; }
        public Reservation? Reservation { get; set; }
        public BookingList? BookingList { get; set; }
        public int NumberOfNights { get; set; }

        public int getTotalPrice()
        {
            return NumberOfNights * Reservation.Apartment.Price_per_night;
        }
    }
}
