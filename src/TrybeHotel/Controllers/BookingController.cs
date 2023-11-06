using Microsoft.AspNetCore.Mvc;
using TrybeHotel.Models;
using TrybeHotel.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TrybeHotel.Dto;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.EntityFrameworkCore;

namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("booking")]

    public class BookingController : Controller
    {
        private readonly IBookingRepository _repository;
        public BookingController(IBookingRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Authorize(Policy = "Client")]
        public IActionResult Add([FromBody] BookingDtoInsert bookingInsert)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value;
                if (email == null)
                {
                    return StatusCode(401, new { message = "Invalid token" });
                }

                Console.WriteLine(email);
                if (email == null)
                {
                    return StatusCode(401, new { message = "Invalid rmail" });
                }

                BookingResponse bookingResponse = _repository.Add(bookingInsert, email);
                if (bookingResponse == null)
                {
                    return StatusCode(409, new { message = "Room is already booked" });
                }

                return Created("booking", bookingResponse);
            }
            catch (Exception e)
            {

                return StatusCode(500, new { message = e.Message });
            }
        }


        [HttpGet("{Bookingid}")]
        [Authorize(Policy = "Client")]
        public IActionResult GetBooking(int Bookingid)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value;
                if (email == null)
                {
                    return StatusCode(401, new { message = "Invalid token" });
                }

                BookingResponse bookingResponse = _repository.GetBooking(Bookingid, email);
                if (bookingResponse == null)
                {
                    return StatusCode(404, new { message = "Booking not found" });
                }

                return Ok(bookingResponse);
            }
            catch (ArgumentException e)
            {
                return StatusCode(404, new { message = e.Message });
            }

            catch (Exception e)
            {
                return StatusCode(401, new { message = e.Message });
            }
        }
    }
}