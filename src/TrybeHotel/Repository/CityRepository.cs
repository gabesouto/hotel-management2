using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class CityRepository : ICityRepository
    {
        protected readonly ITrybeHotelContext _context;
        public CityRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        // 4. Refatore o endpoint GET /city
        public IEnumerable<CityDto> GetCities()
        {
            var cities = from city in this._context.Cities
                         select new CityDto
                         {
                             CityId = city.CityId,
                             Name = city.Name,
                             state = city.State
                         };

            return cities.ToList();


        }

        // 3. Desenvolva o endpoint POST /city
        public CityDto AddCity(City city)
        {
            var add = _context.Cities.Add(city);
            _context.SaveChanges();
            return new CityDto
            {
                CityId = city.CityId,
                Name = city.Name,
                state = city.State
            };
        }


        // 3. Desenvolva o endpoint PUT /city
        public CityDto UpdateCity(City city)
        {
            var update = _context.Cities.Update(city);
            _context.SaveChanges();
            return new CityDto
            {
                CityId = city.CityId,
                Name = city.Name,
                state = city.State
            };
        }

    }
}