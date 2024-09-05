using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using ToDoList.Controllers;
using ToDoList.Models;

namespace ToDoList.Tests
{
    public class ToDoListControllerTests
    {
        private readonly DbContextOptions<ILCSDbContext> _options;

        public ToDoListControllerTests()
        {
            _options = new DbContextOptionsBuilder<ILCSDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }

        [Fact]
        public void CreateTask_ValidModel_ReturnsCreatedResponse()
        {
            using (var context = new ILCSDbContext(_options))
            {
                var controller = new ToDoListController(context);
                var task = new TaskModel
                {
                    Id = Guid.NewGuid(),
                    Title = "Test Task",
                    Description = "This is a test task",
                    Status = "pending"
                };

                var result = controller.CreateTask(task) as ObjectResult;

                Assert.NotNull(result);
                Assert.Equal(201, result.StatusCode);
                Assert.Contains("Task created successfully", result.Value.ToString());
            }
        }

        [Fact]
        public void CreateTask_InvalidModel_ReturnsBadRequest()
        {
            using (var context = new ILCSDbContext(_options))
            {
                var controller = new ToDoListController(context);
                controller.ModelState.AddModelError("Title", "Required");

                var task = new TaskModel
                {
                    Id = Guid.NewGuid(),
                    Description = "This task has no title",
                    Status = "pending"
                };

                var result = controller.CreateTask(task) as BadRequestObjectResult;

                Assert.NotNull(result);
                Assert.Equal(400, result.StatusCode);
            }
        }

        [Fact]
        public async Task GetAllTasks_ReturnsOkResult_WithTasksList()
        {
            using (var context = new ILCSDbContext(_options))
            {
                // Arrange
                var task1 = new TaskModel
                {
                    Id = Guid.NewGuid(),
                    Title = "Task 1",
                    Description = "Description 1",
                    Status = "pending"
                };
                var task2 = new TaskModel
                {
                    Id = Guid.NewGuid(),
                    Title = "Task 2",
                    Description = "Description 2",
                    Status = "completed"
                };
                context.Tasks.Add(task1);
                context.Tasks.Add(task2);
                await context.SaveChangesAsync();

                var controller = new ToDoListController(context);

                // Act
                var result = await controller.GetAllTasks() as OkObjectResult;

                // Assert
                Assert.NotNull(result);
                Assert.Equal(200, result.StatusCode);

                var tasks = result.Value as List<TaskModel>;
                Assert.NotNull(tasks);
                Assert.Equal(2, tasks.Count);
                Assert.Contains(tasks, t => t.Id == task1.Id);
                Assert.Contains(tasks, t => t.Id == task2.Id);
            }
        }

        [Fact]
        public async Task GetTaskById_ValidId_ReturnsOkResult_WithTask()
        {
            using (var context = new ILCSDbContext(_options))
            {
                var task = new TaskModel { Id = Guid.NewGuid(), Title = "Task 1", Description = "Test Task 1", Status = "pending" };
                context.Tasks.Add(task);
                await context.SaveChangesAsync();

                var controller = new ToDoListController(context);

                var result = await controller.GetTaskById(task.Id) as OkObjectResult;

                Assert.NotNull(result);
                Assert.Equal(200, result.StatusCode);

                var returnedTask = result.Value as TaskModel;
                Assert.NotNull(returnedTask);
                Assert.Equal(task.Id, returnedTask.Id);
                Assert.Equal(task.Title, returnedTask.Title);
                Assert.Equal(task.Description, returnedTask.Description);
                Assert.Equal(task.Status, returnedTask.Status);
            }
        }

        [Fact]
        public async Task GetTaskById_InvalidId_ReturnsNotFound()
        {
            using (var context = new ILCSDbContext(_options))
            {
                var invalidId = Guid.NewGuid();
                var controller = new ToDoListController(context);

                var result = await controller.GetTaskById(invalidId);

                Assert.IsType<NotFoundResult>(result);
            }
        }

        public async Task UpdateTask_ValidId_ReturnsOkResult_WithUpdatedTask()
        {
            using (var context = new ILCSDbContext(_options))
            {
                var taskId = Guid.NewGuid();
                var originalTask = new TaskModel
                {
                    Id = taskId,
                    Title = "Original Task",
                    Description = "Original Description",
                    Status = "pending"
                };
                context.Tasks.Add(originalTask);
                await context.SaveChangesAsync();

                var updateTask = new UpdateTaskModel
                {
                    Title = "Updated Task",
                    Description = "Updated Description",
                    Status = "completed"
                };
                var controller = new ToDoListController(context);

                var result = await controller.UpdateTask(taskId, updateTask) as OkObjectResult;

                Assert.NotNull(result);
                Assert.Equal(200, result.StatusCode);

                var response = result.Value as dynamic;
                Assert.Equal("Task updated successfully", response.message);

                var updatedEntity = await context.Tasks.FindAsync(taskId);
                Assert.NotNull(updatedEntity);
                Assert.Equal("Updated Task", updatedEntity.Title);
                Assert.Equal("Updated Description", updatedEntity.Description);
                Assert.Equal("completed", updatedEntity.Status);
            }
        }

        [Fact]
        public async Task UpdateTask_InvalidId_ReturnsNotFound()
        {
            using (var context = new ILCSDbContext(_options))
            {
                var invalidId = Guid.NewGuid();
                var updateTask = new UpdateTaskModel
                {
                    Title = "Updated Task",
                    Description = "Updated Description",
                    Status = "completed"
                };
                var controller = new ToDoListController(context);

                var result = await controller.UpdateTask(invalidId, updateTask);

                Assert.IsType<NotFoundObjectResult>(result);
                var notFoundResult = result as NotFoundObjectResult;
                Assert.Equal("Task not found", ((dynamic)notFoundResult.Value).message);
            }
        }

        [Fact]
        public async Task DeleteTask_ValidId_ReturnsOkResult_WithSuccessMessage()
        {
            using (var context = new ILCSDbContext(_options))
            {
                var taskId = Guid.NewGuid();
                var task = new TaskModel
                {
                    Id = taskId,
                    Title = "Task to Delete",
                    Description = "Description to Delete",
                    Status = "pending"
                };
                context.Tasks.Add(task);
                await context.SaveChangesAsync();

                var controller = new ToDoListController(context);

                var result = await controller.DeleteTask(taskId) as OkObjectResult;

                Assert.NotNull(result);
                Assert.Equal(200, result.StatusCode);

                var response = result.Value as dynamic;
                Assert.Equal("Task deleted successfully", response.message);

                var deletedTask = await context.Tasks.FindAsync(taskId);
                Assert.Null(deletedTask);
            }
        }

        [Fact]
        public async Task DeleteTask_InvalidId_ReturnsNotFound()
        {
            using (var context = new ILCSDbContext(_options))
            {
                var invalidId = Guid.NewGuid();
                var controller = new ToDoListController(context);

                var result = await controller.DeleteTask(invalidId);

                Assert.IsType<NotFoundObjectResult>(result);
                var notFoundResult = result as NotFoundObjectResult;
                Assert.Equal("Task not found", ((dynamic)notFoundResult.Value).message);
            }
        }
    }
}