using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class HotelRepository : IHotelRepository
    {
        protected readonly ITrybeHotelContext _context;
        public HotelRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        // 4. Desenvolva o endpoint GET /hotel
        public IEnumerable<HotelDto> GetHotels()
        {

            var hotels = from hotel in this._context.Hotels
                         select new HotelDto
                         {
                             hotelId = hotel.HotelId,
                             name = hotel.Name,
                             address = hotel.Address,
                             cityId = hotel.City.CityId,
                             cityName = hotel.City.Name,
                             state = hotel.City.State
                         };

            return hotels.ToList();

        }

        // 5. Desenvolva o endpoint POST /hotel
        public HotelDto AddHotel(Hotel hotel)
        {
            var addNewhotel = this._context.Hotels.Add(hotel);
            this._context.SaveChanges();

            var response = (from h in this._context.Hotels
                            where h.Name == hotel.Name
                            select new HotelDto
                            {
                                hotelId = h.HotelId,
                                name = h.Name,
                                address = h.Address,
                                cityId = h.CityId,
                                cityName = h.City.Name,
                                state = h.City.State
                            }).First();
            return response;
        }
    }
}