using MediatR;
using Microsoft.AspNetCore.Mvc;
using StargateAPI.Business.Commands;
using StargateAPI.Business.Dtos;
using StargateAPI.Business.Queries;

namespace StargateAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly ISender _sender;

        public PeopleController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetPeople()
        {
            var result = await _sender.Send(new GetPeopleRequest());

            return this.GetResponse(result);
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetPersonByName(string username)
        {
            var result = await _sender.Send(new GetPersonByUsernameRequest()
            {
                Username = username
            });

            return this.GetResponse(result);
        }

        [HttpPost("")]
        public async Task<IActionResult> CreatePerson(CreatePersonDto dto)
        {
            var result = await _sender.Send(new CreatePersonRequest()
            {
                Username = dto.Username,
                DisplayName = dto.DisplayName,
                DateOfBirth = dto.DateOfBirth
            });

            return this.GetResponse(result);
        }
    }
}