using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using webMVC.ViewsModels;

namespace webMVC.Controllers;

public class HomeController : Controller
{

    //INICIO Inyección de dependencia
    private readonly ILogger<HomeController> _logger;
    private readonly ITodoServices _todoService;

    public HomeController(ILogger<HomeController> logger, ITodoServices todoServices)
    {
        _logger = logger;
        _todoService = todoServices;
    }
    //FIN Inyección de dependencia

    //NO USAR NUNCA: SE trata de una espera bloqueante que da problema.
    //  public IActionResult Index()
    //  {
    //      var tasks= _todoService.GetPendingTodos().Result();
    //      return View();
    //  }

    //Para llamar a metodos asincrono hay que modificar el metodo.
    public async Task<IActionResult> Index()
    {
        //NO USAR NUNCA: SE trata de una espera bloqueante que da problema.
        //var tasks= _todoService.GetPendingTodos().Result();

        var tasks =  await _todoService.GetPendingTodos();
        return View(tasks);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Privacy2()
    {
        return View();
    }

    public async Task<IActionResult> MarkDone(int id)
    {
        await _todoService.MarkDone(id);
        return RedirectToAction("index");
    }

    [HttpPost]
    public async Task<IActionResult> NewTask (TodoTask task)
    {
        await _todoService.NewTask(task);
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
