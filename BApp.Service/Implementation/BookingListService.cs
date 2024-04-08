using BApp.Domain;
using BApp.Repository.Interface;
using BApp.Service.Interface;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BApp.Service.Implementation
{
    public class BookingListService : IBookingListService
    {
        private readonly IRepository<BookingList> _bookingListRepository;
        private readonly IRepository<Reservation> _reservationRepository;
        private readonly IRepository<BookReservation> _bookReservationRepository;
        private readonly IUserRepository userRepository;

        public BookingListService(IRepository<BookingList> bookingListRepository, IRepository<Reservation> reservationRepository, IUserRepository userRepository, IRepository<BookReservation> bookReservationRepository)
        {
            _bookingListRepository = bookingListRepository;
            _reservationRepository = reservationRepository;
            this.userRepository = userRepository;
            _bookReservationRepository = bookReservationRepository;
        }

        public bool AddToBookingListConfirmed(BookReservation model, string userId)
        {

            if (userId != null) { 

                var user = userRepository.Get(userId);

                var bookingList = user?.BookingList;

                var reservation = _reservationRepository.Get(model.ReservationId);

                if (reservation != null && bookingList != null)
                {


                    BookReservation br = new BookReservation()
                    {
                        Id = Guid.NewGuid(),
                        BookingListId = bookingList.Id,
                        BookingList = bookingList,
                        Reservation = model.Reservation,
                        ReservationId = model.ReservationId,
                        NumberOfNights = model.NumberOfNights
                    };

                    bookingList?.BookReservations?.Add(br);

                    _bookingListRepository.Update(bookingList);

                    return true;
                }
        }
            return false;
        }

        public bool book(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var user = userRepository.Get(userId);
                var bookingList = user?.BookingList;

                var allBookReservations = bookingList?.BookReservations?.Select(z => new BookReservation
                {
                    Id = Guid.NewGuid(),
                    BookingList = bookingList,
                    BookingListId = bookingList.Id,
                    Reservation = z.Reservation,
                    ReservationId = z.ReservationId,
                    NumberOfNights = z.NumberOfNights
                });

                _bookReservationRepository.InsertMany(allBookReservations);
                bookingList?.BookReservations.Clear();
                _bookingListRepository.Update(bookingList);
                return true;

            }
            return false;
        }

        public bool deleteFromBookingList(string userId, Guid reservationId)
        {
            if (!string.IsNullOrEmpty(userId) && reservationId!=null) {

                var user = userRepository.Get(userId);
                var bookingList = user?.BookingList;
                var reservationToDelete = user?.BookingList?.BookReservations?.Where(r=>r.ReservationId==reservationId).FirstOrDefault();

                bookingList.BookReservations.Remove(reservationToDelete);

                _bookingListRepository.Update(bookingList);
                return true;
            }
            return false;
        }

        public BookingListDto getBookingListInfo(string userId)
        {
            if(!string.IsNullOrEmpty(userId))
            {
                var user = userRepository.Get(userId);

                var bookingList = user?.BookingList;

                var dto = new BookingListDto()
                {
                    bookReservations = bookingList?.BookReservations.ToList()
                };

                return dto;
            }

            return null;
        }
    }
}
