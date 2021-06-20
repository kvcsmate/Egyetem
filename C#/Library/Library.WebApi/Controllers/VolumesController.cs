using Library.Persistence.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Persistence.DTO;
using Microsoft.AspNetCore.Authorization;
using Library.Persistence;

namespace Library.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Librarian")]
    public class VolumesController : ControllerBase
    {
        private readonly ILibraryService _service;

        public VolumesController(ILibraryService service)
        {
            _service = service;
        }

        // GET: api/Volumes
        [HttpGet("Book/{bookId}")]
        public ActionResult<IEnumerable<VolumeDto>> GetVolumes(int bookId)
        {
            try
            {

                return _service.GetVolumesByBookId(bookId).Select(item => (VolumeDto)item).ToList();
                /*.GetBookByID(bookId)
                .Volumes
                .Select(item => (VolumeDto)item).ToList();*/

            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        public ActionResult<VolumeDto> GetVolume(int id)
        {
            try
            {
                return (VolumeDto)_service.GetVolume(id);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        [Authorize(Roles = "Librarian")]
        [HttpPut("{id}")]
        public IActionResult PutVolume(int id, VolumeDto book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            if (_service.UpdateVolume((Volume)book))
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
        public ActionResult DeleteVolume(int id)
        {

            var res = _service.GetReservationsByVolumeId(id);
            bool isOut = false;
            foreach (Reservation rvm in res)
            {
                if (rvm.IsActive)
                    isOut = true;
            }
            if (isOut)
            {
                return BadRequest();
            }
                if (_service.DeleteVolume(id))
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [Authorize(Roles = "Librarian")]
        [HttpPost]
        public ActionResult<VolumeDto> PostVolume(VolumeDto volumeDto)
        {
            var volume = _service.CreateVolume((Volume)volumeDto);
            if (volume == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            else
            {
                return CreatedAtAction(nameof(GetVolume), new { id = volume.Id }, (VolumeDto)volume);
            }


        }
    }
}
