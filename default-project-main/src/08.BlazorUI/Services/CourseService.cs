using MyApp.BlazorUI.Models;

namespace MyApp.BlazorUI.Services
{
    public class CourseService : ICourseService
    {
        private readonly List<CourseItem> _course = new();
        private int _nextId = 1;

        public CourseService()
        {
            SeedData();
        }

        public async Task<List<CourseItem>> GetCourseAsync()
        {
            await Task.Delay(100); 
            return _course.OrderBy(t => t.Id).ToList();
        }

        public async Task<CourseItem> CreateCourseAsync(CourseItem course)
        {
            await Task.Delay(100);
            course.Id = _nextId++;
            _course.Add(course);
            return course;
        }

        public async Task<CourseItem> UpdateCourseAsync(CourseItem course)
        {
            await Task.Delay(100);
            var existingCourse = _course.FirstOrDefault(t => t.Id == course.Id);
            if (existingCourse != null)
            {
                var index = _course.IndexOf(existingCourse);
                _course[index] = existingCourse;
            }
            return course;
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            await Task.Delay(100);
            var course = _course.FirstOrDefault(t => t.Id == id);
            if (course != null)
            {
                _course.Remove(course);
                return true;
            }
            return false;
        }

        private void SeedData()
        {
            var sampleCourse = new List<CourseItem>
            {
                new CourseItem
                {
                    Id = _nextId++,
                    Name = "Drum Class For Beginner",
                    Description = "Drum Class",
                    Picture = "img/Class1.svg",
                    CategoryId = 1,
                    Harga = 1000000,
                    AllCourse = CourseStatus.Active
                },
                new CourseItem
                {
                    Id = _nextId++,
                    Name = "Piano Class For Beginner",
                    Description = "Piano Class",
                    Picture = "img/Class2.svg",
                    CategoryId = 2,
                    Harga = 2000000,
                    AllCourse = CourseStatus.Active
                },
                new CourseItem
                {
                    Id = _nextId++,
                    Name = "Gitar Class For Beginner",
                    Description = "Gitar Class",
                    Picture = "img/Class4.svg",
                    CategoryId = 3,
                    Harga = 2500000,
                    AllCourse = CourseStatus.Active
                },
                new CourseItem
                {
                    Id = _nextId++,
                    Name = "Bass Class For Beginner",
                    Description = "Bass Class",
                    Picture = "img/Class3.svg",
                    CategoryId = 3,
                    Harga = 1500000,
                    AllCourse = CourseStatus.Active
                }
            };
            _course.AddRange(sampleCourse);
        }
    }
}
