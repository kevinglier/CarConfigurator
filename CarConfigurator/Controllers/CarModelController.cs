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
        private readonly ICarModelService _carModelService;
        private readonly ICarModelOptionService _carModelOptionService;
        private readonly ICarConfiguratorService _carConfiguratorService;

        public CarModelController(
            ILogger<CarModelController> logger,
            ICarModelService carModelService,
            ICarModelOptionService carModelOptionService,
            ICarConfiguratorService carConfiguratorService)
        {
            _logger = logger;
            _carModelService = carModelService;
            _carModelOptionService = carModelOptionService;
            _carConfiguratorService = carConfiguratorService;
        }

        [HttpGet("List")]
        public IActionResult GetList()
        {
            return Ok(new ApiOkResponse(_carModelService.GetCarModels()));
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiBadRequestResponse(ModelState));

            var model = _carModelService.GetCarModelByName(name);

            if (model == null)
                return NotFound(new ApiResponse(400, "Car model invalid."));

            return Ok(new ApiOkResponse(model));
        }

        [HttpGet("{name}/option/list")]
        public IActionResult GetOptionList(string name)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiBadRequestResponse(ModelState));

            var model = _carModelService.GetCarModelByName(name);

            if (model == null)
                return NotFound(new ApiResponse(400, "Car model invalid."));

            var carConfigurator = _carConfiguratorService.GetCarModelsOptionsAndProducts(model);

            return Ok(new ApiOkResponse(carConfigurator));
        }
    }
}
