using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CarConfigurator.BL.Interfaces;
using CarConfigurator.Models.ApiResults;

namespace CarConfigurator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarModelController : ControllerBase
    {
        private readonly ILogger<CarModelController> _logger;
        private readonly ICarModelProvider _carModelProvider;
        private readonly ICarModelOptionProvider _carModelOptionProvider;
        private readonly ICarConfiguratorProvider _carConfiguratorProvider;

        public CarModelController(
            ILogger<CarModelController> logger,
            ICarModelProvider carModelProvider,
            ICarModelOptionProvider carModelOptionProvider,
            ICarConfiguratorProvider carConfiguratorProvider)
        {
            _logger = logger;
            _carModelProvider = carModelProvider;
            _carModelOptionProvider = carModelOptionProvider;
            _carConfiguratorProvider = carConfiguratorProvider;
        }

        [HttpGet("List")]
        public IActionResult GetList()
        {
            return Ok(new ApiOkResponse(_carModelProvider.GetCarModels()));
        }

        [HttpGet("{ean}")]
        public IActionResult GetByEAN(string ean)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiBadRequestResponse(ModelState));

            var model = _carModelProvider.GetCarModelByEAN(ean);

            if (model == null)
                return NotFound(new ApiResponse(400, "Car model invalid."));

            return Ok(new ApiOkResponse(model));
        }

        [HttpGet("{ean}/option/list")]
        public IActionResult GetOptionList(string ean)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiBadRequestResponse(ModelState));

            var model = _carModelProvider.GetCarModelByEAN(ean);

            if (model == null)
                return NotFound(new ApiResponse(400, "Car model invalid."));

            var carConfigurator = _carConfiguratorProvider.GetCarModelsOptionsAndProducts(model);

            return Ok(new ApiOkResponse(carConfigurator));
        }
    }
}
