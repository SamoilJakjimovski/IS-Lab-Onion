using BApp.Domain.Domain;
using System.ComponentModel.DataAnnotations;

namespace BApp.Domain
{
    public class BookingList : BaseEntity
    {
        
        public string? OwnerId { get; set; }
        public BookingApplicationUser? Owner { get; set; }
        public virtual ICollection<BookReservation>? BookReservations { get; set; }
    }
}
