using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoApi.Controllers;

[ApiController]
[Route("/todos")]
public class TodoController : ControllerBase
{
    private readonly ITodoService _svc;
    public TodoController (ITodoService svc) {
        _svc = svc;
    }

    [HttpGet("pending")]
    public async Task<IEnumerable<TodoTask>> GetPending() => await _svc.GetPendingTodos();


    [HttpPost()]
    public async Task<IActionResult> CreateTask(int id, TodoTask task) {
        await _svc.NewTask(task);
        return Ok(task);
    }
}