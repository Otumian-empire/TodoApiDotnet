using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TodoApiDotnet.Models;
using TodoApiDotnet.DTO;

namespace TodoApiDotnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{

    private readonly TodoContext _context;

    public TodoController(TodoContext context)
    {
        _context = context;
    }

    // GET: /api/todo
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoModelDTO>>> GetTodoItems()
    {
        return await _context.TodoModels
            .Select(x => new TodoModelDTO(x))
            .ToListAsync();
    }

    // GET: api/todo/5
    // <snippet_GetByID>
    [HttpGet("{id}")]
    public async Task<ActionResult<TodoModelDTO>> GetTodoItem(int id)
    {
        var todoItem = await _context.TodoModels.FindAsync(id);

        if (todoItem == null)
        {
            return NotFound();
        }

        return new TodoModelDTO(todoItem);
    }

    // PUT: api/todo/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTodoItem(int id, TodoModelDTO todoDTO)
    {
        if (id != todoDTO.Id)
        {
            return BadRequest();
        }

        var todoItem = await _context.TodoModels.FindAsync(id);

        if (todoItem == null)
        {
            return NotFound();
        }

        todoItem.Name = todoDTO.Name;
        todoItem.IsComplete = todoDTO.IsComplete;

        try
        {
            await _context.SaveChangesAsync();
            return Ok("Update successful");
        }
        catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
        {
            return NotFound();
        }

        // return NoContent();
    }

    // POST: api/TodoItems
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    // <snippet_Create>
    [HttpPost]
    public async Task<ActionResult<TodoModelDTO>> PostTodoItem(TodoModelDTO todoDTO)
    {
        var todoItem = new TodoModel
        {
            IsComplete = todoDTO.IsComplete,
            Name = todoDTO.Name
        };

        _context.TodoModels.Add(todoItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetTodoItem),
            new { id = todoItem.Id },
            new TodoModelDTO(todoItem));
    }

    // </snippet_Create>

    // DELETE: api/TodoItems/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(int id)
    {
        var todoItem = await _context.TodoModels.FindAsync(id);
        if (todoItem == null)
        {
            return NotFound();
        }

        _context.TodoModels.Remove(todoItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TodoItemExists(int id)
    {
        return _context.TodoModels.Any(e => e.Id == id);
    }
}
