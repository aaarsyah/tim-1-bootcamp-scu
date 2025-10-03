using MyApp.BlazorUI.Models;

namespace MyApp.BlazorUI.Services
{
    public class UserService : IUserService
    {
        private readonly List<UserItem> _user = new();
        private int _nextId = 1;

        public UserService()
        {

            SeedData();
        }

        public async Task<List<UserItem>> GetUserAsync()
        {
            await Task.Delay(100); 
            return _user.OrderByDescending(t => t.Name).ToList();
        }

        public async Task<UserItem> CreateUserAsync(UserItem user)
        {
            await Task.Delay(100);
            user.Id = _nextId++;
            _user.Add(user);
            return user;
        }

        public async Task<UserItem> UpdateUserAsync(UserItem user)
        {
            await Task.Delay(100);
            var existingUser = _user.FirstOrDefault(t => t.Id == user.Id);
            if (existingUser != null)
            {
                var index = _user.IndexOf(existingUser);
                _user[index] = existingUser;
            }
            return user;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            await Task.Delay(100);
            var user = _user.FirstOrDefault(t => t.Id == id);
            if (user != null)
            {
                _user.Remove(user);
                return true;
            }
            return false;
        }

        private void SeedData()
        {
            var sampleUser = new List<UserItem>
            {
                new UserItem
                {
                    Id = _nextId++,
                    Email = "ahmadbajuri@gmail.com",
                    Name = "Ahmad Bajuri",
                    UserRole = Role.User,
                    AllUser = UserStatus.Active
                },
                new UserItem
                {
                    Id = _nextId++,
                    Email = "sanggarkelana@gmail.com",
                    Name = "Sanggar Kelana",
                    UserRole = Role.Admin,
                    AllUser = UserStatus.Active
                },
                new UserItem
                {
                    Id = _nextId++,
                    Email = "jihanputri@gmail.com",
                    Name = "Jihan Putri",
                    UserRole = Role.User,
                    AllUser = UserStatus.InActive
                }
            };
            _user.AddRange(sampleUser);
        }
    }
}
