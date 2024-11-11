using Application.Repository.ContactRepository;
using Application.Repository;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.Models;

namespace Application.Features.Contacts.Command
{
    public class UpdateContactCommand : IRequest<GeneralJsonResultHelper<bool>>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }

    public sealed class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, GeneralJsonResultHelper<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public UpdateContactCommandHandler(IUnitOfWork unitOfWork,
            IContactRepository contactRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        public async Task<GeneralJsonResultHelper<bool>> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            var contact = _mapper.Map<Contact>(request);

            var updatedContactEntity = await _contactRepository.GetAsync(contact.Id, cancellationToken);

            updatedContactEntity.FirstName = request.FirstName;
            updatedContactEntity.LastName = request.LastName;
            updatedContactEntity.PhoneNumber = request.PhoneNumber;
            updatedContactEntity.Email = request.Email;

            var result = _contactRepository.Update(updatedContactEntity);
            await _unitOfWork.Save(cancellationToken);

            return new GeneralJsonResultHelper<bool>() { Code = 0, Message = "OK", Data = result };
        }
    }
}
