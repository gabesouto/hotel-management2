using TrybeHotel.Models;
using TrybeHotel.Dto;
using TrybeHotel.Services;

namespace TrybeHotel.Repository
{
    public class UserRepository : IUserRepository
    {
        protected readonly ITrybeHotelContext _context;
        public UserRepository(ITrybeHotelContext context)
        {
            _context = context;
        }
        public UserDto GetUserById(int userId)
        {
            var user = _context.Users.Find(userId);
            return new UserDto
            {
                UserId = user.UserId,
                name = user.Name,
                email = user.Email,
                userType = user.UserType
            };
        }

        public UserDto? Add(UserDtoInsert user)
        {

            User? userExists = _context.Users.FirstOrDefault(u => u.Email == user.email);

            if (userExists != null)
            {
                return null;
            }

            var newUser = new User
            {
                Name = user.name,
                Email = user.email,
                Password = user.password,
                UserType = "client"
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return new UserDto
            {
                UserId = newUser.UserId,
                name = newUser.Name,
                email = newUser.Email,
                userType = newUser.UserType
            };
        }

        public UserDto Login(LoginDto login)
        {
            var user = _context.Users.First(u => u.Email == login.Email);

            if (user.Password != login.Password)
            {
                return null;
            }

            return new UserDto
            {
                UserId = user.UserId,
                name = user.Name,
                email = user.Email,
                userType = user.UserType
            };



        }


        public IEnumerable<UserDto> GetUsers()
        {
            var users = _context.Users;
            List<UserDto> usersDto = new List<UserDto>();
            foreach (var user in users)
            {
                usersDto.Add(new UserDto
                {
                    UserId = user.UserId,
                    name = user.Name,
                    email = user.Email,
                    userType = user.UserType
                });
            }
            return usersDto;
        }

        public UserDto GetUserByEmail(string userEmail)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);
            Console.WriteLine(user.Email);
            if (user == null)
            {
                return null;
            }

            return new UserDto
            {
                UserId = user.UserId,
                name = user.Name,
                email = user.Email,
                userType = user.UserType
            };
        }

    }
}


