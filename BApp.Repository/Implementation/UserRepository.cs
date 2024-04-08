using BApp.Domain;
using BApp.Repository.Interface;
using BookingApplication.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BApp.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<BookingApplicationUser> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<BookingApplicationUser>();
        }
        public IEnumerable<BookingApplicationUser> GetAll()
        {
            return entities.AsEnumerable();
        }

        public BookingApplicationUser Get(string id)
        {
            var strGuid = id.ToString();
            return entities.Include(z => z.BookingList).Include(z => z.BookingList.BookReservations).Include("BookingList.BookReservations")
                .Include("BookingList.BookReservations.Reservation")
                .Include("BookingList.BookReservations.Reservation.Apartment").SingleOrDefault(s => s.Id == strGuid);
        }
        public void Insert(BookingApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(BookingApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(BookingApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
    }
}
