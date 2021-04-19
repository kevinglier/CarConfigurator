﻿using System;
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

        [HttpPost("summary")]
        public IActionResult GetSummaryForUserOptionProductSelection(CarModelOptionSelectionRequest carModelOptionSelectionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiBadRequestResponse(ModelState));

            CarConfiguratorSummary priceSummary;
            try
            {
                priceSummary = _carConfiguratorProvider.GetSummaryForSelectedOptionProducts(carModelOptionSelectionRequest.CarModelEAN, carModelOptionSelectionRequest.OptionProducts, carModelOptionSelectionRequest.Code);
            }
            catch(Exception e)
            {
                return BadRequest(new ApiResponse(400, "The configuration is invalid."));
            }

            if (priceSummary == null)
                return BadRequest(new ApiResponse(400, "The summary couldn't be created."));

            return Ok(new ApiOkResponse(priceSummary));
        }

        [HttpGet("savedconfiguration/{code}")]
        public IActionResult GetSavedConfiguration(string code)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiBadRequestResponse(ModelState));

            var configuration = _carConfiguratorProvider.GetSavedUserConfiguration(code);

            return Ok(new ApiOkResponse(configuration));
        }
    }
}
