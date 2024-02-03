using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CarPoolMvc.Controllers;
public class ManifestController : Controller
{
    private readonly ILogger<ManifestController> _logger;
    private readonly IConfiguration _configuration;

    public ManifestController(ILogger<ManifestController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        return View();
    }
}
