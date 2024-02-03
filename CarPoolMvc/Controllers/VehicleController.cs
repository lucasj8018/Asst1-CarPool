using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CarPoolMvc.Controllers;
public class VehicleController : Controller
{
    private readonly ILogger<VehicleController> _logger;
    private readonly IConfiguration _configuration;

    public VehicleController(ILogger<VehicleController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        return View();
    }
    
}
