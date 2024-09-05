using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        private readonly ILCSDbContext _context;

        public ToDoListController(ILCSDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateTask([FromBody] TaskModel task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            task.Id = Guid.NewGuid();

            _context.Tasks.Add(task);
            _context.SaveChanges();

            var response = new
            {
                message = "Task created successfully",
                task = task
            };

            return CreatedAtAction(nameof(CreateTask), new { id = task.Id }, response);
        }

        [HttpGet("tasks")]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _context.Tasks.ToListAsync();
            return Ok(tasks);
        }

        [HttpGet("tasks/{id}")]
        public async Task<IActionResult> GetTaskById(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpPut("tasks/{id}")]
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] UpdateTaskModel updateTask)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound(new { message = "Task not found" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            task.Title = updateTask.Title;
            task.Description = updateTask.Description;
            task.Status = updateTask.Status;

            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Task updated successfully",
                task
            });
        }

        [HttpDelete("tasks/{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound(new { message = "Task not found" });
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Task deleted successfully" });
        }
    }
}
