using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library.Web.Models;
using Library.Web.Services;
using Microsoft.AspNetCore.Authorization;

namespace Library.Web.Controllers
{
    [Authorize]
    public class VolumesController : Controller
    {
        private readonly ILibraryService _service;

        public VolumesController(ILibraryService service)
        {
            _service = service;
        }
        [AllowAnonymous]
        public IActionResult Index(int id)
        {
            var volume = _service.GetVolume(id);
            return View(volume);
            
        }
        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            
            try
            {
                var volume = _service.GetVolumeDetails(id);
                TempData["BookId"] = volume.BookId;
                var reservations = volume.Reservations;
                bool istaken = false;
                foreach(Reservation reservation in reservations)
                {
                    if(reservation.Start <= DateTime.Now && reservation.End>DateTime.Now)
                    {
                        istaken = true;
                    }
                }
                if(istaken)
                {
                    TempData["IsTaken"] = "kikölcsönözve";
                }
                else
                {
                    TempData["IsTaken"] = "kölcsönözhető";
                }
                return View(volume);
            }
            catch
            {
                return NotFound();
            }

        }
        [Authorize]
        public IActionResult Create(int id)
        {

            TempData["VolumeId"] = id;
            return View(); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Reservation reservation)
        {
            
            if (ModelState.IsValid)
            {
                reservation.Id = 0;
                reservation.VolumeId = (int)TempData["VolumeId"]; 
                int id = (int)TempData["VolumeId"];
                if(_service.GetVolume(id)==null)
                {
                    return NotFound();
                }
                TempData["VolumeId"] = TempData["VolumeId"];
                if (DateTime.Compare(reservation.Start,reservation.End)>0)
                 {

                     ViewBag.error = "Az előjegyzés kezdete nem lehet később mint a vége!";
                     return View(reservation);
                 }
                if(DateTime.Compare(reservation.Start,DateTime.Now.AddHours(-1))<0)
                {
                    ViewBag.error = "A kölcsönzés nem kezdődhet egy múltbeli időpontban!";
                    return View(reservation);
                }
                foreach(Reservation _reservation in _service.GetReservationsByVolumeId(id))
                 {
                     if (DateTime.Compare(reservation.End,_reservation.Start)>0 && DateTime.Compare(_reservation.End,reservation.Start)>0)
                     {
                         ViewBag.error = "A kijelölt időpontban már más lefoglalta a könyvet."+ _reservation.Start + "-"+ _reservation.End;
                         return View(reservation);
                     }
                 } 
                if(_service.CreateReservation(reservation))
                 {
                     ViewBag.error = "Sikeres foglalás.";
                     return View();
                 }
                 else
                 {
                     ViewBag.error = "Hiba történt a foglalás során.";
                     return View();
                 }

            }
            return RedirectToAction("Details", "Volumes", new { @id = reservation.VolumeId });
        }
        public IActionResult CreateReservation(int id)
        {
            TempData["VolumeId"] = id;
            return RedirectToAction("Create", "Volumes", new { @id = id});
        }
        public IActionResult BackToReservations()
        {
            return RedirectToAction("Details", "Volumes", new { @id = TempData["VolumeId"] });
        }
        public IActionResult BackToVolumes(int id)
        {
            return RedirectToAction("Details", "Books", new { @id = TempData["BookId"] });
        }
    }
}
