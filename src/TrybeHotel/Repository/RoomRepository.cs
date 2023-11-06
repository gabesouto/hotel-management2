using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class RoomRepository : IRoomRepository
    {
        protected readonly ITrybeHotelContext _context;
        public RoomRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        // 6. Desenvolva o endpoint GET /room/:hotelId
        public IEnumerable<RoomDto> GetRooms(int HotelId)
        {
            var get = (from r in this._context.Rooms
                       where r.HotelId == HotelId
                       select new RoomDto
                       {
                           roomId = r.RoomId,
                           name = r.Name,
                           capacity = r.Capacity,
                           image = r.Image,
                           hotel = new HotelDto
                           {
                               hotelId = r.Hotel.HotelId,
                               name = r.Hotel.Name,
                               address = r.Hotel.Address,
                               cityId = r.Hotel.City.CityId,
                               cityName = r.Hotel.City.Name,
                               state = r.Hotel.City.State
                           },

                       }).ToList();
            return get;
        }

        // 7. Desenvolva o endpoint POST /room
        public RoomDto AddRoom(Room room)
        {
            var addNewRoom = this._context.Rooms.Add(room);
            this._context.SaveChanges();
            var response = (from r in this._context.Rooms
                            where r.Name == room.Name
                            select new RoomDto
                            {
                                roomId = r.RoomId,
                                name = r.Name,
                                capacity = r.Capacity,
                                image = r.Image,
                                hotel = new HotelDto
                                {
                                    hotelId = r.Hotel.HotelId,
                                    name = r.Hotel.Name,
                                    address = r.Hotel.Address,
                                    cityId = r.Hotel.City.CityId,
                                    cityName = r.Hotel.City.Name,
                                    state = r.Hotel.City.State

                                },



                            }).First();
            return response;
        }

        // 8. Desenvolva o endpoint DELETE /room/:roomId
        public void DeleteRoom(int RoomId)
        {
            var deleteRoom = this._context.Rooms.Find(RoomId);
            this._context.Rooms.Remove(deleteRoom);
            this._context.SaveChanges();
        }
    }
}