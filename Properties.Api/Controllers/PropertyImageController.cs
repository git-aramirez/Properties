using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Properties.Domain.Exceptions;
using Properties.Api.IServices;
using Properties.Domain.Entities;
using Properties.Domain.DTOs.PropertyImage;

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
        public IActionResult Create([FromBody] CreatePropertyImageResquest propertyImage)
        {
            try
            {
                var propertyImageResult = _propertyImageService.Create(propertyImage);
                _logger.LogInformation("Request successful!");

                return Ok(propertyImageResult);
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
