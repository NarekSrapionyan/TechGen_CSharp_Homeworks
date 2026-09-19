using Microsoft.AspNetCore.Mvc;
using Project.DTO;
using Project.Services;

namespace Project.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodoController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpPost]
    public IActionResult Create(CreateTodoDto dto)
    {
        var todo = _todoService.Create(dto);

        return Ok(todo);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var todos = _todoService.GetAll();

        return Ok(todos);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var todo = _todoService.GetById(id);

        if (todo == null)
        {
            return NotFound();
        }

        return Ok(todo);
    }

    [HttpPost("{id}/like")]
    public IActionResult Like(int id)
    {
        var todo = _todoService.Like(id);

        if (todo == null)
        {
            return NotFound();
        }

        return Ok(todo);
    }
}