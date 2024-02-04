using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CarPoolMvc.Controllers;
public class MemberListController : Controller
{
    private readonly ILogger<MemberListController> _logger;
    private readonly IConfiguration _configuration;

    public MemberListController(ILogger<MemberListController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        return View();
    }


}
