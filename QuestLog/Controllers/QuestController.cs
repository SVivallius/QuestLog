using Common.Services;
using Microsoft.AspNetCore.Mvc;
using QuestLog_MVC.Models.Quests;
using System.Text.Json;
namespace QuestLog.Controllers;

public class QuestController : Controller
{
    private static List<QuestViewModel> QuestLog = new List<QuestViewModel>();
    private readonly HttpService _http;
    private readonly ILogger<QuestController> _logger;

    public QuestController(HttpService http, ILogger<QuestController> logger)
    {
        _http = http;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.QuestLog = await _http.GetFromServiceAsync<List<QuestViewModel>>(ServiceHostList.Quests, "quests/");
        var cats = await _http.GetFromServiceAsync<List<CategoryViewModel>>(ServiceHostList.Quests, "categories/");
        ViewBag.Categories = cats;
        //_logger.LogInformation(cats.ToString());
        return View();
    }

    public async Task<IActionResult> Add(string name, string description, int experience, int category)
    {
        QuestViewModel q = new QuestViewModel()
        {
            Name = name,
            Description = description,
            Experience = experience,
            Complete = false,
            CategoryId = category
        };
        string payload = JsonSerializer.Serialize(q, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        _logger.LogInformation(payload);
        var result = await _http.PostToServiceAsync<string>(ServiceHostList.Quests, "quests/", payload);
        //_logger.LogInformation(result);

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> HandInOrReset(int id)
    {
        var q = await _http.GetFromServiceAsync<QuestViewModel>(ServiceHostList.Quests, $"quests/{id}");
        q.Complete = (!q.Complete);
        string payload = JsonSerializer.Serialize(q, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        var result = await _http.PutToServiceAsync<string>(ServiceHostList.Quests, $"quests/{q.Id}", payload);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Abandon(int id)
    {
        var result = await _http.DeleteFromServiceAsync<string>(ServiceHostList.Quests, $"quests/{id}");
        return RedirectToAction("Index");
    }
}
