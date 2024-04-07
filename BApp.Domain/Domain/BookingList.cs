using System.ComponentModel.DataAnnotations;

namespace BApp.Domain
{
    public class BookingList
    {
        [Key]
        public Guid Id { get; set; }
        public string? OwnerId { get; set; }
        public BookingApplicationUser? Owner { get; set; }
        public virtual ICollection<BookReservation>? BookReservations { get; set; }
    }
}
