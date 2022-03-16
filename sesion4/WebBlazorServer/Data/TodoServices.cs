using Microsoft.EntityFrameworkCore;
namespace WebBlazorServer.Data;

public interface ITodoServices
{
    Task<IEnumerable<TodoTask>> GetPendingTodos();
    Task MarkDone(int id);
    Task<int> AddNewTask (TodoTask task);
}

public class TodoServices: ITodoServices
{
    //INICIO Inyección de dependencia
    private readonly TodoContext _ctx;

    public TodoServices(TodoContext ctx){
        _ctx = ctx;
    }
    //FIN Inyección de dependencia

    public async Task<IEnumerable<TodoTask>> GetPendingTodos()
    {
         var result = _ctx.Todos.Where(x=> x.DoneWhen == null);
        return await result.ToListAsync();
    }

    public async Task MarkDone(int id)
    {
        //FindAsync -> metodo que busca por la clave primaria (es mas eficiente)
        //SingleAsync -> es equivalente FindAsync pero SingleAsync busca por cualquier campo.
        //var todo = await _ctx.Todos.SingleAsync(t=> t.Id==id);
        var todo = await _ctx.Todos.FindAsync(id);
        if(todo is not null)
        {
            todo.DoneWhen =DateTime.UtcNow;
            await _ctx.SaveChangesAsync();
        }
    }

    public async Task<int> AddNewTask (TodoTask task)
    {
        _ctx.Todos.Add(task);
        await _ctx.SaveChangesAsync();
        return task.Id;
    }
}
