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
    public class PropertyImageController : ControllerBase
    {
        private readonly IPropertyImageService _propertyImageService;
        private readonly ILogger<OwnerController> _logger;
        public PropertyImageController(IPropertyImageService propertyImageService, ILogger<OwnerController> logger)
        {
            _propertyImageService = propertyImageService;
            _logger = logger;
        }

        /*
            <summary>
            This endpoint will try to create a propertyImage
            </summary>
        */
        [HttpPost]
        public IActionResult Create([FromBody] PropertyImage propertyImage)
        {
            try
            {
                var propertyImageResult = _propertyImageService.Create(propertyImage);
                _logger.LogInformation("Request successful!");

                return Ok(propertyImageResult  == true ? "The propertyImage was created successful!" : "");
            }
            catch (Exception e)
            {
                _logger.LogError("Something went wrong! "+ e.Message);
                throw new InternalServerErrorException("Something went wrong! "+ e.Message);
            }
        }

        /*
            <summary>
            This endpoint will try to obtain a propertyImage
            </summary>
        */
        [HttpGet("{propertyImageId}")]
        public IActionResult Get(Guid propertyImageId)
        {
            try
            {
                var propertyImage = _propertyImageService.Get(propertyImageId);
                _logger.LogInformation("Request successful!");

                return Ok(propertyImage);
            }
            catch (Exception e)
            {
                _logger.LogError("Something went wrong! "+ e.Message);
                throw new InternalServerErrorException("Something went wrong! "+ e.Message);
            }
        }

        /*
            <summary>
            This endpoint will try to obtain all the propertiesImage
            </summary>
        */
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var propertiesImage = _propertyImageService.GetAll();
                _logger.LogInformation("Request successful!");

                return Ok(propertiesImage);

            }catch (Exception e)
            {
                _logger.LogError("Something went wrong! "+ e.Message);
                throw new InternalServerErrorException("Something went wrong! "+ e.Message);
            }
        }
    }
}
