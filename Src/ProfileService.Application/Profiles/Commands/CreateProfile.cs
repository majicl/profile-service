using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ProfileService.Application.Errors;
using ProfileService.Domain.Profiles;

namespace ProfileService.Application.Profiles.Commands
{
    public class CreateProfile
    {
        public class CreateCommand : IRequest
        {
            // should be provided with a Identity-Provider 
            public Guid CustomerId { get; set; }

            public string SSN { get; set; }
            public string CountryCode { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
        }

        public class Handler : IRequestHandler<CreateCommand>
        {
            private readonly IProfileRepository _profileRepository;
            public Handler(IProfileRepository profileRepository)
            {
                _profileRepository = profileRepository;
            }

            public async Task<Unit> Handle(CreateCommand request, CancellationToken cancellationToken)
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

        public class CreateProfileValidator : AbstractValidator<CreateCommand>
        {
            public CreateProfileValidator()
            {
                RuleFor(_ => _.PhoneNumber)
                    .MinimumLength(9).When(_ => !string.IsNullOrEmpty(_.PhoneNumber)) // validate lemgth if it is not empty
                    .MaximumLength(9).When(_ => !string.IsNullOrEmpty(_.PhoneNumber)) // validate lemgth if it is not empty
                    .Must(_ => long.TryParse(_, out long x)).When(_ => !string.IsNullOrEmpty(_.PhoneNumber)); // validate being numeric if it is not empty

                RuleFor(_ => _.CountryCode)
                    .Must(_ => int.TryParse(_, out int x)).When(_ => !string.IsNullOrEmpty(_.CountryCode)) // validate being numeric if it is not empty
                    .Length(1, 3).When(_ => !string.IsNullOrEmpty(_.CountryCode)); // validate lemgth if it is not empty

                When(_ => !string.IsNullOrEmpty(_.PhoneNumber), () => // validate PhoneNumber emptiness if CountryCode is not epmty
                {
                    RuleFor(_ => _.CountryCode).NotEmpty();
                });

                When(_ => !string.IsNullOrEmpty(_.CountryCode), () => // validate CountryCode emptiness if PhoneNumber is not epmty
                {
                    RuleFor(_ => _.PhoneNumber).NotEmpty();
                });

                RuleFor(_ => _.Email)
                    .NotEmpty()
                    .EmailAddress().When(_ => string.IsNullOrWhiteSpace(_.Email) == false); // validate email format if it is not empty

                RuleFor(_ => _.SSN)
                    .NotEmpty()
                    .Must(_ => long.TryParse(_, out long x)) 
                    .Length(9, 12);
            }
        }
    }
}
