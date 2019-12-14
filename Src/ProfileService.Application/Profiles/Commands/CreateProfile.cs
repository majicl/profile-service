using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ProfileService.Application.Errors;
using ProfileService.Domain.Profiles;

namespace ProfileService.Application.Profiles.Commands
{
    public class CreateProfile
    {
        public class Command : IRequest
        {
            // should be provided with a Identity-Provider 
            public Guid CustomerId { get; set; }

            public string SSN { get; set; }
            public string CountryCode { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IProfileRepository _profileRepository;
            public Handler(IProfileRepository profileRepository)
            {
                _profileRepository = profileRepository;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var profile = new UserProfile(
                    request.SSN,
                    request.Email,
                    new MobilePhone
                    {
                        CountryCode = request.CountryCode,
                        Number = request.PhoneNumber
                    },
                    new Domain.Users.Customer(request.CustomerId)
                    );

                var success = await _profileRepository.CreateAsync(profile, cancellationToken) > 0;

                if (success) return Unit.Value;

                throw new RestException(System.Net.HttpStatusCode.InternalServerError, new { Message = "Problem saving changes" });
            }
        }
    }
}
