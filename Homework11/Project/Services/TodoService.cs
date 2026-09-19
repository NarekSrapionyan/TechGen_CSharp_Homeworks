using Project.Data;
using Project.DTO;
using Project.Models;

namespace Project.Services;

public class TodoService : ITodoService
{
    private readonly AppDbContext _dbContext;

    public TodoService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<TodoDto> GetAll()
    {
        var todos = _dbContext.Todos.ToList();

        var result = new List<TodoDto>();

        foreach (var todo in todos)
        {
            result.Add(ToDto(todo));
        }

        return result;
    }

    public TodoDto? GetById(int id)
    {
        var todo = _dbContext.Todos.FirstOrDefault(todo => todo.Id == id);

        if (todo == null)
        {
            return null;
        }

        return ToDto(todo);
    }

    public TodoDto Create(CreateTodoDto dto)
    {
        var now = DateTime.UtcNow;

        var todo = new Todo
        {
            Title = dto.Title,
            Description = dto.Description,
            LikeCount = 0,
            CreatedAt = now,
            UpdatedAt = now
        };

        _dbContext.Todos.Add(todo);
        _dbContext.SaveChanges();

        return ToDto(todo);
    }

    public TodoDto? Like(int id)
    {
        var todo = _dbContext.Todos.FirstOrDefault(todo => todo.Id == id);

        if (todo == null)
        {
            return null;
        }

        todo.LikeCount += 1;
        todo.UpdatedAt = DateTime.UtcNow;

        _dbContext.SaveChanges();

        return ToDto(todo);
    }

    private TodoDto ToDto(Todo todo)
    {
        var dto = new TodoDto
        {
            Id = todo.Id,
            Title = todo.Title,
            Description = todo.Description,
            LikeCount = todo.LikeCount,
            CreatedAt = todo.CreatedAt,
            UpdatedAt = todo.UpdatedAt
        };

        return dto;
    }
}