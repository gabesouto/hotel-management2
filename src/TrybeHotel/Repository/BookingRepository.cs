using TrybeHotel.Models;
using TrybeHotel.Dto;
using Microsoft.EntityFrameworkCore;

namespace TrybeHotel.Repository
{
    public class BookingRepository : IBookingRepository
    {
        protected readonly ITrybeHotelContext _context;
        public BookingRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        public BookingResponse Add(BookingDtoInsert booking, string email)
        {
            User user = _context.Users.First(u => email == u.Email);
            Room room = _context.Rooms.Find(booking.RoomId);
            if (booking.GuestQuant > room.Capacity) throw new ArgumentException("Guest quantity over room capacity");

            if (user == null || room == null) throw new ArgumentException("User or room not found");
            try
            {
                Booking newBooking = new()
                {
                    CheckIn = booking.CheckIn,
                    CheckOut = booking.CheckOut,
                    GuestQuant = booking.GuestQuant,
                    User = user, // Set the User navigation property directly, no need for UserId.
                    Room = room, // Set the Room navigation property directly, no need for RoomId.
                    RoomId = room.RoomId,

                };

                _context.Bookings.Add(newBooking);
                _context.SaveChanges();

                var res = GetBooking(newBooking.BookingId, email);
                return res;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null; // Handle exceptions by returning null or a meaningful error response.
            }
        }

        public BookingResponse GetBooking(int bookingId, string email)
        {

            User user = _context.Users.First(u => u.Email == email);

            var booking = _context.Bookings
                .Include(r => r.Room)
                .ThenInclude(h => h.Hotel)
                .ThenInclude(c => c.City)
                .First(b => b.BookingId == bookingId);


            if (booking == null) throw new ArgumentException("Booking not found");
            if (booking.User.Email != user.Email) throw new ArgumentException("invalid user");

            BookingResponse res = new()
            {
                BookingId = booking.BookingId,
                CheckIn = booking.CheckIn,
                CheckOut = booking.CheckOut,
                GuestQuant = booking.GuestQuant,
                Room = new RoomDto
                {
                    roomId = booking.Room.RoomId,
                    name = booking.Room.Name,
                    capacity = booking.Room.Capacity,
                    hotel = new HotelDto
                    {
                        hotelId = booking.Room.HotelId,
                        name = booking.Room.Hotel.Name,
                        address = booking.Room.Hotel.Address,
                        cityId = booking.Room.Hotel.CityId,
                        cityName = booking.Room.Hotel.City.Name,
                        state = booking.Room.Hotel.City.State

                    },

                }
            };
            return res;


        }

        public Room GetRoomById(int RoomId)
        {
            try
            {
                Room room = _context.Rooms.First(r => RoomId == r.RoomId);
                return room;
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
                return null;
            }
        }

    }

}