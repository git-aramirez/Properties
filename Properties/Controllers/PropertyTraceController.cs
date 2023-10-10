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
        public IActionResult Create([FromBody] PropertyTrace owner)
        {
            try
            {
                var propertyImageResult = _propertyTraceService.Create(owner);
                _logger.LogInformation("Request successful!");

                return Ok(propertyImageResult == true ? "The propertyTrace was updated successful!" : "");

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
