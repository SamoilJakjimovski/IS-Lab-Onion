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

namespace BookingApplication.Controllers
{
    public class BookingListsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingListsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BookingLists
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var loggedInUser = _context.Users
                .Include(z => z.BookingList)
                .Include("BookingList.BookReservations")
                .Include("BookingList.BookReservations.Reservation")
                .Include("BookingList.BookReservations.Reservation.Apartment")
                .FirstOrDefault(x => x.Id == userId);

            var userBookingList = loggedInUser?.BookingList;
            var allBookings = userBookingList?.BookReservations?.ToList();

            

            BookingListDto dto = new BookingListDto
            {
                bookReservations = allBookings
            };

            return View(dto);
        }

        // GET: BookingLists/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingList = await _context.BookingLists
                .Include(b => b.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookingList == null)
            {
                return NotFound();
            }

            return View(bookingList);
        }

        // GET: BookingLists/Create
        public IActionResult Create()
        {
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: BookingLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OwnerId")] BookingList bookingList)
        {
            if (ModelState.IsValid)
            {
                bookingList.Id = Guid.NewGuid();
                _context.Add(bookingList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id", bookingList.OwnerId);
            return View(bookingList);
        }

        // GET: BookingLists/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingList = await _context.BookingLists.FindAsync(id);
            if (bookingList == null)
            {
                return NotFound();
            }
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id", bookingList.OwnerId);
            return View(bookingList);
        }

        // POST: BookingLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,OwnerId")] BookingList bookingList)
        {
            if (id != bookingList.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookingList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingListExists(bookingList.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id", bookingList.OwnerId);
            return View(bookingList);
        }

        // GET: BookingLists/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingList = await _context.BookingLists
                .Include(b => b.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookingList == null)
            {
                return NotFound();
            }

            return View(bookingList);
        }

        // POST: BookingLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var bookingList = await _context.BookingLists.FindAsync(id);
            if (bookingList != null)
            {
                _context.BookingLists.Remove(bookingList);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingListExists(Guid id)
        {
            return _context.BookingLists.Any(e => e.Id == id);
        }
        public IActionResult Book()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //if(userId == null)
            var loggedInUser = _context.Users
                .Include(z => z.BookingList)
                .Include("BookingList.BookReservations")
                .Include("BookingList.BookReservations.Reservation")
                .FirstOrDefault(x => x.Id == userId);

            var userBookingList = loggedInUser?.BookingList;

           

            List<BookReservation> bookReservations = new List<BookReservation>();

            var lista = userBookingList.BookReservations.Select(
                x => new BookReservation
                {
                    Id = Guid.NewGuid(),
                    ReservationId = x.Reservation.Id,
                    Reservation = x.Reservation,
                    BookingListId = userBookingList.Id,
                    BookingList = userBookingList,
                    NumberOfNights = x.NumberOfNights
                }
                ).ToList();

            bookReservations.AddRange(lista);

            foreach (var bookRes in bookReservations)
            {
                _context.BookReservations.Add(bookRes);
            }

            loggedInUser.BookingList.BookReservations.Clear();
            _context.Users.Update(loggedInUser);
            _context.SaveChanges();

            return RedirectToAction("Index", "BookingLists");


        }
        public IActionResult DeleteFromBookingList(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var loggedInUser = _context.Users
                .Include(z => z.BookingList)
                .Include("BookingList.BookReservations")
                .Include("BookingList.BookReservations.Reservation")
                .FirstOrDefault(x => x.Id == userId);

            var userBookingList = loggedInUser?.BookingList;
            var reservation = userBookingList?.BookReservations.Where(x => x.ReservationId == id).FirstOrDefault();

            userBookingList?.BookReservations?.Remove(reservation);
            _context.BookingLists.Update(userBookingList);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
