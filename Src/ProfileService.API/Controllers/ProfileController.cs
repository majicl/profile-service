

using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Application.Profiles;
using ProfileService.Application.Profiles.Commands;
using ProfileService.Application.Profiles.Queries;

namespace ProfileService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private IMediator _mediator { get; }

        public ProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.OK)]
        public async Task<Unit> Post([FromBody]CreateProfile.CreateCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpPut()]
        [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.OK)]
        public async Task<Unit> Put([FromBody]ModifyProfile.ModifyCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpDelete]
        [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.OK)]
        public async Task<Unit> Delete([FromBody]DeleteProfile.DeleteCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpGet("{customerId}")]
        [ProducesResponseType(typeof(ProfileDto), (int)HttpStatusCode.OK)]
        public async Task<ProfileDto> Get(string customerId)
        {
            var result = await _mediator.Send(new GetProfileByCustonmerId.Query { CustomerId = new System.Guid(customerId) });
            return result;
        }
    }
}
