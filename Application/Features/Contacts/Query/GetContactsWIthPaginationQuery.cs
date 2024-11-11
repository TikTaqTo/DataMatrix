using Application.Models;
using Application.Repository;
using Application.Repository.ContactRepository;
using AutoMapper;
using MediatR;

namespace Application.Features.Contacts.Query
{
    public class GetContactsWIthPagination : IRequest<PaginationResult<ContactReadModel>>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }

    public sealed class GetContactsWIthPaginationHandler : IRequestHandler<GetContactsWIthPagination, PaginationResult<ContactReadModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public GetContactsWIthPaginationHandler(IUnitOfWork unitOfWork,
            IContactRepository contactRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        public async Task<PaginationResult<ContactReadModel>> Handle(GetContactsWIthPagination request, CancellationToken cancellationToken)
        {
            var contactsByPagination = await _contactRepository.GetByPagination(request.PageSize, request.PageNumber, cancellationToken);
            var mappedContacts = _mapper.Map<List<ContactReadModel>>(contactsByPagination.result);
            return new PaginationResult<ContactReadModel>() 
            { 
                AllCount=contactsByPagination.allCount, 
                CurrentPage = request.PageNumber, 
                PageSize = contactsByPagination.pagesAmount, 
                PagesAmount = contactsByPagination.pagesAmount, 
                List = mappedContacts 
            };
        }
    }
}
