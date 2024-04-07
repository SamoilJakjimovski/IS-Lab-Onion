using System.ComponentModel.DataAnnotations;

namespace BApp.Domain
{
    public class Reservation
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public DateTime Check_in_date { get; set; }
        public Guid ApartmentId { get; set; }
        public Apartment? Apartment { get; set; }
        public virtual BookingApplicationUser? User { get; set; }

        public virtual ICollection<BookReservation>? Reservations { get; set; }
    }
}
