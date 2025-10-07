using MyApp.BlazorUI.Models;

namespace MyApp.BlazorUI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly List<CategoryItem> _category = new();
        private int _nextId = 1;

        public CategoryService()
        {
            SeedData();
        }

        public async Task<List<CategoryItem>> GetCategoryAsync()
        {
            await Task.Delay(100); 
            return _category.OrderBy(t => t.Id).ToList();
        }

        public async Task<CategoryItem> CreateCategoryAsync(CategoryItem category)
        {
            await Task.Delay(100);
            category.Id = _nextId++;
            _category.Add(category);
            return category;
        }

        public async Task<CategoryItem> UpdateCategoryAsync(CategoryItem category)
        {
            await Task.Delay(100);
            var existingCategory = _category.FirstOrDefault(t => t.Id == category.Id);
            if (existingCategory != null)
            {
                var index = _category.IndexOf(existingCategory);
                _category[index] = existingCategory;
            }
            return category;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            await Task.Delay(100);
            var category = _category.FirstOrDefault(t => t.Id == id);
            if (category != null)
            {
                _category.Remove(category);
                return true;
            }
            return false;
        }

        private void SeedData()
        {
            var sampleCategory = new List<CategoryItem>
            {
                new CategoryItem
                {
                    Id = _nextId++,
                    Name = "Drum",
                    Description = "Drum Class",
                    Picture = "img/Class1.svg",
                    AllCategory = CategoryStatus.Active
                },
                new CategoryItem
                {
                    Id = _nextId++,
                    Name = "Piano",
                    Description = "Piano Class",
                    Picture = "img/Class2.svg",
                    AllCategory = CategoryStatus.Active
                },
                new CategoryItem
                {
                    Id = _nextId++,
                    Name = "Gitar",
                    Description = "Gitar Class",
                    Picture = "img/Class3.svg",
                    AllCategory = CategoryStatus.Active
                },
                new CategoryItem
                {
                    Id = _nextId++,
                    Name = "Bass",
                    Description = "Bass Class",
                    Picture = "img/Class4.svg",
                    AllCategory = CategoryStatus.Active
                },
                new CategoryItem
                {
                    Id = _nextId++,
                    Name = "Biola",
                    Description = "Biola Class",
                    Picture = "img/Class5.svg",
                    AllCategory = CategoryStatus.Active
                }
            };
            _category.AddRange(sampleCategory);
        }
    }
}
