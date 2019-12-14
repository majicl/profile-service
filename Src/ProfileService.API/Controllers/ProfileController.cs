﻿

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
    public class ProfileController: ControllerBase
    {
        private IMediator _mediator { get; }

        public ProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.OK)]
        public async Task<Unit> Post([FromBody]CreateProfile.Command command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpPut]
        [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.OK)]
        public async Task<Unit> Put([FromBody]ModifyProfile.Command command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpPut]
        [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.OK)]
        public async Task<Unit> Delete([FromBody]DeleteProfile.Command command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpPut]
        [ProducesResponseType(typeof(ProfileDto), (int)HttpStatusCode.OK)]
        public async Task<ProfileDto> Delete([FromBody]GetProfileByCustonmerId.Query query)
        {
            var result = await _mediator.Send(query);
            return result;
        }
    }
}
