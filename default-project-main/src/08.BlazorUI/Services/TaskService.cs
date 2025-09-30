// using MyApp.BlazorUI.Models;

// namespace MyApp.BlazorUI.Services
// {
//     public class TaskService : ITaskService
//     {
//         private readonly List<TaskItem> _tasks = new();
//         private int _nextId = 1;

//         public TaskService()
//         {
//             // Seed with sample data
//             SeedData();
//         }

//         public async Task<List<TaskItem>> GetTasksAsync()
//         {
//             await Task.Delay(100); // Simulate async operation
//             return _tasks.OrderByDescending(t => t.CreatedDate).ToList();
//         }

//         public async Task<TaskItem?> GetTaskByIdAsync(int id)
//         {
//             await Task.Delay(50);
//             return _tasks.FirstOrDefault(t => t.Id == id);
//         }

//         public async Task<TaskItem> CreateTaskAsync(TaskItem task)
//         {
//             await Task.Delay(100);
//             task.Id = _nextId++;
//             task.CreatedDate = DateTime.Now;
//             _tasks.Add(task);
//             return task;
//         }

//         public async Task<TaskItem> UpdateTaskAsync(TaskItem task)
//         {
//             await Task.Delay(100);
//             var existingTask = _tasks.FirstOrDefault(t => t.Id == task.Id);
//             if (existingTask != null)
//             {
//                 var index = _tasks.IndexOf(existingTask);
//                 _tasks[index] = existingTask;
//             }
//             return task;
//         }

//         public async Task<bool> DeleteTaskAsync(int id)
//         {
//             await Task.Delay(100);
//             var task = _tasks.FirstOrDefault(t => t.Id == id);
//             if (task != null)
//             {
//                 _tasks.Remove(task);
//                 return true;
//             }
//             return false;
//         }

//         public async Task<List<TaskItem>> GetTasksByProjectAsync(int projectId)
//         {
//             await Task.Delay(100);
//             return _tasks.Where(t => t.ProjectId == projectId).ToList();
//         }

//         public async Task<List<TaskItem>> GetTasksByUserAsync(int userId)
//         {
//             await Task.Delay(100);
//             return _tasks.Where(t => t.AssignedUserId == userId).ToList();
//         }

//         private void SeedData()
//         {
//             var sampleTasks = new List<TaskItem>
//             {
//                 new TaskItem
//                 {
//                     Id = _nextId++,
//                     Title = "Setup Development Environment",
//                     Description = "Install required tools and configure development environment",
//                     Status = WorkTaskStatus.Done,
//                     Priority = TaskPriority.High,
//                     AssignedUserId = 1,
//                     AssignedUserName = "John Doe",
//                     ProjectId = 1,
//                     ProjectName = "Website Redesign",
//                     DueDate = DateTime.Now.AddDays(-5)
//                 },
//                 new TaskItem
//                 {
//                     Id = _nextId++,
//                     Title = "Design User Interface",
//                     Description = "Create mockups and wireframes for the new interface",
//                     Status = WorkTaskStatus.InProgress, //Bentrok dengan class bawaan MudBlazor TaskStatus
//                     Priority = TaskPriority.Medium,
//                     AssignedUserId = 2,
//                     AssignedUserName = "Jane Smith",
//                     ProjectId = 1,
//                     ProjectName = "Website Redesign",
//                     DueDate = DateTime.Now.AddDays(3)
//                 },
//                 new TaskItem
//                 {
//                     Id = _nextId++,
//                     Title = "Implement Authentication",
//                     Description = "Add user login and registration functionality",
//                     Status = WorkTaskStatus.Todo,
//                     Priority = TaskPriority.High,
//                     AssignedUserId = 1,
//                     AssignedUserName = "John Doe",
//                     ProjectId = 1,
//                     ProjectName = "Website Redesign",
//                     DueDate = DateTime.Now.AddDays(7)
//                 }
//             };

//             _tasks.AddRange(sampleTasks);
//         }
//     }
// }
