using Common.Services;
using Microsoft.AspNetCore.Mvc;
using QuestLog.Models;
namespace QuestLog.Controllers;

public class QuestController : Controller
{
    private static List<QuestViewModel> QuestLog = new List<QuestViewModel>();
    private readonly HttpService _http;

    public QuestController(HttpService http)
    {
        _http = http;
    }

    public IActionResult Index()
    {
        ViewBag.QuestLog = _http.GetFromService(ServiceHostList.Quests, "quests/");
        return View();
    }

    public IActionResult Add(string title, string description, int experience)
    {
        QuestViewModel q = new QuestViewModel()
        {
            Name = title,
            Description = description,
            Experience = experience
        };

        //QuestLog.Add(q);

        

        return RedirectToAction("Index");
    }

    public IActionResult HandIn(string name)
    {
        var q = FindInList(name);
        var i = QuestLog.IndexOf(q);
        QuestLog[i].Complete = true;
        return RedirectToAction("Index");
    }

    public IActionResult Reset(string name)
    {
        var q = FindInList(name);
        var i = QuestLog.IndexOf(q);
        QuestLog[i].Complete = false;
        return RedirectToAction("Index");
    }

    private QuestViewModel FindInList(string name)
    {
        var q = QuestLog
            .Where(q => q.Name.Equals(name))
            .FirstOrDefault();
        return q;
    }
}
