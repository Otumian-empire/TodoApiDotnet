using TodoApiDotnet.Models;

namespace TodoApiDotnet.DTO;

public class TodoModelDTO
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool IsComplete { get; set; }

    public TodoModelDTO() { }

    public TodoModelDTO(TodoModel item)
    {
        (Id, Name, IsComplete) = (item.Id, item.Name, item.IsComplete);
    }
}