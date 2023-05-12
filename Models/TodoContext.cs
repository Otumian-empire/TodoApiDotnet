using Microsoft.EntityFrameworkCore;

namespace TodoApiDotnet.Models;

public class TodoContext : DbContext
{
    public TodoContext(DbContextOptions<TodoContext> options)
        : base(options)
    {
    }

    public DbSet<TodoModel> TodoModels { get; set; } = null!;
}