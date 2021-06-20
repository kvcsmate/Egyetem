using Library.Persistence;
using Library.Persistence.DTO;
using Library.Persistence.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Librarian")]
    public class ReservationsController : ControllerBase
    {
        private readonly ILibraryService _service;

        public ReservationsController(ILibraryService service)
        {
            _service = service;
        }

        // GET: api/Reservations
        [HttpGet("Volume/{volumeId}")]
        public ActionResult<IEnumerable<ReservationDto>> GetReservations(int volumeId)
        {
            try
            {
                return _service.GetReservationsByVolumeId(volumeId).Select(item => (ReservationDto)item).Where(r => r.IsActive || r.End>DateTime.Now ).ToList();
                /* .GetVolume(reservationId)
                 .Reservations
                 .Select(item => (ReservationDto)item).ToList();*/
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        public ActionResult<ReservationDto> GetReservation(int id)
        {
            try
            {
                return (ReservationDto)_service.GetReservation(id);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        [Authorize(Roles = "Librarian")]
        [HttpPut("{id}")]
        public IActionResult PutReservation(int id, ReservationDto reservation)
        {
            
            bool gtg = true;
            
            if(!reservation.IsActive)
            {
                bool isOut = false;
                var ress = _service.GetReservationsByVolumeId(reservation.VolumeId).Select(item => (ReservationDto)item).ToList();
                foreach (ReservationDto rvm in ress)
                {
                    if (rvm.IsActive)
                        isOut = true;
                }
                if (isOut)
                {
                    gtg = false;
                }
            }
            if (id != reservation.Id || gtg==false)
            {
                return BadRequest();
            }
            reservation.IsActive = !reservation.IsActive;
            if (_service.UpdateReservation((Reservation)reservation))
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [Authorize(Roles = "Librarian")]
        [HttpDelete("{id}")]
        public ActionResult DeleteReservation(int id)
        {
            
            if(_service.GetReservation(id).IsActive)
            {
                return BadRequest();
            }
            if (_service.Deletereservation(id))
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [Authorize]
        [HttpPost]
        public ActionResult<ReservationDto> PostReservation(ReservationDto reservationDto)
        {
            var reservation = _service.CreateReservation((Reservation)reservationDto);
            if (reservation == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            else
            {
                return CreatedAtAction(nameof(GetReservation), new { id = reservation.Id }, (ReservationDto)reservation);
            }


        }
    }
}

