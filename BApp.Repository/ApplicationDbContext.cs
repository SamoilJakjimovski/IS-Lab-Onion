
using BApp.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookingApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext<BookingApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BApp.Domain.Apartment> Apartments { get; set; }
        public virtual DbSet<BApp.Domain.Reservation> Reservations { get; set; }

        public virtual DbSet<BApp.Domain.BookingList> BookingLists { get; set; }

        public virtual DbSet<BApp.Domain.BookReservation> BookReservations { get; set; }


    }
}
