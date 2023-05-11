using Microsoft.AspNetCore.Mvc;
using rep_sprzetu.Models;
using System.Diagnostics;

namespace rep_sprzetu.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ConnectDb connectDb = new ConnectDb();
            connectDb.CreateDbConnection();
            var listOfHardware = connectDb.AllHardware();
            connectDb.Close();
            return View(listOfHardware);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult InsertNewElement(string[] hardwareList)
        {
            HardwareViewModel hardware = new HardwareViewModel();

            hardware.Name = hardwareList[0];
            hardware.Tag = hardwareList[1]?.Split(',').ToList();
            hardware.Description = hardwareList[2];
            hardware.Owner = hardwareList[3];
            hardware.DateOfAssigment = Convert.ToDateTime(hardwareList[4]);
            hardware.DateOfPurchase = Convert.ToDateTime(hardwareList[5]);
            hardware.PreviousOwner = hardwareList[6];
            hardware.Filia = hardwareList[7];
            hardware.IdOfHardware = hardwareList[8];
            hardware.SerwisTag = hardwareList[9];
            hardware.DateOfProduction = Convert.ToInt32(hardwareList[10]);
            ConnectDb connectDb = new ConnectDb();
            connectDb.NewRowInDb(hardware);

            return View();
        }
    }
}