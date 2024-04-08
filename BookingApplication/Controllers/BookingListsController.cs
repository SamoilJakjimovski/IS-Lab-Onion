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
    public class BookingListsController : Controller
    {
        private readonly IBookingListService _bookinListService;

        public BookingListsController(IBookingListService bookinListService)
        {
            _bookinListService = bookinListService;
        }


        // GET: BookingLists
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? null;
          
            return View(_bookinListService.getBookingListInfo(userId??""));
        }

       
        public IActionResult Book()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //if(userId == null)
            var result = _bookinListService.book(userId);

            return RedirectToAction("Index", "BookingLists");

        }
        public IActionResult DeleteFromBookingList(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = _bookinListService.deleteFromBookingList(userId, id);

            return RedirectToAction("Index","BookingLists");

        }
    }
}
