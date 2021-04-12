using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using CarConfigurator.BL.Interfaces;
using CarConfigurator.BL.Models;
using CarConfigurator.BL.Providers;

namespace CarConfigurator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarModelController : ControllerBase
    {
        private readonly ILogger<CarModelController> _logger;
        private readonly ICarModelProvider _carModelProvider;

        public CarModelController(ILogger<CarModelController> logger, ICarModelProvider carModelProvider)
        {
            _logger = logger;
            _carModelProvider = carModelProvider;
        }

        [HttpGet("List")]
        public IEnumerable<CarModel> GetList()
        {
            return _carModelProvider.GetCarModels(false);
        }

        [HttpGet("GetByName/{modelName}")]
        public CarModel GetByName(string modelName)
        {
            return _carModelProvider.GetCarModelByName(modelName, true);
        }
    }
}
