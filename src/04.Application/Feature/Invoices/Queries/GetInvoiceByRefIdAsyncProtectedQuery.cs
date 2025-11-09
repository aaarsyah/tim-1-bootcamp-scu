using AutoMapper;
using MediatR;
using MyApp.Base.Exceptions;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.Invoices.Queries;

public class GetInvoiceByRefIdAsyncProtectedQuery : IRequest<ApiResponse<InvoiceDto>>
{
    public Guid RefId { get; set; }
    public bool IsPrivileged { get; set; }
    public Guid UserRefId { get; set; }
}
public class GetInvoiceByRefIdAsyncProtectedQueryHandler : IRequestHandler<GetInvoiceByRefIdAsyncProtectedQuery, ApiResponse<InvoiceDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetInvoiceByRefIdAsyncProtectedQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ApiResponse<InvoiceDto>> Handle(GetInvoiceByRefIdAsyncProtectedQuery request, CancellationToken cancellationToken)
    {
        // TODO: Tambahkan caching
        var invoice = await _unitOfWork.InvoiceManager.GetByRefIdAsync(request.RefId);
        if (invoice == null)
        {
            throw new NotFoundException("Invoice RefId", request.RefId);
        }
        if (!request.IsPrivileged && (invoice.User?.RefId != request.UserRefId))
        {
            // Demi keamanan: tetap return error yang sama untuk mencegah bocornya invoice pengguna
            throw new NotFoundException("Invoice RefId", request.RefId);
        }
        var invoiceDto = _mapper.Map<InvoiceDto>(invoice);
        return ApiResponse<InvoiceDto>.SuccessResult(invoiceDto);
    }
}
