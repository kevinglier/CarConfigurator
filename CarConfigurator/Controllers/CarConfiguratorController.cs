using System;
using System.Collections.Generic;
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
    public class CarConfiguratorController : ControllerBase
    {
        private readonly ILogger<CarModelController> _logger;
        private readonly ICarModelProvider _carModelProvider;
        private readonly ICarModelOptionProvider _carModelOptionProvider;
        private readonly ICarConfiguratorProvider _carConfiguratorProvider;

        public CarConfiguratorController(
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

        [HttpPost("pricesummary")]
        public IActionResult GetPriceSummaryForUserOptionProductSelection(CarModelOptionSelectionRequest carModelOptionSelectionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiBadRequestResponse(ModelState));

            CarConfiguratorPriceSummary priceSummary;
            try
            {
                priceSummary = _carConfiguratorProvider.GetSummaryForSelectedOptionProducts(carModelOptionSelectionRequest.CarModelEAN, carModelOptionSelectionRequest.OptionProducts);
            }
            catch(Exception)
            {
                return BadRequest(new ApiResponse(400, "The configuration is invalid."));
            }

            if (priceSummary == null)
                return BadRequest(new ApiResponse(400, "The summary couldn't be created."));

            return Ok(new ApiOkResponse(priceSummary));
        }
    }
}
