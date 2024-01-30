using Microsoft.AspNetCore.Mvc;

namespace ImageStorageAPI.Controller;

public class Auth : Microsoft.AspNetCore.Mvc.Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}