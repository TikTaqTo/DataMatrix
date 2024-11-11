using Application.Repository.ContactRepository;
using Application.Repository;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.Models;

namespace Application.Features.Contacts.Command
{
    public class DeleteContactCommand : IRequest<GeneralJsonResultHelper<bool>>
    {
        public Guid Id { get; set; }
    }

    public sealed class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, GeneralJsonResultHelper<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public DeleteContactCommandHandler(IUnitOfWork unitOfWork,
            IContactRepository contactRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        public async Task<GeneralJsonResultHelper<bool>> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            var contact = _mapper.Map<Contact>(request);

            var deletedContactEntity = await _contactRepository.GetAsync(contact.Id, cancellationToken);

            var result = _contactRepository.Delete(deletedContactEntity);
            await _unitOfWork.Save(cancellationToken);

            return new GeneralJsonResultHelper<bool>() { Code = 0, Message = "OK", Data = result };
        }
    }
}
