using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Properties.Domain.Exceptions;
using Properties.Api.IServices;
using Properties.Domain.Entities;
using Properties.Domain.DTOs.PropertyTrace;

namespace Properties.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PropertyTraceController : ControllerBase
    {
        private readonly IPropertyTraceService _propertyTraceService;
        private readonly ILogger<OwnerController> _logger;
        public PropertyTraceController(IPropertyTraceService propertyTraceService, ILogger<OwnerController> logger)
        {
            _propertyTraceService = propertyTraceService;
            _logger = logger;
        }

        /*
            <summary>
            This endpoint will try to create a propertyTrace
            </summary>
        */
        [HttpPost]
        public IActionResult Create([FromBody] CreatePropertyTraceRequest owner)
        {
            try
            {
                var propertyImageResult = _propertyTraceService.Create(owner);
                _logger.LogInformation("Request successful!");

                return Ok(propertyImageResult);

            }catch (Exception e)
            {
                _logger.LogError("Something went wrong! "+ e.Message);
                throw new InternalServerErrorException("Something went wrong! "+ e.Message);
            }
        }

        /*
            <summary>
            This endpoint will try to obtain a propertyTrace
            </summary>
        */
        [HttpGet("{propertyTraceId}")]
        public IActionResult Get(Guid propertyTraceId)
        {
            try
            {
                var propertyTrace = _propertyTraceService.Get(propertyTraceId);
                _logger.LogInformation("Request successful!");

                return Ok(propertyTrace);

            }
            catch (Exception e)
            {
                _logger.LogError("Something went wrong! "+ e.Message);
                throw new InternalServerErrorException("Something went wrong! "+ e.Message);
            }
        }

        /*
           <summary>
           This endpoint will try to obtain all the propertiesTrace
           </summary>
       */
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var propertiesTrace = _propertyTraceService.GetAll();
                _logger.LogInformation("Request successful!");

                return Ok(propertiesTrace);

            }
            catch (Exception e)
            {
                _logger.LogError("Something went wrong! "+ e.Message);
                throw new InternalServerErrorException("Something went wrong! "+ e.Message);
            }
        }
    }
}
