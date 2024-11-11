using Application.Models;
using Application.Repository;
using Application.Repository.ContactRepository;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Contacts.Command
{
    public class CreateContactCommand : IRequest<GeneralJsonResultHelper<bool>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }

    public sealed class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, GeneralJsonResultHelper<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public CreateContactCommandHandler(IUnitOfWork unitOfWork,
            IContactRepository contactRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        public async Task Handle(CreateContactCommand request,
            CancellationToken cancellationToken)
        {
            var contact = _mapper.Map<Contact>(request);
            _contactRepository.Create(contact);
            await _unitOfWork.Save(cancellationToken);
        }

        async Task<GeneralJsonResultHelper<bool>> IRequestHandler<CreateContactCommand, GeneralJsonResultHelper<bool>>.Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            var contact = _mapper.Map<Contact>(request);
            var result = _contactRepository.Create(contact);
            await _unitOfWork.Save(cancellationToken);
            return new GeneralJsonResultHelper<bool>() { Code = 0, Message = "OK", Data = result };
        }
    }
}
