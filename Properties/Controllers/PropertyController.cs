using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Properties.Api.Exceptions;
using Properties.Core.IServices;
using Properties.Domain.Models;

namespace Properties.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;
        private readonly ILogger<OwnerController> _logger;
        public PropertyController(IPropertyService propertyService, ILogger<OwnerController> logger)
        {
            _propertyService = propertyService;
            _logger=logger;
        }

        /*
            <summary>
            This endpoint will try to create a property
            </summary>
        */
        [HttpPost]
        public IActionResult Create([FromBody] Property property)
        {
            try
            {
                var propertyResult = _propertyService.Create(property);
                _logger.LogInformation("Request successful!");

                return Ok(propertyResult == true ? "The property was created successful!" : "");
            }
            catch (Exception e)
            {
                _logger.LogError("Something went wrong! "+ e.Message);
                throw new InternalServerErrorException("Something went wrong! "+ e.Message);
            }
        }


        /*
           <summary>
           This endpoint will try to obtain a property
           </summary>
       */
        [HttpGet("{propertyId}")]
        public IActionResult Get(Guid propertyId)
        {
            try
            {
                var property = _propertyService.Get(propertyId);
                _logger.LogInformation("Request successful!");

                return Ok(property);
            }
            catch (Exception e)
            {
                _logger.LogError("Something went wrong! "+ e.Message);
                throw new InternalServerErrorException("Something went wrong! "+ e.Message);
            }
        }

        /*
           <summary>
           This endpoint will try to obtain all the properties
           </summary>
       */
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var properties = _propertyService.GetAll();
                _logger.LogInformation("Request successful!");

                return Ok(properties);
            }
            catch (Exception e)
            {
                _logger.LogError("Something went wrong! "+ e.Message);
                throw new InternalServerErrorException("Something went wrong! "+ e.Message);
            }
        }

        /*
           <summary>
           This endpoint will try to obtain all properties between the two years
           </summary>
       */
        [HttpGet("{propertyId}/{yearLow}/{yearHigh}")]
        public IActionResult GetAllIntermediateYears(Guid propertyId, int yearLow, int yearHigh)
        {
            try
            {
                var property = _propertyService.GetAllIntermediateYears(propertyId, yearLow, yearHigh);
                _logger.LogInformation("Request successful!");

                return Ok(property);
            }
            catch (Exception e)
            {
                _logger.LogError("Something went wrong! "+ e.Message);
                throw new InternalServerErrorException("Something went wrong! "+ e.Message);
            }
        }

        /*
           <summary>
           This endpoint will try to change the property's price
           </summary>
       */
        [HttpPut("{propertyId}/{price}")]
        public IActionResult ChangePrice(Guid propertyId, decimal price)
        {
            var propertyToUpdate = _propertyService.Get(propertyId);

            if (propertyToUpdate == null)
            {
                _logger.LogError("The property does not exist!");
                throw new BadRequestException("The property does not exist!");
            }

            try
            {
                var result = _propertyService.ChangePrice(propertyId, price);
                _logger.LogInformation("Request successful!");

                return Ok(result == true ? "The price was changed successful!" : "");
            }
            catch (Exception e)
            {
                _logger.LogError("Something went wrong! "+ e.Message);
                throw new InternalServerErrorException("Something went wrong! "+ e.Message);
            }
        }

        /*
           <summary>
           This endpoint will try to update a property
           </summary>
       */
        [HttpPut]
        public IActionResult Update([FromBody] Property property)
        {
            var propertyToUpdate = _propertyService.Get(property.PropertyId);
            
            if (propertyToUpdate == null)
            {
                _logger.LogError("The property does not exist!");
                throw new BadRequestException("The property does not exist!");
            }

            try
            {
                var result = _propertyService.Update(property);
                _logger.LogInformation("Request successful!");

                return Ok(result == true ? "The property was updated successful!" : "");
            }
            catch (Exception e)
            {
                _logger.LogError("Something went wrong! "+ e.Message);
                throw new InternalServerErrorException("Something went wrong! "+ e.Message);
            }
        }
    }
}
