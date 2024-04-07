using Microsoft.AspNetCore.Identity;

namespace BApp.Domain
{
    public class BookingApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Reservation>? Reservations { get; set; }
        
        public virtual BookingList? BookingList { get; set; }
    }
}
