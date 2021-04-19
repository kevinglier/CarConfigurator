using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CarConfigurator.BL.Interfaces;
using CarConfigurator.BL.Models;
using CarConfigurator.Models.ApiRequests;
using CarConfigurator.Models.ApiResults;

namespace CarConfigurator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarOrderController : ControllerBase
    {
        private readonly ILogger<CarModelController> _logger;
        private readonly ICarModelService _carModelService;
        private readonly ICarModelOptionService _carModelOptionService;
        private readonly ICarConfiguratorService _carConfiguratorService;
        private readonly IOrderService _orderService;

        public CarOrderController(
            ILogger<CarModelController> logger,
            ICarModelService carModelService,
            ICarModelOptionService carModelOptionService,
            ICarConfiguratorService carConfiguratorService,
            IOrderService orderService)
        {
            _logger = logger;
            _carModelService = carModelService;
            _carModelOptionService = carModelOptionService;
            _carConfiguratorService = carConfiguratorService;
            _orderService = orderService;
        }

        [HttpPost("send")]
        public IActionResult GetSummaryForUserOptionProductSelection(CarOrderDetails carOrderDetails)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiBadRequestResponse(ModelState));

            if (carOrderDetails == null)
                return BadRequest(new ApiResponse(400, "The order details are missing."));

            var order = _orderService.AddOrder(carOrderDetails);

            return Ok(new ApiOkResponse(order));
        }
    }
}
