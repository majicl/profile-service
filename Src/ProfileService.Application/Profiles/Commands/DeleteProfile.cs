using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ProfileService.Application.Errors;
using ProfileService.Domain.Profiles;

namespace ProfileService.Application.Profiles.Commands
{
    public class DeleteProfile
    {
        public class DeleteCommand : IRequest
        {
            // should be provided with a Identity-Provider 
            public Guid CustomerId { get; set; }
        }

        public class Handler : IRequestHandler<DeleteCommand>
        {
            private readonly IProfileRepository _profileRepository;
            public Handler(IProfileRepository profileRepository)
            {
                _profileRepository = profileRepository;
            }

            public async Task<Unit> Handle(DeleteCommand request, CancellationToken cancellationToken)
            {
                var success = await _profileRepository.DeleteAsyncCustomerId(request.CustomerId, cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new RestException(System.Net.HttpStatusCode.InternalServerError, new { Message = "Problem saving changes" });
            }
        }
    }
}
