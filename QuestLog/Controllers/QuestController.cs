using Common.Services;
using Microsoft.AspNetCore.Mvc;
using QuestLog.Models;
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
        return View();
    }

    public async Task<IActionResult> Add(string title, string description, int experience)
    {
        QuestViewModel q = new QuestViewModel()
        {
            Name = title,
            Description = description,
            Experience = experience,
            Complete = false
        };
        string payload = JsonSerializer.Serialize(q, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        var resource = await _http.PostToServiceAsync<string>(ServiceHostList.Quests, "quests", payload);
        _logger.LogInformation(resource);

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> HandInOrReset(QuestViewModel q)
    {
        q.Complete = !q.Complete;
        string payload = JsonSerializer.Serialize(q, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        var result = await _http.PutToServiceAsync<string>(ServiceHostList.Quests, $"quests/{q.Id}", payload);
        _logger.LogInformation(result);

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Abandon(int id)
    {
        var result = await _http.DeleteFromServiceAsync<string>(ServiceHostList.Quests, $"quests/{id}");
        return RedirectToAction("Index");
    }
}
