using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookingApplication.Data;
using BookingApplication.Models;
using System.Security.Claims;
using BApp.Domain;
using BApp.Service.Interface;

namespace BookingApplication.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IBookingListService _bookingListService;
        private readonly IApartmentService _apartmentService;

        public ReservationsController(IReservationService reservationService, IBookingListService bookingListService,IApartmentService ap)
        {
            _reservationService = reservationService;
            _bookingListService = bookingListService;
            _apartmentService = ap;
        }


        // GET: Reservations
        public  IActionResult Index()
        {
            return View(_reservationService.GetReservations().Include("Apartment"));
        }

        // GET: Reservations/Details/5
        public IActionResult Details(Guid? id)
        {
            var reservation = _reservationService.GetReservationById(id).Include("Apartment").FirstOrDefault();
            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {

            ViewData["ApartmentId"] = new SelectList(_apartmentService.GetApartments(), "Id", "ApartmentName");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Check_in_date,ApartmentId")] Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return View(reservation);
            }
            _reservationService.CreateNewReservation(reservation);
            return RedirectToAction(nameof(Index));
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = _reservationService.GetReservationById(id).FirstOrDefault();

            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["ApartmentId"] = new SelectList(_apartmentService.GetApartments(), "Id", "ApartmentName");
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Check_in_date,ApartmentId")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _reservationService.UpdateReservation(reservation);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
          
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = _reservationService.GetReservationById(id).FirstOrDefault();

            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var reservation = _reservationService.GetReservationById(id).FirstOrDefault();
            if (reservation != null)
            {
                _reservationService.DeleteReservation(id);
            }

            return RedirectToAction(nameof(Index));
        }

       
        public async Task<IActionResult> AddToList(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = _reservationService.GetReservationById(id).FirstOrDefault();
           
            BookReservation br = new BookReservation();

            br.ReservationId = reservation.Id;

            return View(br);
        }

        [HttpPost]
        public async Task<IActionResult> AddToListConfirmed(BookReservation model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _bookingListService.AddToBookingListConfirmed(model, userId);


            return View("Index", _reservationService.GetReservations());
        }


    }
}
