using Project.DTO;

namespace Project.Services;

public interface ITodoService
{
    List<TodoDto> GetAll();

    TodoDto? GetById(int id);

    TodoDto Create(CreateTodoDto dto);

    TodoDto? Like(int id);
}