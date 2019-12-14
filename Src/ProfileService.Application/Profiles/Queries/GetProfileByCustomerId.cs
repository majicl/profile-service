using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ProfileService.Application.Errors;
using ProfileService.Domain.Profiles;

namespace ProfileService.Application.Profiles.Queries
{
    public class GetProfileByCustonmerId
    {
        public class Query : IRequest<ProfileDto>
        {
            // should be provided with a Identity-Provider 
            public Guid CustomerId { get; set; }
        }

        public class Handler : IRequestHandler<Query, ProfileDto>
        {
            private readonly IProfileRepository _profileRepository;
            public Handler(IProfileRepository profileRepository)
            {
                _profileRepository = profileRepository;
            }

            public async Task<ProfileDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var profile = await _profileRepository.GetCustomerProfileByIdAsync(request.CustomerId, cancellationToken);

                if (profile != null)
                {
                    return new ProfileDto
                    {
                        CountryCode = profile.MobilePhone.CountryCode,
                        Email = profile.Email,
                        PhoneNumber = profile.MobilePhone.Number,
                        SSN = profile.SSN
                    };
                }

                throw new RestException(System.Net.HttpStatusCode.InternalServerError, new { Message = "Problem finding related data" });
            }
        }
    }
}
