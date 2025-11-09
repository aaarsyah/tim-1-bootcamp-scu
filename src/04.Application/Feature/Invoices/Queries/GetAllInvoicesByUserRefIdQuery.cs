using AutoMapper;
using MediatR;
using MyApp.Base.Exceptions;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.Invoices.Queries;

public class GetAllInvoicesByUserRefIdQuery : IRequest<ApiResponse<List<InvoiceDto>>>
{
    public Guid UserRefId { get; set; }
}
public class GetAllInvoicesByUserRefIdQueryHandler : IRequestHandler<GetAllInvoicesByUserRefIdQuery, ApiResponse<List<InvoiceDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetAllInvoicesByUserRefIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ApiResponse<List<InvoiceDto>>> Handle(GetAllInvoicesByUserRefIdQuery request, CancellationToken cancellationToken)
    {
        // TODO: Tambahkan caching
        var invoices = await _unitOfWork.InvoiceManager.GetAllByUserRefIdAsync(request.UserRefId);
        if (invoices == null)
        {
            throw new NotFoundException("User RefId", request.UserRefId);
        }

        var invoicesDto = _mapper.Map<List<InvoiceDto>>(invoices);

        return ApiResponse<List<InvoiceDto>>.SuccessResult(invoicesDto);
    }
}
