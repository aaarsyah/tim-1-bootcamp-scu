using AutoMapper;
using MediatR;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.Invoices.Queries;

public class GetAllInvoicesQuery : IRequest<ApiResponse<List<InvoiceDto>>>
{
}
public class GetAllInvoicesQueryHandler : IRequestHandler<GetAllInvoicesQuery, ApiResponse<List<InvoiceDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetAllInvoicesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ApiResponse<List<InvoiceDto>>> Handle(GetAllInvoicesQuery request, CancellationToken cancellationToken)
    {
        // TODO: Tambahkan caching
        var invoices = await _unitOfWork.InvoiceManager.GetAllAsync();

        var invoicesDto = _mapper.Map<List<InvoiceDto>>(invoices);

        return ApiResponse<List<InvoiceDto>>.SuccessResult(invoicesDto);
    }
}
