using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Properties.Domain.Exceptions;
using Properties.Api.IServices;
using Properties.Domain.Entities;
using Properties.Domain.DTOs.Owner;

namespace Properties.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _ownerService;
        private readonly ILogger<OwnerController> _logger;
        public OwnerController(IOwnerService ownerService, ILogger<OwnerController> logger)
        {
            _ownerService = ownerService;
            _logger = logger;
        }

        /*
            <summary>
            This endpoint will try to create a owner
            </summary>
        */
        [HttpPost]
        public IActionResult Create([FromBody] CreateOwnerRequest owner)
        {
            try
            {
                var ownerResult = _ownerService.Create(owner);
                _logger.LogInformation("Request successful!");
                
                return Ok(ownerResult);
            }
            catch (Exception e)
            {
                _logger.LogError("Something went wrong! "+ e.Message);
                throw new InternalServerErrorException("Something went wrong! "+ e.Message);
            }
        }

        /*
            <summary>
            This endpoint will try to obtain a owner
            </summary>
        */
        [HttpGet("{ownerId}")]
        public IActionResult Get(Guid ownerId)
        {
            try
            {
                var owner = _ownerService.Get(ownerId);
                _logger.LogInformation("Request successful!");

                return Ok(owner);
            }
            catch (Exception e)
            {
                _logger.LogError("Something went wrong! "+ e.Message);
                throw new InternalServerErrorException("Something went wrong! "+ e.Message);
            }
        }

        /*
            <summary>
            This endpoint will try to obtain all the owners
            </summary>
        */
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var owners = _ownerService.GetAll();
                _logger.LogInformation("Request successful!");

                return Ok(owners);
            }
            catch (Exception e)
            {
                _logger.LogError("Something went wrong! "+ e.Message);
                throw new InternalServerErrorException("Something went wrong! "+ e.Message);
            }
        }
    }
}
