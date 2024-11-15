using Microsoft.AspNetCore.Mvc;

namespace QuestLog_MVC.Controllers;

public class CategoryController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
