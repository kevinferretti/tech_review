using MediatR;
using Microsoft.AspNetCore.Mvc;
using StargateAPI.Business.Commands;
using StargateAPI.Business.Dtos;
using StargateAPI.Business.Queries;

namespace StargateAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AstronautDutiesController : ControllerBase
    {
        private readonly ISender _sender;

        public AstronautDutiesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("{username}")]
        public async Task<AstronautDutiesDto> GetPersonsAstronautDuties(string username)
        {
            var result = await _sender.Send(new GetAstronautDutiesByUsername
            {
                Username = username
            });

            return new AstronautDutiesDto(result.Person, result.AstronautDuties);
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateAstronautDuty([FromBody] CreateAstronautDutyRequest request)
        {
            var result = await _sender.Send(request);
            return this.GetResponse(result);
        }
    }
}