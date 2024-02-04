using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CarPoolMvc.Controllers;
public class TripController : Controller
{
    private readonly ILogger<TripController> _logger;
    private readonly IConfiguration _configuration;

    public TripController(ILogger<TripController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        return View();
    }
}
