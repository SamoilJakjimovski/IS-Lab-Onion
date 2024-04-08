using BApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BApp.Service.Interface
{
    public interface IBookingListService
    {
        BookingListDto getBookingListInfo (string userId);
        bool deleteFromBookingList (string userId, Guid reservationId);
        bool book (string userId);
        bool AddToBookingListConfirmed (BookReservation model, string userId);
    }
}
