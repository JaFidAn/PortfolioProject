using Application.Core;
using Application.Features.Contacts.DTOs;
using Application.Repositories.ContactRepository;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Contacts.Commands;

public class CreateContact
{
    public class Command : IRequest<Result<Unit>>
    {
        public CreateContactDto ContactDto { get; set; } = null!;
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly IContactWriteRepository _contactWriteRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public Handler(
            IContactWriteRepository contactWriteRepository,
            IMapper mapper,
            IEmailService emailService)
        {
            _contactWriteRepository = contactWriteRepository;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var contact = _mapper.Map<Contact>(request.ContactDto);

            await _contactWriteRepository.AddAsync(contact);
            var result = await _contactWriteRepository.SaveAsync() > 0;

            if (!result)
            {
                return Result<Unit>.Failure("Contact form could not be submitted", 400);
            }

            var subject = "Portfolio - New Contact Message";
            var body = $@"
                <h3>New contact form submission from Portfolio project</h3>
                <p><strong>Name:</strong> {request.ContactDto.Name}</p>
                <p><strong>Email:</strong> {request.ContactDto.Email}</p>
                <p><strong>Message:</strong></p>
                <p>{request.ContactDto.Message}</p>
            ";

            await _emailService.SendEmailAsync(
                toEmail: "r.alagezov@gmail.com",
                subject: subject,
                body: body
            );

            return Result<Unit>.Success(Unit.Value, "Contact form submitted successfully");
        }
    }
}
