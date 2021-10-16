using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoAPI.Models;

namespace TodoAPI.Controllers.V1_1
{

    [ApiVersion("1.1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoDBContext _context;
        public TodoController(TodoDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodos()
        {
            return await _context.Todos.Where(x=>x.IsDeleted != 1).ToListAsync();
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Todo>> GetTodo(int id)
        {
            var obj = await _context.Todos.FindAsync(id);

            if (obj.IsDeleted == 1)
            {
                return NotFound();
            }

            return obj;
        }

        [HttpPut("{id}")]

        public async Task<ActionResult<Todo>> PutTodo(int id, Todo newObj)
        {
            if (id != newObj.Id)
            {
                return BadRequest();
            }

            var obj = await _context.Todos.FindAsync(id);

            if(obj.IsDeleted == 1)
            {
                return NotFound();
            }

            obj.IsComplete = newObj.IsComplete;
            obj.Name = newObj.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return obj;
        }

        [HttpPost]

        public async Task<ActionResult<Todo>> PostTodo(Todo newObj)
        {
            var obj = new Todo
            {
                Name = newObj.Name,
                IsComplete = 0,
                IsDeleted = 0
            };
            _context.Todos.Add(obj); 
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodo), new { id = obj.Id }, obj);
        }
        [HttpDelete("{id}")]

        public async Task<ActionResult<Todo>> DeleteTodo(int id)
        {
            var obj = await _context.Todos.FindAsync(id);
            obj.IsDeleted = 1;

            if (obj == null)
            {
                return NotFound();
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return obj;
        }

        private bool TodoExists(int id)
        {
            return _context.Todos.Any(e => e.Id == id);
        }
    }
}
