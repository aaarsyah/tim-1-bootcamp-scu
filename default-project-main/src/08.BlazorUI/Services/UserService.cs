using MyApp.BlazorUI.Models;

namespace MyApp.BlazorUI.Services
{
    public class UserService : IUserService
    {
        private readonly List<UserItem> _user = new();
        private int _nextId = 1;

        // Properti untuk menyimpan user yang sedang login
        public UserItem? CurrentUser { get; set; }

        public UserService()
        {
            SeedData();
        }

        // Mendapatkan semua user
        public async Task<List<UserItem>> GetUserAsync()
        {
            await Task.Delay(100); // Simulasi delay
            return _user.OrderByDescending(t => t.Name).ToList();
        }

        // Menambahkan user baru
        public async Task<UserItem> CreateUserAsync(UserItem user)
        {
            await Task.Delay(100);
            user.Id = _nextId++;
            _user.Add(user);
            return user;
        }

        // Memperbarui user yang sudah ada
        public async Task<UserItem> UpdateUserAsync(UserItem user)
        {
            await Task.Delay(100);
            var existingUser = _user.FirstOrDefault(t => t.Id == user.Id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                existingUser.UserRole = user.UserRole;
                existingUser.AllUser = user.AllUser;
                existingUser.BirthDate = user.BirthDate;
            }
            return user;
        }

        // Menghapus user
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

        // Data dummy
        private void SeedData()
        {
            var sampleUsers = new List<UserItem>
            {
                new UserItem
                {
                    Id = _nextId++,
                    Name = "Ahmad Bajuri",
                    Email = "ahmadbajuri@gmail.com",
                    UserRole = Role.User,
                    AllUser = UserStatus.Active,
                    BirthDate = new DateTime(1995, 3, 12)
                },
                new UserItem
                {
                    Id = _nextId++,
                    Name = "Sanggar Kelana",
                    Email = "sanggarkelana@gmail.com",
                    UserRole = Role.Admin,
                    AllUser = UserStatus.Active,
                    BirthDate = new DateTime(1990, 8, 25)
                },
                new UserItem
                {
                    Id = _nextId++,
                    Name = "Jihan Putri",
                    Email = "jihanputri@gmail.com",
                    UserRole = Role.User,
                    AllUser = UserStatus.InActive,
                    BirthDate = new DateTime(1998, 1, 4)
                },
                new UserItem
                {
                    Id = _nextId++,
                    Name = "Admin",
                    Email = "admin@gmail.com",
                    UserRole = Role.User,
                    AllUser = UserStatus.InActive,
                    BirthDate = new DateTime(1998, 1, 4)
                }
            };

            _user.AddRange(sampleUsers);

            // Simulasikan user pertama sebagai user yang sedang login
            CurrentUser = _user.First();
        }
    }
}
