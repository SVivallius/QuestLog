using Microsoft.AspNetCore.Mvc;
using QuestLog.Models;
namespace QuestLog.Controllers;

public class QuestController : Controller
{
    private static List<Quest> QuestLog = new List<Quest>();

    public IActionResult Index()
    {
        ViewBag.QuestLog = QuestLog;
        return View();
    }

    public IActionResult Add(string title, string description, int experience)
    {
        Quest q = new Quest()
        {
            name = title,
            description = description,
            experience = experience,
            complete = false
        };

        QuestLog.Add(q);
        return RedirectToAction("Index");
    }

    public IActionResult HandIn(string name)
    {
        var q = FindInList(name);
        var i = QuestLog.IndexOf(q);
        QuestLog[i].complete = true;
        return RedirectToAction("Index");
    }

    public IActionResult Reset(string name)
    {
        var q = FindInList(name);
        var i = QuestLog.IndexOf(q);
        QuestLog[i].complete = false;
        return RedirectToAction("Index");
    }

    private Quest FindInList(string name)
    {
        var q = QuestLog
            .Where(q => q.name.Equals(name))
            .FirstOrDefault();
        return q;
    }
}
